using UnityEngine;
using UnityEditor;
using System;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    internal class CustomTextureImporter: CustomImporterClass<CustomTextureImporterValue> {

        public CustomTextureImporter() {
            ImporterSetting             = new CustomTextureImporterValue( true );
            OverrideForAndroidSetting   = new CustomTextureImporterValue( false );
            OverrideForiOSSetting       = new CustomTextureImporterValue( false );
        }

        public CustomTextureImporter( CustomTextureImporter copy ) {
            ImporterSetting             = new CustomTextureImporterValue( copy.ImporterSetting );
            OverrideForAndroidSetting   = new CustomTextureImporterValue( copy.OverrideForAndroidSetting );
            OverrideForiOSSetting       = new CustomTextureImporterValue( copy.OverrideForiOSSetting );
        }

        public static CustomTextureImporter Root {
            get {
                return new CustomTextureImporter {
                    name = "",
                    depth = -1,
                    id = 0
                };
            }
        }

    }

    [Serializable]
    internal class CustomTextureImporterValue: ImporterValue<CustomTextureImporterSettingValue> {

        public CustomTextureImporterValue( bool editable ) {
            IsConfigurable  = editable;
            Value           = new CustomTextureImporterSettingValue();
        }

        public CustomTextureImporterValue( CustomTextureImporterValue copy ) {
            IsConfigurable = copy.IsConfigurable;
            Value          = new CustomTextureImporterSettingValue( copy.Value );
        }

    }

    [Serializable]
    internal class CustomTextureImporterSettingValue {

        // 35
        // common settings
        public ImporterTextureTypeValue TextureType;
        public ImporterTextureShapeValue TextureShape;
        public ImporterWrapModeValue WrapMode;
        public ImporterFilterModeValue FilterMode;
        public ImporterIntValue AnisoLevel;

        // platform settings
        public ImporterBoolValue FitSize;
        public ImporterIntValue MaxSize;
        public ImporterCompressionValue Compression;
        public ImporterBoolValue AllowAlphaSplitting;
        public ImporterTextureFormatValue Format;
        public ImporterBoolValue UseCrunchCompression;
        public ImporterIntValue CompressionQuality;
#if UNITY_2017_2_OR_NEWER
        public ImporterResizeAlgorithmValue ResizeAlgorithm;
#endif

        // advance menu
        public bool isAdvanced;
        public ImporterNPOTScaleValue NonPowerOf2;
        public ImporterBoolValue ReadWriteEnabled;
        public ImporterBoolValue GenerateMipMaps;
        public ImporterBoolValue BorderMipMaps;
        public ImporterMipFilterValue MipMapFiltering;
        public ImporterBoolValue FadeoutMipMaps;
        public ImporterFloatValue FadeoutStartValue;
        public ImporterFloatValue FadeoutEndValue;
#if UNITY_2017_1_OR_NEWER
        public ImporterBoolValue MipMapsPreserveCover;
        public ImporterFloatValue AlphaCutoffValue;
#endif

        // texture menu
        public ImporterBoolValue sRGB;
        public ImporterAlphaSourceValue AlphaSource;
        public ImporterBoolValue AlphaIsTransparency;

        // normalmap
        public ImporterBoolValue CreateFromGrayScale;

        // sprite menu
        public ImporterSpriteModeValue SpriteMode;
        public ImporterSpriteMeshTypeValue MeshType;
        public ImporterStringValue PackingTag;
        public ImporterIntValue PixelsPerUnit;
        public ImporterIntValue ExtrudeEdges;
        public ImporterSpritePivotValue Pivot;

        // cookie
        public ImporterLightTypeValue LightType;

        public CustomTextureImporterSettingValue() {
            TextureType              = new ImporterTextureTypeValue();
            TextureShape             = new ImporterTextureShapeValue();
            TextureShape.Value       = TextureImporterShape.Texture2D;
            WrapMode                 = new ImporterWrapModeValue();
            FilterMode               = new ImporterFilterModeValue();
            AnisoLevel               = new ImporterIntValue();
            AnisoLevel.Value         = 1;

            FitSize                  = new ImporterBoolValue();
            MaxSize                  = new ImporterIntValue();
            MaxSize.Value            = 1024;
            Compression              = new ImporterCompressionValue();
            AllowAlphaSplitting      = new ImporterBoolValue();
            Format                   = new ImporterTextureFormatValue();
            UseCrunchCompression     = new ImporterBoolValue();
            CompressionQuality       = new ImporterIntValue();
            CompressionQuality.Value = 50;
#if UNITY_2017_2_OR_NEWER
            ResizeAlgorithm          = new ImporterResizeAlgorithmValue();
#endif

            isAdvanced               = false;
            NonPowerOf2              = new ImporterNPOTScaleValue();
            ReadWriteEnabled         = new ImporterBoolValue();
            GenerateMipMaps          = new ImporterBoolValue();
            BorderMipMaps            = new ImporterBoolValue();
            MipMapFiltering          = new ImporterMipFilterValue();
            MipMapFiltering.Value    = TextureImporterMipFilter.BoxFilter;
            FadeoutMipMaps           = new ImporterBoolValue();
            FadeoutStartValue        = new ImporterFloatValue();
            FadeoutStartValue.Value  = 1;
            FadeoutEndValue          = new ImporterFloatValue();
            FadeoutEndValue.Value    = 3;
#if UNITY_2017_1_OR_NEWER
            MipMapsPreserveCover     = new ImporterBoolValue();
            AlphaCutoffValue         = new ImporterFloatValue();
            AlphaCutoffValue.Value   = 0.5f;
#endif

            SpriteMode               = new ImporterSpriteModeValue();
            SpriteMode.Value         = SpriteImportMode.Single;
            MeshType                 = new ImporterSpriteMeshTypeValue();
            PackingTag               = new ImporterStringValue();
            PixelsPerUnit            = new ImporterIntValue();
            PixelsPerUnit.Value      = 100;
            ExtrudeEdges             = new ImporterIntValue();
            Pivot                    = new ImporterSpritePivotValue();

            sRGB                     = new ImporterBoolValue();
            AlphaSource              = new ImporterAlphaSourceValue();
            AlphaIsTransparency      = new ImporterBoolValue();

            CreateFromGrayScale      = new ImporterBoolValue();

            LightType                = new ImporterLightTypeValue();
        }

        public CustomTextureImporterSettingValue( CustomTextureImporterSettingValue copy ) {
            TextureType          = new ImporterTextureTypeValue( copy.TextureType );
            TextureShape         = new ImporterTextureShapeValue( copy.TextureShape );
            WrapMode             = new ImporterWrapModeValue( copy.WrapMode );
            FilterMode           = new ImporterFilterModeValue( copy.FilterMode );
            AnisoLevel           = new ImporterIntValue( copy.AnisoLevel );

            FitSize              = new ImporterBoolValue( copy.FitSize );
            MaxSize              = new ImporterIntValue( copy.MaxSize );
            Compression          = new ImporterCompressionValue( copy.Compression );
            AllowAlphaSplitting  = new ImporterBoolValue( copy.AllowAlphaSplitting );
            Format               = new ImporterTextureFormatValue( copy.Format );
            UseCrunchCompression = new ImporterBoolValue( copy.UseCrunchCompression );
            CompressionQuality   = new ImporterIntValue( copy.CompressionQuality );
#if UNITY_2017_2_OR_NEWER
            ResizeAlgorithm      = new ImporterResizeAlgorithmValue( copy.ResizeAlgorithm );
#endif

            NonPowerOf2          = new ImporterNPOTScaleValue( copy.NonPowerOf2 );
            ReadWriteEnabled     = new ImporterBoolValue( copy.ReadWriteEnabled );
            GenerateMipMaps      = new ImporterBoolValue( copy.GenerateMipMaps );
            BorderMipMaps        = new ImporterBoolValue( copy.BorderMipMaps );
            MipMapFiltering      = new ImporterMipFilterValue( copy.MipMapFiltering );
            FadeoutMipMaps       = new ImporterBoolValue( copy.FadeoutMipMaps );
            FadeoutStartValue    = new ImporterFloatValue( copy.FadeoutStartValue );
            FadeoutEndValue      = new ImporterFloatValue( copy.FadeoutEndValue );
#if UNITY_2017_1_OR_NEWER
            MipMapsPreserveCover = new ImporterBoolValue( copy.MipMapsPreserveCover );
            AlphaCutoffValue     = new ImporterFloatValue( copy.AlphaCutoffValue );
#endif

            SpriteMode           = new ImporterSpriteModeValue( copy.SpriteMode );
            MeshType             = new ImporterSpriteMeshTypeValue( copy.MeshType );
            PackingTag           = new ImporterStringValue( copy.PackingTag );
            PixelsPerUnit        = new ImporterIntValue( copy.PixelsPerUnit );
            ExtrudeEdges         = new ImporterIntValue( copy.ExtrudeEdges );
            Pivot                = new ImporterSpritePivotValue( copy.Pivot );

            sRGB                 = new ImporterBoolValue( copy.sRGB );
            AlphaSource          = new ImporterAlphaSourceValue( copy.AlphaSource );
            AlphaIsTransparency  = new ImporterBoolValue( copy.AlphaIsTransparency );

            CreateFromGrayScale  = new ImporterBoolValue( copy.CreateFromGrayScale );

            LightType            = new ImporterLightTypeValue( copy.LightType );
        }

    }

    [Serializable]
    public class ImporterTextureTypeValue: ImporterValue<TextureImporterType> {
        public ImporterTextureTypeValue() {}
        public ImporterTextureTypeValue( ImporterValue<TextureImporterType> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterTextureShapeValue: ImporterValue<TextureImporterShape> {
        public ImporterTextureShapeValue() {}
        public ImporterTextureShapeValue( ImporterValue<TextureImporterShape> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterWrapModeValue: ImporterValue<TextureWrapMode> {
        public ImporterWrapModeValue() {}
        public ImporterWrapModeValue( ImporterValue<TextureWrapMode> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterFilterModeValue: ImporterValue<FilterMode> {
        public ImporterFilterModeValue() {}
        public ImporterFilterModeValue( ImporterValue<FilterMode> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterCompressionValue: ImporterValue<TextureImporterCompression> {
        public ImporterCompressionValue() {}
        public ImporterCompressionValue( ImporterValue<TextureImporterCompression> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterNPOTScaleValue: ImporterValue<TextureImporterNPOTScale> {
        public ImporterNPOTScaleValue() {}
        public ImporterNPOTScaleValue( ImporterValue<TextureImporterNPOTScale> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterSpriteModeValue: ImporterValue<SpriteImportMode> {
        public ImporterSpriteModeValue() {}
        public ImporterSpriteModeValue( ImporterValue<SpriteImportMode> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterSpriteMeshTypeValue: ImporterValue<SpriteMeshType> {
        public ImporterSpriteMeshTypeValue() {}
        public ImporterSpriteMeshTypeValue( ImporterValue<SpriteMeshType> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterSpritePivotValue: ImporterValue<PivotMode> {
        public ImporterSpritePivotValue() {}
        public ImporterSpritePivotValue( ImporterValue<PivotMode> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterAlphaSourceValue: ImporterValue<TextureImporterAlphaSource> {
        public ImporterAlphaSourceValue() {}
        public ImporterAlphaSourceValue( ImporterValue<TextureImporterAlphaSource> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterTextureFormatValue: ImporterValue<TextureImporterFormat> {
        public ImporterTextureFormatValue() {}
        public ImporterTextureFormatValue( ImporterValue<TextureImporterFormat> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterMipFilterValue: ImporterValue<TextureImporterMipFilter> {
        public ImporterMipFilterValue() { }
        public ImporterMipFilterValue( ImporterValue<TextureImporterMipFilter> copy ) : base( copy ) { }
    }

    [Serializable]
    public class ImporterLightTypeValue: ImporterValue<LightType> {
        public ImporterLightTypeValue() { }
        public ImporterLightTypeValue( ImporterValue<LightType> copy ) : base( copy ) { }
    }

#if UNITY_2017_2_OR_NEWER
    [Serializable]
    public class ImporterResizeAlgorithmValue: ImporterValue<TextureResizeAlgorithm> {
        public ImporterResizeAlgorithmValue() { }
        public ImporterResizeAlgorithmValue( ImporterValue<TextureResizeAlgorithm> copy ) : base( copy ) { }
    }
#endif

}