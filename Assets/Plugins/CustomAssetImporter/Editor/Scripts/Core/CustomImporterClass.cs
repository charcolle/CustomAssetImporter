using UnityEngine;
using UnityEditor;
using System;
using System.IO;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    public class CustomImporterClass<T> {

        public string Target;
        public ImportTargetType Type;
        public T ImporterSetting;
        public T OverrideForAndroidSetting;
        public T OverrideForiOSSetting;

        public bool isFoldout = true;
        public bool isLogger  = false;
        public string Log = "";

        public bool CheckCustomImporter( string assetPath ) {
            if ( Type.Equals( ImportTargetType.DirectoryPath ) ) {
                if ( Path.GetDirectoryName( assetPath ).Equals( Target ) )
                    return true;

            } else if ( Type.Equals( ImportTargetType.DirectoryPathRecursively ) ) {
                if ( Path.GetDirectoryName( assetPath ).Contains( Target ) )
                    return true;

            } else if ( Type.Equals( ImportTargetType.FilePath ) ) {
                if ( assetPath.Equals( Target ) )
                    return true;

            } else if( Type.Equals( ImportTargetType.FileName ) ) {
                var targetFileName = Path.GetFileNameWithoutExtension( Target );
                var fileName       = Path.GetFileNameWithoutExtension( assetPath );
                if ( fileName.Equals( targetFileName ) )
                    return true;
            }

            return false;
        }

        public virtual void Draw() {
            EditorGUILayout.BeginHorizontal( CustomAssetImporterStyles.ToolBox );
            {
                GUILayout.Label( Path.GetFileNameWithoutExtension( Target ) );
                GUILayout.FlexibleSpace();
                if ( GUILayout.Button( "ReImport", EditorStyles.toolbarButton, GUILayout.Width( 60f ) ) ) {
                    FileHelper.ReImport( this );
                }
                // furture
                //if ( GUILayout.Button( "Copy", EditorStyles.toolbarButton, GUILayout.Width( 60f ) ) ) {
                //    ImportPerform( importer );
                //}
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space( 2f );


            var curEvent = Event.current;
            var dropArea = EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space( 5f );
                Type = ( ImportTargetType )EditorGUILayout.EnumPopup( Type, GUILayout.Width( 100f ) );
                Target = EditorGUILayout.TextField( Target );
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space( 7f );
                isLogger = EditorGUILayout.Toggle( isLogger, GUILayout.Width( 10f ) );
                GUILayout.Label( "Show Log", GUILayout.Width( 84f ) );
                Log = EditorGUILayout.TextField( Log );
            }
            EditorGUILayout.EndHorizontal();

            // drop perform
            var dropFileName = FileHelper.GetDraggedAssetPath( curEvent, dropArea );
            if ( !string.IsNullOrEmpty( dropFileName ) )
                Target = dropFileName;

            GUILayout.Space( 5f );

            //EditorGUILayout.BeginHorizontal();
            //{
            //    GUILayout.Space( 20f );
            //    isFoldout = EditorGUILayout.Foldout( isFoldout, "Show Setting" );
            //}
            //EditorGUILayout.EndHorizontal();

            //if ( isFoldout ) {
            //    SettingDrawer( ImporterSetting );
            //    GUILayout.Space( 3f );
            //    OverrideForiOSSetting.isEditable = EditorGUILayout.Toggle( "iOS Setting", OverrideForiOSSetting.isEditable );
            //    if ( OverrideForiOSSetting.isEditable )
            //        SettingDrawer( OverrideForiOSSetting, false );
            //    OverrideForAndroidSetting.isEditable = EditorGUILayout.Toggle( "Android Setting", OverrideForAndroidSetting.isEditable );
            //    if ( OverrideForAndroidSetting.isEditable )
            //        SettingDrawer( OverrideForAndroidSetting, false );

            //}
            GUILayout.Space( 3f );
        }

    }

    public enum ImportTargetType {
        DirectoryPath,
        DirectoryPathRecursively,
        FilePath,
        FileName,
    }

}