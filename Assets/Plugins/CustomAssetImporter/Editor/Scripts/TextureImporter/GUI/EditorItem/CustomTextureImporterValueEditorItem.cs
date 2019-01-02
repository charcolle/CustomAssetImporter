using UnityEngine;
using UnityEditor;

using GUIHelper = charcolle.Utility.CustomAssetImporter.GUIHelper;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomTextureImporterValueEditorItem : EditorWindowItem<CustomTextureImporterSettingValue> {

        public bool isDefault;

        public CustomTextureImporterValueEditorItem( CustomTextureImporterSettingValue data ) : base( data ) { }

        protected override void Draw() { }

        private Rect boxRect = Rect.zero;
        protected override void Draw( ref Rect rect ) {
            if( Event.current.type == EventType.Repaint && boxRect != Rect.zero )
                EditorStyles.helpBox.Draw( boxRect, false, false, false, false );

            ImporterValueHeightCalc.Begin();
            var firstRect = rect;

            rect.xMin += 15f;
            rect.xMax -= 5f;
            rect.xMin += GUIHelper.Rects.DefaultLabelWidth;
            rect.y += 3f;
            rect.height = EditorGUIUtility.singleLineHeight;

            {
                if( isDefault ) {
                    var preValue = TextureType.Value;
                    var preState = TextureType.IsConfigurable;
                    using( new ImporterValueScopeRect<TextureImporterType>( TextureType, "TextureType", ref rect ) )
                        TextureType.Value = ( TextureImporterType )EditorGUI.EnumPopup( rect, TextureType );
                    checkTextureTypeChange( preValue, preState );

                    using( new ImporterValueScopeRect<TextureImporterShape>( TextureShape, "TextureShape", ref rect ) )
                        TextureShape.Value = ( TextureImporterShape )EditorGUI.EnumPopup( rect, TextureShape );

                    rect.y += 5f;
                    ImporterValueHeightCalc.Height += 5f;

                    using( new ImporterValueScopeRect<TextureWrapMode>( WrapMode, "WrapMode", ref rect ) )
                        WrapMode.Value = ( TextureWrapMode )EditorGUI.EnumPopup( rect, WrapMode );

                    using( new ImporterValueScopeRect<FilterMode>( FilterMode, "FilterMode", ref rect ) )
                        FilterMode.Value = ( FilterMode )EditorGUI.EnumPopup( rect, FilterMode );

                    using( new ImporterValueScopeRect<int>( AnisoLevel, "AnisoLevel", ref rect ) )
                        AnisoLevel.Value = EditorGUI.IntSlider( rect, AnisoLevel, 0, 16 );

                    rect.y += 5f;
                    ImporterValueHeightCalc.Height += 5f;

                    switch( TextureType.Value ) {
                        case TextureImporterType.Default:
                            using( new ImporterValueScopeRect<bool>( sRGB, "sRGB", ref rect ) )
                                sRGB.Value = EditorGUI.Toggle( rect, sRGB );

                            using( new ImporterValueScopeRect<TextureImporterAlphaSource>( AlphaSource, "AlphaSource", ref rect ) )
                                AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUI.EnumPopup( rect, AlphaSource );

                            using( new ImporterValueScopeRect<bool>( AlphaIsTransparency, "AlphaIsTransparency", ref rect ) )
                                AlphaIsTransparency.Value = EditorGUI.Toggle( rect, AlphaIsTransparency );

                            break;
                        case TextureImporterType.NormalMap:
                            // cannot find importsetting... :(
                            //using ( new ImporterValueScope<bool>( CreateFromGrayScale, "CreateFromGrayScale" ) )
                            //    CreateFromGrayScale.Value = EditorGUILayout.Toggle( CreateFromGrayScale );
                            break;

                        case TextureImporterType.Sprite:
                            using( new ImporterValueScopeRect<SpriteImportMode>( SpriteMode, "SpriteMode", ref rect ) )
                                SpriteMode.Value = ( SpriteImportMode )EditorGUI.EnumPopup( rect, SpriteMode );

                            if( SpriteMode.Value == SpriteImportMode.Polygon ) {
                                using( new ImporterValueScopeRect<SpriteMeshType>( MeshType, "MeshType", ref rect ) )
                                    MeshType.Value = ( SpriteMeshType )EditorGUI.EnumPopup( rect, MeshType );
                            } else {
                                MeshType.IsConfigurable = false;
                            }

                            using( new ImporterValueScopeRect<string>( PackingTag, "PackingTag", ref rect ) )
                                PackingTag.Value = EditorGUI.TextField( rect, PackingTag );

                            using( new ImporterValueScopeRect<int>( PixelsPerUnit, "PixelsPerUnit", ref rect ) )
                                PixelsPerUnit.Value = EditorGUI.IntField( rect, PixelsPerUnit );

                            using( new ImporterValueScopeRect<int>( ExtrudeEdges, "ExtrudeEdges", ref rect ) )
                                ExtrudeEdges.Value = EditorGUI.IntSlider( rect, ExtrudeEdges, 0, 32 );

                            //using ( new ImporterValueScope<PivotMode>( Pivot, "Pivot" ) )
                            //    Pivot.Value = ( PivotMode )EditorGUILayout.EnumPopup( Pivot );

                            break;
                        case TextureImporterType.Cookie:
                            using( new ImporterValueScopeRect<LightType>( LightType, "LightType", ref rect ) )
                                LightType.Value = ( LightType )EditorGUI.EnumPopup( rect, LightType );

                            using( new ImporterValueScopeRect<TextureImporterAlphaSource>( AlphaSource, "AlphaSource", ref rect ) )
                                AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUI.EnumPopup( rect, AlphaSource );

                            using( new ImporterValueScopeRect<bool>( AlphaIsTransparency, "AlphaIsTransparency", ref rect ) )
                                AlphaIsTransparency.Value = EditorGUI.Toggle( rect, AlphaIsTransparency );
                            break;
                        case TextureImporterType.SingleChannel:
                            using( new ImporterValueScopeRect<TextureImporterAlphaSource>( AlphaSource, "AlphaSource", ref rect ) )
                                AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUI.EnumPopup( rect, AlphaSource );

                            using( new ImporterValueScopeRect<bool>( AlphaIsTransparency, "AlphaIsTransparency", ref rect ) )
                                AlphaIsTransparency.Value = EditorGUI.Toggle( rect, AlphaIsTransparency );
                            break;
                        default:
                            break;
                    }

                    rect.xMin -= GUIHelper.Rects.DefaultLabelWidth;
                    rect.y += GUIHelper.Rects.NextItemY;
                    isAdvanced = EditorGUI.Foldout( rect, isAdvanced, "Advanced" );
                    rect.xMin += GUIHelper.Rects.DefaultLabelWidth;
                    ImporterValueHeightCalc.Height += GUIHelper.Rects.NextItemY;

                    if( isAdvanced ) {
                        rect.x += GUIHelper.Rects.Indent;
                        rect.xMax -= GUIHelper.Rects.Indent;
                        switch( TextureType.Value ) {
                            case TextureImporterType.Sprite:
                                using( new ImporterValueScopeRect<bool>( sRGB, "sRGB", ref rect ) )
                                    sRGB.Value = EditorGUI.Toggle( rect, sRGB );

                                using( new ImporterValueScopeRect<TextureImporterAlphaSource>( AlphaSource, "AlphaSource", ref rect ) )
                                    AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUI.EnumPopup( rect, AlphaSource );

                                using( new ImporterValueScopeRect<bool>( AlphaIsTransparency, "AlphaIsTransparency", ref rect ) )
                                    AlphaIsTransparency.Value = EditorGUI.Toggle( rect, AlphaIsTransparency );
                                break;
                            case TextureImporterType.GUI:
                                using( new ImporterValueScopeRect<TextureImporterAlphaSource>( AlphaSource, "AlphaSource", ref rect ) )
                                    AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUI.EnumPopup( rect, AlphaSource );

                                using( new ImporterValueScopeRect<bool>( AlphaIsTransparency, "AlphaIsTransparency", ref rect ) )
                                    AlphaIsTransparency.Value = EditorGUI.Toggle( rect, AlphaIsTransparency );
                                break;
                        }

                        using( new ImporterValueScopeRect<bool>( ReadWriteEnabled, "ReadWriteEnabled", ref rect ) )
                            ReadWriteEnabled.Value = EditorGUI.Toggle( rect, ReadWriteEnabled );

                        using( new ImporterValueScopeRect<TextureImporterNPOTScale>( NonPowerOf2, "NonPowerOf2", ref rect ) )
                            NonPowerOf2.Value = ( TextureImporterNPOTScale )EditorGUI.EnumPopup( rect, NonPowerOf2 );

                        using( new ImporterValueScopeRect<bool>( GenerateMipMaps, "GenerateMipMaps", ref rect ) )
                            GenerateMipMaps.Value = EditorGUI.Toggle( rect, GenerateMipMaps );

                        if( GenerateMipMaps.IsConfigurable && GenerateMipMaps.Value ) {
                            using( new ImporterValueScopeRect<bool>( BorderMipMaps, "BorderMipMaps", ref rect ) )
                                BorderMipMaps.Value = EditorGUI.Toggle( rect, BorderMipMaps );

                            using( new ImporterValueScopeRect<TextureImporterMipFilter>( MipMapFiltering, "MipMapFiltering", ref rect ) )
                                MipMapFiltering.Value = ( TextureImporterMipFilter )EditorGUI.EnumPopup( rect, MipMapFiltering );

#if UNITY_2017_1_OR_NEWER
                            using( new ImporterValueScopeRect<bool>( MipMapsPreserveCover, "MipMapsPreserveCover", ref rect ) )
                                MipMapsPreserveCover.Value = EditorGUI.Toggle( rect, MipMapsPreserveCover );

                            if( MipMapsPreserveCover.Value ) {
                                using( new ImporterValueScopeRect<float>( AlphaCutoffValue, "AlphaCutoffValue", ref rect ) )
                                    AlphaCutoffValue.Value = EditorGUI.Slider( rect, AlphaCutoffValue, 0, 1f );
                            } else {
                                AlphaCutoffValue.IsConfigurable = false;
                            }
#endif
                            using( new ImporterValueScopeRect<bool>( FadeoutMipMaps, "FadeoutMipMaps", ref rect ) )
                                FadeoutMipMaps.Value = EditorGUI.Toggle( rect, FadeoutMipMaps );

                            if( FadeoutMipMaps.Value ) {
                                FadeoutStartValue.IsConfigurable = true;
                                FadeoutEndValue.IsConfigurable = true;
                                EditorGUI.MinMaxSlider( rect, ref FadeoutStartValue.Value, ref FadeoutEndValue.Value, 0, 10 );
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
                        rect.x -= GUIHelper.Rects.Indent;
                        rect.xMax += GUIHelper.Rects.Indent;
                    }
                }

                //! platform setting
                using( new ImporterValueScopeRect<bool>( FitSize, "FitSize", ref rect ) )
                    FitSize.Value = EditorGUI.Toggle( rect, FitSize );

                if( !FitSize.Value ) {
                    using( new ImporterValueScopeRect<int>( MaxSize, "MaxSize", ref rect ) )
                        MaxSize.Value = EditorGUI.IntPopup( rect, MaxSize, TextureImporterHelper.TexutureSizeLabel, TextureImporterHelper.TextureSize );
                } else {
                    MaxSize.IsConfigurable = false;
                }

#if UNITY_2017_2_OR_NEWER
                using( new ImporterValueScopeRect<TextureResizeAlgorithm>( ResizeAlgorithm, "ResizeAlgorithm", ref rect ) )
                    ResizeAlgorithm.Value = ( TextureResizeAlgorithm )EditorGUI.EnumPopup( rect, ResizeAlgorithm );
#endif

                using( new ImporterValueScopeRect<TextureImporterCompression>( Compression, "Compression", ref rect ) )
                    Compression.Value = ( TextureImporterCompression )EditorGUI.EnumPopup( rect, Compression );

                if( !isDefault ) {
                    using( new ImporterValueScopeRect<bool>( AllowAlphaSplitting, "AllowAlphaSplitting", ref rect ) )
                        AllowAlphaSplitting.Value = EditorGUI.Toggle( rect, AllowAlphaSplitting );
                }

                using( new ImporterValueScopeRect<TextureImporterFormat>( Format, "Format", ref rect ) )
                    Format.Value = ( TextureImporterFormat )EditorGUI.EnumPopup( rect, Format );

                using( new ImporterValueScopeRect<bool>( UseCrunchCompression, "UseCrunchCompression", ref rect ) )
                    UseCrunchCompression.Value = EditorGUI.Toggle( rect, UseCrunchCompression );

                if( UseCrunchCompression.Value ) {
                    using( new ImporterValueScopeRect<int>( CompressionQuality, "CompressorQuality", ref rect ) )
                        CompressionQuality.Value = EditorGUI.IntSlider( rect, CompressionQuality, 0, 100 );
                } else {
                    CompressionQuality.IsConfigurable = false;
                }

            }


            boxRect = new Rect( firstRect.x, firstRect.y + GUIHelper.Rects.NextItemY, firstRect.width, ImporterValueHeightCalc.Height + 6f );

            rect.xMin -= 15;
            rect.xMax += 5f;
            rect.xMin -= GUIHelper.Rects.DefaultLabelWidth;
            rect.y += 3f;

        }

        private void checkTextureTypeChange( TextureImporterType preValue, bool preEditable ) {
            if( preValue == TextureType && preEditable == TextureType.IsConfigurable )
                return;

            switch( TextureType.Value ) {
                case TextureImporterType.Default:
                case TextureImporterType.NormalMap:
                    GenerateMipMaps.Value = true;
                    break;
                case TextureImporterType.Sprite:
                    GenerateMipMaps.Value = false;
                    break;
            }
        }

        #region property

        public ImporterTextureTypeValue TextureType {
            get {
                return data.TextureType;
            }
            set {
                data.TextureType = value;
            }
        }

        public ImporterTextureShapeValue TextureShape {
            get {
                return data.TextureShape;
            }
            set {
                data.TextureShape = value;
            }
        }

        public ImporterWrapModeValue WrapMode {
            get {
                return data.WrapMode;
            }
            set {
                data.WrapMode = value;
            }
        }

        public ImporterFilterModeValue FilterMode {
            get {
                return data.FilterMode;
            }
            set {
                data.FilterMode = value;
            }
        }

        public ImporterIntValue AnisoLevel {
            get {
                return data.AnisoLevel;
            }
            set {
                data.AnisoLevel = value;
            }
        }

        public ImporterBoolValue FitSize {
            get {
                return data.FitSize;
            }
            set {
                data.FitSize = value;
            }
        }

        public ImporterIntValue MaxSize {
            get {
                return data.MaxSize;
            }
            set {
                data.MaxSize = value;
            }
        }

        public ImporterCompressionValue Compression {
            get {
                return data.Compression;
            }
            set {
                data.Compression = value;
            }
        }

        public ImporterBoolValue AllowAlphaSplitting {
            get {
                return data.AllowAlphaSplitting;
            }
            set {
                data.AllowAlphaSplitting = value;
            }
        }

        public ImporterTextureFormatValue Format {
            get {
                return data.Format;
            }
            set {
                data.Format = value;
            }
        }

        public ImporterBoolValue UseCrunchCompression {
            get {
                return data.UseCrunchCompression;
            }
            set {
                data.UseCrunchCompression = value;
            }
        }

        public ImporterIntValue CompressionQuality {
            get {
                return data.CompressionQuality;
            }
            set {
                data.CompressionQuality = value;
            }
        }

#if UNITY_2017_2_OR_NEWER
        public ImporterResizeAlgorithmValue ResizeAlgorithm {
            get {
                return data.ResizeAlgorithm;
            }
            set {
                data.ResizeAlgorithm = value;
            }
        }
#endif

        public bool isAdvanced {
            get {
                return data.isAdvanced;
            }
            set {
                data.isAdvanced = value;
            }
        }

        public ImporterNPOTScaleValue NonPowerOf2 {
            get {
                return data.NonPowerOf2;
            }
            set {
                data.NonPowerOf2 = value;
            }
        }

        public ImporterBoolValue ReadWriteEnabled {
            get {
                return data.ReadWriteEnabled;
            }
            set {
                data.ReadWriteEnabled = value;
            }
        }

        public ImporterBoolValue GenerateMipMaps {
            get {
                return data.GenerateMipMaps;
            }
            set {
                data.GenerateMipMaps = value;
            }
        }

        public ImporterBoolValue BorderMipMaps {
            get {
                return data.BorderMipMaps;
            }
            set {
                data.BorderMipMaps = value;
            }
        }

        public ImporterMipFilterValue MipMapFiltering {
            get {
                return data.MipMapFiltering;
            }
            set {
                data.MipMapFiltering = value;
            }
        }

        public ImporterBoolValue FadeoutMipMaps {
            get {
                return data.FadeoutMipMaps;
            }
            set {
                data.FadeoutMipMaps = value;
            }
        }

        public ImporterFloatValue FadeoutStartValue {
            get {
                return data.FadeoutStartValue;
            }
            set {
                data.FadeoutStartValue = value;
            }
        }

        public ImporterFloatValue FadeoutEndValue {
            get {
                return data.FadeoutEndValue;
            }
            set {
                data.FadeoutEndValue = value;
            }
        }


#if UNITY_2017_1_OR_NEWER
        public ImporterBoolValue MipMapsPreserveCover {
            get {
                return data.MipMapsPreserveCover;
            }
            set {
                data.MipMapsPreserveCover = value;
            }
        }

        public ImporterFloatValue AlphaCutoffValue {
            get {
                return data.AlphaCutoffValue;
            }
            set {
                data.AlphaCutoffValue = value;
            }
        }
#endif

        public ImporterSpriteModeValue SpriteMode {
            get {
                return data.SpriteMode;
            }
            set {
                data.SpriteMode = value;
            }
        }

        public ImporterSpriteMeshTypeValue MeshType {
            get {
                return data.MeshType;
            }
            set {
                data.MeshType = value;
            }
        }

        public ImporterStringValue PackingTag {
            get {
                return data.PackingTag;
            }
            set {
                data.PackingTag = value;
            }
        }

        public ImporterIntValue PixelsPerUnit {
            get {
                return data.PixelsPerUnit;
            }
            set {
                data.PixelsPerUnit = value;
            }
        }

        public ImporterIntValue ExtrudeEdges {
            get {
                return data.ExtrudeEdges;
            }
            set {
                data.ExtrudeEdges = value;
            }
        }

        public ImporterSpritePivotValue Pivot {
            get {
                return data.Pivot;
            }
            set {
                data.Pivot = value;
            }
        }

        public ImporterBoolValue sRGB {
            get {
                return data.sRGB;
            }
            set {
                data.sRGB = value;
            }
        }

        public ImporterAlphaSourceValue AlphaSource {
            get {
                return data.AlphaSource;
            }
            set {
                data.AlphaSource = value;
            }
        }

        public ImporterBoolValue AlphaIsTransparency {
            get {
                return data.AlphaIsTransparency;
            }
            set {
                data.AlphaIsTransparency = value;
            }
        }

        public ImporterBoolValue CreateFromGrayScale {
            get {
                return data.CreateFromGrayScale;
            }
            set {
                data.CreateFromGrayScale = value;
            }
        }

        public ImporterLightTypeValue LightType {
            get {
                return data.LightType;
            }
            set {
                data.LightType = value;
            }
        }

        #endregion

    }

}