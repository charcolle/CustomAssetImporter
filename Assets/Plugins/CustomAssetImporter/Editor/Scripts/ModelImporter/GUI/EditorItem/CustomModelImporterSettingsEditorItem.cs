using UnityEngine;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomModelImporterSettingsEditorItem : CustomImporterSettingBaseEditorItem<CustomModelImporter, CustomModelImporterValue> {

        public CustomModelImporterSettingsEditorItem( CustomModelImporterSettings data ) : base( data ) { }

        public override void Initialize() {
            ImporterItem = new List<EditorWindowItem<CustomModelImporter>>();
            for( int i = 0; i < CustomImporterSettings.Count; i++ ) {
                CustomImporterSettings[ i ].id = i;
                var settingItem = new CustomModelImporterClassEditorItem( CustomImporterSettings[ i ] );
                ImporterItem.Add( settingItem );
            }

            if( treeViewState == null )
                treeViewState = new TreeViewState();

            var treeModel = new TreeModel<CustomModelImporter>( Data.CustomImporterSettings );
            TreeView = new CustomModelImporterTreeView( treeViewState, treeModel );
            TreeView.treeModel.modelChanged += () => {
                GUI.changed = true;
            };
            TreeView.Reload();
        }
    }

}