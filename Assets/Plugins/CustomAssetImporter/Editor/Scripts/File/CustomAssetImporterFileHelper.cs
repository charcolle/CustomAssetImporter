using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Reflection;

namespace charcolle.Utility.CustomAssetImporter {

    internal static class FileHelper {

        private const string SEARCH_AUDIOIMPORTER_KEY   = "CustomAudioImporterSettings";
        private const string SEARCH_TEXTUREIMPORTER_KEY = "CustomTextureImporterSettings";
        private const string SEARCH_MODELIMPORTER_KEY   = "CustomModelImporterSettings";
        private const string SEARCH_MAINSCRIPT_KEY      = "CustomAssetImporterMenu";

        private const string NAME_AUDIOIMPORTER         = "CustomAudioImporterSettings.asset";
        private const string NAME_TEXTUREIMPORTER       = "CustomTextureImporterSettings.asset";
        private const string NAME_MODELIMPORTER         = "CustomModelImporterSettings.asset";

        private readonly static string NAME_DEFAULT_ASSEMBLY  = "CustomAssetImporter.Editor";
        private readonly static string NAME_DEFAULT_ASSEMBLY2 = "Assembly-CSharp";
        private readonly static string RELATIVEPATH_DEFAULT   = "Assets/Plugins/CustomAssetImporter/Editor/Data/";

        /// <summary>
        /// find CustomAudioImporterSettings
        /// if cannot, create the asset
        /// </summary>
        public static CustomAudioImporterSettings GetAudioImporter() {
            var asset = FindAssetByType<CustomAudioImporterSettings>( SEARCH_AUDIOIMPORTER_KEY );
            if ( asset == null )
                asset = CreateCustomAudioImporterSetting();
            asset.ImporterName = "AudioImporter";
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
            asset.ImporterName = "TextureImporter";
            return asset;
        }

        /// <summary>
        /// find CustomModelImporterSettings
        /// if cannot, create the asset
        /// </summary>
        public static CustomModelImporterSettings GetModelImporter() {
            var asset = FindAssetByType<CustomModelImporterSettings>( SEARCH_MODELIMPORTER_KEY );
            if ( asset == null )
                asset = CreateCustomModelImporterSetting();
            asset.ImporterName = "ModelImporter";
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
                default:
                    break;
            }
            return null;
        }

        public static string AssetPathToTargetPath( string assetPath, ImportTargetType type ) {
            if( string.IsNullOrEmpty( assetPath ) )
                return null;
            switch( type ) {
                case ImportTargetType.DirectoryPath:
                case ImportTargetType.DirectoryPathRecursively:
                    return pathSlashFix( Path.GetDirectoryName( assetPath ) );
                case ImportTargetType.DirectoryName:
                    return pathSlashFix( Path.GetFileNameWithoutExtension( Path.GetDirectoryName( assetPath ) ) );
                case ImportTargetType.FileName:
                    return pathSlashFix( Path.GetFileNameWithoutExtension( assetPath ) );
                default:
                    return assetPath;
            }
        }

        public static string TargetPathForType( string target, ImportTargetType type ) {
            if( string.IsNullOrEmpty( target ) )
                return "";
            switch( type ) {
                case ImportTargetType.DirectoryPath:
                case ImportTargetType.DirectoryPathRecursively:
                case ImportTargetType.FilePath:
                    return target;
                case ImportTargetType.DirectoryName:
                case ImportTargetType.FileName:
                    return Path.GetFileNameWithoutExtension( target );
                default:
                    return "";
            }
        }

        public static bool IsTargetNameValid( string target, ImportTargetType type ) {
            if( string.IsNullOrEmpty( target ) )
                return true;

            switch( type ) {
                case ImportTargetType.DirectoryPath:
                case ImportTargetType.DirectoryPathRecursively:
                    return Directory.Exists( AssetPathToSystemPath( target ) );
                case ImportTargetType.FilePath:
                    return File.Exists( AssetPathToSystemPath( target ) );
                case ImportTargetType.DirectoryName:
                case ImportTargetType.FileName:
                    return !target.Contains( "/" ) && !target.Contains( "\\" );
                default:
                    return false;
            }
        }

        //=============================================================================
        // create settings
        //=============================================================================

        /// <summary>
        /// create CustomAudioImporter ScriptableObject
        /// </summary>
        private static CustomAudioImporterSettings CreateCustomAudioImporterSetting() {
            var savePath = Path.Combine( saveDataPath, NAME_AUDIOIMPORTER );

            var asset = ScriptableObject.CreateInstance<CustomAudioImporterSettings>();
            asset.CustomImporterSettings.Add( CustomAudioImporter.Root );
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
            asset.CustomImporterSettings.Add( CustomTextureImporter.Root );
            AssetDatabase.CreateAsset( asset, savePath );
            AssetDatabase.Refresh();
            return asset;
        }

