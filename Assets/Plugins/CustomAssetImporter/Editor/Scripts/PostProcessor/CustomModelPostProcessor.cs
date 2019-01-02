using UnityEngine;
using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    public class CustomModelProcessor: AssetPostprocessor {

        void OnPostprocessModel( GameObject g ) {
            Process();
        }

        private void Process() {
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
            if ( customImporter == null || !customImporter.IsEnable )
                return;

            ModelImporter modelImporter = assetImporter as ModelImporter;
            CustomModelImporterSettingValue customSettings = customImporter.ImporterSetting;
            SetAnimationClips( modelImporter, customSettings );

            // model
            if ( customSettings.ScaleFactor.IsConfigurable )
                modelImporter.globalScale = customSettings.ScaleFactor;
#if UNITY_2017_1_OR_NEWER
            if ( customSettings.UseFileScale.IsConfigurable )
                modelImporter.useFileScale = customSettings.UseFileScale;
#endif
            if ( customSettings.MeshCompression.IsConfigurable )
                modelImporter.meshCompression = customSettings.MeshCompression;

            if ( customSettings.ReadWriteEnabled.IsConfigurable )
                modelImporter.isReadable = customSettings.ReadWriteEnabled;

            if ( customSettings.OptimizeMesh.IsConfigurable )
                modelImporter.optimizeMesh = customSettings.OptimizeMesh;

            if ( customSettings.ImportBlendShapes.IsConfigurable )
                modelImporter.importBlendShapes = customSettings.ImportBlendShapes;

            if ( customSettings.GenerateColliders.IsConfigurable )
                modelImporter.addCollider = customSettings.GenerateColliders;

#if UNITY_5_6_OR_NEWER
            if ( customSettings.KeepQuads.IsConfigurable )
                modelImporter.keepQuads = customSettings.KeepQuads;
#endif
#if UNITY_2017_3_OR_NEWER
            if ( customSettings.IndexFormat.IsConfigurable )
                modelImporter.indexFormat = customSettings.IndexFormat;
#endif
#if UNITY_5_6_OR_NEWER
            if ( customSettings.WeldVertics.IsConfigurable )
                modelImporter.weldVertices = customSettings.WeldVertics;
#endif
#if UNITY_2017_1_OR_NEWER
            if ( customSettings.ImportVisibility.IsConfigurable )
                modelImporter.importVisibility = customSettings.ImportVisibility;

            if ( customSettings.ImportCameras.IsConfigurable )
                modelImporter.importCameras = customSettings.ImportCameras;

            if ( customSettings.ImportLights.IsConfigurable )
                modelImporter.importLights = customSettings.ImportLights;
#endif
#if UNITY_2017_3_OR_NEWER
            if ( customSettings.PreserverHierarchy.IsConfigurable )
                modelImporter.preserveHierarchy = customSettings.PreserverHierarchy;
#endif
            if ( customSettings.SwapUVs.IsConfigurable )
                modelImporter.swapUVChannels = customSettings.SwapUVs;

            if ( customSettings.GenerateLightMapUVs.IsConfigurable ) {
                modelImporter.generateSecondaryUV = customSettings.GenerateLightMapUVs;

                if( customSettings.GenerateLightMapUVs.Value ) {
                    if ( customSettings.HardAngle.IsConfigurable )
                        modelImporter.secondaryUVHardAngle = customSettings.HardAngle;

                    if ( customSettings.PackMargin.IsConfigurable )
                        modelImporter.secondaryUVPackMargin = customSettings.PackMargin;

                    if ( customSettings.AngleError.IsConfigurable )
                        modelImporter.secondaryUVAngleDistortion = customSettings.AngleError;

                    if ( customSettings.AreaError.IsConfigurable )
                        modelImporter.secondaryUVAreaDistortion = customSettings.AreaError;
                }
            }

            if ( customSettings.Normals.IsConfigurable )
                modelImporter.importNormals = customSettings.Normals;
#if UNITY_2017_1_OR_NEWER
            if ( customSettings.NormalsMode.IsConfigurable )
                modelImporter.normalCalculationMode = customSettings.NormalsMode;
#endif
            if ( customSettings.SmoothingAngle.IsConfigurable )
                modelImporter.normalSmoothingAngle = customSettings.SmoothingAngle;

            if ( customSettings.Tangents.IsConfigurable )
                modelImporter.importTangents = customSettings.Tangents;

            // material
            if ( customSettings.ImportMaterials.IsConfigurable ) {
                modelImporter.importMaterials = customSettings.ImportMaterials;

                if( customSettings.ImportMaterials.Value ) {
#if UNITY_2017_3_OR_NEWER
                    if ( customSettings.MaterialLocation.IsConfigurable )
                        modelImporter.materialLocation = customSettings.MaterialLocation;
#endif
                    if ( customSettings.MaterialNaming.IsConfigurable )
                        modelImporter.materialName = customSettings.MaterialNaming;

                    if ( customSettings.MaterialSearch.IsConfigurable )
                        modelImporter.materialSearch = customSettings.MaterialSearch;
                }
            }

            // rig
            if ( customSettings.AnimationType.IsConfigurable )
                modelImporter.animationType = customSettings.AnimationType;

            if ( customSettings.OptimizeGameObject.IsConfigurable )
                modelImporter.optimizeGameObjects = customSettings.OptimizeGameObject;

            // animation
            if ( customSettings.ImportAnimation.IsConfigurable )
                modelImporter.importAnimation = customSettings.ImportAnimation;

            //if ( customSettings.BakeAnimations.isEditable )
            //    modelImporter.bakeIK = customSettings.BakeAnimations;

            if ( customSettings.ResampleCurves.IsConfigurable )
                modelImporter.resampleCurves = customSettings.ResampleCurves;

            if ( customSettings.AnimCompression.IsConfigurable )
                modelImporter.animationCompression = customSettings.AnimCompression;

            if ( customSettings.RotaionError.IsConfigurable )
                modelImporter.animationRotationError = customSettings.RotaionError;

            if ( customSettings.PositionError.IsConfigurable )
                modelImporter.animationPositionError = customSettings.PositionError;

            if ( customSettings.ScaleError.IsConfigurable )
                modelImporter.animationScaleError = customSettings.ScaleError;
#if UNITY_2017_2_OR_NEWER
            if ( customSettings.AnimatedCustomProperties.IsConfigurable )
                modelImporter.importAnimatedCustomProperties = customSettings.AnimatedCustomProperties;
#endif

            if ( customImporter.IsLogger )
                Debug.Log( string.Format( "CustomModelImporter:" + customImporter.Log + "\nProcessed: {0}", assetPath ) );
        }

        /// <summary>
        /// set AnimationClip
        /// </summary>
        private void SetAnimationClips( ModelImporter importer, CustomModelImporterSettingValue customSettings ) {
            var clips = importer.clipAnimations;
            if ( clips == null )
                return;

            for( int i = 0; i < clips.Length; i++ ) {
                var clip = clips[i];

                if ( customSettings.LoopTime.IsConfigurable )
                    clip.loopTime = customSettings.LoopTime;
            }

            importer.clipAnimations = clips;
        }

    }
}