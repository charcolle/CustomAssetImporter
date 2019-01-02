using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    [CustomEditor( typeof( CustomModelImporterSettings ) )]
    public class CustomModelImporterSettingsEditor: Editor {

        public override void OnInspectorGUI() {
            EditorGUI.BeginDisabledGroup( true );
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();
        }

    }
}