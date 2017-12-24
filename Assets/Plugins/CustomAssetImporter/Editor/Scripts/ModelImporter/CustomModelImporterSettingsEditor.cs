using UnityEngine;
using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    [CustomEditor( typeof( CustomModelImporterSettings ) )]
    public class CustomModelImporterSettingsEditor: Editor {

        private CustomModelImporterSettings myTarget;

        private const string TEXT_UNDO = "Change CustomModelImporter Setting";
        private const string TEXT_LABEL = "Model Importer Settings";

        private void OnEnable() {
            myTarget = target as CustomModelImporterSettings;
        }

        public override void OnInspectorGUI() {

            myTarget = target as CustomModelImporterSettings;
            myTarget.Draw();

            if ( GUI.changed ) {
                Undo.RegisterCompleteObjectUndo( target, TEXT_UNDO );
                Undo.FlushUndoRecordObjects();
                EditorUtility.SetDirty( target );
            }
        }

    }
}