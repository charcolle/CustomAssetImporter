using UnityEngine;
using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    public class CustomModelProcessor: AssetPostprocessor {

        void OnPreprocessModel() {
            var customSettings = FileHelper.GetModelImporter();
            if ( customSettings != null )
                ImporterCustomModel( customSettings.GetCustomImporter( assetPath ) );
        }
        //=============================================================================
        // process
        //=============================================================================
        /// <summary>
        /// set importer
        /// </summary>
        private void ImporterCustomModel( CustomModelImporter customImporter ) {
            if ( customImporter == null )
                return;

            ModelImporter modelImporter = assetImporter as ModelImporter;
            CustomModelImporterValues customSettings = customImporter.ImporterSetting;
            SetAnimationClips( modelImporter, customSettings );

            // model
            if ( customSettings.ScaleFactor.isEditable )
                modelImporter.globalScale = customSettings.ScaleFactor;

            if ( customSettings.UseFileScale.isEditable )
                modelImporter.useFileScale = customSettings.UseFileScale;

            if ( customSettings.MeshCompression.isEditable )
                modelImporter.meshCompression = customSettings.MeshCompression;

            if ( customSettings.ReadWriteEnabled.isEditable )
                modelImporter.isReadable = customSettings.ReadWriteEnabled;

            if ( customSettings.OptimizeMesh.isEditable )
                modelImporter.optimizeMesh = customSettings.OptimizeMesh;

            if ( customSettings.ImportBlendShapes.isEditable )
                modelImporter.importBlendShapes = customSettings.ImportBlendShapes;

            if ( customSettings.GenerateColliders.isEditable )
                modelImporter.addCollider = customSettings.GenerateColliders;

            if ( customSettings.KeepQuads.isEditable )
                modelImporter.keepQuads = customSettings.KeepQuads;

            if ( customSettings.WeldVertics.isEditable )
                modelImporter.weldVertices = customSettings.WeldVertics;

            if ( customSettings.ImportVisibility.isEditable )
                modelImporter.importVisibility = customSettings.ImportVisibility;

            if ( customSettings.ImportCameras.isEditable )
                modelImporter.importCameras = customSettings.ImportCameras;

            if ( customSettings.ImportLights.isEditable )
                modelImporter.importLights = customSettings.ImportLights;

            if ( customSettings.SwapUVs.isEditable )
                modelImporter.swapUVChannels = customSettings.SwapUVs;

            //if ( customSettings.GenerateLightMapUVs.isEditable )
            //    modelImporter.add = customSettings.GenerateLightMapUVs;

            if ( customSettings.Normals.isEditable )
                modelImporter.importNormals = customSettings.Normals;

            if ( customSettings.NormalsMode.isEditable )
                modelImporter.normalCalculationMode = customSettings.NormalsMode;

            if ( customSettings.SmoothingAngle.isEditable )
                modelImporter.normalSmoothingAngle = customSettings.SmoothingAngle;

            if ( customSettings.Tangents.isEditable )
                modelImporter.importTangents = customSettings.Tangents;

            if ( customSettings.ImportMaterials.isEditable )
                modelImporter.importMaterials = customSettings.ImportMaterials;

            if ( customSettings.MaterialNaming.isEditable )
                modelImporter.materialName = customSettings.MaterialNaming;

            if ( customSettings.MaterialSearch.isEditable )
                modelImporter.materialSearch = customSettings.MaterialSearch;

            // rig
            if ( customSettings.AnimationType.isEditable )
                modelImporter.animationType = customSettings.AnimationType;

            if ( customSettings.OptimizeGameObject.isEditable )
                modelImporter.optimizeGameObjects = customSettings.OptimizeGameObject;

            // animation
            if ( customSettings.ImportAnimation.isEditable )
                modelImporter.importAnimation = customSettings.ImportAnimation;

            //if ( customSettings.BakeAnimations.isEditable )
            //    modelImporter.bakeIK = customSettings.BakeAnimations;

            if ( customSettings.ResampleCurves.isEditable )
                modelImporter.resampleCurves = customSettings.ResampleCurves;

            if ( customSettings.AnimCompression.isEditable )
                modelImporter.animationCompression = customSettings.AnimCompression;

            if ( customSettings.RotaionError.isEditable )
                modelImporter.animationRotationError = customSettings.RotaionError;

            if ( customSettings.PositionError.isEditable )
                modelImporter.animationPositionError = customSettings.PositionError;

            if ( customSettings.ScaleError.isEditable )
                modelImporter.animationScaleError = customSettings.ScaleError;

            if ( customImporter.isLogger )
                Debug.Log( "CustomAssetImporter: " + customImporter.Log );
        }

        /// <summary>
        /// set AnimationClip
        /// </summary>
        private void SetAnimationClips( ModelImporter importer, CustomModelImporterValues customSettings ) {
            var clips = importer.clipAnimations;
            if ( clips == null )
                return;

            for( int i = 0; i < clips.Length; i++ ) {
                var clip = clips[i];

                if ( customSettings.LoopTime.isEditable )
                    clip.loopTime = customSettings.LoopTime;
            }

            importer.clipAnimations = clips;
        }

    }
}