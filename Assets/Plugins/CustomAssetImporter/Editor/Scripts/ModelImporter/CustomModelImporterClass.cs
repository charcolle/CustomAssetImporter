using System;
using UnityEditor;
using UnityEngine;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    public class CustomModelImporter: CustomImporterClass<GenericOfCustomModelImporter> {

        public CustomModelImporter() {
            ImporterSetting           = new GenericOfCustomModelImporter( true );
        }

        public CustomModelImporter( CustomModelImporter copy ) {

        }

    }

    [Serializable]
    public class GenericOfCustomModelImporter: ImporterValue<CustomModelImporterValues> {

        public GenericOfCustomModelImporter( bool editable ) {
            isEditable = editable;
            Value      = new CustomModelImporterValues();
        }

    }

    [Serializable]
    public class CustomModelImporterValues {

        // Model
        public ImporterIntValue ScaleFactor;
        public ImporterBoolValue UseFileScale;
        public ImporterMeshCompressionValue MeshCompression;

        public ImporterBoolValue ReadWriteEnabled;
        public ImporterBoolValue OptimizeMesh;
        public ImporterBoolValue ImportBlendShapes;
        public ImporterBoolValue GenerateColliders;
        public ImporterBoolValue KeepQuads;
        public ImporterBoolValue WeldVertics;
        public ImporterBoolValue ImportVisibility;
        public ImporterBoolValue ImportCameras;
        public ImporterBoolValue ImportLights;
        public ImporterBoolValue SwapUVs;
        public ImporterBoolValue GenerateLightMapUVs; // ?

        public ImporterImportNormalsValue Normals;
        public ImporterNormalsModeValue NormalsMode;
        public ImporterIntValue SmoothingAngle;
        public ImporterImportTangensValue Tangents;

        public ImporterBoolValue ImportMaterials;
        public ImporterMaterialNamingValue MaterialNaming;
        public ImporterMaterialSearchValue MaterialSearch;

        // Rig
        public ImporterAnimationTypeValue AnimationType;
        public ImporterBoolValue OptimizeGameObject;

        // animation
        public ImporterBoolValue ImportAnimation;
        public ImporterBoolValue BakeAnimations; // ?
        public ImporterBoolValue ResampleCurves;
        public ImporterAnimCompressionValue AnimCompression;
        public ImporterFloatValue RotaionError;
        public ImporterFloatValue PositionError;
        public ImporterFloatValue ScaleError;

        // animation clip
        public ImporterBoolValue LoopTime;

        public CustomModelImporterValues() {
            ScaleFactor           = new ImporterIntValue();
            UseFileScale          = new ImporterBoolValue();
            MeshCompression       = new ImporterMeshCompressionValue();
            MeshCompression.Value = ModelImporterMeshCompression.Medium;

            ReadWriteEnabled      = new ImporterBoolValue();
            OptimizeMesh          = new ImporterBoolValue();
            ImportBlendShapes     = new ImporterBoolValue();
            GenerateColliders     = new ImporterBoolValue();
            KeepQuads             = new ImporterBoolValue();
            WeldVertics           = new ImporterBoolValue();
            ImportVisibility      = new ImporterBoolValue();
            ImportCameras         = new ImporterBoolValue();
            ImportLights          = new ImporterBoolValue();
            SwapUVs               = new ImporterBoolValue();
            GenerateLightMapUVs   = new ImporterBoolValue();

            Normals               = new ImporterImportNormalsValue();
            Normals.Value         = ModelImporterNormals.Import;
            NormalsMode           = new ImporterNormalsModeValue();
            SmoothingAngle        = new ImporterIntValue();
            SmoothingAngle.Value  = 60;
            Tangents              = new ImporterImportTangensValue();
            Tangents.Value        = ModelImporterTangents.None;

            ImportMaterials       = new ImporterBoolValue();
            ImportMaterials.Value = true;
            MaterialNaming        = new ImporterMaterialNamingValue();
            MaterialSearch        = new ImporterMaterialSearchValue();

            AnimationType         = new ImporterAnimationTypeValue();
            AnimationType.Value   = ModelImporterAnimationType.Generic;
            OptimizeGameObject    = new ImporterBoolValue();

            ImportAnimation       = new ImporterBoolValue();
            ImportAnimation.Value = true;
            BakeAnimations        = new ImporterBoolValue();
            ResampleCurves        = new ImporterBoolValue();
            AnimCompression       = new ImporterAnimCompressionValue();
            AnimCompression.Value = ModelImporterAnimationCompression.Optimal;
            RotaionError          = new ImporterFloatValue();
            RotaionError.Value    = 0.5f;
            PositionError         = new ImporterFloatValue();
            PositionError.Value   = 0.5f;
            ScaleError            = new ImporterFloatValue();
            ScaleError.Value      = 0.5f;

            LoopTime              = new ImporterBoolValue();
        }
    }

    [Serializable]
    public class ImporterMeshCompressionValue: ImporterValue<ModelImporterMeshCompression> { }

    [Serializable]
    public class ImporterImportNormalsValue: ImporterValue<ModelImporterNormals> { }

    [Serializable]
    public class ImporterNormalsModeValue: ImporterValue<ModelImporterNormalCalculationMode> { }

    [Serializable]
    public class ImporterImportTangensValue: ImporterValue<ModelImporterTangents> { }

    [Serializable]
    public class ImporterMaterialNamingValue: ImporterValue<ModelImporterMaterialName> { }

    [Serializable]
    public class ImporterMaterialSearchValue: ImporterValue<ModelImporterMaterialSearch> { }

    [Serializable]
    public class ImporterAnimationTypeValue: ImporterValue<ModelImporterAnimationType> { }

    [Serializable]
    public class ImporterAnimCompressionValue: ImporterValue<ModelImporterAnimationCompression> { }

}