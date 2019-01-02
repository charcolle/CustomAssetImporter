using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using System.Collections.Generic;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomModelImporterTreeView : TreeViewWithTreeModel<CustomModelImporter, CustomModelImporterValue> {

        public CustomModelImporterTreeView( TreeViewState state, TreeModel<CustomModelImporter> model )
            : base( state, model ) {
            showBorder = true;
            customFoldoutYOffset = 3f;
            showAlternatingRowBackgrounds = true;

            DefaultSettingNum = 0;
            OverrideSettingNum = 0;
            Reload();
        }

        protected override IList<TreeViewItem> BuildRows( TreeViewItem root ) {
            var list = base.BuildRows( root );
            for( int i = 0; i < list.Count; i++ ) {
                var item = ( TreeViewItem<CustomModelImporter> )list[ i ];
                item.editorItem = new CustomModelImporterClassEditorItem( item.data );
            }

            return list;
        }

        //=======================================================
        // gui
        //=======================================================

        protected override float GetCustomRowHeight( int row, TreeViewItem item ) {
            var myItem = ( TreeViewItem<CustomModelImporter> )item;
            var editorItem = ( CustomModelImporterClassEditorItem )myItem.editorItem;

            if( !myItem.data.IsEnable ) {
                return 30f;
            } else {
                var space = 0f;
                if( myItem.data.isFoldout ) {
                    space = DefaultSettingHeight;
                    switch( editorItem.ImporterSettingItem.tabSelected ) {
                        case 0:
                            space += GUIHelper.Rects.NextItemY * 11 + 5f;
                            if( myItem.data.ImporterSetting.Value.GenerateLightMapUVs.Value )
                                space += GUIHelper.Rects.NextItemY * 4;
                            if( myItem.data.ImporterSetting.Value.Normals.Value == ModelImporterNormals.Calculate )
                                space += GUIHelper.Rects.NextItemY;
#if UNITY_5_6_OR_NEWER
                            space += GUIHelper.Rects.NextItemY;
#endif
#if UNITY_2017_1_OR_NEWER
                            if( myItem.data.ImporterSetting.Value.Normals.Value == ModelImporterNormals.Calculate )
                                space += GUIHelper.Rects.NextItemY;
                            space += GUIHelper.Rects.NextItemY * 4;
#endif
#if UNITY_2017_3_OR_NEWER
                            space += GUIHelper.Rects.NextItemY * 2;
#endif
                            break;
                        case 1:
                            space += GUIHelper.Rects.NextItemY * 2;
                            break;
                        case 2:
                            space += GUIHelper.Rects.NextItemY;
                            if( myItem.data.ImporterSetting.Value.ImportAnimation.Value ) {
                                space += GUIHelper.Rects.NextItemY * 4;
#if UNITY_2017_2_OR_NEWER
                                space += GUIHelper.Rects.NextItemY;
#endif
                            }
                            break;
                        case 3:
                            space += GUIHelper.Rects.NextItemY;
                            if( myItem.data.ImporterSetting.Value.ImportMaterials.Value ) {
                                space += GUIHelper.Rects.NextItemY * 2;
#if UNITY_2017_3_OR_NEWER
                                space += GUIHelper.Rects.NextItemY;
#endif
                            }
                            break;
                    }

                } else {
                    space = EditorGUIUtility.singleLineHeight;
                }

                return space + BasicItemHeight;
            }
        }
    }

}