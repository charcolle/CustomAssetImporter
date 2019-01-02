using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

using GUIHelper  = charcolle.Utility.CustomAssetImporter.GUIHelper;
using FileHelper = charcolle.Utility.CustomAssetImporter.FileHelper;

// this code from unity technologies tree view sample
// http://files.unity3d.com/mads/TreeViewExamples.zip

namespace charcolle.Utility.CustomAssetImporter {

    internal class TreeViewItem<T> : TreeViewItem where T : TreeElement {

        public T data { get; set; }
        public EditorWindowItem<T> editorItem;

        public TreeViewItem( int id, int depth, string displayName, T data ) : base( id, depth, displayName ) {
            this.data = data;
        }

    }

    internal class TreeViewWithTreeModel<T, U> : TreeView where T : CustomImporterClass<U> {

        #region default treeView
        private TreeModel<T> m_TreeModel;
        private readonly List<TreeViewItem> m_Rows = new List<TreeViewItem>( 100 );

        public event Action treeChanged;

        public TreeModel<T> treeModel { get { return m_TreeModel; } }

        public event Action<IList<TreeViewItem>> beforeDroppingDraggedItems;

        public TreeViewWithTreeModel( TreeViewState state, TreeModel<T> model ) : base( state ) {
            Init( model );
        }

        public TreeViewWithTreeModel( TreeViewState state, MultiColumnHeader multiColumnHeader, TreeModel<T> model )
            : base( state, multiColumnHeader ) {
            Init( model );
        }

        private void Init( TreeModel<T> model ) {
            m_TreeModel = model;
            m_TreeModel.modelChanged += ModelChanged;
        }

        private void ModelChanged() {
            if( treeChanged != null )
                treeChanged();

            Reload();
        }

        protected override TreeViewItem BuildRoot() {
            int depthForHiddenRoot = -1;
            return new TreeViewItem<T>( m_TreeModel.root.id, depthForHiddenRoot, m_TreeModel.root.name, m_TreeModel.root );
        }

        protected override bool CanMultiSelect( TreeViewItem item ) {
            return false;
        }

        protected override IList<TreeViewItem> BuildRows( TreeViewItem root ) {
            if( m_TreeModel.root == null ) {
                Debug.LogError( "tree model root is null. did you call SetData()?" );
            }

            m_Rows.Clear();
            if( !string.IsNullOrEmpty( searchString ) ) {
                Search( m_TreeModel.root, searchString, m_Rows );
            } else {
                if( m_TreeModel.root.hasChildren )
                    AddChildrenRecursive( m_TreeModel.root, 0, m_Rows );
            }

            // We still need to setup the child parent information for the rows since this
            // information is used by the TreeView internal logic (navigation, dragging etc)
            SetupParentsAndChildrenFromDepths( root, m_Rows );

            return m_Rows;
        }

        private void AddChildrenRecursive( T parent, int depth, IList<TreeViewItem> newRows ) {
            foreach( T child in parent.children ) {
                var item = new TreeViewItem<T>( child.id, depth, child.name, child );
                newRows.Add( item );

                if( child.hasChildren ) {
                    if( IsExpanded( child.id ) ) {
                        AddChildrenRecursive( child, depth + 1, newRows );
                    } else {
                        item.children = CreateChildListForCollapsedParent();
                    }
                }
            }
        }

        private void Search( T searchFromThis, string search, List<TreeViewItem> result ) {
            if( string.IsNullOrEmpty( search ) )
                throw new ArgumentException( "Invalid search: cannot be null or empty", "search" );
            if( searchFromThis.children == null )
                return;

            const int kItemDepth = 0; // tree is flattened when searching

            Stack<T> stack = new Stack<T>();
            foreach( var element in searchFromThis.children )
                stack.Push( ( T )element );
            while( stack.Count > 0 ) {
                T current = stack.Pop();
                // Matches search?
                if( current.name.IndexOf( search, StringComparison.OrdinalIgnoreCase ) >= 0 ) {
                    result.Add( new TreeViewItem<T>( current.id, kItemDepth, current.name, current ) );
                }

                if( current.children != null && current.children.Count > 0 ) {
                    foreach( var element in current.children ) {
                        stack.Push( ( T )element );
                    }
                }
            }
            SortSearchResult( result );
        }

        protected virtual void SortSearchResult( List<TreeViewItem> rows ) {
            rows.Sort( ( x, y ) => EditorUtility.NaturalCompare( x.displayName, y.displayName ) ); // sort by displayName by default, can be overriden for multicolumn solutions
        }

        protected override IList<int> GetAncestors( int id ) {
            return m_TreeModel.GetAncestors( id );
        }

        protected override IList<int> GetDescendantsThatHaveChildren( int id ) {
            return m_TreeModel.GetDescendantsThatHaveChildren( id );
        }

