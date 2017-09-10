using UnityEngine;
using UnityEditor;
using System.IO;

namespace charcolle.Utility.CustomAssetImporter {

    [CustomEditor( typeof( CustomTextureImporterSettings ) )]
    public class CustomTextureImporterSettingsEditor: Editor {

        private CustomTextureImporterSettings myTarget;

        private const string TEXT_UNDO = "Change CustomTextureImporter Setting";
        private const string TEXT_LABEL = "Texture Importer Settings";

        private void OnEnable() {
            myTarget = target as CustomTextureImporterSettings;
        }

        public override void OnInspectorGUI() {

            myTarget = target as CustomTextureImporterSettings;

            EditorGUILayout.BeginHorizontal( GUILayout.ExpandWidth( true ) );
            {
                EditorGUILayout.BeginVertical();
                {
                    Header();
                    Main();
                    Footer();
                }
                EditorGUILayout.EndVertical();
                GUILayout.Space( 3f );
            }
            EditorGUILayout.EndHorizontal();

            if ( myTarget.CustomImporterSettings.Count == 0 )
                EditorGUILayout.HelpBox( "No Importers", MessageType.Warning );

            if ( GUI.changed ) {
                Undo.RegisterCompleteObjectUndo( target, TEXT_UNDO );
                Undo.FlushUndoRecordObjects();
                EditorUtility.SetDirty( target );
            }
        }

        //=============================================================================
        // Drawer
        //=============================================================================

        private void Header() {
            GUI.backgroundColor = Color.grey;
            EditorGUILayout.BeginHorizontal( CustomAssetImporterStyles.Box );
            {
                GUILayout.Label( TEXT_LABEL );
                GUILayout.FlexibleSpace();
                GUI.backgroundColor = Color.green;
                if ( GUILayout.Button( "+", CustomAssetImporterStyles.ToolBox, GUILayout.Width( 70f ) ) ) {
                    myTarget.CustomImporterSettings.Add( new CustomTextureImporter() );
                }
            }
            EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
        }

