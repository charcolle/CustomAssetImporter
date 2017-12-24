using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace charcolle.Utility.CustomAssetImporter {

    public static class CustomAssetImporterStyles {

        static CustomAssetImporterStyles() {

            ToolBox = new GUIStyle( EditorStyles.toolbarButton );
            ToolBox.margin = new RectOffset( 0, 0, 0, 0 );
            ToolBox.padding = new RectOffset( 0, 0, 0, 0 );
            ToolBox.alignment = TextAnchor.MiddleCenter;

            Box = new GUIStyle( GUI.skin.box );
            Box.margin = new RectOffset( 0, 0, 0, 0 );
            Box.padding = new RectOffset( 0, 0, 0, 0 );
            Box.alignment = TextAnchor.MiddleCenter;

            Header = new GUIStyle( "RL Header" );
            Header.margin = new RectOffset( 0, 0, 0, 0 );
            Header.padding = new RectOffset( 0, 0, 0, 0 );
            Header.alignment = TextAnchor.MiddleCenter;
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

}