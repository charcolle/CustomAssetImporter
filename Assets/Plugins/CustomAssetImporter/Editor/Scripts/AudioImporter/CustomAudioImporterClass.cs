using UnityEngine;
using UnityEditor;
using System;

namespace charcolle.Utility.CustomAssetImporter {

    /// <summary>
    /// CustomImporterClass of audio.
    /// </summary>
    [Serializable]
    internal class CustomAudioImporter: CustomImporterClass<CustomAudioImporterValue> {

        public CustomAudioImporter() {
            ImporterSetting             = new CustomAudioImporterValue( true );
            OverrideForAndroidSetting   = new CustomAudioImporterValue( false );
            OverrideForiOSSetting       = new CustomAudioImporterValue( false );
        }

        public CustomAudioImporter( CustomAudioImporter copy ) {
            ImporterSetting           = new CustomAudioImporterValue( copy.ImporterSetting );
            OverrideForAndroidSetting = new CustomAudioImporterValue( copy.OverrideForAndroidSetting );
            OverrideForiOSSetting     = new CustomAudioImporterValue( copy.OverrideForiOSSetting );
        }

        public static CustomAudioImporter Root {
            get {
                return new CustomAudioImporter {
                    name = "",
                    depth = -1,
                    id = 0
                };
            }
        }
    }

    /// <summary>
    /// CustomAudioImporterValue has a importerValue for each platforms.
    /// </summary>
    [Serializable]
    internal class CustomAudioImporterValue: ImporterValue<CustomAudioImporterSettingValue> {

        public CustomAudioImporterValue( bool editable ) {
            IsConfigurable  = editable;
            Value           = new CustomAudioImporterSettingValue();
        }

        public CustomAudioImporterValue( CustomAudioImporterValue copy ) {
            IsConfigurable  = copy.IsConfigurable;
            Value           = new CustomAudioImporterSettingValue( copy.Value );
        }

    }

    /// <summary>
    /// CustomAudioImporterSettingValue is a setting of audio importer.
    /// </summary>
    [Serializable]
    internal class CustomAudioImporterSettingValue {

        // common settings
        public ImporterBoolValue ForceToMono;
        public ImporterBoolValue LoadInBackGround;
#if UNITY_2017_1_OR_NEWER
        public ImporterBoolValue Ambisonic;
#endif

        // platform settings
        public ImporterAudioClipValue LoadType;
        public ImporterBoolValue PreloadAudioData;
        public ImporterAudioCompressionValue CompressionFormat;
        public ImporterFloatValue Quality;
        public ImporterSampleRateValue SampleRateSetting;
        public ImporterIntValue SampleRate;

        #region constructor

        public CustomAudioImporterSettingValue() {
            ForceToMono         = new ImporterBoolValue();
            LoadInBackGround    = new ImporterBoolValue();
#if UNITY_2017_1_OR_NEWER
            Ambisonic           = new ImporterBoolValue();
#endif

            LoadType            = new ImporterAudioClipValue();
            PreloadAudioData    = new ImporterBoolValue();
            CompressionFormat   = new ImporterAudioCompressionValue();
            Quality             = new ImporterFloatValue();
            SampleRateSetting   = new ImporterSampleRateValue();
            SampleRate          = new ImporterIntValue();
            SampleRate.Value    = 44100;
        }

        public CustomAudioImporterSettingValue( CustomAudioImporterSettingValue copy ) {
            ForceToMono         = new ImporterBoolValue( copy.ForceToMono );
            LoadInBackGround    = new ImporterBoolValue( copy.LoadInBackGround );
#if UNITY_2017_1_OR_NEWER
            Ambisonic           = new ImporterBoolValue( copy.Ambisonic );
#endif

            LoadType            = new ImporterAudioClipValue( copy.LoadType );
            PreloadAudioData    = new ImporterBoolValue( copy.PreloadAudioData );
            CompressionFormat   = new ImporterAudioCompressionValue( copy.CompressionFormat );
            Quality             = new ImporterFloatValue( copy.Quality );
            SampleRateSetting   = new ImporterSampleRateValue( copy.SampleRateSetting );
            SampleRate          = new ImporterIntValue( copy.SampleRate );

        }
#endregion

    }

    [Serializable]
    public class ImporterAudioClipValue: ImporterValue<AudioClipLoadType> {
        public ImporterAudioClipValue() {}
        public ImporterAudioClipValue( ImporterValue<AudioClipLoadType> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterAudioCompressionValue: ImporterValue<AudioCompressionFormat> {
        public ImporterAudioCompressionValue() {}
        public ImporterAudioCompressionValue( ImporterValue<AudioCompressionFormat> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterSampleRateValue: ImporterValue<AudioSampleRateSetting> {
        public ImporterSampleRateValue() {}
        public ImporterSampleRateValue( ImporterValue<AudioSampleRateSetting> copy ) : base( copy ) {}
    }

}