        private Vector2 SCROLL_VIEW;
        private void Main() {
            if ( myTarget.CustomImporterSettings.Count == 0 )
                return;
            SCROLL_VIEW = EditorGUILayout.BeginScrollView( SCROLL_VIEW, CustomAssetImporterStyles.Box, GUILayout.ExpandHeight( true ) );
            {
                var importers = myTarget.CustomImporterSettings;
                try {
                    for ( int i = 0; i < importers.Count; i++ )
                        ImporterDrawer( importers[i] );
                } catch { }
            }
            EditorGUILayout.EndScrollView();
        }
        private void Footer() {
            if ( myTarget.CustomImporterSettings.Count == 0 )
                return;
            EditorGUILayout.BeginHorizontal( CustomAssetImporterStyles.Box );
            {
                GUILayout.FlexibleSpace();
                if ( GUILayout.Button( "Collapse all", EditorStyles.toolbarButton, GUILayout.Width( 120f ) ) ) {
                    for ( int i = 0; i < myTarget.CustomImporterSettings.Count; i++ ) {
                        myTarget.CustomImporterSettings[i].isFoldout = false;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void ImporterDrawer( CustomTextureImporter importer ) {
            var deleteFlag = false;
            EditorGUILayout.BeginVertical( CustomAssetImporterStyles.Box, GUILayout.ExpandWidth( true ) );
            {
                EditorGUILayout.BeginHorizontal( CustomAssetImporterStyles.ToolBox );
                {
                    GUILayout.Label( Path.GetFileNameWithoutExtension( importer.TargetName ) );
                    GUILayout.FlexibleSpace();
                    if ( GUILayout.Button( "ReImport", EditorStyles.toolbarButton, GUILayout.Width( 60f ) ) ) {
                        FileHelper.ReImport( importer );
                    }
                    // furture
                    //if ( GUILayout.Button( "Copy", EditorStyles.toolbarButton, GUILayout.Width( 60f ) ) ) {
                    //    ImportPerform( importer );
                    //}
                    GUI.backgroundColor = Color.red;
                    if ( GUILayout.Button( "x", EditorStyles.toolbarButton, GUILayout.Width( 20f ) ) ) {
                        deleteFlag = true;
                    }
                    GUI.backgroundColor = Color.white;
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space( 2f );


                var curEvent = Event.current;
                var dropArea = EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Space( 5f );
                    importer.Type = ( ImportTargetType )EditorGUILayout.EnumPopup( importer.Type, GUILayout.Width( 100f ) );
                    importer.TargetName = EditorGUILayout.TextField( importer.TargetName );
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Space( 7f );
                    importer.isLogger = EditorGUILayout.Toggle( importer.isLogger, GUILayout.Width( 10f ) );
                    GUILayout.Label( "Show Log", GUILayout.Width( 84f ) );
                    importer.Log = EditorGUILayout.TextField( importer.Log );
                }
                EditorGUILayout.EndHorizontal();

                // drop perform
                var dropFileName = FileHelper.GetDraggedAssetPath( curEvent, dropArea );
                if ( !string.IsNullOrEmpty( dropFileName ) )
                    importer.TargetName = dropFileName;

                GUILayout.Space( 5f );

                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Space( 20f );
                    importer.isFoldout = EditorGUILayout.Foldout( importer.isFoldout, "Show Setting" );
                }
                EditorGUILayout.EndHorizontal();

                if ( importer.isFoldout ) {
                    SettingDrawer( importer.ImporterSetting );
                    GUILayout.Space( 3f );
                    importer.OverrideForiOSSetting.isEditable = EditorGUILayout.Toggle( "iOS Setting", importer.OverrideForiOSSetting.isEditable );
                    if ( importer.OverrideForiOSSetting.isEditable )
                        SettingDrawer( importer.OverrideForiOSSetting, false );
                    importer.OverrideForAndroidSetting.isEditable = EditorGUILayout.Toggle( "Android Setting", importer.OverrideForAndroidSetting.isEditable );
                    if ( importer.OverrideForAndroidSetting.isEditable )
                        SettingDrawer( importer.OverrideForAndroidSetting, false );

                }
                GUILayout.Space( 3f );
            }
            EditorGUILayout.EndVertical();

            if ( deleteFlag )
                myTarget.CustomImporterSettings.Remove( importer );
        }

        private void SettingDrawer( CustomTextureImporterValues v, bool isDefault = true ) {
            EditorGUILayout.BeginVertical( EditorStyles.helpBox );
            {
                if ( isDefault ) {
                    using ( new ImporterValueScope<TextureImporterType>( v.TextureType, "TextureType" ) )
                        v.TextureType.Value = ( TextureImporterType )EditorGUILayout.EnumPopup( v.TextureType );

                    using ( new ImporterValueScope<TextureImporterShape>( v.TextureShape, "TextureShape" ) )
                        v.TextureShape.Value = ( TextureImporterShape )EditorGUILayout.EnumPopup( v.TextureShape );

                    using ( new ImporterValueScope<TextureWrapMode>( v.WrapMode, "WrapMode" ) )
                        v.WrapMode.Value = ( TextureWrapMode )EditorGUILayout.EnumPopup( v.WrapMode );

                    using ( new ImporterValueScope<FilterMode>( v.FilterMode, "FilterMode" ) )
                        v.FilterMode.Value = ( FilterMode )EditorGUILayout.EnumPopup( v.FilterMode );

                    using ( new ImporterValueScope<int>( v.AnisoLevel, "AnisoLevel" ) )
                        v.AnisoLevel.Value = EditorGUILayout.IntSlider( v.AnisoLevel, 0, 16 );

                    GUILayout.Space( 5f );

                    switch ( v.TextureType.Value ) {
                        case TextureImporterType.Default:
                            using ( new ImporterValueScope<bool>( v.sRGB, "sRGB" ) )
                                v.sRGB.Value = EditorGUILayout.Toggle( v.sRGB );
                        break;
                        case TextureImporterType.Sprite:
                            using ( new ImporterValueScope<bool>( v.sRGB, "sRGB" ) )
                                v.sRGB.Value = EditorGUILayout.Toggle( v.sRGB );

                            using ( new ImporterValueScope<SpriteImportMode>( v.SpriteMode, "SpriteMode" ) )
                                v.SpriteMode.Value = ( SpriteImportMode )EditorGUILayout.EnumPopup( v.SpriteMode );

                            using ( new ImporterValueScope<SpriteMeshType>( v.MeshType, "MeshType" ) )
                                v.MeshType.Value = ( SpriteMeshType )EditorGUILayout.EnumPopup( v.MeshType );

                            using ( new ImporterValueScope<string>( v.PackingTag, "PackingTag" ) )
                                v.PackingTag.Value = EditorGUILayout.TextField( v.PackingTag );

                            using ( new ImporterValueScope<int>( v.PixelsPerUnit, "PixelsPerUnit" ) )
                                v.PixelsPerUnit.Value = EditorGUILayout.IntField( v.PixelsPerUnit );

                            using ( new ImporterValueScope<int>( v.ExtrudeEdges, "ExtrudeEdges" ) )
                                v.ExtrudeEdges.Value = EditorGUILayout.IntSlider( v.ExtrudeEdges, 0, 32 );

                            //using ( new ImporterValueScope<PivotMode>( v.Pivot, "Pivot" ) )
                            //    v.Pivot.Value = ( PivotMode )EditorGUILayout.EnumPopup( v.Pivot );
                            break;
                        case TextureImporterType.NormalMap:
                            using ( new ImporterValueScope<bool>( v.CreateFromGrayScale, "CreateFromGrayScale" ) )
                                v.CreateFromGrayScale.Value = EditorGUILayout.Toggle( v.CreateFromGrayScale );
                            break;
                        default:
                            break;
                    }

                    GUILayout.Space( 5f );

                    // advance settings
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space( 20f );
                        v.isAdvanced = EditorGUILayout.Foldout( v.isAdvanced, "Advanced" );
                    }
                    EditorGUILayout.EndHorizontal();

                    if ( v.isAdvanced ) {
                        using ( new ImporterValueScope<bool>( v.ReadWriteEnabled, "ReadWriteEnabled" ) )
                            v.ReadWriteEnabled.Value = EditorGUILayout.Toggle( v.ReadWriteEnabled );

                        using ( new ImporterValueScope<TextureImporterNPOTScale>( v.NonPowerOf2, "NonPowerOf2" ) )
                            v.NonPowerOf2.Value = ( TextureImporterNPOTScale )EditorGUILayout.EnumPopup( v.NonPowerOf2 );

                        using ( new ImporterValueScope<TextureImporterAlphaSource>( v.AlphaSource, "AlphaSource" ) )
                            v.AlphaSource.Value = ( TextureImporterAlphaSource )EditorGUILayout.EnumPopup( v.AlphaSource );

                        using ( new ImporterValueScope<bool>( v.AlphaIsTransparency, "AlphaIsTransparency" ) )
                            v.AlphaIsTransparency.Value = EditorGUILayout.Toggle( v.AlphaIsTransparency );

                        using ( new ImporterValueScope<bool>( v.GenerateMipMaps, "GenerateMipMaps" ) )
                            v.GenerateMipMaps.Value = EditorGUILayout.Toggle( v.GenerateMipMaps );
                    }

                    GUILayout.Space( 5f );
                }

                // platform setting
                using ( new ImporterValueScope<bool>( v.FitSize, "FitSize" ) )
                    v.FitSize.Value = EditorGUILayout.Toggle( v.FitSize );

                if ( !v.FitSize.Value ) {
                    using ( new ImporterValueScope<int>( v.MaxSize, "MaxSize" ) )
                        v.MaxSize.Value = EditorGUILayout.IntPopup( v.MaxSize, TextureImporterHelper.TexutureSizeLabel, TextureImporterHelper.TextureSize );
                } else {
                    v.MaxSize.isEditable = false;
                }

                using ( new ImporterValueScope<TextureImporterCompression>( v.Compression, "Compression" ) )
                    v.Compression.Value = ( TextureImporterCompression )EditorGUILayout.EnumPopup( v.Compression );

                if ( !isDefault ) {
                    using ( new ImporterValueScope<bool>( v.AllowAlphaSplitting, "AllowAlphaSplitting" ) )
                        v.AllowAlphaSplitting.Value = EditorGUILayout.Toggle( v.AllowAlphaSplitting );
                }

                using ( new ImporterValueScope<TextureImporterFormat>( v.Format, "Format" ) )
                    v.Format.Value = ( TextureImporterFormat )EditorGUILayout.EnumPopup( v.Format );

                using ( new ImporterValueScope<bool>( v.UseCrunchCompression, "UseCrunchCompression" ) )
                    v.UseCrunchCompression.Value = EditorGUILayout.Toggle( v.UseCrunchCompression );

                if ( v.UseCrunchCompression.Value ) {
                    using ( new ImporterValueScope<int>( v.CompressionQuality, "CompressorQuality" ) )
                        v.CompressionQuality.Value = EditorGUILayout.IntSlider( v.CompressionQuality, 0, 100 );
                } else {
                    v.CompressionQuality.isEditable = false;
                }

            }
            EditorGUILayout.EndVertical();
        }

    }
}