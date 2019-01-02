using UnityEngine;
using UnityEditor;

using GUIHelper = charcolle.Utility.CustomAssetImporter.GUIHelper;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomAudioImporterValueEditorItem : EditorWindowItem<CustomAudioImporterSettingValue> {

        public bool isDefault;

        public CustomAudioImporterValueEditorItem( CustomAudioImporterSettingValue data ) : base( data ) { }

        protected override void Draw() { }

        private Rect boxRect = Rect.zero;
        private string[] SamplingRateLabel = new string[] { "8,000 Hz", "11,025 Hz", "22,050 Hz", "44,100 Hz", "48,000 Hz", "96,000 Hz", "192,000 Hz" };
        private int[] SamplingRate = new int[] { 8000, 11025, 22050, 44100, 48000, 96000 };
        protected override void Draw( ref Rect rect ) {
            if( Event.current.type == EventType.Repaint && boxRect != Rect.zero )
                EditorStyles.helpBox.Draw( boxRect, false, false, false, false );

            ImporterValueHeightCalc.Begin();
            var firstRect = rect;

            rect.xMin += 15f;
            rect.xMax -= 5f;
            rect.xMin += GUIHelper.Rects.DefaultLabelWidth;
            rect.y    += 3f;
            rect.height = EditorGUIUtility.singleLineHeight;

            if( isDefault ) {
                using( new ImporterValueScopeRect<bool>( ForceToMono, "ForceToMono", ref rect ) )
                    ForceToMono.Value = EditorGUI.Toggle( rect, ForceToMono );

                using( new ImporterValueScopeRect<bool>( LoadInBackGround, "LoadInBackGround", ref rect ) )
                    LoadInBackGround.Value = EditorGUI.Toggle( rect, LoadInBackGround );

#if UNITY_2017_1_OR_NEWER
                using( new ImporterValueScopeRect<bool>( Ambisonic, "Ambisonic", ref rect ) )
                    Ambisonic.Value = EditorGUI.Toggle( rect, Ambisonic );
#endif
            }

            using( new ImporterValueScopeRect<AudioClipLoadType>( LoadType, "LoadType", ref rect ) )
                LoadType.Value = ( AudioClipLoadType )EditorGUI.EnumPopup( rect,LoadType );

            if( LoadType.Value != AudioClipLoadType.Streaming )
                using( new ImporterValueScopeRect<bool>( PreloadAudioData, "PreloadAudioData", ref rect ) )
                    PreloadAudioData.Value = EditorGUI.Toggle( rect, PreloadAudioData );

            using( new ImporterValueScopeRect<AudioCompressionFormat>( CompressionFormat, "CompressionFormat", ref rect ) )
                CompressionFormat.Value = ( AudioCompressionFormat )EditorGUI.EnumPopup( rect, CompressionFormat );

            if( CompressionFormat.Value.Equals( AudioCompressionFormat.Vorbis ) ) {
                using( new ImporterValueScopeRect<float>( Quality, "Quality", ref rect ) )
                    Quality.Value = EditorGUI.IntSlider( rect, (int)Quality, 0, 100 );
            }

            using( new ImporterValueScopeRect<AudioSampleRateSetting>( SampleRateSetting, "SampleRateSetting", ref rect ) )
                SampleRateSetting.Value = ( AudioSampleRateSetting )EditorGUI.EnumPopup( rect, SampleRateSetting );

            if( SampleRateSetting.Value == AudioSampleRateSetting.OverrideSampleRate ) {
                using( new ImporterValueScopeRect<int>( SampleRate, "SampleRate", ref rect ) )
                    SampleRate.Value = EditorGUI.IntPopup( rect, SampleRate.Value, SamplingRateLabel, SamplingRate );
            } else {
                SampleRate.IsConfigurable = false;
            }

            boxRect = new Rect( firstRect.x, firstRect.y + GUIHelper.Rects.NextItemY, firstRect.width, ImporterValueHeightCalc.Height + 6f );

            rect.xMin -= 15;
            rect.xMax += 5f;
            rect.xMin -= GUIHelper.Rects.DefaultLabelWidth;
            rect.y    += 3f;

        }

        #region property

        private ImporterBoolValue ForceToMono {
            get {
                return data.ForceToMono;
            }
            set {
                data.ForceToMono = value;
            }
        }

        private ImporterBoolValue LoadInBackGround {
            get {
                return data.LoadInBackGround;
            }
            set {
                data.LoadInBackGround = value;
            }
        }
#if UNITY_2017_1_OR_NEWER
        private ImporterBoolValue Ambisonic {
            get {
                return data.Ambisonic;
            }
            set {
                data.Ambisonic = value;
            }
        }
#endif
        private ImporterAudioClipValue LoadType {
            get {
                return data.LoadType;
            }
            set {
                data.LoadType = value;
            }
        }

        private ImporterBoolValue PreloadAudioData {
            get {
                return data.PreloadAudioData;
            }
            set {
                data.PreloadAudioData = value;
            }
        }

        private ImporterAudioCompressionValue CompressionFormat {
            get {
                return data.CompressionFormat;
            }
            set {
                data.CompressionFormat = value;
            }
        }

        private ImporterFloatValue Quality {
            get {
                return data.Quality;
            }
            set {
                data.Quality = value;
            }
        }

        private ImporterSampleRateValue SampleRateSetting {
            get {
                return data.SampleRateSetting;
            }
            set {
                data.SampleRateSetting = value;
            }
        }

        private ImporterIntValue SampleRate {
            get {
                return data.SampleRate;
            }
            set {
                data.SampleRate = value;
            }
        }

        #endregion

    }

}