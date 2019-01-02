using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomAssetImporterEditorWindowTreeView : TreeView {
        public CustomAssetImporterEditorWindowTreeView( TreeViewState treeViewState ) : base( treeViewState ) {
            Reload();
        }

        protected override TreeViewItem BuildRoot() {
            var root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };
            var allItems = new List<TreeViewItem>
            {
                new TreeViewItem {id = 1, depth = 0, displayName = "Audio"},
                new TreeViewItem {id = 2, depth = 0, displayName = "Texture"},
                new TreeViewItem {id = 3, depth = 0, displayName = "Model"},
            };

            SetupParentsAndChildrenFromDepths( root, allItems );

            return root;
        }

        protected override bool CanMultiSelect( TreeViewItem item ) {
            return false;
        }

    }
}