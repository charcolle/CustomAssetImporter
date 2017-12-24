using UnityEngine;
using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    [CustomEditor( typeof( CustomAudioImporterSettings ) )]
    public class CustomAudioImporterSettingsEditor : Editor {

        private CustomAudioImporterSettings myTarget;

        private const string TEXT_UNDO = "Change CustomAudioImporter Setting";
        private const string TEXT_LABEL = "Audio Importer Settings";

        private void OnEnable() {
            myTarget = target as CustomAudioImporterSettings;
        }

        public override void OnInspectorGUI() {

            myTarget = target as CustomAudioImporterSettings;
            myTarget.Draw();

            if ( GUI.changed ) {
                Undo.RegisterCompleteObjectUndo( target, TEXT_UNDO );
                Undo.FlushUndoRecordObjects();
                EditorUtility.SetDirty( target );
            }

        }

    }
}