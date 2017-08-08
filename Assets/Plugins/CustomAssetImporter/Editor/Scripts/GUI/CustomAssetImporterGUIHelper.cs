using UnityEngine;
using UnityEditor;

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
        }

        public static GUIStyle Box {
            get;
            private set;
        }

        public static GUIStyle ToolBox {
            get;
            private set;
        }

    }

    public class ImporterValueScope<T>: GUI.Scope {

        public ImporterValueScope( ImporterValue<T> b, string valueName ) {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space( 20f );
            b.isEditable = EditorGUILayout.Toggle( b.isEditable, GUILayout.Width( 10f ) );
            EditorGUI.BeginDisabledGroup( !b.isEditable );
            GUILayout.Label( valueName, GUILayout.Width( 120f ) );
        }

        protected override void CloseScope() {
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
    }

}