        //=======================================================
        // drag and drop settings
        //=======================================================

        private const string k_GenericDragID = "GenericDragColumnDragging";

        protected override bool CanStartDrag( CanStartDragArgs args ) {
            return true;
        }

        protected override bool CanBeParent( TreeViewItem item ) {
            return false;
        }

        protected override void SetupDragAndDrop( SetupDragAndDropArgs args ) {
            if( hasSearch )
                return;

            DragAndDrop.PrepareStartDrag();
            var draggedRows = GetRows().Where( item => args.draggedItemIDs.Contains( item.id ) ).ToList();
            DragAndDrop.SetGenericData( k_GenericDragID, draggedRows );
            DragAndDrop.objectReferences = new UnityEngine.Object[] { }; // this IS required for dragging to work
            string title = draggedRows.Count == 1 ? draggedRows[ 0 ].displayName : "< Multiple >";
            DragAndDrop.StartDrag( title );
        }

        protected override DragAndDropVisualMode HandleDragAndDrop( DragAndDropArgs args ) {
            // Check if we can handle the current drag data (could be dragged in from other areas/windows in the editor)
            var draggedRows = DragAndDrop.GetGenericData( k_GenericDragID ) as List<TreeViewItem>;
            if( draggedRows == null )
                return DragAndDropVisualMode.None;

            // Parent item is null when dragging outside any tree view items.
            switch( args.dragAndDropPosition ) {
                case DragAndDropPosition.UponItem:
                case DragAndDropPosition.BetweenItems: {
                        bool validDrag = ValidDrag( args.parentItem, draggedRows );
                        if( args.performDrop && validDrag ) {
                            T parentData = ( ( TreeViewItem<T> )args.parentItem ).data;
                            OnDropDraggedElementsAtIndex( draggedRows, parentData, args.insertAtIndex == -1 ? 0 : args.insertAtIndex );
                        }
                        return validDrag ? DragAndDropVisualMode.Move : DragAndDropVisualMode.None;
                    }

                case DragAndDropPosition.OutsideItems: {
                        if( args.performDrop )
                            OnDropDraggedElementsAtIndex( draggedRows, m_TreeModel.root, m_TreeModel.root.children.Count );

                        return DragAndDropVisualMode.Move;
                    }
                default:
                    Debug.LogError( "Unhandled enum " + args.dragAndDropPosition );
                    return DragAndDropVisualMode.None;
            }
        }

        public virtual void OnDropDraggedElementsAtIndex( List<TreeViewItem> draggedRows, T parent, int insertIndex ) {
            if( beforeDroppingDraggedItems != null )
                beforeDroppingDraggedItems( draggedRows );

            var draggedElements = new List<TreeElement>();
            foreach( var x in draggedRows )
                draggedElements.Add( ( ( TreeViewItem<T> )x ).data );

            var selectedIDs = draggedElements.Select( x => x.id ).ToArray();
            m_TreeModel.MoveElements( parent, insertIndex, draggedElements );
            SetSelection( selectedIDs, TreeViewSelectionOptions.RevealAndFrame );
        }

        private bool ValidDrag( TreeViewItem parent, List<TreeViewItem> draggedItems ) {
            TreeViewItem currentParent = parent;
            while( currentParent != null ) {
                if( draggedItems.Contains( currentParent ) )
                    return false;
                currentParent = currentParent.parent;
            }
            return true;
        }

        #endregion

        protected override void ContextClickedItem( int id ) {
            var target = FindItem( id, rootItem );
            if( target != null ) {
                var item = ( TreeViewItem<T> )target;
                item.data.IsContextClicked = true;
            }
        }

        protected override void DoubleClickedItem( int id ) {
            var target = FindItem( id, rootItem );
            if( target != null ) {
                var item = ( TreeViewItem<T> )target;
                if( !string.IsNullOrEmpty( item.data.Target ) )
                    EditorGUIUtility.PingObject( AssetDatabase.LoadAssetAtPath( item.data.Target, typeof(UnityEngine.Object) ) );
            }
        }

        #region gui
        //=======================================================
        // basic gui
        //=======================================================

        private bool changeHeightFlag = false;
        private bool isPreDragUpdate = false;
        private bool isPreDragPerform = false;
        protected override void RowGUI( RowGUIArgs args ) {
            changeHeightFlag = false;
            var item = ( TreeViewItem<T> )args.item;

            var bgRect = args.rowRect;
            bgRect.width = bgRect.width;
            bgRect.yMin += 2f;
            bgRect.yMax -= 2f;

            var header = headerRect( bgRect );
            var bg = backgroundRect( bgRect );

            DrawRect( bgRect, item );
            HeaderItem( header, item );

            if( item.data.IsEnable )
                Draw( item, bg );

            if( changeHeightFlag )
                RefreshCustomRowHeights();

            isPreDragUpdate = Event.current.type == EventType.DragUpdated;
            isPreDragPerform = Event.current.type == EventType.DragPerform;
        }

