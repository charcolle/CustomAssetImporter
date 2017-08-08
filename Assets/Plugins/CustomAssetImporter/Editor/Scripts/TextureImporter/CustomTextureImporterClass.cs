using System;
using UnityEditor;
using UnityEngine;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    public class CustomTextureImporter : CustomImporterClass<GenericOfCustomTextureImporter> {

        public CustomTextureImporter() {
            ImporterSetting             = new GenericOfCustomTextureImporter( true );
            OverrideForAndroidSetting   = new GenericOfCustomTextureImporter( false );
            OverrideForiOSSetting       = new GenericOfCustomTextureImporter( false );
        }

    }

    [Serializable]
    public class GenericOfCustomTextureImporter: ImporterValue<CustomTextureImporterValues> {

        public GenericOfCustomTextureImporter( bool editable ) {
            isEditable  = editable;
            Value       = new CustomTextureImporterValues();
        }

    }

    [Serializable]
    public class CustomTextureImporterValues {

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

        // advance menu
        public bool isAdvanced;
        public ImporterNPOTScaleValue NonPowerOf2;
        public ImporterBoolValue ReadWriteEnabled;
        public ImporterBoolValue GenerateMipMaps;

        // sprite menu
        public ImporterSpriteModeValue SpriteMode;
        public ImporterSpriteMeshTypeValue MeshType;
        public ImporterStringValue PackingTag;
        public ImporterIntValue PixelsPerUnit;
        public ImporterIntValue ExtrudeEdges;
        public ImporterSpritePivotValue Pivot; // ?

        // texture menu
        public ImporterBoolValue sRGB;
        public ImporterAlphaSourceValue AlphaSource;
        public ImporterBoolValue AlphaIsTransparency;

        // normalmap
        public ImporterBoolValue CreateFromGrayScale;

        public CustomTextureImporterValues() {
            TextureType         = new ImporterTextureTypeValue();
            TextureShape        = new ImporterTextureShapeValue();
            TextureShape.Value  = TextureImporterShape.Texture2D;
            WrapMode            = new ImporterWrapModeValue();
            FilterMode          = new ImporterFilterModeValue();
            AnisoLevel          = new ImporterIntValue();
            AnisoLevel.Value    = 1;

            FitSize                 = new ImporterBoolValue();
            MaxSize                 = new ImporterIntValue();
            MaxSize.Value           = 1024;
            Compression             = new ImporterCompressionValue();
            AllowAlphaSplitting     = new ImporterBoolValue();
            Format                  = new ImporterTextureFormatValue();
            UseCrunchCompression    = new ImporterBoolValue();
            CompressionQuality      = new ImporterIntValue();
            CompressionQuality.Value = 50;

            isAdvanced          = false;
            NonPowerOf2         = new ImporterNPOTScaleValue();
            ReadWriteEnabled    = new ImporterBoolValue();
            GenerateMipMaps     = new ImporterBoolValue();

            SpriteMode          = new ImporterSpriteModeValue();
            SpriteMode.Value    = SpriteImportMode.Single;
            MeshType            = new ImporterSpriteMeshTypeValue();
            PackingTag          = new ImporterStringValue();
            PixelsPerUnit       = new ImporterIntValue();
            PixelsPerUnit.Value = 100;
            Pivot               = new ImporterSpritePivotValue();

            sRGB                = new ImporterBoolValue();
            AlphaSource         = new ImporterAlphaSourceValue();
            AlphaIsTransparency = new ImporterBoolValue();

            CreateFromGrayScale = new ImporterBoolValue();
        }

    }

    [Serializable]
    public class ImporterTextureTypeValue: ImporterValue<TextureImporterType> { }

    [Serializable]
    public class ImporterTextureShapeValue: ImporterValue<TextureImporterShape> { }

    [Serializable]
    public class ImporterWrapModeValue: ImporterValue<TextureWrapMode> { }

    [Serializable]
    public class ImporterFilterModeValue: ImporterValue<FilterMode> { }

    [Serializable]
    public class ImporterCompressionValue: ImporterValue<TextureImporterCompression> { }

    [Serializable]
    public class ImporterNPOTScaleValue: ImporterValue<TextureImporterNPOTScale> { }

    [Serializable]
    public class ImporterSpriteModeValue: ImporterValue<SpriteImportMode> { }

    [Serializable]
    public class ImporterSpriteMeshTypeValue: ImporterValue<SpriteMeshType> { }

    [Serializable]
    public class ImporterSpritePivotValue: ImporterValue<PivotMode> { }

    [Serializable]
    public class ImporterAlphaSourceValue: ImporterValue<TextureImporterAlphaSource> { }

    [Serializable]
    public class ImporterTextureFormatValue: ImporterValue<TextureImporterFormat> { }

}