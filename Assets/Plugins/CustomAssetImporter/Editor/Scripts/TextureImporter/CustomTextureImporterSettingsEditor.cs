using UnityEngine;
using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    [CustomEditor( typeof( CustomTextureImporterSettings ) )]
    public class CustomTextureImporterSettingsEditor: Editor {

        private CustomTextureImporterSettings myTarget;

        private const string TEXT_UNDO = "Change CustomTextureImporter Setting";
        private const string TEXT_LABEL = "Texture Importer Settings";

        private void OnEnable() {
            myTarget = target as CustomTextureImporterSettings;
        }

        public override void OnInspectorGUI() {

            myTarget = target as CustomTextureImporterSettings;
            myTarget.Draw();

            if ( GUI.changed ) {
                Undo.RegisterCompleteObjectUndo( target, TEXT_UNDO );
                Undo.FlushUndoRecordObjects();
                EditorUtility.SetDirty( target );
            }
        }
    }
}