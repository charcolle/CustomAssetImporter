using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace charcolle.Utility.CustomAssetImporter {

    /// <summary>
    /// get orizinal texture size
    /// https://forum.unity.com/threads/getting-original-size-of-texture-asset-in-pixels.165295/
    /// </summary>
    public static class TextureImporterHelper {

        public static string[] TexutureSizeLabel    = new string[] { "32", "64", "128", "256", "512", "1024", "2048", "4096", "8192" };
        public static int[] TextureSize             = new int[] { 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192 };

        public static int GetTextureFitSize( Texture texture, TextureImporter importer ) {
            if ( texture == null || importer == null )
                return 0;

            object[] args = new object[2] { 0, 0 };
            MethodInfo info = typeof( TextureImporter ).GetMethod( "GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance );
            info.Invoke( importer, args );
            var textureSize = ( int )args[0] >= (int)args[1] ? ( int )args[0] : ( int )args[1];

            //Debug.Log( textureSize );
            if ( textureSize <= TextureSize[0] )
                return TextureSize[0];
            if ( textureSize >= TextureSize[8] )
                return TextureSize[8];

            for ( int i = 0; i < TextureSize.Length - 1; i++ ) {
                var baseSize = TextureSize[i];
                var nextSize = TextureSize[i + 1];
                if ( baseSize <= textureSize && textureSize < nextSize )
                    return nextSize;
            }

            return 0;
        }

    }

}