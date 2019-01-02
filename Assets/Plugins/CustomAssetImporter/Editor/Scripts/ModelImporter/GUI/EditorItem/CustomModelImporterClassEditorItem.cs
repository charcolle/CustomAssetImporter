using UnityEngine;
using UnityEditor;
using System;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomModelImporterClassEditorItem : EditorWindowItem<CustomModelImporter> {

        public CustomModelImporterValueEditorItem ImporterSettingItem;

        public CustomModelImporterClassEditorItem( CustomModelImporter data ) : base( data ) {
            ImporterSettingItem = new CustomModelImporterValueEditorItem( data.ImporterSetting );
        }

        protected override void Draw() { }

        protected override void Draw( ref Rect rect ) {
            rect.y += GUIHelper.Rects.NextItemY;

            var foldOutRect = rect;
            foldOutRect.width = 16f;
            var fold = EditorGUI.Foldout( foldOutRect, IsFoldOut, "Setting" );
            if( fold != IsFoldOut )
                IsFoldOut = fold;

            if( IsFoldOut )
                ImporterSettingItem.OnGUI( ref rect );
        }

        #region property
        private bool IsFoldOut {
            get {
                return data.isFoldout;
            }
            set {
                data.isFoldout = value;
            }
        }
        #endregion

    }

}