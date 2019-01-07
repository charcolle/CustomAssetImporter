using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

using FileHelper = charcolle.Utility.CustomAssetImporter.FileHelper;
using UndoHelper = charcolle.Utility.CustomAssetImporter.UndoHelper;
using GUIHelper  = charcolle.Utility.CustomAssetImporter.GUIHelper;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomAssetImporterEditorWindow: EditorWindow {

        private static CustomAssetImporterEditorWindow win;
        private static bool IsInitialized = false;
        private static bool IsFirstGUI = false;

        private CustomAudioImporterSettings audioImporterSettings;
        private CustomTextureImporterSettings textureImporterSettings;
        private CustomModelImporterSettings modelImporterSettings;

        private CustomAudioImporterSettingsEditorItem audioImporterEditorItem;
        private CustomTextureImporterSettingsEditorItem textureImporterEditorItem;
        private CustomModelImporterSettingsEditorItem modelImporterEditorItem;

        private readonly static string Title = "CustomAssetImporter";

        [SerializeField]private TreeViewState treeViewState;
        private CustomAssetImporterEditorWindowTreeView treeView;
        private SplitterState horizontalState;

        public static void Open( int importerIdx ) {
            win                   = GetWindow<CustomAssetImporterEditorWindow>();
            win.titleContent.text = Title;
            win.minSize           = new Vector2( 350, 400 );
            win.selectedImporter  = importerIdx;
            win.Show();
            win.Initialize();
            win.treeViewState.lastClickedID = importerIdx;
        }

        private void OnEnable() {
            Undo.undoRedoPerformed -= Initialize;
            Undo.undoRedoPerformed += Initialize;
        }

        public void Initialize() {
            if( treeViewState == null )
                treeViewState = new TreeViewState();
            treeView = new CustomAssetImporterEditorWindowTreeView( treeViewState );
            horizontalState = new SplitterState( new float[] { position.width * 0.15f, position.width * 0.85f },
                                                                new int[] { 100, 250 }, new int[] { 200, 1000 } );

            if ( audioImporterSettings == null )
                audioImporterSettings = FileHelper.GetAudioImporter();
            if ( textureImporterSettings == null )
                textureImporterSettings = FileHelper.GetTextureImporter();
            if ( modelImporterSettings == null )
                modelImporterSettings = FileHelper.GetModelImporter();

            if( audioImporterEditorItem == null || audioImporterEditorItem.Data == null )
                audioImporterEditorItem = new CustomAudioImporterSettingsEditorItem( audioImporterSettings );
            if( textureImporterEditorItem == null || textureImporterEditorItem.Data == null )
                textureImporterEditorItem = new CustomTextureImporterSettingsEditorItem( textureImporterSettings );
            if( modelImporterEditorItem == null || modelImporterEditorItem.Data == null )
                modelImporterEditorItem = new CustomModelImporterSettingsEditorItem( modelImporterSettings );
            audioImporterEditorItem.Initialize();
            textureImporterEditorItem.Initialize();
            modelImporterEditorItem.Initialize();

            Repaint();

            IsInitialized = true;
            IsFirstGUI = true;
        }
        //=============================================================================
        // OnGUI
        //=============================================================================

        private int selectedImporter = 0;
        private void OnGUI() {
            if( !IsInitialized )
                Initialize();

            GUI.skin.label.richText = true;
            SplitterGUI.BeginHorizontalSplit( horizontalState );
            {
                LeftSide();
                RightSide();
            }
            SplitterGUI.EndHorizontalSplit();
            GUI.skin.label.richText = false;

            if( IsFirstGUI ) { // because GUIStyle.Draw is only for EventType.Repaint... too ugly :(
                Repaint();
                IsFirstGUI = false;
            }
        }

        private void LeftSide() {
            EditorGUILayout.BeginVertical();
            {
                GUILayout.Box( "", EditorStyles.toolbar, GUILayout.ExpandWidth( true ) );

                EditorGUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                selectedImporter = treeView.state.lastClickedID;
                switch( selectedImporter ) {
                    case 1:
                        GUILayout.Box( GUIHelper.Textures.Audio, GUIStyle.none );
                        break;
                    case 2:
                        GUILayout.Box( GUIHelper.Textures.Texture, GUIStyle.none );
                        break;
                    case 3:
                        GUILayout.Box( GUIHelper.Textures.Model, GUIStyle.none );
                        break;
                }
                GUILayout.Space( 20 );
                EditorGUILayout.EndVertical();

                treeView.OnGUI( GUILayoutUtility.GetLastRect() );
            }
            EditorGUILayout.EndVertical();
        }

        private void RightSide() {
            EditorGUILayout.BeginVertical();
            {
                selectedImporter = treeView.state.lastClickedID;
                switch( selectedImporter ) {
                    case 1:
                        OnAudioImporterGUI();
                        break;
                    case 2:
                        OnTextureImporterGUI();
                        break;
                    case 3:
                        OnModelImporterGUI();
                        break;
                    default:
                        GUILayout.Box( "", EditorStyles.toolbar, GUILayout.ExpandWidth( true ) );
                        break;
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void OnAudioImporterGUI() {
            if ( audioImporterSettings == null ) {
                EditorGUILayout.HelpBox( "Fatal Error.", MessageType.Error );
                Initialize();
                return;
            }

            audioImporterEditorItem.OnGUI();
            if ( GUI.changed ) {
                Repaint();
                EditorUtility.SetDirty( audioImporterSettings );
            }
        }

        private void OnTextureImporterGUI() {
            if ( textureImporterSettings == null ) {
                EditorGUILayout.HelpBox( "Fatal Error.", MessageType.Error );
                Initialize();
                return;
            }

            textureImporterEditorItem.OnGUI();
            if ( GUI.changed ) {
                Repaint();
                EditorUtility.SetDirty( textureImporterSettings );
            }
        }

        private void OnModelImporterGUI() {
            if( modelImporterSettings == null ) {
                EditorGUILayout.HelpBox( "Fatal Error.", MessageType.Error );
                Initialize();
                return;
            }

            Undo.RecordObject( modelImporterSettings, string.Format( "Change {0}", modelImporterSettings.ImporterName ) );
            modelImporterEditorItem.OnGUI();
            if ( GUI.changed ) {
                Repaint();
                EditorUtility.SetDirty( modelImporterSettings );
            }
        }

    }
}