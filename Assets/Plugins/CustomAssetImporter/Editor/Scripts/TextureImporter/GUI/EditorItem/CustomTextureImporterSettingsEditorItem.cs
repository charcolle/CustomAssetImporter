using UnityEngine;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomTextureImporterSettingsEditorItem : CustomImporterSettingBaseEditorItem<CustomTextureImporter, CustomTextureImporterValue> {

        public CustomTextureImporterSettingsEditorItem( CustomTextureImporterSettings data ) : base( data ) { }

        public override void Initialize() {
            ImporterItem = new List<EditorWindowItem<CustomTextureImporter>>();
            for( int i = 0; i < CustomImporterSettings.Count; i++ ) {
                CustomImporterSettings[ i ].id = i;
                var settingItem = new CustomTextureImporterClassEditorItem( CustomImporterSettings[ i ] );
                ImporterItem.Add( settingItem );
            }

            if( treeViewState == null )
                treeViewState = new TreeViewState();

            var treeModel = new TreeModel<CustomTextureImporter>( Data.CustomImporterSettings );
            TreeView = new CustomTextureImporterTreeView( treeViewState, treeModel );
            TreeView.treeModel.modelChanged += () => {
                GUI.changed = true;
            };
            TreeView.Reload();
        }

    }

}