using UnityEngine;
using UnityEditor;
using System.IO;

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

            EditorGUILayout.BeginHorizontal( GUILayout.ExpandWidth( true ) );
            {
                EditorGUILayout.BeginVertical();
                {
                    Header();
                    Main();
                    Footer();
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space( 3f );
            }
            EditorGUILayout.EndHorizontal();

            if ( myTarget.CustomImporterSettings.Count == 0 )
                EditorGUILayout.HelpBox( "No Importers", MessageType.Warning );

            if ( GUI.changed ) {
                Undo.RegisterCompleteObjectUndo( target, TEXT_UNDO );
                Undo.FlushUndoRecordObjects();
                EditorUtility.SetDirty( target );
            }
        }

        //=============================================================================
        // Drawer
        //=============================================================================

        private void Header() {
            GUI.backgroundColor = Color.grey;
            EditorGUILayout.BeginHorizontal( CustomAssetImporterStyles.Box );
            {
                GUILayout.Label( TEXT_LABEL );
                GUILayout.FlexibleSpace();
                GUI.backgroundColor = Color.green;
                if ( GUILayout.Button( "+", CustomAssetImporterStyles.ToolBox, GUILayout.Width( 70f ) ) ) {
                    myTarget.CustomImporterSettings.Add( new CustomAudioImporter() );
                }
            }
            EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
        }

        private void Footer() {
            if ( myTarget.CustomImporterSettings.Count == 0 )
                return;
            EditorGUILayout.BeginHorizontal( CustomAssetImporterStyles.Box );
            {
                GUILayout.FlexibleSpace();
                if ( GUILayout.Button( "Collapse all", EditorStyles.toolbarButton, GUILayout.Width( 120f ) ) ) {
                    for ( int i = 0; i < myTarget.CustomImporterSettings.Count; i++ ) {
                        myTarget.CustomImporterSettings[i].isFoldout = false;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private Vector2 SCROLL_VIEW;
        private void Main() {
            if ( myTarget.CustomImporterSettings.Count == 0 )
                return;
            SCROLL_VIEW = EditorGUILayout.BeginScrollView( SCROLL_VIEW, CustomAssetImporterStyles.Box, GUILayout.ExpandHeight( true ) );
            {
                var importers = myTarget.CustomImporterSettings;
                try {
                    for ( int i = 0; i < importers.Count; i++ )
                        ImporterDrawer( importers[i] );
                } catch { }
            }
            EditorGUILayout.EndScrollView();
        }

        private void ImporterDrawer( CustomAudioImporter importer ) {
            var deleteFlag = false;
            EditorGUILayout.BeginVertical( CustomAssetImporterStyles.Box, GUILayout.ExpandWidth( true ) );
            {
                EditorGUILayout.BeginHorizontal( CustomAssetImporterStyles.ToolBox );
                {
                    GUILayout.Label( Path.GetFileNameWithoutExtension( importer.TargetName ) );
                    GUILayout.FlexibleSpace();
                    if ( GUILayout.Button( "ReImport", EditorStyles.toolbarButton, GUILayout.Width( 60f ) ) ) {
                        FileHelper.ReImport( importer );
                    }
                    // furture
                    //if ( GUILayout.Button( "Copy", EditorStyles.toolbarButton, GUILayout.Width( 60f ) ) ) {
                    //    ImportPerform( importer );
                    //}
                    GUI.backgroundColor = Color.red;
                    if ( GUILayout.Button( "x", EditorStyles.toolbarButton, GUILayout.Width( 20f ) ) ) {
                        deleteFlag = true;
                    }
                    GUI.backgroundColor = Color.white;
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space( 2f );


                var curEvent = Event.current;
                var dropArea = EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Space( 5f );
                    importer.Type = ( ImportTargetType )EditorGUILayout.EnumPopup( importer.Type, GUILayout.Width( 100f ) );
                    importer.TargetName = EditorGUILayout.TextField( importer.TargetName );
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Space( 7f );
                    importer.isLogger = EditorGUILayout.Toggle( importer.isLogger, GUILayout.Width( 10f ) );
                    GUILayout.Label( "Show Log", GUILayout.Width( 84f ) );
                    importer.Log = EditorGUILayout.TextField( importer.Log );
                }
                EditorGUILayout.EndHorizontal();

                // drop perform
                var dropFileName = FileHelper.GetDraggedAssetPath( curEvent, dropArea );
                if( !string.IsNullOrEmpty( dropFileName ) )
                    importer.TargetName = dropFileName;

                GUILayout.Space( 5f );

                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Space( 20f );
                    importer.isFoldout = EditorGUILayout.Foldout( importer.isFoldout, "Show Setting" );
                }
                EditorGUILayout.EndHorizontal();

                if ( importer.isFoldout ) {
                    SettingDrawer( importer.ImporterSetting );
                    GUILayout.Space( 3f );
                    importer.OverrideForiOSSetting.isEditable = EditorGUILayout.Toggle( "iOS Setting", importer.OverrideForiOSSetting.isEditable );
                    if ( importer.OverrideForiOSSetting.isEditable )
                        SettingDrawer( importer.OverrideForiOSSetting, false );
                    importer.OverrideForAndroidSetting.isEditable = EditorGUILayout.Toggle( "Android Setting", importer.OverrideForAndroidSetting.isEditable );
                    if ( importer.OverrideForAndroidSetting.isEditable )
                        SettingDrawer( importer.OverrideForAndroidSetting, false );

                }
                GUILayout.Space( 3f );
            }
            EditorGUILayout.EndVertical();

            if ( deleteFlag )
                myTarget.CustomImporterSettings.Remove( importer );
        }

        private void SettingDrawer( CustomAudioImporterValues v, bool isDefault = true ) {
            EditorGUILayout.BeginVertical( EditorStyles.helpBox );
            {
                if ( isDefault ) {
                    using ( new ImporterValueScope<bool>( v.ForceToMono, "ForceToMono" ) )
                        v.ForceToMono.Value = EditorGUILayout.Toggle( v.ForceToMono );

                    using ( new ImporterValueScope<bool>( v.LoadInBackGround, "LoadInBackGround" ) )
                        v.LoadInBackGround.Value = EditorGUILayout.Toggle( v.LoadInBackGround );

                    using ( new ImporterValueScope<bool>( v.Ambisonic, "Ambisonic" ) )
                        v.Ambisonic.Value = EditorGUILayout.Toggle( v.Ambisonic );
                }

                using ( new ImporterValueScope<AudioClipLoadType>( v.LoadType, "LoadType" ) )
                    v.LoadType.Value = ( AudioClipLoadType )EditorGUILayout.EnumPopup( v.LoadType );

                using ( new ImporterValueScope<bool>( v.PreloadAudioData, "PreloadAudioData" ) )
                    v.PreloadAudioData.Value = EditorGUILayout.Toggle( v.PreloadAudioData );

                using ( new ImporterValueScope<AudioCompressionFormat>( v.CompressionFormat, "CompressionFormat" ) )
                    v.CompressionFormat.Value = ( AudioCompressionFormat )EditorGUILayout.EnumPopup( v.CompressionFormat );

                if ( v.CompressionFormat.Value.Equals( AudioCompressionFormat.Vorbis ) ) {
                    using ( new ImporterValueScope<float>( v.Quality, "Quality" ) )
                        v.Quality.Value = EditorGUILayout.Slider( v.Quality, 0f, 100f );
                }

                using ( new ImporterValueScope<AudioSampleRateSetting>( v.SampleRateSetting, "SampleRateSetting" ) )
                    v.SampleRateSetting.Value = ( AudioSampleRateSetting )EditorGUILayout.EnumPopup( v.SampleRateSetting );

            }
            EditorGUILayout.EndVertical();
        }

    }
}