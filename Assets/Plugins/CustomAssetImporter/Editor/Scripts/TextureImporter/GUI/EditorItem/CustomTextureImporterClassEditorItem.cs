using UnityEngine;
using UnityEditor;

using GUIHelper = charcolle.Utility.CustomAssetImporter.GUIHelper;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomTextureImporterClassEditorItem : EditorWindowItem<CustomTextureImporter> {

        private CustomTextureImporterValueEditorItem ImporterSettingItem;
        private CustomTextureImporterValueEditorItem OverrideForAndroidSettingItem;
        private CustomTextureImporterValueEditorItem OverrideForiOSSettingItem;

        public CustomTextureImporterClassEditorItem( CustomTextureImporter data ) : base( data ) {
            ImporterSettingItem = new CustomTextureImporterValueEditorItem( data.ImporterSetting );
            OverrideForAndroidSettingItem = new CustomTextureImporterValueEditorItem( data.OverrideForAndroidSetting );
            OverrideForiOSSettingItem = new CustomTextureImporterValueEditorItem( data.OverrideForiOSSetting );

            ImporterSettingItem.isDefault = true;
        }

        protected override void Draw() { }

        protected override void Draw( ref Rect rect ) {
            rect.y += GUIHelper.Rects.NextItemY;

            var foldOutRect = rect;
            foldOutRect.width = 16f;
            var fold = EditorGUI.Foldout( foldOutRect, IsFoldOut, "Setting" );
            if( fold != IsFoldOut )
                IsFoldOut = fold;

            if( IsFoldOut ) {
                ImporterSettingItem.OnGUI( ref rect );

                rect.y += GUIHelper.Rects.NextItemY + 5f;
                rect.xMin += 15;
                OverrideForiOSSetting.IsConfigurable = EditorGUI.Toggle( rect, new GUIContent( "iOS Setting" ), OverrideForiOSSetting.IsConfigurable );
                if( OverrideForiOSSetting.IsConfigurable )
                    OverrideForiOSSettingItem.OnGUI( ref rect );

                rect.y += GUIHelper.Rects.NextItemY;
                OverrideForAndroidSetting.IsConfigurable = EditorGUI.Toggle( rect, new GUIContent( "Android Setting" ), OverrideForAndroidSetting.IsConfigurable );
                if( OverrideForAndroidSetting.IsConfigurable )
                    OverrideForAndroidSettingItem.OnGUI( ref rect );
            }
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

        private CustomTextureImporterValue OverrideForiOSSetting {
            get {
                return data.OverrideForiOSSetting;
            }
            set {
                data.OverrideForiOSSetting = value;
            }
        }

        private CustomTextureImporterValue OverrideForAndroidSetting {
            get {
                return data.OverrideForAndroidSetting;
            }
            set {
                data.OverrideForAndroidSetting = value;
            }
        }

        #endregion

    }

}