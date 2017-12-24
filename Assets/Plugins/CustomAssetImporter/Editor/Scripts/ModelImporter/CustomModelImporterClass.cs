using UnityEngine;
using UnityEditor;
using System;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    public class CustomModelImporter: CustomImporterClass<CustomModelImporterValue> {

        public CustomModelImporter() {
            ImporterSetting           = new CustomModelImporterValue( true );
        }

        public CustomModelImporter( CustomModelImporter copy ) {
            ImporterSetting           = new CustomModelImporterValue( copy.ImporterSetting );
        }

        public override void Draw() {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space( 20f );
                isFoldout = EditorGUILayout.Foldout( isFoldout, "Show Setting" );
            }
            EditorGUILayout.EndHorizontal();

            if ( isFoldout ) {
                ImporterSetting.Value.Draw();
                GUILayout.Space( 3f );
            }
        }

    }

    [Serializable]
    public class CustomModelImporterValue: ImporterValue<CustomModelImporterSettingValue> {

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
    public class CustomModelImporterSettingValue {

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

        #region drawer
        private int tabSelected = 0;
        private string[] tab = new string[] { "Model", "Rig", "Animation", "Material" };
        public void Draw() {

            tabSelected = GUILayout.Toolbar( tabSelected, tab );
            EditorGUILayout.BeginVertical( EditorStyles.helpBox );
            {
                switch ( tabSelected ) {
                    case 0:
                        using ( new ImporterValueScope<int>( ScaleFactor, "ScaleFactor" ) )
                            ScaleFactor.Value = EditorGUILayout.IntField( ScaleFactor );
#if UNITY_2017_1_OR_NEWER
                        using ( new ImporterValueScope<bool>( UseFileScale, "UseFileScale" ) )
                            UseFileScale.Value = EditorGUILayout.Toggle( UseFileScale );
#endif
                        using ( new ImporterValueScope<ModelImporterMeshCompression>( MeshCompression, "MeshCompression" ) )
                            MeshCompression.Value = ( ModelImporterMeshCompression )EditorGUILayout.EnumPopup( MeshCompression );

                        using ( new ImporterValueScope<bool>( ReadWriteEnabled, "ReadWriteEnabled" ) )
                            ReadWriteEnabled.Value = EditorGUILayout.Toggle( ReadWriteEnabled );

                        using ( new ImporterValueScope<bool>( OptimizeMesh, "OptimizeMesh" ) )
                            OptimizeMesh.Value = EditorGUILayout.Toggle( OptimizeMesh );

                        using ( new ImporterValueScope<bool>( ImportBlendShapes, "ImportBlendShapes" ) )
                            ImportBlendShapes.Value = EditorGUILayout.Toggle( ImportBlendShapes );

                        using ( new ImporterValueScope<bool>( GenerateColliders, "GenerateColliders" ) )
                            GenerateColliders.Value = EditorGUILayout.Toggle( GenerateColliders );

                        using ( new ImporterValueScope<bool>( KeepQuads, "KeepQuads" ) )
                            KeepQuads.Value = EditorGUILayout.Toggle( KeepQuads );
#if UNITY_2017_3_OR_NEWER
                        using ( new ImporterValueScope<ModelImporterIndexFormat>( IndexFormat, "IndexFormat" ) )
                            IndexFormat.Value = ( ModelImporterIndexFormat )EditorGUILayout.EnumPopup( IndexFormat );
#endif
#if UNITY_5_6_OR_NEWER
                        using ( new ImporterValueScope<bool>( WeldVertics, "WeldVertics" ) )
                            WeldVertics.Value = EditorGUILayout.Toggle( WeldVertics );
#endif
#if UNITY_2017_1_OR_NEWER
                        using ( new ImporterValueScope<bool>( ImportVisibility, "ImportVisibility" ) )
                            ImportVisibility.Value = EditorGUILayout.Toggle( ImportVisibility );

                        using ( new ImporterValueScope<bool>( ImportCameras, "ImportCameras" ) )
                            ImportCameras.Value = EditorGUILayout.Toggle( ImportCameras );

                        using ( new ImporterValueScope<bool>( ImportLights, "ImportLights" ) )
                            ImportLights.Value = EditorGUILayout.Toggle( ImportLights );
#endif
#if UNITY_2017_3_OR_NEWER
                        using ( new ImporterValueScope<bool>( PreserverHierarchy, "PreserverHierarchy" ) )
                            PreserverHierarchy.Value = EditorGUILayout.Toggle( PreserverHierarchy );
#endif
                        using ( new ImporterValueScope<bool>( SwapUVs, "SwapUVs" ) )
                            SwapUVs.Value = EditorGUILayout.Toggle( SwapUVs );

                        using ( new ImporterValueScope<bool>( GenerateLightMapUVs, "GenerateLightMapUVs" ) )
                            GenerateLightMapUVs.Value = EditorGUILayout.Toggle( GenerateLightMapUVs );

                        if( GenerateLightMapUVs.Value ) {
                            EditorGUILayout.BeginHorizontal();
                            {
                                GUILayout.Space( 10f );
                                EditorGUILayout.BeginVertical();
                                {
                                    using ( new ImporterValueScope<int>( HardAngle, "HardAngle" ) )
                                        HardAngle.Value = EditorGUILayout.IntSlider( HardAngle, 0, 180 );

                                    using ( new ImporterValueScope<int>( PackMargin, "PackMargin" ) )
                                        PackMargin.Value = EditorGUILayout.IntSlider( PackMargin, 1, 64 );

                                    using ( new ImporterValueScope<int>( AngleError, "AngleError" ) )
                                        AngleError.Value = EditorGUILayout.IntSlider( AngleError, 1, 75 );

                                    using ( new ImporterValueScope<int>( AreaError, "AreaError" ) )
                                        AreaError.Value = EditorGUILayout.IntSlider( AreaError, 1, 75 );
                                }
                                EditorGUILayout.EndVertical();
                            }
                            EditorGUILayout.EndHorizontal();
                        } else {
                            HardAngle.IsConfigurable  = false;
                            PackMargin.IsConfigurable = false;
                            AngleError.IsConfigurable = false;
                            AreaError.IsConfigurable  = false;
                        }

                        GUILayout.Space( 5f );

                        using ( new ImporterValueScope<ModelImporterNormals>( Normals, "Normals" ) )
                            Normals.Value = ( ModelImporterNormals )EditorGUILayout.EnumPopup( Normals );

                        if ( Normals.Value.Equals( ModelImporterNormals.Calculate ) ) {
#if UNITY_2017_1_OR_NEWER
                            using ( new ImporterValueScope<ModelImporterNormalCalculationMode>( NormalsMode, "NormalsMode" ) )
                                NormalsMode.Value = ( ModelImporterNormalCalculationMode )EditorGUILayout.EnumPopup( NormalsMode );
#endif
                            using ( new ImporterValueScope<int>( SmoothingAngle, "SmoothingAngle" ) )
                                SmoothingAngle.Value = EditorGUILayout.IntSlider( SmoothingAngle, 0, 180 );
                        } else {
#if UNITY_2017_1_OR_NEWER
                            NormalsMode.IsConfigurable = false;
#endif
                            SmoothingAngle.IsConfigurable = false;
                        }

                        using ( new ImporterValueScope<ModelImporterTangents>( Tangents, "Tangents" ) )
                            Tangents.Value = ( ModelImporterTangents )EditorGUILayout.EnumPopup( Tangents );

                        GUILayout.Space( 5f );
                        break;
                    case 1:
                        using ( new ImporterValueScope<ModelImporterAnimationType>( AnimationType, "AnimationType" ) )
                            AnimationType.Value = ( ModelImporterAnimationType )EditorGUILayout.EnumPopup( AnimationType );

                        using ( new ImporterValueScope<bool>( OptimizeGameObject, "OptimizeGameObject" ) )
                            OptimizeGameObject.Value = EditorGUILayout.Toggle( OptimizeGameObject );

                        break;
                    case 2:
                        using ( new ImporterValueScope<bool>( ImportAnimation, "ImportAnimation" ) )
                            ImportAnimation.Value = EditorGUILayout.Toggle( ImportAnimation );

                        if ( ImportAnimation.Value ) {
                            using ( new ImporterValueScope<ModelImporterAnimationCompression>( AnimCompression, "AnimCompression" ) )
                                AnimCompression.Value = ( ModelImporterAnimationCompression )EditorGUILayout.EnumPopup( AnimCompression );

                            using ( new ImporterValueScope<float>( RotaionError, "RotaionError" ) )
                                RotaionError.Value = EditorGUILayout.FloatField( RotaionError );

                            using ( new ImporterValueScope<float>( PositionError, "PositionError" ) )
                                PositionError.Value = EditorGUILayout.FloatField( PositionError );

                            using ( new ImporterValueScope<float>( ScaleError, "ScaleError" ) )
                                ScaleError.Value = EditorGUILayout.FloatField( ScaleError );
#if UNITY_2017_2_OR_NEWER
                            using ( new ImporterValueScope<bool>( AnimatedCustomProperties, "AnimatedCustomProperties" ) )
                                AnimatedCustomProperties.Value = EditorGUILayout.Toggle( AnimatedCustomProperties );
#endif
                        } else {
                            BakeAnimations.IsConfigurable = false;
                            RotaionError.IsConfigurable = false;
                            ScaleError.IsConfigurable = false;
#if UNITY_2017_2_OR_NEWER
                            AnimatedCustomProperties.IsConfigurable = false;
#endif
                        }
                        GUILayout.Space( 5 );
                        //GUILayout.Label( "Animation Clip Default" );
                        //using ( new ImporterValueScope<bool>( LoopTime, "LoopTime" ) )
                        //    LoopTime.Value = EditorGUILayout.Toggle( LoopTime );
                        break;
                    case 3:
                        using ( new ImporterValueScope<bool>( ImportMaterials, "ImportMaterials" ) )
                            ImportMaterials.Value = EditorGUILayout.Toggle( ImportMaterials );

                        if ( ImportMaterials.Value ) {
#if UNITY_2017_3_OR_NEWER
                            using ( new ImporterValueScope<ModelImporterMaterialLocation>( MaterialLocation, "MaterialLocation" ) )
                                MaterialLocation.Value = ( ModelImporterMaterialLocation )EditorGUILayout.EnumPopup( MaterialLocation );
#endif
                            using ( new ImporterValueScope<ModelImporterMaterialName>( MaterialNaming, "MaterialNaming" ) )
                                MaterialNaming.Value = ( ModelImporterMaterialName )EditorGUILayout.EnumPopup( MaterialNaming );
                            using ( new ImporterValueScope<ModelImporterMaterialSearch>( MaterialSearch, "MaterialSearch" ) )
                                MaterialSearch.Value = ( ModelImporterMaterialSearch )EditorGUILayout.EnumPopup( MaterialSearch );
                        } else {
#if UNITY_2017_3_OR_NEWER
                            MaterialLocation.IsConfigurable = false;
#endif
                            MaterialNaming.IsConfigurable = false;
                            MaterialSearch.IsConfigurable = false;
                        }
                        break;
                }
            }
            EditorGUILayout.EndVertical();
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