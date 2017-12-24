using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using charcolle.Utility.CustomAssetImporter;

using FileHelper = charcolle.Utility.CustomAssetImporter.FileHelper;

public class CustomImporterSettingsBase<T, U> : ScriptableObject where T : CustomImporterClass<U>, new() {

    public List<T> CustomImporterSettings = new List<T>();
    public string ImporterName;

    /// <summary>
    /// check the asset is custom asset
    /// </summary>
    public T GetCustomImporter( string assetPath ) {
        for ( int i = 0; i < CustomImporterSettings.Count; i++ ) {
            var importer = CustomImporterSettings[i] as T;
            if ( string.IsNullOrEmpty( importer.Target ) )
                continue;

            if ( importer.CheckCustomImporter( assetPath ) )
                return importer;
        }
        return null;
    }

    #region drawer
    //=============================================================================
    // Drawer
    //=============================================================================

    public void Draw() {
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
    }


    private void Header() {
        GUI.backgroundColor = Color.grey;
        EditorGUILayout.BeginHorizontal( CustomAssetImporterStyles.Header );
        {
            GUILayout.Label( "<b>" + ImporterName + "</b>" );
            GUILayout.FlexibleSpace();
            GUI.backgroundColor = Color.green;
            if ( GUILayout.Button( "+", CustomAssetImporterStyles.ToolBox, GUILayout.Width( 70f ) ) )
                CustomImporterSettings.Add( new T() );
        }
        EditorGUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;

        if ( CustomImporterSettings.Count == 0 )
            EditorGUILayout.HelpBox( "No Importers", MessageType.Warning );

    }

    private void Footer() {
        if ( CustomImporterSettings.Count == 0 )
            return;

        EditorGUILayout.BeginHorizontal( CustomAssetImporterStyles.ToolBox );
        {
            if ( GUILayout.Button( "Collapse all", EditorStyles.toolbarButton, GUILayout.Width( 120f ) ) ) {
                for ( int i = 0; i < CustomImporterSettings.Count; i++ )
                    CustomImporterSettings[i].isFoldout = false;
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private Vector2 ScrollView;
    private void Main() {
        if ( CustomImporterSettings.Count == 0 )
            return;
        ScrollView = EditorGUILayout.BeginScrollView( ScrollView, CustomAssetImporterStyles.Box, GUILayout.ExpandHeight( true ) );
        {
            var importers = CustomImporterSettings;
            try {
                for ( int i = 0; i < importers.Count; i++ )
                    ImporterDrawer( importers[i] );
            } catch { }
            GUILayout.FlexibleSpace();
        }
        EditorGUILayout.EndScrollView();
    }

    private Rect dropArea = Rect.zero;
    private void ImporterDrawer( T importer ) {
        var deleteFlag = false;
        var curEvent = Event.current;
        dropArea = EditorGUILayout.BeginVertical( CustomAssetImporterStyles.Box, GUILayout.ExpandWidth( true ) );
        {
            EditorGUILayout.BeginHorizontal( CustomAssetImporterStyles.ToolBox );
            {
                GUILayout.Label( Path.GetFileNameWithoutExtension( importer.Target ) );
                GUILayout.FlexibleSpace();
                GUI.backgroundColor = Color.yellow;
                if ( GUILayout.Button( "ReImport", EditorStyles.toolbarButton, GUILayout.Width( 60f ) ) ) {
                    FileHelper.ReImport( importer );
                }
                GUI.backgroundColor = Color.cyan;
                if ( GUILayout.Button( "Copy", EditorStyles.toolbarButton, GUILayout.Width( 40f ) ) ) {
                    Type type = typeof( T );
                    ConstructorInfo info = type.GetConstructor( new Type[] { typeof( T ) } );
                    if ( info == null )
                        throw new NotSupportedException( "fatal error." );
                    CustomImporterSettings.Add( ( T )info.Invoke( new object[] { importer } ) );
                }
                GUI.backgroundColor = Color.red;
                if ( GUILayout.Button( "x", EditorStyles.toolbarButton, GUILayout.Width( 20f ) ) ) {
                    deleteFlag = true;
                }
                GUI.backgroundColor = Color.white;
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space( 2f );

            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space( 5f );
                importer.Type = ( ImportTargetType )EditorGUILayout.EnumPopup( importer.Type, GUILayout.Width( 100f ) );
                importer.Target = EditorGUILayout.TextField( importer.Target );
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


            GUILayout.Space( 5f );

            importer.Draw();
        }
        EditorGUILayout.EndVertical();

        eventProcess( curEvent, importer );
        if ( deleteFlag )
            CustomImporterSettings.Remove( importer );
    }

    private void eventProcess( Event evnt, T importer ) {
        switch ( evnt.type ) {
            case EventType.ContextClick:
                break;
            default:
                var dropFileName = FileHelper.GetDraggedAssetPath( evnt, dropArea );
                if ( !string.IsNullOrEmpty( dropFileName ) )
                    importer.Target = dropFileName;
                break;
        }
    }

    #endregion

}
