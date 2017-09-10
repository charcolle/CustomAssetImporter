﻿using UnityEngine;
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

            if ( importer.CheckCustomImporter( assetPath ) )
                return importer;

        }
        return null;
    }

}