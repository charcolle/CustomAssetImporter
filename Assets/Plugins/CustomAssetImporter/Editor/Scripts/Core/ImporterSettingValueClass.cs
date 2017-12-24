using System;

namespace charcolle.Utility.CustomAssetImporter {

    [Serializable]
    public class ImporterValue<T> {

        public bool IsConfigurable = false;
        public T Value;

        public ImporterValue() {}

        public ImporterValue( ImporterValue<T> copy ) {
            this.IsConfigurable = copy.IsConfigurable;
            this.Value          = copy.Value;
        }

        public static implicit operator T( ImporterValue<T> s ) {
            return s.Value;
        }
    }

    [Serializable]
    public class ImporterIntValue: ImporterValue<int> {
        public ImporterIntValue() {}
        public ImporterIntValue( ImporterValue<int> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterBoolValue: ImporterValue<bool> {
        public ImporterBoolValue() {}
        public ImporterBoolValue( ImporterValue<bool> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterFloatValue: ImporterValue<float> {
        public ImporterFloatValue() {}
        public ImporterFloatValue( ImporterValue<float> copy ) : base( copy ) {}
    }

    [Serializable]
    public class ImporterStringValue: ImporterValue<string> {
        public ImporterStringValue() {}
        public ImporterStringValue( ImporterValue<string> copy ) : base( copy ) {}
    }

}