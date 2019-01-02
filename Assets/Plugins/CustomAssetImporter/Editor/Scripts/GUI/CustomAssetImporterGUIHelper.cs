using System;
using UnityEngine;
using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    public enum SettingColor {
        Normal,
        Red,
        Yellow,
        Green,
        Cyan,
        Blue,
        Magenta,
    }

    public static class GUIHelper {

        public static class Styles {

            static Styles() {
                RLHeader          = new GUIStyle( "RL Header" );
                RLBackGround      = new GUIStyle( "RL Background" );
                SearchField       = new GUIStyle( "ToolbarSeachTextField" );
                SearchFieldCancel = new GUIStyle( "ToolbarSeachCancelButton" );

                ToolBox = new GUIStyle( EditorStyles.toolbarButton ) {
                    margin        = new RectOffset( 0, 0, 0, 0 ),
                    padding       = new RectOffset( 0, 0, 0, 0 ),
                    alignment     = TextAnchor.MiddleCenter
                };

                Box = new GUIStyle( GUI.skin.box ) {
                    margin        = new RectOffset( 0, 0, 0, 0 ),
                    padding       = new RectOffset( 0, 0, 0, 0 ),
                    alignment     = TextAnchor.MiddleCenter
                };

                Header = new GUIStyle( "RL Header" ) {
                    alignment     = TextAnchor.MiddleCenter
                };

                NoSpaceBox = new GUIStyle( GUI.skin.box ) {
                    margin        = new RectOffset( 0, 0, 0, 0 ),
                    padding       = new RectOffset( 0, 0, 0, 0 ),
                };

            }

            public static GUIStyle RLHeader {
                get;
                private set;
            }

            public static GUIStyle RLBackGround {
                get;
                private set;
            }

            public static GUIStyle SearchField {
                get;
                private set;
            }

            public static GUIStyle SearchFieldCancel {
                get;
                private set;
            }

            public static GUIStyle Box {
                get;
                private set;
            }

            public static GUIStyle ToolBox {
                get;
                private set;
            }

            public static GUIStyle Header {
                get;
                private set;
            }

            public static GUIStyle NoSpaceBox {
                get;
                private set;
            }
        }

        public static class Rects {

            public static float NextItemY {
                get {
                    return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                }
            }

            public static float DefaultLabelWidth {
                get {
                    return 150f;
                }
            }

            public static float Indent {
                get {
                    return 15f;
                }
            }

        }

        public static class Textures {

            static Textures() {
                Android = EditorGUIUtility.Load( "BuildSettings.Android.Small" ) as Texture2D;
                iOS     = EditorGUIUtility.Load( "BuildSettings.iPhone.Small" ) as Texture2D;
                Texture = EditorGUIUtility.Load( "Texture Icon" ) as Texture2D;
                Audio   = EditorGUIUtility.Load( "AudioClip Icon" ) as Texture2D;
                Model   = EditorGUIUtility.Load( "PrefabModel Icon" ) as Texture2D;
            }

            public static Texture2D Android {
                get;
                private set;
            }

            public static Texture2D iOS {
                get;
                private set;
            }
            public static Texture2D Texture {
                get;
                private set;
            }
            public static Texture2D Audio {
                get;
                private set;
            }
            public static Texture2D Model {
                get;
                private set;
            }

        }

        public static class Colors {

            static Colors() {
                Red = Color.red + new Color( 0, 0.2f, 0.2f );
                Yellow = Color.yellow;
                Green = Color.green;
                Cyan = Color.cyan;
                Blue = Color.blue + new Color( 0.2f, 0.2f, 0f );
                Magenta = Color.magenta;
            }

            public static Color GetColor( SettingColor type ) {
                switch( type ) {
                    case SettingColor.Red:
                        return Red;
                    case SettingColor.Yellow:
                        return Yellow;
                    case SettingColor.Green:
                        return Green;
                    case SettingColor.Cyan:
                        return Cyan;
                    case SettingColor.Blue:
                        return Blue;
                    case SettingColor.Magenta:
                        return Magenta;
                    default:
                        return Color.white;
                }
            }

            private static Color Red {
                get; set;
            }
            private static Color Yellow {
                get; set;
            }
            private static Color Green {
                get; set;
            }
            private static Color Cyan {
                get; set;
            }
            private static Color Blue {
                get; set;
            }
            private static Color Magenta {
                get; set;
            }

        }

    }

    public class ImporterValueScope<T>: GUI.Scope {

        public ImporterValueScope( ImporterValue<T> b, string valueName ) {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space( 20f );
            b.IsConfigurable = EditorGUILayout.Toggle( b.IsConfigurable, GUILayout.Width( 10f ) );
            EditorGUI.BeginDisabledGroup( !b.IsConfigurable );
            GUILayout.Label( valueName, GUILayout.Width( 120f ) );
        }

        protected override void CloseScope() {
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }

    public class ImporterValueScopeRect<T> : GUI.Scope {

        public ImporterValueScopeRect( ImporterValue<T> b, string valueName, ref Rect rect, float indent = 0 ) {
            rect.y += GUIHelper.Rects.NextItemY;
            rect.xMin -= GUIHelper.Rects.DefaultLabelWidth;

            var toggleRect = rect;
            toggleRect.width = 16f;
            b.IsConfigurable = EditorGUI.Toggle( toggleRect, b.IsConfigurable );
            var labelRect = rect;
            labelRect.xMin += GUIHelper.Rects.Indent;
            labelRect.width = 135f;
            EditorGUI.LabelField( labelRect, valueName );

            rect.xMin += GUIHelper.Rects.DefaultLabelWidth;
            EditorGUI.BeginDisabledGroup( !b.IsConfigurable );

            ImporterValueHeightCalc.Height += GUIHelper.Rects.NextItemY;
        }

        protected override void CloseScope() {
            EditorGUI.EndDisabledGroup();
        }
    }

    public static class ImporterValueHeightCalc {

        public static float Height;

        public static void Begin() {
            Height = 0;
        }

    }
}