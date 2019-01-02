using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    internal static class UndoHelper {

        public readonly static string UNDO_CHANGEWIN = "change tab";

        public readonly static string UNDO_ADDSETTING = "{0}: add a setting";
        public readonly static string UNDO_DELETESETTING = "{0}: delete a setting";
        public readonly static string UNDO_CHANGESETTING = "{0}: change settings";



        internal static void WindowUndo( EditorWindow win, string text ) {
            if( win != null )
                Undo.RecordObject( win, text );
        }

        internal static void CustomImporterUndo<T,U>( CustomImporterSettingsBase<T,U> importer, string text ) where T : CustomImporterClass<U>, new() {
            if( importer != null )
                Undo.RecordObject( importer, string.Format( text, importer.ImporterName ) );
        }


    }

}