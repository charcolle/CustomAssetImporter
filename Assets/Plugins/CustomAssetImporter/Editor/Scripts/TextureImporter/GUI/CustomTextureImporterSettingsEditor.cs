using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    [CustomEditor( typeof( CustomTextureImporterSettings ) )]
    public class CustomTextureImporterSettingsEditor: Editor {

        public override void OnInspectorGUI() {
            EditorGUI.BeginDisabledGroup( true );
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();
        }
    }
}