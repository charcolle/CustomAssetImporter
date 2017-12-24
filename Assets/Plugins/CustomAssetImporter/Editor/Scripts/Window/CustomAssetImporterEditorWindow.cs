using UnityEngine;
using UnityEditor;

using FileHelper = charcolle.Utility.CustomAssetImporter.FileHelper;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomAssetImporterEditorWindow: EditorWindow {

        private static CustomAssetImporterEditorWindow win;

        private CustomAudioImporterSettings audioImporterSettings;
        private CustomTextureImporterSettings textureImporterSettings;
        private CustomModelImporterSettings modelImporterSettings;

        private const string Title = "CustomAssetImporter";

        public static void Open( int importerIdx ) {
            win = GetWindow<CustomAssetImporterEditorWindow>();
            win.titleContent.text = Title;
            win.minSize = new Vector2( 350, 400 );
            win.selectedImporter = importerIdx;
            Initialize();
        }

        public static void Initialize() {
            if ( win.audioImporterSettings == null )
                win.audioImporterSettings = FileHelper.GetAudioImporter();
            if ( win.textureImporterSettings == null )
                win.textureImporterSettings = FileHelper.GetTextureImporter();
            if ( win.modelImporterSettings == null )
                win.modelImporterSettings = FileHelper.GetModelImporter();

            Undo.undoRedoPerformed -= win.Repaint;
            Undo.undoRedoPerformed += win.Repaint;
        }

        //=============================================================================
        // OnGUI
        //=============================================================================

        private int selectedImporter = 0;
        private static string[] importerTags = new string[] { "Audio", "Texture", "Model" };
        private void OnGUI() {
            if ( win == null )
                Open( selectedImporter );

            GUI.skin.label.richText = true;
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space( 5f );
                EditorGUILayout.BeginVertical();
                {
                    //GUILayout.Label( "<size=20>CustomImporterSetting</size>" );
                    selectedImporter = GUILayout.Toolbar( selectedImporter, importerTags, CustomAssetImporterStyles.ToolBox, GUILayout.ExpandWidth( true ) );
                    GUILayout.Space( 5f );
                    switch ( selectedImporter ) {
                        case 0:
                            OnAudioImporterGUI();
                            break;
                        case 1:
                            OnTextureImporterGUI();
                            break;
                        case 2:
                            OnModelImporterGUI();
                            break;
                    }
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space( 5f );
            }
            EditorGUILayout.EndHorizontal();
            GUI.skin.label.richText = false;
        }

        private void OnAudioImporterGUI() {
            if ( audioImporterSettings == null ) {
                EditorGUILayout.HelpBox( "Fatal Error.", MessageType.Error );
                return;
            }

            Undo.RecordObject( audioImporterSettings, string.Format( "Change {0}", audioImporterSettings.ImporterName ) );
            audioImporterSettings.Draw();
            if ( GUI.changed ) {
                EditorUtility.SetDirty( audioImporterSettings );
            }
        }

        private void OnTextureImporterGUI() {
            if ( textureImporterSettings == null ) {
                EditorGUILayout.HelpBox( "Fatal Error.", MessageType.Error );
                return;
            }

            Undo.RecordObject( textureImporterSettings, string.Format( "Change {0}", textureImporterSettings.ImporterName ) );
            textureImporterSettings.Draw();
            if ( GUI.changed ) {
                EditorUtility.SetDirty( textureImporterSettings );
            }
        }

        private void OnModelImporterGUI() {
            if( modelImporterSettings == null ) {
                EditorGUILayout.HelpBox( "Fatal Error.", MessageType.Error );
                return;
            }

            Undo.RecordObject( modelImporterSettings, string.Format( "Change {0}", modelImporterSettings.ImporterName ) );
            modelImporterSettings.Draw();
            if ( GUI.changed ) {
                EditorUtility.SetDirty( modelImporterSettings );
            }
        }

    }
}