using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;

using GUIHelper = charcolle.Utility.CustomAssetImporter.GUIHelper;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomTextureImporterTreeView : TreeViewWithTreeModel<CustomTextureImporter, CustomTextureImporterValue> {

        public CustomTextureImporterTreeView( TreeViewState state, TreeModel<CustomTextureImporter> model )
            : base( state, model ) {
            showBorder = true;
            customFoldoutYOffset = 3f;
            showAlternatingRowBackgrounds = true;

#if UNITY_2017_2_OR_NEWER
            DefaultSettingNum = 11;
            OverrideSettingNum = 7;
#else
            DefaultSettingNum = 10;
            OverrideSettingNum = 6;
#endif
            Reload();
        }

        protected override IList<TreeViewItem> BuildRows( TreeViewItem root ) {
            var list = base.BuildRows( root );
            for( int i = 0; i < list.Count; i++ ) {
                var item = ( TreeViewItem<CustomTextureImporter> )list[ i ];
                item.editorItem = new CustomTextureImporterClassEditorItem( item.data );
            }

            return list;
        }

        //=======================================================
        // gui
        //=======================================================

        protected override float GetCustomRowHeight( int row, TreeViewItem item ) {
            var myItem = ( TreeViewItem<CustomTextureImporter> )item;

            if( !myItem.data.IsEnable ) {
                return 30f;
            } else {
                var space = 0f;
                if( myItem.data.isFoldout ) {
                    space = DefaultSettingHeight + 10f;
                    if( myItem.data.ImporterSetting.Value.UseCrunchCompression.Value )
                        space += GUIHelper.Rects.NextItemY;
                    if( !myItem.data.ImporterSetting.Value.FitSize.Value )
                        space += GUIHelper.Rects.NextItemY;

                    if( myItem.data.OverrideForAndroidSetting.IsConfigurable ) {
                        space += OverrideSettingHeight - GUIHelper.Rects.NextItemY;
                        if( myItem.data.OverrideForAndroidSetting.Value.UseCrunchCompression.Value )
                            space += GUIHelper.Rects.NextItemY;
                        if( !myItem.data.OverrideForAndroidSetting.Value.FitSize.Value )
                            space += GUIHelper.Rects.NextItemY;
                    }
                    if( myItem.data.OverrideForiOSSetting.IsConfigurable ) {
                        space += OverrideSettingHeight - GUIHelper.Rects.NextItemY;
                        if( myItem.data.OverrideForiOSSetting.Value.UseCrunchCompression.Value )
                            space += GUIHelper.Rects.NextItemY;
                        if( !myItem.data.OverrideForiOSSetting.Value.FitSize.Value )
                            space += GUIHelper.Rects.NextItemY;
                    }

                    // custom height
                    switch( myItem.data.ImporterSetting.Value.TextureType.Value ) {
                        case TextureImporterType.Default:
                            space += GUIHelper.Rects.NextItemY * 3;
                            break;

                        case TextureImporterType.Sprite:
                            space += GUIHelper.Rects.NextItemY * 4;
                            if( myItem.data.ImporterSetting.Value.SpriteMode.Value != SpriteImportMode.Polygon )
                                space += GUIHelper.Rects.NextItemY;
                            break;

                        case TextureImporterType.Cookie:
                            space += GUIHelper.Rects.NextItemY * 3;
                            break;

                        case TextureImporterType.SingleChannel:
                            space += GUIHelper.Rects.NextItemY * 2;
                            break;
                    }

                    if( myItem.data.ImporterSetting.Value.isAdvanced ) {
                        space += GUIHelper.Rects.NextItemY * 3;
                        switch( myItem.data.ImporterSetting.Value.TextureType.Value ) {
                            case TextureImporterType.Sprite:
                                space += GUIHelper.Rects.NextItemY * 3;
                                break;

                            case TextureImporterType.GUI:
                                space += GUIHelper.Rects.NextItemY * 2;
                                break;
                        }
                        if( myItem.data.ImporterSetting.Value.GenerateMipMaps.IsConfigurable && myItem.data.ImporterSetting.Value.GenerateMipMaps.Value ) {
                            space += GUIHelper.Rects.NextItemY * 3;
#if UNITY_2017_1_OR_NEWER
                            space += GUIHelper.Rects.NextItemY;
                            if( myItem.data.ImporterSetting.Value.MipMapsPreserveCover.Value )
                                space += GUIHelper.Rects.NextItemY;
#endif
                        }
                    }
                } else {
                    space = EditorGUIUtility.singleLineHeight;
                }

                return space + BasicItemHeight;
            }
        }

    }

}