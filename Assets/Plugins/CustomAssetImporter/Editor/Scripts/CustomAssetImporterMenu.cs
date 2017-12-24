using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    internal static class CustomAssetImporterMenu {

        [MenuItem( "Window/Custom Asset Importer/Audio Importer" )]
        static void OpenCustomAudioImporter() {
            //var customAudioImporter = FileHelper.GetAudioImporter();
            //if ( customAudioImporter == null ) {
            //    Debug.LogError( "check if this asset's folder is correct." );
            //    return;
            //}
            //EditorGUIUtility.PingObject( customAudioImporter );
            //Selection.activeObject = customAudioImporter;
            CustomAssetImporterEditorWindow.Open( 0 );
        }

        [MenuItem( "Window/Custom Asset Importer/Texture Importer" )]
        static void OpenCustomTextureImporter() {
            //var customTextureImporter = FileHelper.GetTextureImporter();
            //if ( customTextureImporter == null ) {
            //    Debug.LogError( "check if this asset's folder is correct." );
            //    return;
            //}
            //EditorGUIUtility.PingObject( customTextureImporter );
            //Selection.activeObject = customTextureImporter;
            CustomAssetImporterEditorWindow.Open( 1 );
        }

        [MenuItem( "Window/Custom Asset Importer/Model Importer" )]
        static void OpenCustomModelImporter() {
            //var customModelImporter = FileHelper.GetModelImporter();
            //if ( customModelImporter == null ) {
            //    Debug.LogError( "check if this asset's folder is correct." );
            //    return;
            //}
            //EditorGUIUtility.PingObject( customModelImporter );
            //Selection.activeObject = customModelImporter;
            CustomAssetImporterEditorWindow.Open( 2 );
        }

    }
}