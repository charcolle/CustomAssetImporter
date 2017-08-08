using UnityEngine;
using System.IO;
using System.Collections.Generic;
using charcolle.Utility.CustomAssetImporter;

public class CustomAudioImporterSettings: ScriptableObject {

    [SerializeField]
    public List<CustomAudioImporter> CustomImporterSettings = new List<CustomAudioImporter>();

    /// <summary>
    /// check the asset is custom asset
    /// </summary>
    public CustomAudioImporter GetCustomImporter( string assetPath ) {

        for ( int i = 0; i < CustomImporterSettings.Count; i++ ) {
            var importer = CustomImporterSettings[i];
            if ( string.IsNullOrEmpty( importer.TargetName ) )
                continue;

            if ( importer.Type.Equals( ImportTargetType.DirectoryPath ) ) {
                if ( Path.GetDirectoryName( assetPath ).Equals( importer.TargetName ) )
                    return importer;

            } else if ( importer.Type.Equals( ImportTargetType.AllInDirectoryPath ) ) {
                if ( Path.GetDirectoryName( assetPath ).Contains( importer.TargetName ) )
                    return importer;

            } else if ( importer.Type.Equals( ImportTargetType.DirectoryName ) ) {
                var targetDirPath   = Path.GetFileName( importer.TargetName );
                var dirPath         = Path.GetDirectoryName( assetPath );
                if ( Path.GetFileName( dirPath ).Equals( targetDirPath ) )
                    return importer;

            } else if ( importer.Type.Equals( ImportTargetType.FilePath ) ) {
                if ( assetPath.Equals( importer.TargetName ) )
                    return importer;

            } else {
                var targetFileName = Path.GetFileNameWithoutExtension( importer.TargetName );
                var fileName = Path.GetFileNameWithoutExtension( assetPath );
                if ( fileName.Equals( targetFileName ) )
                    return importer;

            }
        }
        return null;
    }

}