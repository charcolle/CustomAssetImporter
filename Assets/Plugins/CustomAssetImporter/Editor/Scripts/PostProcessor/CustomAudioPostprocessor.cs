using UnityEngine;
using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    public class CustomAudioPostprocessor: AssetPostprocessor {

        void OnPostprocessAudio(AudioClip audio) {
            Process();
        }

        private void Process() {
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
            if ( customImporter == null || !customImporter.IsEnable )
                return;

            AudioImporter audioImporter = assetImporter as AudioImporter;
            CustomAudioImporterSettingValue customSettings = customImporter.ImporterSetting;

            if ( customSettings.ForceToMono.IsConfigurable )
                audioImporter.forceToMono = customSettings.ForceToMono;

            if ( customSettings.LoadInBackGround.IsConfigurable )
                audioImporter.loadInBackground = customSettings.LoadInBackGround;

#if UNITY_2017_1_OR_NEWER
            if ( customSettings.Ambisonic.IsConfigurable )
                audioImporter.ambisonic = customSettings.Ambisonic;
#endif

            if ( customSettings.PreloadAudioData.IsConfigurable )
                audioImporter.preloadAudioData = customSettings.PreloadAudioData;

            audioImporter.defaultSampleSettings = SetCustomAudioSettings( customSettings );

            // override settings
            if( customImporter.OverrideForAndroidSetting.IsConfigurable )
                audioImporter.SetOverrideSampleSettings( "Android", SetCustomAudioSettings( customImporter.OverrideForAndroidSetting ) );
            else
                audioImporter.ClearSampleSettingOverride( "Android" );

            if( customImporter.OverrideForiOSSetting.IsConfigurable )
                audioImporter.SetOverrideSampleSettings( "iOS", SetCustomAudioSettings( customImporter.OverrideForiOSSetting ) );
            else
                audioImporter.ClearSampleSettingOverride( "iOS" );

            if ( customImporter.IsLogger )
                Debug.Log( string.Format( "CustomAudioImporter : " + customImporter.Log + "\nProcessed : {0}", assetPath ) );
        }

        /// <summary>
        /// get custom AudioImporterSampleSettings
        /// </summary>
        private AudioImporterSampleSettings SetCustomAudioSettings( CustomAudioImporterSettingValue customSettings ) {
            var importerSettings = new AudioImporterSampleSettings();

            if ( customSettings.LoadType.IsConfigurable )
                importerSettings.loadType = customSettings.LoadType;

            if ( customSettings.CompressionFormat.IsConfigurable )
                importerSettings.compressionFormat = customSettings.CompressionFormat;

            if ( customSettings.CompressionFormat.Value.Equals( AudioCompressionFormat.Vorbis ) ) {
                if ( customSettings.Quality.IsConfigurable )
                    importerSettings.quality = customSettings.Quality / 100f;
            }

            // cannot find preload AudioData ;(
            //if( customSettings.PreloadAudioData.IsConfigurable )
            //    importerSettings.p = customSettings.PreloadAudioData;

            //Debug.Log( customSettings.SampleRateSetting.IsConfigurable );
            if ( customSettings.SampleRateSetting.IsConfigurable ) {
                importerSettings.sampleRateSetting = customSettings.SampleRateSetting;
                Debug.Log( customSettings.SampleRateSetting.Value );
                Debug.Log( importerSettings.sampleRateSetting );
            }

            if( customSettings.SampleRate.IsConfigurable )
                importerSettings.sampleRateOverride = (uint)customSettings.SampleRate.Value;

            return importerSettings;
        }
    }
}