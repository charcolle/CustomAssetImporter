using UnityEditor;

namespace charcolle.Utility.CustomAssetImporter {

    internal static class CustomAssetImporterMenu {

        [MenuItem( "Window/Custom Asset Importer/Audio Importer" )]
        static void OpenCustomAudioImporter() {
            CustomAssetImporterEditorWindow.Open( 1 );
        }

        [MenuItem( "Window/Custom Asset Importer/Texture Importer" )]
        static void OpenCustomTextureImporter() {
            CustomAssetImporterEditorWindow.Open( 2 );
        }

        [MenuItem( "Window/Custom Asset Importer/Model Importer" )]
        static void OpenCustomModelImporter() {
            CustomAssetImporterEditorWindow.Open( 3 );
        }

    }
}