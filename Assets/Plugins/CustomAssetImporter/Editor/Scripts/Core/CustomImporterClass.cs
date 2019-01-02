using System;

using FileHelper = charcolle.Utility.CustomAssetImporter.FileHelper;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    internal class CustomImporterClass<T> : TreeElement {

        public string Target;
        public ImportTargetType Type;
        public T ImporterSetting;
        public T OverrideForAndroidSetting;
        public T OverrideForiOSSetting;
        //public List<T> OverrideSettings;

        public SettingColor Color = SettingColor.Normal;
        public bool IsEnable  = true;
        public bool isFoldout = true;
        public bool IsLogger  = false;
        public string Log = "";

        public CustomImporterClass() {
            // OverrideSettings = hogehoge;
        }

        public bool CheckCustomImporter( string assetPath ) {
            var assetTargetPath = FileHelper.AssetPathToTargetPath( assetPath, Type );
            //UnityEngine.Debug.Log( Target + " " + assetTargetPath + "   " + assetPath );

            switch( Type ) {
                case ImportTargetType.DirectoryPathRecursively:
                    return assetTargetPath.Contains( Target );

                case ImportTargetType.DirectoryPath:
                case ImportTargetType.DirectoryName:
                case ImportTargetType.FilePath:
                case ImportTargetType.FileName:
                    return assetTargetPath.Equals( Target );
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