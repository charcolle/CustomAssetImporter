using System;
using UnityEditor;
using UnityEngine;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    public class CustomAudioImporter: CustomImporterClass<GenericOfCustomAudioImporter> {

        public CustomAudioImporter() {
            ImporterSetting             = new GenericOfCustomAudioImporter( true );
            OverrideForAndroidSetting   = new GenericOfCustomAudioImporter( false );
            OverrideForiOSSetting       = new GenericOfCustomAudioImporter( false );
        }

    }

    [Serializable]
    public class GenericOfCustomAudioImporter: ImporterValue<CustomAudioImporterValues> {

        public GenericOfCustomAudioImporter( bool editable ) {
            isEditable  = editable;
            Value       = new CustomAudioImporterValues();
        }

    }

    [Serializable]
    public class CustomAudioImporterValues {
        // common settings
        public ImporterBoolValue ForceToMono;
        public ImporterBoolValue LoadInBackGround;
        public ImporterBoolValue Ambisonic;

        // platform settings
        public ImporterAudioClipValue LoadType;
        public ImporterBoolValue PreloadAudioData;
        public ImporterAudioCompressionValue CompressionFormat;
        public ImporterFloatValue Quality;
        public ImporterSampleRateValue SampleRateSetting;

        public CustomAudioImporterValues() {
            ForceToMono         = new ImporterBoolValue();
            LoadInBackGround    = new ImporterBoolValue();
            Ambisonic           = new ImporterBoolValue();

            LoadType            = new ImporterAudioClipValue();
            PreloadAudioData    = new ImporterBoolValue();
            CompressionFormat   = new ImporterAudioCompressionValue();
            Quality             = new ImporterFloatValue();
            SampleRateSetting   = new ImporterSampleRateValue();
        }
    }

    [Serializable]
    public class ImporterAudioClipValue : ImporterValue<AudioClipLoadType> { }

    [Serializable]
    public class ImporterAudioCompressionValue : ImporterValue<AudioCompressionFormat> { }

    [Serializable]
    public class ImporterSampleRateValue : ImporterValue<AudioSampleRateSetting> { }

}