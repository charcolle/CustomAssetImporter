using UnityEditor;
using UnityEngine;

namespace charcolle.Utility.CustomAssetImporter {

    public static class CustomAssetImporterMenu {

        [MenuItem( "Window/Custom Asset Importer/Audio Importer" )]
        static void OpenCustomAudioImporter() {
            var customAudioImporter = FileHelper.GetAudioImporter();
            if ( customAudioImporter == null ) {
                Debug.LogError( "check if this asset's folder is correct." );
                return;
            }
            EditorGUIUtility.PingObject( customAudioImporter );
            Selection.activeObject = customAudioImporter;
        }

        [MenuItem( "Window/Custom Asset Importer/Texture Importer" )]
        static void OpenCustomTextureImporter() {
            var customAudioImporter = FileHelper.GetTextureImporter();
            if ( customAudioImporter == null ) {
                Debug.LogError( "check if this asset's folder is correct." );
                return;
            }
            EditorGUIUtility.PingObject( customAudioImporter );
            Selection.activeObject = customAudioImporter;
        }

    }

}