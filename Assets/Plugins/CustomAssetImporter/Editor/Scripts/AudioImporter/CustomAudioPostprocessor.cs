using UnityEngine;
using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    public class CustomAudioPostprocessor: AssetPostprocessor {

        void OnPreprocessAudio() {
            var customSettings = FileHelper.GetAudioImporter();
            if ( customSettings != null )
                ImportCustomAudio( customSettings.GetCustomImporter( assetPath ) );
        }

        //=============================================================================
        // process
        //=============================================================================
        /// <summary>
        /// set importer
        /// </summary>
        private void ImportCustomAudio( CustomAudioImporter customImporter ) {
            if ( customImporter == null )
                return;

            AudioImporter audioImporter = assetImporter as AudioImporter;
            CustomAudioImporterValues customSettings = customImporter.ImporterSetting;

            if ( customSettings.ForceToMono.isEditable )
                audioImporter.forceToMono = customSettings.ForceToMono;

            if ( customSettings.LoadInBackGround.isEditable )
                audioImporter.loadInBackground = customSettings.LoadInBackGround;

            if ( customSettings.PreloadAudioData.isEditable )
                audioImporter.preloadAudioData = customSettings.PreloadAudioData;

            audioImporter.defaultSampleSettings = SetCustomAudioSettings( customSettings );

            // override settings
            if ( customImporter.OverrideForAndroidSetting.isEditable )
                audioImporter.SetOverrideSampleSettings( "Android", SetCustomAudioSettings( customImporter.OverrideForAndroidSetting ) );
            if ( customImporter.OverrideForiOSSetting.isEditable )
                audioImporter.SetOverrideSampleSettings( "iOS", SetCustomAudioSettings( customImporter.OverrideForiOSSetting ) );

            if ( customImporter.isLogger )
                Debug.Log( "CustomAssetImporter: " + customImporter.Log );
        }

        /// <summary>
        /// get custom AudioImporterSampleSettings
        /// </summary>
        private AudioImporterSampleSettings SetCustomAudioSettings( CustomAudioImporterValues customSettings ) {
            var importerSettings = new AudioImporterSampleSettings();

            if ( customSettings.LoadType.isEditable )
                importerSettings.loadType = customSettings.LoadType;

            if ( customSettings.CompressionFormat.isEditable )
                importerSettings.compressionFormat = customSettings.CompressionFormat;

            if ( customSettings.CompressionFormat.Value.Equals( AudioCompressionFormat.Vorbis ) ) {
                if ( customSettings.Quality.isEditable )
                    importerSettings.quality = customSettings.Quality / 100f;
            }

            if ( customSettings.SampleRateSetting.isEditable )
                importerSettings.sampleRateSetting = customSettings.SampleRateSetting;

            return importerSettings;
        }
    }
}