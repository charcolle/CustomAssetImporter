using UnityEngine;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomAudioImporterSettingsEditorItem : CustomImporterSettingBaseEditorItem<CustomAudioImporter, CustomAudioImporterValue> {

        public CustomAudioImporterSettingsEditorItem( CustomAudioImporterSettings data ) : base( data ) { }

        public override void Initialize() {
            ImporterItem = new List<EditorWindowItem<CustomAudioImporter>>();
            for( int i = 0; i < CustomImporterSettings.Count; i++ ) {
                CustomImporterSettings[ i ].id = i;
                var settingItem = new CustomAudioImporterClassEditorItem( CustomImporterSettings[ i ] );
                ImporterItem.Add( settingItem );
            }

            if( treeViewState == null )
                treeViewState = new TreeViewState();

            var treeModel = new TreeModel<CustomAudioImporter>( Data.CustomImporterSettings );
            TreeView = new CustomAudioImporterTreeView( treeViewState, treeModel );
            TreeView.treeModel.modelChanged += () => {
                GUI.changed = true;
            };
            TreeView.Reload();
        }

    }

}