using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    [CustomEditor( typeof( CustomAudioImporterSettings ) )]
    public class CustomAudioImporterSettingsEditor : Editor {

        public override void OnInspectorGUI() {
            EditorGUI.BeginDisabledGroup( true );
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();
        }

    }
}