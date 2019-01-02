using UnityEngine;
using System.Collections.Generic;
using charcolle.Utility.CustomAssetImporter;

using FileHelper = charcolle.Utility.CustomAssetImporter.FileHelper;

internal class CustomImporterSettingsBase<T, U> : ScriptableObject where T : CustomImporterClass<U>, new() {

    public List<T> CustomImporterSettings = new List<T>();
    public string ImporterName;

    /// <summary>
    /// check the asset is custom asset
    /// </summary>
    public T GetCustomImporter( string assetPath ) {
        for ( int i = 0; i < CustomImporterSettings.Count; i++ ) {
            var importer = CustomImporterSettings[i] as T;
            if ( string.IsNullOrEmpty( importer.Target ) )
                continue;

            if ( importer.CheckCustomImporter( assetPath ) )
                return importer;
        }
        return null;
    }

}
