using System;
using UnityEngine;

namespace charcolle.Utility.CustomAssetImporter {
    
    [Serializable]
    public class ImporterValue<T> {

        public bool isEditable = false;
        public T Value;

        public static implicit operator T( ImporterValue<T> s ) {
            return s.Value;
        }
    }

    [Serializable]
    public class ImporterIntValue : ImporterValue<int> { }

    [Serializable]
    public class ImporterBoolValue : ImporterValue<bool> { }

    [Serializable]
    public class ImporterFloatValue: ImporterValue<float> { }

    [Serializable]
    public class ImporterStringValue: ImporterValue<string> { }

}