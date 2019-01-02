using UnityEngine;
using UnityEditor;
using System;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    internal class CustomModelImporter: CustomImporterClass<CustomModelImporterValue> {

        public CustomModelImporter() {
            ImporterSetting           = new CustomModelImporterValue( true );
        }

        public CustomModelImporter( CustomModelImporter copy ) {
            ImporterSetting           = new CustomModelImporterValue( copy.ImporterSetting );
        }

        public static CustomModelImporter Root {
            get {
                return new CustomModelImporter {
                    name = "",
                    depth = -1,
                    id = 0
                };
            }
        }

    }

    [Serializable]
    internal class CustomModelImporterValue: ImporterValue<CustomModelImporterSettingValue> {

        public CustomModelImporterValue( bool editable ) {
            IsConfigurable = editable;
            Value      = new CustomModelImporterSettingValue();
        }
        public CustomModelImporterValue( CustomModelImporterValue copy ) {
            IsConfigurable = copy.IsConfigurable;
            Value      = new CustomModelImporterSettingValue( copy.Value );
        }
    }

    [Serializable]
    internal class CustomModelImporterSettingValue {

        // Model
        public ImporterIntValue ScaleFactor;
#if UNITY_2017_1_OR_NEWER
        public ImporterBoolValue UseFileScale;
#endif
        public ImporterMeshCompressionValue MeshCompression;

        public ImporterBoolValue ReadWriteEnabled;
        public ImporterBoolValue OptimizeMesh;
        public ImporterBoolValue ImportBlendShapes;
        public ImporterBoolValue GenerateColliders;
        public ImporterBoolValue KeepQuads;
#if UNITY_2017_3_OR_NEWER
        public ImporterIndexFormatValue IndexFormat;
#endif
#if UNITY_5_6_OR_NEWER
        public ImporterBoolValue WeldVertics;
#endif
#if UNITY_2017_1_OR_NEWER
        public ImporterBoolValue ImportVisibility;
        public ImporterBoolValue ImportCameras;
        public ImporterBoolValue ImportLights;
#endif
#if UNITY_2017_3_OR_NEWER
        public ImporterBoolValue PreserverHierarchy;
#endif
        public ImporterBoolValue SwapUVs;
        public ImporterBoolValue GenerateLightMapUVs; // ?
        public ImporterIntValue HardAngle;
        public ImporterIntValue PackMargin;
        public ImporterIntValue AngleError;
        public ImporterIntValue AreaError;

        // Normal
        public ImporterImportNormalsValue Normals;
#if UNITY_2017_1_OR_NEWER
        public ImporterNormalsModeValue NormalsMode;
#endif
        public ImporterIntValue SmoothingAngle;
        public ImporterImportTangensValue Tangents;

        // Material
        public ImporterBoolValue ImportMaterials;
#if UNITY_2017_3_OR_NEWER
        public ImporterMaterialLocationValue MaterialLocation;
#endif
        public ImporterMaterialNamingValue MaterialNaming;
        public ImporterMaterialSearchValue MaterialSearch;

        // Rig
        public ImporterAnimationTypeValue AnimationType;
        public ImporterBoolValue OptimizeGameObject;

        // Animation
        public ImporterBoolValue ImportAnimation;
        public ImporterBoolValue BakeAnimations; // ?
        public ImporterBoolValue ResampleCurves;
        public ImporterAnimCompressionValue AnimCompression;
        public ImporterFloatValue RotaionError;
        public ImporterFloatValue PositionError;
        public ImporterFloatValue ScaleError;
#if UNITY_2017_2_OR_NEWER
        public ImporterBoolValue AnimatedCustomProperties;
#endif

        // animation clip
        public ImporterBoolValue LoopTime;

        #region constructor
        public CustomModelImporterSettingValue() {
            ScaleFactor              = new ImporterIntValue();
            ScaleFactor.Value        = 1;
#if UNITY_2017_1_OR_NEWER
            UseFileScale = new ImporterBoolValue();
#endif
            MeshCompression = new ImporterMeshCompressionValue();
            MeshCompression.Value    = ModelImporterMeshCompression.Medium;

            ReadWriteEnabled         = new ImporterBoolValue();
            OptimizeMesh             = new ImporterBoolValue();
            ImportBlendShapes        = new ImporterBoolValue();
            GenerateColliders        = new ImporterBoolValue();
            KeepQuads                = new ImporterBoolValue();
#if UNITY_2017_3_OR_NEWER
            IndexFormat              = new ImporterIndexFormatValue();
#endif
#if UNITY_5_6_OR_NEWER
            WeldVertics              = new ImporterBoolValue();
#endif
#if UNITY_2017_1_OR_NEWER
            ImportVisibility         = new ImporterBoolValue();
            ImportCameras            = new ImporterBoolValue();
            ImportLights             = new ImporterBoolValue();
#endif
#if UNITY_2017_3_OR_NEWER
            PreserverHierarchy       = new ImporterBoolValue();
#endif
            SwapUVs                  = new ImporterBoolValue();
            GenerateLightMapUVs      = new ImporterBoolValue();

            HardAngle                = new ImporterIntValue();
            HardAngle.Value          = 88;
            PackMargin               = new ImporterIntValue();
            PackMargin.Value         = 4;
            AngleError               = new ImporterIntValue();
            AngleError.Value         = 8;
            AreaError                = new ImporterIntValue();
            AreaError.Value          = 15;

            Normals                  = new ImporterImportNormalsValue();
            Normals.Value            = ModelImporterNormals.Import;
#if UNITY_2017_1_OR_NEWER
            NormalsMode              = new ImporterNormalsModeValue();
#endif
            SmoothingAngle           = new ImporterIntValue();
            SmoothingAngle.Value     = 60;
            Tangents                 = new ImporterImportTangensValue();
            Tangents.Value           = ModelImporterTangents.None;

            ImportMaterials          = new ImporterBoolValue();
            ImportMaterials.Value    = true;
#if UNITY_2017_3_OR_NEWER
            MaterialLocation         = new ImporterMaterialLocationValue();
#endif
            MaterialNaming           = new ImporterMaterialNamingValue();
            MaterialSearch           = new ImporterMaterialSearchValue();

            AnimationType            = new ImporterAnimationTypeValue();
            AnimationType.Value      = ModelImporterAnimationType.Generic;
            OptimizeGameObject       = new ImporterBoolValue();

            ImportAnimation          = new ImporterBoolValue();
            ImportAnimation.Value    = true;
            BakeAnimations           = new ImporterBoolValue();
            ResampleCurves           = new ImporterBoolValue();
            AnimCompression          = new ImporterAnimCompressionValue();
            AnimCompression.Value    = ModelImporterAnimationCompression.Optimal;
            RotaionError             = new ImporterFloatValue();
            RotaionError.Value       = 0.5f;
            PositionError            = new ImporterFloatValue();
            PositionError.Value      = 0.5f;
            ScaleError               = new ImporterFloatValue();
            ScaleError.Value         = 0.5f;
#if UNITY_2017_2_OR_NEWER
            AnimatedCustomProperties = new ImporterBoolValue();
#endif

            LoopTime                 = new ImporterBoolValue();
        }

        public CustomModelImporterSettingValue( CustomModelImporterSettingValue copy ) {
            ScaleFactor              = new ImporterIntValue( copy.ScaleFactor );
#if UNITY_2017_1_OR_NEWER
            UseFileScale = new ImporterBoolValue( copy.UseFileScale );
#endif
            MeshCompression = new ImporterMeshCompressionValue( copy.MeshCompression );

            ReadWriteEnabled         = new ImporterBoolValue( copy.ReadWriteEnabled );
            OptimizeMesh             = new ImporterBoolValue( copy.OptimizeMesh );
            ImportBlendShapes        = new ImporterBoolValue( copy.ImportBlendShapes );
            GenerateColliders        = new ImporterBoolValue( copy.GenerateColliders );
            KeepQuads                = new ImporterBoolValue( copy.KeepQuads );
#if UNITY_2017_3_OR_NEWER
            IndexFormat              = new ImporterIndexFormatValue( copy.IndexFormat );
#endif
#if UNITY_5_6_OR_NEWER
            WeldVertics              = new ImporterBoolValue( copy.WeldVertics );
#endif
#if UNITY_2017_1_OR_NEWER
            ImportVisibility         = new ImporterBoolValue( copy.ImportVisibility );
            ImportCameras            = new ImporterBoolValue( copy.ImportCameras );
            ImportLights             = new ImporterBoolValue( copy.ImportLights );
#endif
#if UNITY_2017_3_OR_NEWER
            PreserverHierarchy       = new ImporterBoolValue( copy.PreserverHierarchy );
#endif
            SwapUVs                  = new ImporterBoolValue( copy.SwapUVs );
            GenerateLightMapUVs      = new ImporterBoolValue( copy.GenerateLightMapUVs );

            HardAngle                = new ImporterIntValue( copy.HardAngle );
            PackMargin               = new ImporterIntValue( copy.PackMargin );
            AngleError               = new ImporterIntValue( copy.AngleError );
            AreaError                = new ImporterIntValue( copy.AreaError );

            Normals                  = new ImporterImportNormalsValue( copy.Normals );
#if UNITY_2017_1_OR_NEWER
            NormalsMode              = new ImporterNormalsModeValue( copy.NormalsMode );
#endif
            SmoothingAngle           = new ImporterIntValue( copy.SmoothingAngle );
            Tangents                 = new ImporterImportTangensValue( copy.Tangents );

            ImportMaterials          = new ImporterBoolValue( copy.ImportMaterials );
#if UNITY_2017_3_OR_NEWER
            MaterialLocation         = new ImporterMaterialLocationValue( copy.MaterialLocation );
#endif
            MaterialNaming           = new ImporterMaterialNamingValue( copy.MaterialNaming );
            MaterialSearch           = new ImporterMaterialSearchValue( copy.MaterialSearch );

            AnimationType            = new ImporterAnimationTypeValue( copy.AnimationType );
            OptimizeGameObject       = new ImporterBoolValue( copy.OptimizeGameObject );

            ImportAnimation          = new ImporterBoolValue( copy.ImportAnimation );
            BakeAnimations           = new ImporterBoolValue( copy.BakeAnimations );
            ResampleCurves           = new ImporterBoolValue( copy.ResampleCurves );
            AnimCompression          = new ImporterAnimCompressionValue( copy.AnimCompression );
            RotaionError             = new ImporterFloatValue( copy.RotaionError );
            PositionError            = new ImporterFloatValue( copy.PositionError );
            ScaleError               = new ImporterFloatValue( copy.ScaleError );
#if UNITY_2017_2_OR_NEWER
            AnimatedCustomProperties = new ImporterBoolValue( copy.AnimatedCustomProperties );
#endif

            LoopTime                 = new ImporterBoolValue( copy.LoopTime );
        }
        #endregion

    }

    [Serializable]
    public class ImporterMeshCompressionValue: ImporterValue<ModelImporterMeshCompression> {
        public ImporterMeshCompressionValue() {}
        public ImporterMeshCompressionValue( ImporterValue<ModelImporterMeshCompression> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterImportNormalsValue: ImporterValue<ModelImporterNormals> {
        public ImporterImportNormalsValue() {}
        public ImporterImportNormalsValue( ImporterValue<ModelImporterNormals> copy ) : base( copy ) {}
    }
#if UNITY_2017_1_OR_NEWER
    [Serializable]
    public class ImporterNormalsModeValue: ImporterValue<ModelImporterNormalCalculationMode> {
        public ImporterNormalsModeValue() {}
        public ImporterNormalsModeValue( ImporterValue<ModelImporterNormalCalculationMode> copy ) : base( copy ) {}
    }
#endif

    [Serializable]
    public class ImporterImportTangensValue: ImporterValue<ModelImporterTangents> {
        public ImporterImportTangensValue() {}
        public ImporterImportTangensValue( ImporterValue<ModelImporterTangents> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterMaterialNamingValue: ImporterValue<ModelImporterMaterialName> {
        public ImporterMaterialNamingValue() {}
        public ImporterMaterialNamingValue( ImporterValue<ModelImporterMaterialName> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterMaterialSearchValue: ImporterValue<ModelImporterMaterialSearch> {
        public ImporterMaterialSearchValue() {}
        public ImporterMaterialSearchValue( ImporterValue<ModelImporterMaterialSearch> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterAnimationTypeValue: ImporterValue<ModelImporterAnimationType> {
        public ImporterAnimationTypeValue() {}
        public ImporterAnimationTypeValue( ImporterValue<ModelImporterAnimationType> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterAnimCompressionValue: ImporterValue<ModelImporterAnimationCompression> {
        public ImporterAnimCompressionValue() {}
        public ImporterAnimCompressionValue( ImporterValue<ModelImporterAnimationCompression> copy ) : base( copy ) {}
    }

#if UNITY_2017_3_OR_NEWER
    [Serializable]
    public class ImporterMaterialLocationValue: ImporterValue<ModelImporterMaterialLocation> {
        public ImporterMaterialLocationValue() { }
        public ImporterMaterialLocationValue( ImporterValue<ModelImporterMaterialLocation> copy ) : base( copy ) { }
    }
    [Serializable]
    public class ImporterIndexFormatValue: ImporterValue<ModelImporterIndexFormat> {
        public ImporterIndexFormatValue() { }
        public ImporterIndexFormatValue( ImporterValue<ModelImporterIndexFormat> copy ) : base( copy ) { }
    }
#endif

}