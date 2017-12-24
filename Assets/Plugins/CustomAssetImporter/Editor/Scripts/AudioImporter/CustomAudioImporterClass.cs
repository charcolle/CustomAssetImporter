using UnityEngine;
using UnityEditor;
using System;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    public class CustomAudioImporter: CustomImporterClass<CustomAudioImporterValue> {

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
    public class CustomAudioImporterValue: ImporterValue<CustomAudioImporterSettingValue> {

        public CustomAudioImporterValue( bool editable ) {
            IsConfigurable  = editable;
            Value       = new CustomAudioImporterSettingValue();
        }

        public CustomAudioImporterValue( CustomAudioImporterValue copy ) {
            IsConfigurable  = copy.IsConfigurable;
            Value       = new CustomAudioImporterSettingValue( copy.Value );
        }

    }

    [Serializable]
    public class CustomAudioImporterSettingValue {

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
        }
        #endregion

        #region drawer
        public void Draw( bool isDefault ) {
            EditorGUILayout.BeginVertical( EditorStyles.helpBox );
            {
                if ( isDefault ) {
                    using ( new ImporterValueScope<bool>( ForceToMono, "ForceToMono" ) )
                        ForceToMono.Value = EditorGUILayout.Toggle( ForceToMono );

                    using ( new ImporterValueScope<bool>( LoadInBackGround, "LoadInBackGround" ) )
                        LoadInBackGround.Value = EditorGUILayout.Toggle( LoadInBackGround );

#if UNITY_2017_1_OR_NEWER
                    using ( new ImporterValueScope<bool>( Ambisonic, "Ambisonic" ) )
                        Ambisonic.Value = EditorGUILayout.Toggle( Ambisonic );
#endif
                }

                using ( new ImporterValueScope<AudioClipLoadType>( LoadType, "LoadType" ) )
                    LoadType.Value = ( AudioClipLoadType )EditorGUILayout.EnumPopup( LoadType );

                using ( new ImporterValueScope<bool>( PreloadAudioData, "PreloadAudioData" ) )
                    PreloadAudioData.Value = EditorGUILayout.Toggle( PreloadAudioData );

                using ( new ImporterValueScope<AudioCompressionFormat>( CompressionFormat, "CompressionFormat" ) )
                    CompressionFormat.Value = ( AudioCompressionFormat )EditorGUILayout.EnumPopup( CompressionFormat );

                if ( CompressionFormat.Value.Equals( AudioCompressionFormat.Vorbis ) ) {
                    using ( new ImporterValueScope<float>( Quality, "Quality" ) )
                        Quality.Value = EditorGUILayout.Slider( Quality, 0f, 100f );
                }

                using ( new ImporterValueScope<AudioSampleRateSetting>( SampleRateSetting, "SampleRateSetting" ) )
                    SampleRateSetting.Value = ( AudioSampleRateSetting )EditorGUILayout.EnumPopup( SampleRateSetting );

            }
            EditorGUILayout.EndVertical();
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