        private void Draw( TreeViewItem<T> item, Rect rect ) {
            var showSettingRect    = rect;
            showSettingRect.xMin += 5f;
            showSettingRect.xMax -= 5f;
            showSettingRect.yMin += 2f;
            showSettingRect.height = EditorGUIUtility.singleLineHeight;

            // directory Setting
            var directoryRect   = showSettingRect;
            directoryRect.width = 100f;
            var type      = ( ImportTargetType )EditorGUI.EnumPopup( directoryRect, item.data.Type );
            if( item.data.Type != type ) {
                item.data.Target = FileHelper.TargetPathForType( item.data.Target, type );
                if( !FileHelper.IsTargetNameValid( item.data.Target, type ) )
                    Debug.LogWarning( "CustomAssetImporter: The importer target is invalid" );
                item.data.Type = type;
            }
            var targetRect      = showSettingRect;
            targetRect.xMin += 103f;
            item.data.Target    = EditorGUI.TextField( targetRect, item.data.Target );

            //// drag drop process... too ugly :(
            {
                var preType = Event.current.type;
                if( isPreDragUpdate )
                    Event.current.type = EventType.DragUpdated;
                if( isPreDragPerform )
                    Event.current.type = EventType.DragPerform;
                var draggedTarget = FileHelper.GetDraggedAssetPath( Event.current, rect );
                if( !string.IsNullOrEmpty( draggedTarget ) )
                    item.data.Target = FileHelper.TargetPathForType( draggedTarget, item.data.Type );
                Event.current.type = preType;
            }

            showSettingRect.y += GUIHelper.Rects.NextItemY;

            // show Log
            var showLogToggle   = showSettingRect;
            showLogToggle.width = 16f;
            item.data.IsLogger  = EditorGUI.Toggle( showLogToggle, item.data.IsLogger );
            showLogToggle.width = 100f;
            showLogToggle.x += 16f;
            EditorGUI.LabelField( showLogToggle, "Show Log" );
            var logRect         = showSettingRect;
            logRect.xMin += 103f;
            item.data.Log = EditorGUI.TextField( logRect, item.data.Log );
            EditorGUI.BeginChangeCheck();
            item.editorItem.OnGUI( ref showSettingRect );
            if( EditorGUI.EndChangeCheck() )
                changeHeightFlag = true;

        }

        private void DrawRect( Rect rect, TreeViewItem<T> item ) {
            if( Event.current.type == EventType.Repaint ) {
                GUI.backgroundColor = !item.data.IsEnable ? Color.grey : GUIHelper.Colors.GetColor( item.data.Color );
                GUIHelper.Styles.RLHeader.Draw( headerRect( rect ), false, false, false, false );
                GUIHelper.Styles.RLBackGround.Draw( backgroundRect( rect ), false, false, false, false );
                GUI.backgroundColor = Color.white;
            }
        }

        private void HeaderItem( Rect rect, TreeViewItem<T> item ) {
            var toggleRect = rect;
            toggleRect.xMin += 3f;
            toggleRect.width = 16;

            var isEnable = EditorGUI.Toggle( toggleRect, item.data.IsEnable );
            if( isEnable != item.data.IsEnable ) {
                changeHeightFlag = true;
                item.data.IsEnable = isEnable;
            }

            var labelRect = rect;
            labelRect.y += 1f;
            labelRect.xMin += toggleRect.width + 2f;
            GUI.Label( labelRect, item.data.Target );
            item.data.name = item.data.Target;
        }

        //=======================================================
        // gui property
        //=======================================================

        protected readonly static float BasicItemHeight = 64f;

        protected int DefaultSettingNum {
            get; set;
        }

        protected int OverrideSettingNum {
            get; set;
        }

        protected float DefaultSettingHeight {
            get {
                return EditorGUIUtility.singleLineHeight * ( DefaultSettingNum + 1 ) + EditorGUIUtility.standardVerticalSpacing * ( DefaultSettingNum + 1 )
                    + EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing * 2 + 12f;
            }
        }

        protected float OverrideSettingHeight {
            get {
                return EditorGUIUtility.singleLineHeight * OverrideSettingNum + EditorGUIUtility.standardVerticalSpacing * OverrideSettingNum + 6f;
            }
        }

        private Rect headerRect( Rect bgRect ) {
            var rect = bgRect;
            rect.xMin += 5f;
            rect.xMax -= 5f;
            rect.height = GUIHelper.Styles.RLHeader.fixedHeight;
            return rect;
        }

        private Rect backgroundRect( Rect bgRect ) {
            var rect = headerRect( bgRect );
            rect.y += rect.height;
            rect.height = bgRect.height - rect.height;
            return rect;
        }
        #endregion

    }
}