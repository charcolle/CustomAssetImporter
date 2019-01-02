using UnityEngine;
using UnityEditor;
using System.IO;

namespace charcolle.Utility.CustomAssetImporter {

    public class CustomTexturePostProcessor: AssetPostprocessor {

        private Texture2D texture;

        void OnPostprocessTexture( Texture2D tex ) {
            texture = tex;
            Process();
        }

        private void Process() {
            texture = ( Texture2D )AssetDatabase.LoadAssetAtPath( assetPath, typeof( Texture2D ) );
            var customSettings = FileHelper.GetTextureImporter();
            if ( customSettings != null )
                ImportCustomAudio( customSettings.GetCustomImporter( assetPath ) );
        }

        //=============================================================================
        // process
        //=============================================================================
        /// <summary>
        /// set importer
        /// </summary>
        private void ImportCustomAudio( CustomTextureImporter customImporter ) {
            if ( customImporter == null || !customImporter.IsEnable )
                return;

            TextureImporter textureImporter                  = assetImporter as TextureImporter;
            CustomTextureImporterSettingValue customSettings = customImporter.ImporterSetting;
            TextureImporterSettings importerSettings         = new TextureImporterSettings();
            textureImporter.ReadTextureSettings( importerSettings );
            importerSettings.spriteMode = 1;

            // pre setting
            if ( customSettings.ExtrudeEdges.IsConfigurable )
                importerSettings.spriteExtrude = ( uint )( int )customSettings.ExtrudeEdges;

            if ( customSettings.MeshType.IsConfigurable )
                importerSettings.spriteMeshType = customSettings.MeshType;

            if ( customSettings.PixelsPerUnit.IsConfigurable )
                importerSettings.spritePixelsPerUnit = customSettings.PixelsPerUnit;

            textureImporter.SetTextureSettings( importerSettings );

            // common
            if ( customSettings.TextureType.IsConfigurable )
                textureImporter.textureType     = customSettings.TextureType;

            if ( customSettings.TextureShape.IsConfigurable )
                textureImporter.textureShape = customSettings.TextureShape;

            if ( customSettings.WrapMode.IsConfigurable )
                textureImporter.wrapMode = customSettings.WrapMode;

            if ( customSettings.FilterMode.IsConfigurable )
                textureImporter.filterMode = customSettings.FilterMode;

            if ( customSettings.AnisoLevel.IsConfigurable )
                textureImporter.anisoLevel = customSettings.AnisoLevel;

            // advance
            if ( customSettings.sRGB.IsConfigurable )
                textureImporter.sRGBTexture = customSettings.sRGB;

            if ( customSettings.AlphaSource.IsConfigurable )
                textureImporter.alphaSource = customSettings.AlphaSource;

            if ( customSettings.AlphaIsTransparency.IsConfigurable )
                textureImporter.alphaIsTransparency = customSettings.AlphaIsTransparency;

            if ( customSettings.NonPowerOf2.IsConfigurable )
                textureImporter.npotScale = customSettings.NonPowerOf2;

            if ( customSettings.ReadWriteEnabled.IsConfigurable )
                textureImporter.isReadable = customSettings.ReadWriteEnabled;

            if ( customSettings.GenerateMipMaps.IsConfigurable ) {
                textureImporter.mipmapEnabled = customSettings.GenerateMipMaps;

                if ( customSettings.BorderMipMaps.IsConfigurable )
                    textureImporter.borderMipmap = customSettings.BorderMipMaps;

                if ( customSettings.MipMapFiltering.IsConfigurable )
                    textureImporter.mipmapFilter = customSettings.MipMapFiltering;

                if ( customSettings.FadeoutMipMaps.IsConfigurable ) {
                    textureImporter.mipmapFadeDistanceStart = ( int )customSettings.FadeoutStartValue;
                    textureImporter.mipmapFadeDistanceEnd   = ( int )customSettings.FadeoutEndValue;
                }
#if UNITY_2017_1_OR_NEWER
                if ( customSettings.MipMapsPreserveCover.IsConfigurable ) {
                    textureImporter.mipMapsPreserveCoverage = customSettings.MipMapsPreserveCover;

                    if ( customSettings.AlphaCutoffValue.IsConfigurable && customSettings.MipMapsPreserveCover.Value )
                        textureImporter.mipMapBias = customSettings.AlphaCutoffValue;
                }
#endif
            }

            // sprite
            if ( customSettings.PackingTag.IsConfigurable )
                textureImporter.spritePackingTag = customSettings.PackingTag;

            if ( customSettings.SpriteMode.IsConfigurable )
                textureImporter.spriteImportMode    = customSettings.SpriteMode;

            // normal map
            if( customSettings.CreateFromGrayScale.IsConfigurable ) {
                //textureImporter.normalmapFilter = TextureImporterNormalFilter.Standard;
            }

            SetCustomTextureSettings( "Default", customSettings, textureImporter );

            // override settings
            if ( customImporter.OverrideForAndroidSetting.IsConfigurable )
                textureImporter.SetPlatformTextureSettings( SetCustomTextureSettings( "Android", customImporter.OverrideForAndroidSetting ) );
            if ( customImporter.OverrideForiOSSetting.IsConfigurable )
                textureImporter.SetPlatformTextureSettings( SetCustomTextureSettings( "iPhone", customImporter.OverrideForiOSSetting ) );

            // texture cannot be get at first import
            //if ( texture == null && customSettings.FitSize.Value ) {
            //    AssetDatabase.ImportAsset( assetPath );
            //    return;
            //}

            if ( customImporter.IsLogger )
                    Debug.Log( string.Format( "CustomTextureImporter:" + customImporter.Log + "\nProcessed: {0}", assetPath ) );
        }