        /// <summary>
        /// create CustomModelImporter ScriptableObject
        /// </summary>
        private static CustomModelImporterSettings CreateCustomModelImporterSetting() {
            var savePath = Path.Combine( saveDataPath, NAME_MODELIMPORTER );

            var asset = ScriptableObject.CreateInstance<CustomModelImporterSettings>();
            asset.CustomImporterSettings.Add( CustomModelImporter.Root );
            AssetDatabase.CreateAsset( asset, savePath );
            AssetDatabase.Refresh();
            return asset;
        }

        //=============================================================================
        // utility
        //=============================================================================

        /// <summary>
        /// get save data path of this asset.
        /// TODO : refactoring
        /// </summary>
        private static string saveDataPath {
            get {
                var assembly = Assembly.GetExecutingAssembly().GetName().Name;
                if( assembly.Equals( NAME_DEFAULT_ASSEMBLY ) || assembly.Contains( NAME_DEFAULT_ASSEMBLY2 ) ) {
                    var scriptPath = FileDirectoryAssetPath( SEARCH_MAINSCRIPT_KEY );
                    if( string.IsNullOrEmpty( scriptPath ) ) {
                        Debug.LogError( "fatal error." );
                        return null;
                    }
                    var dirPath = pathSlashFix( Path.GetDirectoryName( scriptPath ) + "/Data/" );
                    if( !Directory.Exists( dirPath ) )
                        Directory.CreateDirectory( dirPath );
                    return dirPath;
                } else {
                    var guid = getAssetGUID( assembly );
                    if( string.IsNullOrEmpty( guid ) ) {
                        Debug.LogError( "fatal error." );
                        return RELATIVEPATH_DEFAULT;
                    }
                    var dirPath = pathSlashFix( Path.GetDirectoryName( AssetDatabase.GUIDToAssetPath( guid ) ) + "/Data/" );
                    if( !Directory.Exists( dirPath ) )
                        Directory.CreateDirectory( dirPath );
                    return dirPath;
                }
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

        //=============================================================================
        // Importer
        //=============================================================================

        internal static void ReImport<T>( CustomImporterClass<T> importer ) {
            if ( string.IsNullOrEmpty( importer.Target ) )
                return;
            switch ( importer.Type ) {
                case ImportTargetType.FilePath:
                    AssetDatabase.ImportAsset( importer.Target, ImportAssetOptions.Default );
                    break;
                case ImportTargetType.FileName:
                    {
                        var files = AssetDatabase.FindAssets( importer.Target );
                        for ( int i = 0; i < files.Length; i++ ) {
                            var assetPath = AssetDatabase.GUIDToAssetPath( files[i] );
                            if ( Path.GetFileNameWithoutExtension( assetPath ).Equals( Path.GetFileNameWithoutExtension( importer.Target ) ) )
                                AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.Default );
                        }
                    }
                    break;
                case ImportTargetType.DirectoryPath:
                    {
                        var files = Directory.GetFiles( AssetPathToSystemPath( importer.Target ) );
                        files = files.Where( f => !Path.GetExtension( f ).Equals( ".meta" ) ).ToArray();
                        for ( int i = 0; i < files.Length; i++ )
                            AssetDatabase.ImportAsset( SystemPathToAssetPath( files[ i ] ), ImportAssetOptions.Default );
                    }
                    break;

                case ImportTargetType.DirectoryName: {
                        var files = AssetDatabase.FindAssets( importer.Target );
                        for( int i = 0; i < files.Length; i++ ) {
                            var assetPath = AssetDatabase.GUIDToAssetPath( files[ i ] );
                            var dirFiles = Directory.GetFiles( AssetPathToSystemPath( assetPath ) );
                            dirFiles = dirFiles.Where( f => !Path.GetExtension( f ).Equals( ".meta" ) ).ToArray();
                            for( int j = 0; j < dirFiles.Length; j++ )
                                AssetDatabase.ImportAsset( SystemPathToAssetPath( dirFiles[ i ] ), ImportAssetOptions.Default );
                        }
                    }
                    break;

                default:
                    {
                        AssetDatabase.ImportAsset( importer.Target, ImportAssetOptions.ImportRecursive );
                    }
                    break;
            }
        }

        internal static string AssetPathToSystemPath( string path ) {
            return pathSlashFix( Path.Combine( dataPathWithoutAssets, path ) );
        }

        internal static string SystemPathToAssetPath( string path ) {
            return pathSlashFix( path ).Replace( Application.dataPath, "Assets" );
        }

        private static string dataPathWithoutAssets {
            get {
                return pathSlashFix( Application.dataPath.Replace( "Assets", "" ) );
            }
        }

        private const string forwardSlash   = "/";
        private const string backSlash      = "\\";
        private static string pathSlashFix( string path ) {
            return path.Replace( backSlash, forwardSlash );
        }

    }
}