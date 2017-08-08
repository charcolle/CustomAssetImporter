using UnityEngine;

namespace charcolle.Utility.CustomAssetImporter {

    public static class TextureImporterHelper {

        public static string[] TexutureSizeLabel    = new string[] { "32", "64", "128", "256", "512", "1024", "2048", "4096", "8192" };
        public static int[] TextureSize             = new int[] { 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192 };

        public static int GetTextureFitSize( Texture2D texture ) {
            if ( texture == null )
                return 0;
            var textureSize = texture.width >= texture.height ? texture.width : texture.height;

            if ( textureSize < TextureSize[0] )
                return TextureSize[0];
            if ( textureSize > TextureSize[8] )
                return TextureSize[8];

            for ( int i = 0; i < TextureSize.Length - 1; i++ ) {
                var baseSize = TextureSize[i];
                var nextSize = TextureSize[i + 1];
                if ( baseSize < textureSize && textureSize <= nextSize )
                    return nextSize;
            }

            return 0;
        }

    }

}