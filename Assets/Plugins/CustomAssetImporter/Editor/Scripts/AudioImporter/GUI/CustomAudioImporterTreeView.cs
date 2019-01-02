using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using System.Collections.Generic;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomAudioImporterTreeView : TreeViewWithTreeModel<CustomAudioImporter, CustomAudioImporterValue> {

        public CustomAudioImporterTreeView( TreeViewState state, TreeModel<CustomAudioImporter> model )
            : base( state, model ) {
            showBorder = true;
            customFoldoutYOffset = 3f;
            showAlternatingRowBackgrounds = true;

            DefaultSettingNum = 7;
            OverrideSettingNum = 4;
            Reload();
        }

        protected override IList<TreeViewItem> BuildRows( TreeViewItem root ) {
            var list = base.BuildRows( root );
            for( int i = 0; i < list.Count; i++ ) {
                var item = ( TreeViewItem<CustomAudioImporter> )list[i];
                item.editorItem = new CustomAudioImporterClassEditorItem( item.data );
            }

            return list;
        }

        //=======================================================
        // gui
        //=======================================================

        protected override float GetCustomRowHeight( int row, TreeViewItem item ) {
            var myItem = ( TreeViewItem<CustomAudioImporter> )item;

            if( !myItem.data.IsEnable ) {
                return 30f;
            } else {
                var space = 0f;
                if( myItem.data.isFoldout ) {
                    space = DefaultSettingHeight;
                    if( myItem.data.OverrideForAndroidSetting.IsConfigurable ) {
                        if( myItem.data.OverrideForAndroidSetting.Value.SampleRateSetting.Value.Equals( AudioSampleRateSetting.OverrideSampleRate ) )
                            space += GUIHelper.Rects.NextItemY;
                        if( myItem.data.OverrideForAndroidSetting.Value.CompressionFormat.Value.Equals( AudioCompressionFormat.Vorbis ) )
                            space += GUIHelper.Rects.NextItemY;
                        space += OverrideSettingHeight;
                    }
                    if( myItem.data.OverrideForiOSSetting.IsConfigurable ) {
                        if( myItem.data.OverrideForiOSSetting.Value.SampleRateSetting.Value.Equals( AudioSampleRateSetting.OverrideSampleRate ) )
                            space += GUIHelper.Rects.NextItemY;
                        if( myItem.data.OverrideForiOSSetting.Value.CompressionFormat.Value.Equals( AudioCompressionFormat.Vorbis ) )
                            space += GUIHelper.Rects.NextItemY;
                        space += OverrideSettingHeight;
                    }

                    if( myItem.data.ImporterSetting.Value.CompressionFormat.Value.Equals( AudioCompressionFormat.Vorbis ) )
                        space += GUIHelper.Rects.NextItemY;

                    if( myItem.data.ImporterSetting.Value.SampleRateSetting.Value.Equals( AudioSampleRateSetting.OverrideSampleRate ) )
                        space += GUIHelper.Rects.NextItemY;

                } else {
                    space = EditorGUIUtility.singleLineHeight;
                }

                return space + BasicItemHeight;
            }
        }

    }

}