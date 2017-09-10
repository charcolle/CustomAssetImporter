using UnityEngine;
using System.IO;
using System.Collections.Generic;
using charcolle.Utility.CustomAssetImporter;

[CreateAssetMenu()]
public class CustomModelImporterSettings: ScriptableObject {

    [SerializeField]
    public List<CustomModelImporter> CustomImporterSettings = new List<CustomModelImporter>();

    /// <summary>
    /// check the asset is custom asset
    /// </summary>
    public CustomModelImporter GetCustomImporter( string assetPath ) {

        for ( int i = 0; i < CustomImporterSettings.Count; i++ ) {
            var importer = CustomImporterSettings[i];
            if ( string.IsNullOrEmpty( importer.TargetName ) )
                continue;

            if ( importer.CheckCustomImporter( assetPath ) )
                return importer;

        }
        return null;
    }

}
