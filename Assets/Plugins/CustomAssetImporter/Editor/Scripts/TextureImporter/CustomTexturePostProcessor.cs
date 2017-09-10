using UnityEngine;
using UnityEditor;
using System.IO;

namespace charcolle.Utility.CustomAssetImporter {

    public class CustomTexturePostProcessor: AssetPostprocessor {

        private Texture2D texture;

        void OnPreprocessTexture() {

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
            if ( customImporter == null )
                return;

            TextureImporter textureImporter = assetImporter as TextureImporter;
            CustomTextureImporterValues customSettings = customImporter.ImporterSetting;
            TextureImporterSettings importerSettings = new TextureImporterSettings();
            textureImporter.ReadTextureSettings( importerSettings );
            importerSettings.spriteMode = 1;

            // pre setting
            if ( customSettings.ExtrudeEdges.isEditable )
                importerSettings.spriteExtrude = ( uint )( int )customSettings.ExtrudeEdges;

            if ( customSettings.MeshType.isEditable )
                importerSettings.spriteMeshType = customSettings.MeshType;

            if ( customSettings.PixelsPerUnit.isEditable )
                importerSettings.spritePixelsPerUnit = customSettings.PixelsPerUnit;

            textureImporter.SetTextureSettings( importerSettings );

            // common
            if ( customSettings.TextureType.isEditable )
                textureImporter.textureType     = customSettings.TextureType;

            if ( customSettings.TextureShape.isEditable )
                textureImporter.textureShape = customSettings.TextureShape;

            if ( customSettings.WrapMode.isEditable )
                textureImporter.wrapMode = customSettings.WrapMode;

            if ( customSettings.FilterMode.isEditable )
                textureImporter.filterMode = customSettings.FilterMode;

            if ( customSettings.AnisoLevel.isEditable )
                textureImporter.anisoLevel = customSettings.AnisoLevel;

            if ( customSettings.sRGB.isEditable )
                textureImporter.sRGBTexture = customSettings.sRGB;

            if ( customSettings.AlphaSource.isEditable )
                textureImporter.alphaSource = customSettings.AlphaSource;

            if ( customSettings.AlphaIsTransparency.isEditable )
                textureImporter.alphaIsTransparency = customSettings.AlphaIsTransparency;

            if ( customSettings.NonPowerOf2.isEditable )
                textureImporter.npotScale = customSettings.NonPowerOf2;

            if ( customSettings.ReadWriteEnabled.isEditable )
                textureImporter.isReadable = customSettings.ReadWriteEnabled;

            if ( customSettings.GenerateMipMaps.isEditable )
                textureImporter.mipmapEnabled = customSettings.GenerateMipMaps;

            // sprite
            if ( customSettings.PackingTag.isEditable )
                textureImporter.spritePackingTag = customSettings.PackingTag;

            if ( customSettings.SpriteMode.isEditable )
                textureImporter.spriteImportMode    = customSettings.SpriteMode;

            // normal map
            // obsolute
            //if ( customSettings.CreateFromGrayScale.isEditable )
            //    textureImporter.grayscaleToAlpha = customSettings.CreateFromGrayScale;

            SetCustomTextureSettings( "Default", customSettings, textureImporter );

            // override settings
            if ( customImporter.OverrideForAndroidSetting.isEditable )
                textureImporter.SetPlatformTextureSettings( SetCustomTextureSettings( "Android", customImporter.OverrideForAndroidSetting ) );
            if ( customImporter.OverrideForiOSSetting.isEditable )
                textureImporter.SetPlatformTextureSettings( SetCustomTextureSettings( "iPhone", customImporter.OverrideForiOSSetting ) );

            // texture cannot be get at first import
            if ( texture == null && customSettings.FitSize.Value ) {
                AssetDatabase.ImportAsset( assetPath );
                return;
            }

            if ( customImporter.isLogger )
                Debug.Log( "CustomAssetImporter: " + customImporter.Log );
        }

        /// <summary>
        /// get custom AudioImporterSampleSettings
        /// </summary>
        private TextureImporterPlatformSettings SetCustomTextureSettings( string platform, CustomTextureImporterValues customSettings, TextureImporter importer = null ) {
            var platformSettings = new TextureImporterPlatformSettings();

            platformSettings.name = platform;
            platformSettings.overridden = true;

            if ( customSettings.FitSize.isEditable && customSettings.FitSize.Value ) {
                var fitSize = TextureImporterHelper.GetTextureFitSize( texture );
                if ( fitSize != 0 ) {
                    platformSettings.maxTextureSize = fitSize;
                    if ( importer != null )
                        importer.maxTextureSize = fitSize;
                }
            } else {
                if ( customSettings.MaxSize.isEditable ) {
                    platformSettings.maxTextureSize = customSettings.MaxSize;
                    if( importer != null )
                        importer.maxTextureSize = customSettings.MaxSize;
                }
            }

            if ( customSettings.Compression.isEditable ) {
                platformSettings.textureCompression = customSettings.Compression;
                if ( importer != null )
                    importer.textureCompression = customSettings.Compression;
            }

            if ( customSettings.UseCrunchCompression.isEditable ) {
                platformSettings.crunchedCompression = customSettings.UseCrunchCompression;
                if ( importer != null )
                    importer.crunchedCompression = customSettings.UseCrunchCompression;
            }

            if ( customSettings.Format.isEditable ) {
                platformSettings.format = customSettings.Format;
                // obsolute
                //if ( importer != null )
                //    importer.textureFormat = customSettings.Format;
            }

            if ( customSettings.UseCrunchCompression.Value ) {
                if ( customSettings.CompressionQuality.isEditable ) {
                    platformSettings.compressionQuality = customSettings.CompressionQuality;
                    if ( importer != null )
                        importer.compressionQuality = customSettings.CompressionQuality;
                }
            }

            return platformSettings;
        }
    }
}