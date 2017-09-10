using System;
using System.IO;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    public class CustomImporterClass<T> {

        public string TargetName;
        public ImportTargetType Type;
        public T ImporterSetting;
        public T OverrideForAndroidSetting;
        public T OverrideForiOSSetting;

        public bool isFoldout = true;
        public bool isLogger  = false;
        public string Log = "";

        public bool CheckCustomImporter( string assetPath ) {
            if ( Type.Equals( ImportTargetType.DirectoryPath ) ) {
                if ( Path.GetDirectoryName( assetPath ).Equals( TargetName ) )
                    return true;

            } else if ( Type.Equals( ImportTargetType.DirectoryPathRecursively ) ) {
                if ( Path.GetDirectoryName( assetPath ).Contains( TargetName ) )
                    return true;

            } else if ( Type.Equals( ImportTargetType.DirectoryName ) ) {
                var targetDirPath = Path.GetFileName( TargetName );
                var dirPath = Path.GetDirectoryName( assetPath );
                if ( Path.GetFileName( dirPath ).Equals( targetDirPath ) )
                    return true;

            } else if ( Type.Equals( ImportTargetType.FilePath ) ) {
                if ( assetPath.Equals( TargetName ) )
                    return true;

            } else if( Type.Equals( ImportTargetType.FileName ) ) {
                var targetFileName = Path.GetFileNameWithoutExtension( TargetName );
                var fileName = Path.GetFileNameWithoutExtension( assetPath );
                if ( fileName.Equals( targetFileName ) )
                    return true;

            }

            return false;
        }
    }

    public enum ImportTargetType {
        DirectoryPath,
        DirectoryPathRecursively,
        DirectoryName,
        FilePath,
        FileName,
    }

}