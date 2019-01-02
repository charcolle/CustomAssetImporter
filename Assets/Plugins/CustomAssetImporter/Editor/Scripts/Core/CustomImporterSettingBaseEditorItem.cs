using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System;
using System.Reflection;
using System.Collections.Generic;

using UndoHelper = charcolle.Utility.CustomAssetImporter.UndoHelper;
using GUIHelper  = charcolle.Utility.CustomAssetImporter.GUIHelper;

namespace charcolle.Utility.CustomAssetImporter {

    internal abstract class CustomImporterSettingBaseEditorItem<T, U> : EditorWindowItem<CustomImporterSettingsBase<T, U>> where T : CustomImporterClass<U>, new() {

        protected List<EditorWindowItem<T>> ImporterItem;
        protected TreeViewWithTreeModel<T, U> TreeView;
        protected TreeViewState treeViewState;

        public CustomImporterSettingBaseEditorItem( CustomImporterSettingsBase<T, U> data ) : base( data ) { }

        public abstract void Initialize();

        protected override void Draw() {

            UndoHelper.CustomImporterUndo<T, U>( Data, UndoHelper.UNDO_CHANGESETTING );

            EditorGUILayout.BeginVertical();
            {
                Header();
                Main();
            }
            EditorGUILayout.EndVertical();

            CheckIsContextClicked();
        }

        #region drawer

        [SerializeField]
        private string searchText = "";
        private int selectedMenu = 0;
        private readonly static string[] menuTag = new string[] { "Menu", "", "Collapse All", "Expand All" };
        private void Header() {
            EditorGUILayout.BeginHorizontal( EditorStyles.toolbar );
            {
                searchText = EditorGUILayout.TextField( searchText, GUIHelper.Styles.SearchField );
                if( GUILayout.Button( "", GUIHelper.Styles.SearchFieldCancel ) ) {
                    searchText = "";
                    EditorGUIUtility.keyboardControl = 0;
                }
                TreeView.searchString = searchText;

                selectedMenu = EditorGUILayout.Popup( selectedMenu, menuTag, EditorStyles.toolbarPopup, GUILayout.Width( 60 ) );
                switch( selectedMenu ) {
                    case 2:
                        for( int i = 0; i < CustomImporterSettings.Count; i++ )
                            CustomImporterSettings[ i ].isFoldout = false;
                        TreeView.Reload();
                        EditorGUIUtility.keyboardControl = 0;
                        break;
                    case 3:
                        for( int i = 0; i < CustomImporterSettings.Count; i++ )
                            CustomImporterSettings[ i ].isFoldout = true;
                        TreeView.Reload();
                        EditorGUIUtility.keyboardControl = 0;
                        break;
                }
                selectedMenu = 0;

                GUI.backgroundColor = Color.green;
                if( GUILayout.Button( "+", GUIHelper.Styles.ToolBox, GUILayout.Width( 35 ) ) ) {
                    var setting = new T {
                        name = "",
                        id = Data.CustomImporterSettings.Count,
                        depth = 0
                    };
                    OnAddImporterProcess( setting );
                    EditorGUIUtility.keyboardControl = 0;
                }
            }
            EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
        }

        private void Main() {
            if( ImporterItem == null || ImporterItem.Count == 0 || TreeView == null )
                return;

            EditorGUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();

            TreeView.OnGUI( GUILayoutUtility.GetLastRect() );
        }

        #endregion

        #region event

        private Rect dropArea = Rect.zero;
        private void eventProcess( Event evnt, T importer ) {
            switch( evnt.type ) {
                case EventType.ContextClick:
                    break;
                default:
                    var dropFileName = FileHelper.GetDraggedAssetPath( evnt, dropArea );
                    if( !string.IsNullOrEmpty( dropFileName ) )
                        importer.Target = dropFileName;
                    break;
            }
        }

        private void OnAddImporterProcess( T importer ) {
            UndoHelper.CustomImporterUndo( Data, UndoHelper.UNDO_ADDSETTING );
            CustomImporterSettings.Add( importer );
            Initialize();
            EditorGUIUtility.keyboardControl = 0;
        }

        private void OnRemoveImporterProcess( T importer ) {
            UndoHelper.CustomImporterUndo( Data, UndoHelper.UNDO_DELETESETTING );
            CustomImporterSettings.Remove( importer );
            Initialize();
            EditorGUIUtility.keyboardControl = 0;
        }

        private void CheckIsContextClicked() {
            for( int i = 0; i < CustomImporterSettings.Count; i++ ) {
                var setting = CustomImporterSettings[ i ];
                if( setting.IsContextClicked ) {
                    var menu = new GenericMenu();
                    menu.AddItem( new GUIContent( "ReImport" ), false, () => {
                        FileHelper.ReImport( setting );
                    } );
                    menu.AddItem( new GUIContent( "Copy" ), false, () => {
                        Type type = typeof( T );
                        ConstructorInfo info = type.GetConstructor( new Type[] { typeof( T ) } );
                        if( info == null )
                            throw new NotSupportedException( "fatal error." );

                        var item = ( T )info.Invoke( new object[] { setting } );
                        item.id = CustomImporterSettings.Count;
                        item.depth = 0;
                        OnAddImporterProcess( item );
                    } );
                    menu.AddSeparator( "" );
                    menu.AddItem( new GUIContent( "Color/Normal" ), false, () => {
                        setting.Color = SettingColor.Normal;
                    } );
                    menu.AddItem( new GUIContent( "Color/Red" ), false, () => {
                        setting.Color = SettingColor.Red;
                    } );
                    menu.AddItem( new GUIContent( "Color/Yellow" ), false, () => {
                        setting.Color = SettingColor.Yellow;
                    } );
                    menu.AddItem( new GUIContent( "Color/Green" ), false, () => {
                        setting.Color = SettingColor.Green;
                    } );
                    menu.AddItem( new GUIContent( "Color/Cyan" ), false, () => {
                        setting.Color = SettingColor.Cyan;
                    } );
                    menu.AddItem( new GUIContent( "Color/Blue" ), false, () => {
                        setting.Color = SettingColor.Blue;
                    } );
                    menu.AddItem( new GUIContent( "Color/Magenta" ), false, () => {
                        setting.Color = SettingColor.Magenta;
                    } );
                    menu.AddSeparator( "" );
                    menu.AddItem( new GUIContent( "Delete" ), false, () => {
                        OnRemoveImporterProcess( setting );
                    } );
                    menu.ShowAsContext();

                    Event.current.Use();
                    setting.IsContextClicked = false;
                }
            }
        }

        #endregion

        #region property

        protected List<T> CustomImporterSettings {
            get {
                return data.CustomImporterSettings;
            }
            set {
                data.CustomImporterSettings = value;
            }
        }

        protected string ImporterName {
            get {
                return data.ImporterName;
            }
            set {
                data.ImporterName = value;
            }
        }

        #endregion

    }

}