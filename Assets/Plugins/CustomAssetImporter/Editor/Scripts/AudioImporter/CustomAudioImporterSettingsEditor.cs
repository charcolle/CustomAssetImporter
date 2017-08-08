using UnityEngine;
using UnityEditor;
using System.IO;

namespace charcolle.Utility.CustomAssetImporter {

    [CustomEditor( typeof( CustomAudioImporterSettings ) )]
    public class CustomAudioImporterSettingsEditor : Editor {

        private CustomAudioImporterSettings myTarget;

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
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space( 3f );
            }
            EditorGUILayout.EndHorizontal();

            if ( myTarget.CustomImporterSettings.Count == 0 )
                EditorGUILayout.HelpBox( "No Importers", MessageType.Warning );

            if ( GUI.changed ) {
                Undo.RegisterCompleteObjectUndo( target, "Change CustomAudioImporter Setting" );
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
                GUILayout.Label( "Audio Importer Settings" );
                GUILayout.FlexibleSpace();
                GUI.backgroundColor = Color.green;
                if ( GUILayout.Button( "+", CustomAssetImporterStyles.ToolBox, GUILayout.Width( 70f ) ) ) {
                    myTarget.CustomImporterSettings.Add( new CustomAudioImporter() );
                }
            }
            EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
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
                        ImportPerform( importer );
                    }
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

                // drop perform
                var dropFileName = FileHelper.GetDraggedAssetPath( curEvent, dropArea );
                if( !string.IsNullOrEmpty( dropFileName ) )
                    importer.TargetName = dropFileName;

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

        private void ImportPerform( CustomAudioImporter importer ) {
            if ( string.IsNullOrEmpty( importer.TargetName ) )
                return;

            if ( importer.Type.Equals( ImportTargetType.FilePath ) ) {
                AssetDatabase.ImportAsset( importer.TargetName, ImportAssetOptions.Default );

            } else if ( importer.Type.Equals( ImportTargetType.FileName ) ) {
                var files = AssetDatabase.FindAssets( Path.GetFileNameWithoutExtension( importer.TargetName ) );
                for( int i = 0; i < files.Length; i++ )
                    AssetDatabase.ImportAsset( AssetDatabase.GUIDToAssetPath( files[i] ), ImportAssetOptions.Default );

            } else if ( importer.Type.Equals( ImportTargetType.DirectoryName ) ) {
                var dirName = Path.GetFileName( importer.TargetName );
                var dirs = Directory.GetDirectories( Application.dataPath, dirName, SearchOption.AllDirectories );

                for( int i = 0; i < dirs.Length; i++ ) {
                    var dirPath = dirs[i].Replace( "\\", "/" ).Replace( Application.dataPath, "Assets" );
                    AssetDatabase.ImportAsset( dirPath, ImportAssetOptions.ImportRecursive );
                }

            } else {
                AssetDatabase.ImportAsset( importer.TargetName, ImportAssetOptions.ImportRecursive );
            }
        }
    }
}