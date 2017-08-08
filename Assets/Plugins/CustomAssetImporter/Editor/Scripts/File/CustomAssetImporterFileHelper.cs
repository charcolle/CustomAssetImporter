using UnityEngine;
using UnityEditor;
using System.IO;

namespace charcolle.Utility.CustomAssetImporter {

    public static class FileHelper {

        private const string SEARCH_AUDIOIMPORTER_KEY   = "CustomAudioImporterSettings";
        private const string SEARCH_TEXTUREIMPORTER_KEY = "CustomTextureImporterSettings";
        private const string SEARCH_MAINSCRIPT_KEY      = "CustomAssetImporterMenu";

        private const string NAME_AUDIOIMPORTER     = "CustomAudioImporterSettings.asset";
        private const string NAME_TEXTUREIMPORTER   = "CustomTextureImporterSettings.asset";

        /// <summary>
        /// find CustomAudioImporterSettings
        /// if cannot, create the asset
        /// </summary>
        public static CustomAudioImporterSettings GetAudioImporter() {
            var asset = FindAssetByType<CustomAudioImporterSettings>( SEARCH_AUDIOIMPORTER_KEY );
            if ( asset == null )
                asset = CreateCustomAudioImporterSetting();
            return asset;
        }

        /// <summary>
        /// find CustomTextureImporterSettings
        /// if cannot, create the asset
        /// </summary>
        public static CustomTextureImporterSettings GetTextureImporter() {
            var asset = FindAssetByType<CustomTextureImporterSettings>( SEARCH_TEXTUREIMPORTER_KEY );
            if ( asset == null )
                asset = CreateCustomTextureImporterSetting();
            return asset;
        }

        /// <summary>
        /// get path of dropped file
        /// </summary>
        public static string GetDraggedAssetPath( Event curEvent, Rect dropArea ) {
            int ctrlID = GUIUtility.GetControlID( FocusType.Passive );
            switch ( curEvent.type ) {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if ( !dropArea.Contains( curEvent.mousePosition ) )
                        break;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    DragAndDrop.activeControlID = ctrlID;

                    if ( curEvent.type == EventType.DragPerform ) {
                        DragAndDrop.AcceptDrag();
                        foreach ( var draggedObj in DragAndDrop.objectReferences ) {
                            return AssetDatabase.GetAssetPath( draggedObj );
                        }
                    }
                    break;
            }
            return null;
        }

        //=============================================================================
        // create
        //=============================================================================

        /// <summary>
        /// create CustomAudioImporter ScriptableObject
        /// </summary>
        private static CustomAudioImporterSettings CreateCustomAudioImporterSetting() {
            var savePath = Path.Combine( saveDataPath, NAME_AUDIOIMPORTER );

            var asset = ScriptableObject.CreateInstance<CustomAudioImporterSettings>();
            AssetDatabase.CreateAsset( asset, savePath );
            AssetDatabase.Refresh();
            return asset;
        }

        /// <summary>
        /// create CustomTextureImporter ScriptableObject
        /// </summary>
        private static CustomTextureImporterSettings CreateCustomTextureImporterSetting() {
            var savePath = Path.Combine( saveDataPath, NAME_TEXTUREIMPORTER );

            var asset = ScriptableObject.CreateInstance<CustomTextureImporterSettings>();
            AssetDatabase.CreateAsset( asset, savePath );
            AssetDatabase.Refresh();
            return asset;
        }

        //=============================================================================
        // utility
        //=============================================================================

        /// <summary>
        /// get save data path of this asset.
        /// </summary>
        private static string saveDataPath {
            get {
                var scriptPath = FileDirectoryAssetPath( SEARCH_MAINSCRIPT_KEY );
                if ( string.IsNullOrEmpty( scriptPath ) ) {
                    Debug.LogError( "Serious error." );
                    return null;
                }
                var dirPath = Path.GetDirectoryName( scriptPath ) + "/Data/";
                if ( !Directory.Exists( dirPath ) )
                    Directory.CreateDirectory( dirPath );
                return dirPath;
            }
        }

        /// <summary>
        /// find the file and get directory AssetPath of it.
        /// </summary>
        public static string FileDirectoryAssetPath( string fileName ) {
            var guid = getAssetGUID( fileName );
            if ( string.IsNullOrEmpty( guid ) )
                return null;
            return Path.GetDirectoryName( AssetDatabase.GUIDToAssetPath( guid ) );
        }

        /// <summary>
        /// find the file by type name and return FileInfo.
        /// </summary>
        private static T FindAssetByType<T>( string type ) where T : UnityEngine.Object {
            var searchFilter = "t:" + type;
            var guid = getAssetGUID( searchFilter );
            if ( string.IsNullOrEmpty( guid ) )
                return null;
            return getObjectFromGUID<T>( guid );
        }

        private static string getAssetGUID( string searchFilter ) {
            var guids = AssetDatabase.FindAssets( searchFilter );
            if ( guids.Length == 0 ) {
                return null;
            }
            return guids[0];
        }

        private static T getObjectFromGUID<T>( string guid ) where T : UnityEngine.Object {
            var assetPath = AssetDatabase.GUIDToAssetPath( guid );
            return AssetDatabase.LoadAssetAtPath<T>( assetPath );
        }

    }
}