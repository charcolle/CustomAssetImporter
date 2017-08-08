using System;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    public class CustomImporterClass<T> {

        public string TargetName;
        public ImportTargetType Type;
        public T ImporterSetting;
        public T OverrideForAndroidSetting;
        public T OverrideForiOSSetting;

        public bool isFoldout = true;

    }

    public enum ImportTargetType {
        DirectoryPath,
        AllInDirectoryPath,
        DirectoryName,
        FilePath,
        FileName,
    }

}