        /// <summary>
        /// get custom AudioImporterSampleSettings
        /// </summary>
        private TextureImporterPlatformSettings SetCustomTextureSettings( string platform, CustomTextureImporterSettingValue customSettings, TextureImporter importer = null ) {
            var platformSettings = new TextureImporterPlatformSettings();

            platformSettings.name = platform;
            platformSettings.overridden = true;

            if ( customSettings.FitSize.IsConfigurable && customSettings.FitSize.Value ) {
                var fitSize = TextureImporterHelper.GetTextureFitSize( texture, importer );
                if ( fitSize != 0 ) {
                    platformSettings.maxTextureSize = fitSize;
                    if ( importer != null )
                        importer.maxTextureSize = fitSize;
                }
            } else {
                if ( customSettings.MaxSize.IsConfigurable ) {
                    platformSettings.maxTextureSize = customSettings.MaxSize;
                    if( importer != null )
                        importer.maxTextureSize = customSettings.MaxSize;
                }
            }

#if UNITY_2017_2_OR_NEWER
            if ( customSettings.ResizeAlgorithm.IsConfigurable )
                platformSettings.resizeAlgorithm = customSettings.ResizeAlgorithm;
#endif

            if ( customSettings.Compression.IsConfigurable ) {
                platformSettings.textureCompression = customSettings.Compression;
                if ( importer != null )
                    importer.textureCompression = customSettings.Compression;
            }

            if ( customSettings.UseCrunchCompression.IsConfigurable ) {
                platformSettings.crunchedCompression = customSettings.UseCrunchCompression;
                if ( importer != null )
                    importer.crunchedCompression = customSettings.UseCrunchCompression;
            }

            if ( customSettings.Format.IsConfigurable ) {
                platformSettings.format = customSettings.Format;
                // obsolute
                //if ( importer != null )
                //    importer.textureFormat = customSettings.Format;
            }

            if ( customSettings.UseCrunchCompression.Value ) {
                if ( customSettings.CompressionQuality.IsConfigurable ) {
                    platformSettings.compressionQuality = customSettings.CompressionQuality;
                    if ( importer != null )
                        importer.compressionQuality = customSettings.CompressionQuality;
                }
            }

            return platformSettings;
        }
    }
}