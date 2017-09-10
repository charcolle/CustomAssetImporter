using UnityEngine;
using UnityEditor;
using System.IO;

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
                    myTarget.CustomImporterSettings.Add( new CustomModelImporter() );
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
                if( GUILayout.Button( "Collapse all", EditorStyles.toolbarButton, GUILayout.Width( 120f ) ) ) {
                    for( int i = 0; i < myTarget.CustomImporterSettings.Count; i++ ) {
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

        private void ImporterDrawer( CustomModelImporter importer ) {
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
                if ( !string.IsNullOrEmpty( dropFileName ) )
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

                }
                GUILayout.Space( 3f );
            }
            EditorGUILayout.EndVertical();

            if ( deleteFlag )
                myTarget.CustomImporterSettings.Remove( importer );
        }

        private void SettingDrawer( CustomModelImporterValues v ) {

            GUILayout.Label( "Model" );
            EditorGUILayout.BeginVertical( EditorStyles.helpBox );
            {
                // model
                using ( new ImporterValueScope<int>( v.ScaleFactor, "ScaleFactor" ) )
                    v.ScaleFactor.Value = EditorGUILayout.IntField( v.ScaleFactor );

                using ( new ImporterValueScope<bool>( v.UseFileScale, "UseFileScale" ) )
                    v.UseFileScale.Value = EditorGUILayout.Toggle( v.UseFileScale );

                using ( new ImporterValueScope<ModelImporterMeshCompression>( v.MeshCompression, "MeshCompression" ) )
                    v.MeshCompression.Value = ( ModelImporterMeshCompression )EditorGUILayout.EnumPopup( v.MeshCompression );

                GUILayout.Space( 5f );

                using ( new ImporterValueScope<bool>( v.ReadWriteEnabled, "ReadWriteEnabled" ) )
                    v.ReadWriteEnabled.Value = EditorGUILayout.Toggle( v.ReadWriteEnabled );

                using ( new ImporterValueScope<bool>( v.OptimizeMesh, "OptimizeMesh" ) )
                    v.OptimizeMesh.Value = EditorGUILayout.Toggle( v.OptimizeMesh );

                using ( new ImporterValueScope<bool>( v.ImportBlendShapes, "ImportBlendShapes" ) )
                    v.ImportBlendShapes.Value = EditorGUILayout.Toggle( v.ImportBlendShapes );

                using ( new ImporterValueScope<bool>( v.GenerateColliders, "GenerateColliders" ) )
                    v.GenerateColliders.Value = EditorGUILayout.Toggle( v.GenerateColliders );

                using ( new ImporterValueScope<bool>( v.KeepQuads, "KeepQuads" ) )
                    v.KeepQuads.Value = EditorGUILayout.Toggle( v.KeepQuads );

                using ( new ImporterValueScope<bool>( v.WeldVertics, "WeldVertics" ) )
                    v.WeldVertics.Value = EditorGUILayout.Toggle( v.WeldVertics );

                using ( new ImporterValueScope<bool>( v.ImportVisibility, "ImportVisibility" ) )
                    v.ImportVisibility.Value = EditorGUILayout.Toggle( v.ImportVisibility );

                using ( new ImporterValueScope<bool>( v.ImportCameras, "ImportCameras" ) )
                    v.ImportCameras.Value = EditorGUILayout.Toggle( v.ImportCameras );

                using ( new ImporterValueScope<bool>( v.ImportLights, "ImportLights" ) )
                    v.ImportLights.Value = EditorGUILayout.Toggle( v.ImportLights );

                using ( new ImporterValueScope<bool>( v.SwapUVs, "SwapUVs" ) )
                    v.SwapUVs.Value = EditorGUILayout.Toggle( v.SwapUVs );

                GUILayout.Space( 5f );

                using ( new ImporterValueScope<ModelImporterNormals>( v.Normals, "Normals" ) )
                    v.Normals.Value = ( ModelImporterNormals )EditorGUILayout.EnumPopup( v.Normals );

                if( v.Normals.Value.Equals( ModelImporterNormals.Calculate ) ) {
                    using ( new ImporterValueScope<ModelImporterNormalCalculationMode>( v.NormalsMode, "NormalsMode" ) )
                        v.NormalsMode.Value = ( ModelImporterNormalCalculationMode )EditorGUILayout.EnumPopup( v.NormalsMode );

                    using ( new ImporterValueScope<int>( v.SmoothingAngle, "SmoothingAngle" ) )
                        v.SmoothingAngle.Value = EditorGUILayout.IntSlider( v.SmoothingAngle, 0, 180 );
                } else {
                    v.NormalsMode.isEditable = false;
                    v.SmoothingAngle.isEditable = false;
                }

                using ( new ImporterValueScope<ModelImporterTangents>( v.Tangents, "Tangents" ) )
                    v.Tangents.Value = ( ModelImporterTangents )EditorGUILayout.EnumPopup( v.Tangents );

                GUILayout.Space( 5f );

                using ( new ImporterValueScope<bool>( v.ImportMaterials, "ImportMaterials" ) )
                    v.ImportMaterials.Value = EditorGUILayout.Toggle( v.ImportMaterials );

                if( v.ImportMaterials.Value ) {
                    using ( new ImporterValueScope<ModelImporterMaterialName>( v.MaterialNaming, "MaterialNaming" ) )
                        v.MaterialNaming.Value = ( ModelImporterMaterialName )EditorGUILayout.EnumPopup( v.MaterialNaming );

                    using ( new ImporterValueScope<ModelImporterMaterialSearch>( v.MaterialSearch, "MaterialSearch" ) )
                        v.MaterialSearch.Value = ( ModelImporterMaterialSearch )EditorGUILayout.EnumPopup( v.MaterialSearch );
                } else {
                    v.MaterialNaming.isEditable = false;
                    v.MaterialSearch.isEditable = false;
                }
            }
            EditorGUILayout.EndVertical();

            GUILayout.Label( "Rig" );
            EditorGUILayout.BeginVertical( EditorStyles.helpBox );
            {
                // model
                using ( new ImporterValueScope<ModelImporterAnimationType>( v.AnimationType, "AnimationType" ) )
                    v.AnimationType.Value = ( ModelImporterAnimationType )EditorGUILayout.EnumPopup( v.AnimationType );

                using ( new ImporterValueScope<bool>( v.OptimizeGameObject, "OptimizeGameObject" ) )
                    v.OptimizeGameObject.Value = EditorGUILayout.Toggle( v.OptimizeGameObject );

            }
            EditorGUILayout.EndVertical();

            GUILayout.Label( "Animation" );
            EditorGUILayout.BeginVertical( EditorStyles.helpBox );
            {
                using ( new ImporterValueScope<bool>( v.ImportAnimation, "ImportAnimation" ) )
                    v.ImportAnimation.Value = EditorGUILayout.Toggle( v.ImportAnimation );

                if( v.ImportAnimation.Value ) {
                    using ( new ImporterValueScope<ModelImporterAnimationCompression>( v.AnimCompression, "AnimCompression" ) )
                        v.AnimCompression.Value = ( ModelImporterAnimationCompression )EditorGUILayout.EnumPopup( v.AnimCompression );

                    using ( new ImporterValueScope<float>( v.RotaionError, "RotaionError" ) )
                        v.RotaionError.Value = EditorGUILayout.FloatField( v.RotaionError );

                    using ( new ImporterValueScope<float>( v.PositionError, "PositionError" ) )
                        v.PositionError.Value = EditorGUILayout.FloatField( v.PositionError );

                    using ( new ImporterValueScope<float>( v.ScaleError, "ScaleError" ) )
                        v.ScaleError.Value = EditorGUILayout.FloatField( v.ScaleError );
                } else {
                    v.BakeAnimations.isEditable = false;
                    v.RotaionError.isEditable = false;
                    v.ScaleError.isEditable = false;
                }
            }
            EditorGUILayout.EndVertical();

            GUILayout.Label( "Animation Clip Default" );
            EditorGUILayout.BeginVertical( EditorStyles.helpBox );
            {
                using ( new ImporterValueScope<bool>( v.LoopTime, "LoopTime" ) )
                    v.LoopTime.Value = EditorGUILayout.Toggle( v.LoopTime );
            }
            EditorGUILayout.EndVertical();
        }

        private void ImportPerform( CustomModelImporter importer ) {
            if ( string.IsNullOrEmpty( importer.TargetName ) )
                return;

            if ( importer.Type.Equals( ImportTargetType.FilePath ) ) {
                AssetDatabase.ImportAsset( importer.TargetName, ImportAssetOptions.Default );

            } else if ( importer.Type.Equals( ImportTargetType.FileName ) ) {
                var files = AssetDatabase.FindAssets( Path.GetFileNameWithoutExtension( importer.TargetName ) );
                for ( int i = 0; i < files.Length; i++ )
                    AssetDatabase.ImportAsset( AssetDatabase.GUIDToAssetPath( files[i] ), ImportAssetOptions.Default );

            } else if ( importer.Type.Equals( ImportTargetType.DirectoryName ) ) {
                var dirName = Path.GetFileName( importer.TargetName );
                var dirs = Directory.GetDirectories( Application.dataPath, dirName, SearchOption.AllDirectories );

                for ( int i = 0; i < dirs.Length; i++ ) {
                    var dirPath = dirs[i].Replace( "\\", "/" ).Replace( Application.dataPath, "Assets" );
                    AssetDatabase.ImportAsset( dirPath, ImportAssetOptions.ImportRecursive );
                }

            } else {
                AssetDatabase.ImportAsset( importer.TargetName, ImportAssetOptions.ImportRecursive );
            }
        }

    }
}