using UnityEngine;
using UnityEditor;
using System;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    public class CustomTextureImporter: CustomImporterClass<CustomTextureImporterValue> {

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

        public override void Draw() {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space( 20f );
                isFoldout = EditorGUILayout.Foldout( isFoldout, "Show Setting" );
            }
            EditorGUILayout.EndHorizontal();

            if ( isFoldout ) {
                ImporterSetting.Value.Draw( true );
                GUILayout.Space( 3f );
                OverrideForiOSSetting.IsConfigurable = EditorGUILayout.Toggle( "iOS Setting", OverrideForiOSSetting.IsConfigurable );
                if ( OverrideForiOSSetting.IsConfigurable )
                    OverrideForiOSSetting.Value.Draw( false );
                OverrideForAndroidSetting.IsConfigurable = EditorGUILayout.Toggle( "Android Setting", OverrideForAndroidSetting.IsConfigurable );
                if ( OverrideForAndroidSetting.IsConfigurable )
                    OverrideForAndroidSetting.Value.Draw( false );
            }
        }

    }

    [Serializable]
    public class CustomTextureImporterValue: ImporterValue<CustomTextureImporterSettingValue> {

        public CustomTextureImporterValue( bool editable ) {
            IsConfigurable  = editable;
            Value       = new CustomTextureImporterSettingValue();
        }

        public CustomTextureImporterValue( CustomTextureImporterValue copy ) {
            IsConfigurable = copy.IsConfigurable;
            Value      = new CustomTextureImporterSettingValue( copy.Value );
        }
    }

    [Serializable]
    public class CustomTextureImporterSettingValue {

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
            Pivot                = new ImporterSpritePivotValue( copy.Pivot );

            sRGB                 = new ImporterBoolValue( copy.sRGB );
            AlphaSource          = new ImporterAlphaSourceValue( copy.AlphaSource );
            AlphaIsTransparency  = new ImporterBoolValue( copy.AlphaIsTransparency );

            CreateFromGrayScale  = new ImporterBoolValue( copy.CreateFromGrayScale );

            LightType            = new ImporterLightTypeValue( copy.LightType );
        }

        public void Draw( bool isDefault ) {
            EditorGUILayout.BeginVertical( EditorStyles.helpBox );
            {
                if ( isDefault ) {
                    var preValue = TextureType.Value;
                    var preState = TextureType.IsConfigurable;
                    using ( new ImporterValueScope<TextureImporterType>( TextureType, "TextureType" ) )
                        TextureType.Value = ( TextureImporterType )EditorGUILayout.EnumPopup( TextureType );
                    checkTextureTypeChange( preValue, preState );

                    using ( new ImporterValueScope<TextureImporterShape>( TextureShape, "TextureShape" ) )
                        TextureShape.Value = ( TextureImporterShape )EditorGUILayout.EnumPopup( TextureShape );

                    using ( new ImporterValueScope<TextureWrapMode>( WrapMode, "WrapMode" ) )
                        WrapMode.Value = ( TextureWrapMode )EditorGUILayout.EnumPopup( WrapMode );

                    using ( new ImporterValueScope<FilterMode>( FilterMode, "FilterMode" ) )
                        FilterMode.Value = ( FilterMode )EditorGUILayout.EnumPopup( FilterMode );

                    using ( new ImporterValueScope<int>( AnisoLevel, "AnisoLevel" ) )
                        AnisoLevel.Value = EditorGUILayout.IntSlider( AnisoLevel, 0, 16 );

                    GUILayout.Space( 5f );

                    switch ( TextureType.Value ) {
                        case TextureImporterType.Default:
                            using ( new ImporterValueScope<bool>( sRGB, "sRGB" ) )
                                sRGB.Value = EditorGUILayout.Toggle( sRGB );

                            using ( new ImporterValueScope<TextureImporterAlphaSource>( AlphaSource, "AlphaSource" ) )
                                AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUILayout.EnumPopup( AlphaSource );

                            using ( new ImporterValueScope<bool>( AlphaIsTransparency, "AlphaIsTransparency" ) )
                                AlphaIsTransparency.Value = EditorGUILayout.Toggle( AlphaIsTransparency );

                            break;
                        case TextureImporterType.NormalMap: // cannot find importsetting... :(
                            //using ( new ImporterValueScope<bool>( CreateFromGrayScale, "CreateFromGrayScale" ) )
                            //    CreateFromGrayScale.Value = EditorGUILayout.Toggle( CreateFromGrayScale );
                            break;
                        case TextureImporterType.Sprite:
                            using ( new ImporterValueScope<SpriteImportMode>( SpriteMode, "SpriteMode" ) )
                                SpriteMode.Value = ( SpriteImportMode )EditorGUILayout.EnumPopup( SpriteMode );

                            using ( new ImporterValueScope<SpriteMeshType>( MeshType, "MeshType" ) )
                                MeshType.Value = ( SpriteMeshType )EditorGUILayout.EnumPopup( MeshType );

                            using ( new ImporterValueScope<string>( PackingTag, "PackingTag" ) )
                                PackingTag.Value = EditorGUILayout.TextField( PackingTag );

                            using ( new ImporterValueScope<int>( PixelsPerUnit, "PixelsPerUnit" ) )
                                PixelsPerUnit.Value = EditorGUILayout.IntField( PixelsPerUnit );

                            using ( new ImporterValueScope<int>( ExtrudeEdges, "ExtrudeEdges" ) )
                                ExtrudeEdges.Value = EditorGUILayout.IntSlider( ExtrudeEdges, 0, 32 );

                            //using ( new ImporterValueScope<PivotMode>( Pivot, "Pivot" ) )
                            //    Pivot.Value = ( PivotMode )EditorGUILayout.EnumPopup( Pivot );
                            break;
                        case TextureImporterType.Cookie:
                            using ( new ImporterValueScope<LightType>( LightType, "LightType" ) )
                                LightType.Value = ( LightType )EditorGUILayout.EnumPopup( LightType );

                            using ( new ImporterValueScope<TextureImporterAlphaSource>( AlphaSource, "AlphaSource" ) )
                                AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUILayout.EnumPopup( AlphaSource );

                            using ( new ImporterValueScope<bool>( AlphaIsTransparency, "AlphaIsTransparency" ) )
                                AlphaIsTransparency.Value = EditorGUILayout.Toggle( AlphaIsTransparency );
                            break;
                        case TextureImporterType.SingleChannel:
                            using ( new ImporterValueScope<TextureImporterAlphaSource>( AlphaSource, "AlphaSource" ) )
                                AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUILayout.EnumPopup( AlphaSource );

                            using ( new ImporterValueScope<bool>( AlphaIsTransparency, "AlphaIsTransparency" ) )
                                AlphaIsTransparency.Value = EditorGUILayout.Toggle( AlphaIsTransparency );
                            break;
                        default:
                            break;
                    }

                    GUILayout.Space( 5f );

                    //! advance settings
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space( 20f );
                        isAdvanced = EditorGUILayout.Foldout( isAdvanced, "Advanced" );
                    }
                    EditorGUILayout.EndHorizontal();

                    if ( isAdvanced ) {
                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.Space( 10f );
                            EditorGUILayout.BeginVertical();
                            {

                                switch ( TextureType.Value ) {
                                    case TextureImporterType.Sprite:
                                        using ( new ImporterValueScope<bool>( sRGB, "sRGB" ) )
                                            sRGB.Value = EditorGUILayout.Toggle( sRGB );

                                        using ( new ImporterValueScope<TextureImporterAlphaSource>( AlphaSource, "AlphaSource" ) )
                                            AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUILayout.EnumPopup( AlphaSource );

                                        using ( new ImporterValueScope<bool>( AlphaIsTransparency, "AlphaIsTransparency" ) )
                                            AlphaIsTransparency.Value = EditorGUILayout.Toggle( AlphaIsTransparency );
                                        break;
                                    case TextureImporterType.GUI:
                                        using ( new ImporterValueScope<TextureImporterAlphaSource>( AlphaSource, "AlphaSource" ) )
                                            AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUILayout.EnumPopup( AlphaSource );

                                        using ( new ImporterValueScope<bool>( AlphaIsTransparency, "AlphaIsTransparency" ) )
                                            AlphaIsTransparency.Value = EditorGUILayout.Toggle( AlphaIsTransparency );
                                        break;
                                }

                                using ( new ImporterValueScope<bool>( ReadWriteEnabled, "ReadWriteEnabled" ) )
                                    ReadWriteEnabled.Value = EditorGUILayout.Toggle( ReadWriteEnabled );

                                using ( new ImporterValueScope<TextureImporterNPOTScale>( NonPowerOf2, "NonPowerOf2" ) )
                                    NonPowerOf2.Value = ( TextureImporterNPOTScale )EditorGUILayout.EnumPopup( NonPowerOf2 );

                                using ( new ImporterValueScope<bool>( GenerateMipMaps, "GenerateMipMaps" ) )
                                    GenerateMipMaps.Value = EditorGUILayout.Toggle( GenerateMipMaps );

                                if ( GenerateMipMaps.IsConfigurable && GenerateMipMaps.Value ) {
                                    using ( new ImporterValueScope<bool>( BorderMipMaps, "BorderMipMaps" ) )
                                        BorderMipMaps.Value = EditorGUILayout.Toggle( BorderMipMaps );

                                    using ( new ImporterValueScope<TextureImporterMipFilter>( MipMapFiltering, "MipMapFiltering" ) )
                                        MipMapFiltering.Value = ( TextureImporterMipFilter )EditorGUILayout.EnumPopup( MipMapFiltering );

#if UNITY_2017_1_OR_NEWER
                                    using ( new ImporterValueScope<bool>( MipMapsPreserveCover, "MipMapsPreserveCover" ) )
                                        MipMapsPreserveCover.Value = EditorGUILayout.Toggle( MipMapsPreserveCover );
                                    if ( MipMapsPreserveCover.Value ) {
                                        using ( new ImporterValueScope<float>( AlphaCutoffValue, "AlphaCutoffValue" ) )
                                            AlphaCutoffValue.Value = EditorGUILayout.Slider( AlphaCutoffValue, 0, 1f );
                                    } else {
                                        AlphaCutoffValue.IsConfigurable = false;
                                    }
#endif
                                    using ( new ImporterValueScope<bool>( FadeoutMipMaps, "FadeoutMipMaps" ) )
                                        FadeoutMipMaps.Value = EditorGUILayout.Toggle( FadeoutMipMaps );
                                    if ( FadeoutMipMaps.Value ) {
                                        FadeoutStartValue.IsConfigurable = true;
                                        FadeoutEndValue.IsConfigurable = true;
                                        EditorGUILayout.MinMaxSlider( ref FadeoutStartValue.Value, ref FadeoutEndValue.Value, 0, 10 );
                                        FadeoutStartValue.Value = ( int )FadeoutStartValue;
                                        FadeoutEndValue.Value = ( int )FadeoutEndValue;
                                    } else {
                                        FadeoutStartValue.IsConfigurable = false;
                                        FadeoutEndValue.IsConfigurable = false;
                                    }

                                } else {
                                    BorderMipMaps.IsConfigurable = false;
                                    MipMapFiltering.IsConfigurable = false;
                                    FadeoutMipMaps.IsConfigurable = false;
                                    FadeoutStartValue.IsConfigurable = false;
                                    FadeoutEndValue.IsConfigurable = false;
#if UNITY_2017_1_OR_NEWER
                                    MipMapsPreserveCover.IsConfigurable = false;
                                    AlphaCutoffValue.IsConfigurable = false;
#endif
                                }
                            }
                            EditorGUILayout.EndVertical();
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    GUILayout.Space( 5f );
                }

                //! platform setting
                using ( new ImporterValueScope<bool>( FitSize, "FitSize" ) )
                    FitSize.Value = EditorGUILayout.Toggle( FitSize );

                if ( !FitSize.Value ) {
                    using ( new ImporterValueScope<int>( MaxSize, "MaxSize" ) )
                        MaxSize.Value = EditorGUILayout.IntPopup( MaxSize, TextureImporterHelper.TexutureSizeLabel, TextureImporterHelper.TextureSize );
                } else {
                    MaxSize.IsConfigurable = false;
                }

#if UNITY_2017_2_OR_NEWER
                using ( new ImporterValueScope<TextureResizeAlgorithm>( ResizeAlgorithm, "ResizeAlgorithm" ) )
                    ResizeAlgorithm.Value = ( TextureResizeAlgorithm )EditorGUILayout.EnumPopup( ResizeAlgorithm );
#endif

                using ( new ImporterValueScope<TextureImporterCompression>( Compression, "Compression" ) )
                    Compression.Value = ( TextureImporterCompression )EditorGUILayout.EnumPopup( Compression );

                if ( !isDefault ) {
                    using ( new ImporterValueScope<bool>( AllowAlphaSplitting, "AllowAlphaSplitting" ) )
                        AllowAlphaSplitting.Value = EditorGUILayout.Toggle( AllowAlphaSplitting );
                }

                using ( new ImporterValueScope<TextureImporterFormat>( Format, "Format" ) )
                    Format.Value = ( TextureImporterFormat )EditorGUILayout.EnumPopup( Format );

                using ( new ImporterValueScope<bool>( UseCrunchCompression, "UseCrunchCompression" ) )
                    UseCrunchCompression.Value = EditorGUILayout.Toggle( UseCrunchCompression );

                if ( UseCrunchCompression.Value ) {
                    using ( new ImporterValueScope<int>( CompressionQuality, "CompressorQuality" ) )
                        CompressionQuality.Value = EditorGUILayout.IntSlider( CompressionQuality, 0, 100 );
                } else {
                    CompressionQuality.IsConfigurable = false;
                }

            }
            EditorGUILayout.EndVertical();
        }

        private void checkTextureTypeChange( TextureImporterType preValue, bool preEditable ) {
            if ( preValue == TextureType && preEditable == TextureType.IsConfigurable )
                return;

            switch ( TextureType.Value ) {
                case TextureImporterType.Default:
                case TextureImporterType.NormalMap:
                    GenerateMipMaps.Value = true;
                    break;
                case TextureImporterType.Sprite:
                    GenerateMipMaps.Value = false;
                    break;
            }
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