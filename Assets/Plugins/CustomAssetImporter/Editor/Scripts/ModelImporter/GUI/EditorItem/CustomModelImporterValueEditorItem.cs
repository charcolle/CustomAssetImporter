using UnityEngine;
using UnityEditor;

using GUIHelper = charcolle.Utility.CustomAssetImporter.GUIHelper;

namespace charcolle.Utility.CustomAssetImporter {

    internal class CustomModelImporterValueEditorItem : EditorWindowItem<CustomModelImporterSettingValue> {

        public int tabSelected = 0;

        public CustomModelImporterValueEditorItem( CustomModelImporterSettingValue data ) : base( data ) { }

        protected override void Draw() { }

        private Rect boxRect = Rect.zero;
        private string[] tab = new string[] { "Model", "Rig", "Animation", "Material" };
        protected override void Draw( ref Rect rect ) {
            if( Event.current.type == EventType.Repaint && boxRect != Rect.zero )
                EditorStyles.helpBox.Draw( boxRect, false, false, false, false );
            ImporterValueHeightCalc.Begin();
            var firstRect = rect;

            rect.xMin += 15f;
            rect.xMax -= 5f;
            rect.y += 3f;
            rect.height = EditorGUIUtility.singleLineHeight;

            {
                rect.y += GUIHelper.Rects.NextItemY + 5f;
                tabSelected = GUI.Toolbar( rect, tabSelected, tab );
                rect.y += 5f;
                rect.xMin += GUIHelper.Rects.DefaultLabelWidth;
                ImporterValueHeightCalc.Height += GUIHelper.Rects.NextItemY + 10f;

                switch( tabSelected ) {
                    case 0:
                        using( new ImporterValueScopeRect<int>( ScaleFactor, "ScaleFactor", ref rect ) )
                            ScaleFactor.Value = EditorGUI.IntField( rect, ScaleFactor );
#if UNITY_2017_1_OR_NEWER
                        using( new ImporterValueScopeRect<bool>( UseFileScale, "UseFileScale", ref rect ) )
                            UseFileScale.Value = EditorGUI.Toggle( rect, UseFileScale );
#endif
                        using( new ImporterValueScopeRect<ModelImporterMeshCompression>( MeshCompression, "MeshCompression", ref rect ) )
                            MeshCompression.Value = ( ModelImporterMeshCompression )EditorGUI.EnumPopup( rect, MeshCompression );

                        using( new ImporterValueScopeRect<bool>( ReadWriteEnabled, "ReadWriteEnabled", ref rect ) )
                            ReadWriteEnabled.Value = EditorGUI.Toggle( rect, ReadWriteEnabled );

                        using( new ImporterValueScopeRect<bool>( OptimizeMesh, "OptimizeMesh", ref rect ) )
                            OptimizeMesh.Value = EditorGUI.Toggle( rect, OptimizeMesh );

                        using( new ImporterValueScopeRect<bool>( ImportBlendShapes, "ImportBlendShapes", ref rect ) )
                            ImportBlendShapes.Value = EditorGUI.Toggle( rect, ImportBlendShapes );

                        using( new ImporterValueScopeRect<bool>( GenerateColliders, "GenerateColliders", ref rect ) )
                            GenerateColliders.Value = EditorGUI.Toggle( rect, GenerateColliders );

                        using( new ImporterValueScopeRect<bool>( KeepQuads, "KeepQuads", ref rect ) )
                            KeepQuads.Value = EditorGUI.Toggle( rect, KeepQuads );
#if UNITY_2017_3_OR_NEWER
                        using( new ImporterValueScopeRect<ModelImporterIndexFormat>( IndexFormat, "IndexFormat", ref rect ) )
                            IndexFormat.Value = ( ModelImporterIndexFormat )EditorGUI.EnumPopup( rect, IndexFormat );
#endif
#if UNITY_5_6_OR_NEWER
                        using( new ImporterValueScopeRect<bool>( WeldVertics, "WeldVertics", ref rect ) )
                            WeldVertics.Value = EditorGUI.Toggle( rect, WeldVertics );
#endif
#if UNITY_2017_1_OR_NEWER
                        using( new ImporterValueScopeRect<bool>( ImportVisibility, "ImportVisibility", ref rect ) )
                            ImportVisibility.Value = EditorGUI.Toggle( rect, ImportVisibility );

                        using( new ImporterValueScopeRect<bool>( ImportCameras, "ImportCameras", ref rect ) )
                            ImportCameras.Value = EditorGUI.Toggle( rect, ImportCameras );

                        using( new ImporterValueScopeRect<bool>( ImportLights, "ImportLights", ref rect ) )
                            ImportLights.Value = EditorGUI.Toggle( rect, ImportLights );
#endif
#if UNITY_2017_3_OR_NEWER
                        using( new ImporterValueScopeRect<bool>( PreserverHierarchy, "PreserverHierarchy", ref rect ) )
                            PreserverHierarchy.Value = EditorGUI.Toggle( rect, PreserverHierarchy );
#endif
                        using( new ImporterValueScopeRect<bool>( SwapUVs, "SwapUVs", ref rect ) )
                            SwapUVs.Value = EditorGUI.Toggle( rect, SwapUVs );

                        using( new ImporterValueScopeRect<bool>( GenerateLightMapUVs, "GenerateLightMapUVs", ref rect ) )
                            GenerateLightMapUVs.Value = EditorGUI.Toggle( rect, GenerateLightMapUVs );

                        if( GenerateLightMapUVs.Value ) {
                            rect.x += GUIHelper.Rects.Indent;
                            rect.xMax -= GUIHelper.Rects.Indent;

                            using( new ImporterValueScopeRect<int>( HardAngle, "HardAngle", ref rect ) )
                                HardAngle.Value = EditorGUI.IntSlider( rect, HardAngle, 0, 180 );

                            using( new ImporterValueScopeRect<int>( PackMargin, "PackMargin", ref rect ) )
                                PackMargin.Value = EditorGUI.IntSlider( rect, PackMargin, 1, 64 );

                            using( new ImporterValueScopeRect<int>( AngleError, "AngleError", ref rect ) )
                                AngleError.Value = EditorGUI.IntSlider( rect, AngleError, 1, 75 );

                            using( new ImporterValueScopeRect<int>( AreaError, "AreaError", ref rect ) )
                                AreaError.Value = EditorGUI.IntSlider( rect, AreaError, 1, 75 );

                            rect.x -= GUIHelper.Rects.Indent;
                            rect.xMax += GUIHelper.Rects.Indent;
                        } else {
                            HardAngle.IsConfigurable = false;
                            PackMargin.IsConfigurable = false;
                            AngleError.IsConfigurable = false;
                            AreaError.IsConfigurable = false;
                        }

                        rect.y += 5f;
                        ImporterValueHeightCalc.Height += 5f;

                        using( new ImporterValueScopeRect<ModelImporterNormals>( Normals, "Normals", ref rect ) )
                            Normals.Value = ( ModelImporterNormals )EditorGUI.EnumPopup( rect, Normals );

                        if( Normals.Value.Equals( ModelImporterNormals.Calculate ) ) {
#if UNITY_2017_1_OR_NEWER
                            using( new ImporterValueScopeRect<ModelImporterNormalCalculationMode>( NormalsMode, "NormalsMode", ref rect ) )
                                NormalsMode.Value = ( ModelImporterNormalCalculationMode )EditorGUI.EnumPopup( rect, NormalsMode );
#endif
                            using( new ImporterValueScopeRect<int>( SmoothingAngle, "SmoothingAngle", ref rect ) )
                                SmoothingAngle.Value = EditorGUI.IntSlider( rect, SmoothingAngle, 0, 180 );
                        } else {
#if UNITY_2017_1_OR_NEWER
                            NormalsMode.IsConfigurable = false;
#endif
                            SmoothingAngle.IsConfigurable = false;
                        }

                        using( new ImporterValueScopeRect<ModelImporterTangents>( Tangents, "Tangents", ref rect ) )
                            Tangents.Value = ( ModelImporterTangents )EditorGUI.EnumPopup( rect, Tangents );

                        rect.y += 5f;
                        ImporterValueHeightCalc.Height += 5f;
                        break;
                    case 1:
                        using( new ImporterValueScopeRect<ModelImporterAnimationType>( AnimationType, "AnimationType", ref rect ) )
                            AnimationType.Value = ( ModelImporterAnimationType )EditorGUI.EnumPopup( rect, AnimationType );

                        using( new ImporterValueScopeRect<bool>( OptimizeGameObject, "OptimizeGameObject", ref rect ) )
                            OptimizeGameObject.Value = EditorGUI.Toggle( rect, OptimizeGameObject );

                        break;
                    case 2:
                        using( new ImporterValueScopeRect<bool>( ImportAnimation, "ImportAnimation", ref rect ) )
                            ImportAnimation.Value = EditorGUI.Toggle( rect, ImportAnimation );

                        if( ImportAnimation.Value ) {
                            using( new ImporterValueScopeRect<ModelImporterAnimationCompression>( AnimCompression, "AnimCompression", ref rect ) )
                                AnimCompression.Value = ( ModelImporterAnimationCompression )EditorGUI.EnumPopup( rect, AnimCompression );

                            using( new ImporterValueScopeRect<float>( RotaionError, "RotaionError", ref rect ) )
                                RotaionError.Value = EditorGUI.FloatField( rect, RotaionError );

                            using( new ImporterValueScopeRect<float>( PositionError, "PositionError", ref rect ) )
                                PositionError.Value = EditorGUI.FloatField( rect, PositionError );

                            using( new ImporterValueScopeRect<float>( ScaleError, "ScaleError", ref rect ) )
                                ScaleError.Value = EditorGUI.FloatField( rect, ScaleError );
#if UNITY_2017_2_OR_NEWER
                            using( new ImporterValueScopeRect<bool>( AnimatedCustomProperties, "AnimatedCustomProperties", ref rect ) )
                                AnimatedCustomProperties.Value = EditorGUI.Toggle( rect, AnimatedCustomProperties );
#endif
                        } else {
                            BakeAnimations.IsConfigurable = false;
                            RotaionError.IsConfigurable = false;
                            ScaleError.IsConfigurable = false;
#if UNITY_2017_2_OR_NEWER
                            AnimatedCustomProperties.IsConfigurable = false;
#endif
                        }
                        rect.y += 5f;
                        ImporterValueHeightCalc.Height += 5f;
                        //GUILayout.Label( "Animation Clip Default" );
                        //using ( new ImporterValueScopeRect<bool>( LoopTime, "LoopTime" ) )
                        //    LoopTime.Value = EditorGUI.Toggle( LoopTime );
                        break;
                    case 3:
                        using( new ImporterValueScopeRect<bool>( ImportMaterials, "ImportMaterials", ref rect ) )
                            ImportMaterials.Value = EditorGUI.Toggle( rect, ImportMaterials );

                        if( ImportMaterials.Value ) {
#if UNITY_2017_3_OR_NEWER
                            using( new ImporterValueScopeRect<ModelImporterMaterialLocation>( MaterialLocation, "MaterialLocation", ref rect ) )
                                MaterialLocation.Value = ( ModelImporterMaterialLocation )EditorGUI.EnumPopup( rect, MaterialLocation );
#endif
                            using( new ImporterValueScopeRect<ModelImporterMaterialName>( MaterialNaming, "MaterialNaming", ref rect ) )
                                MaterialNaming.Value = ( ModelImporterMaterialName )EditorGUI.EnumPopup( rect, MaterialNaming );
                            using( new ImporterValueScopeRect<ModelImporterMaterialSearch>( MaterialSearch, "MaterialSearch", ref rect ) )
                                MaterialSearch.Value = ( ModelImporterMaterialSearch )EditorGUI.EnumPopup( rect, MaterialSearch );
                        } else {
#if UNITY_2017_3_OR_NEWER
                            MaterialLocation.IsConfigurable = false;
#endif
                            MaterialNaming.IsConfigurable = false;
                            MaterialSearch.IsConfigurable = false;
                        }
                        break;
                }
            }

            boxRect = new Rect( firstRect.x, firstRect.y + GUIHelper.Rects.NextItemY, firstRect.width, ImporterValueHeightCalc.Height + 6f );

            rect.xMin -= 15;
            rect.xMax += 5f;
            rect.xMin -= GUIHelper.Rects.DefaultLabelWidth;
            rect.y += 3f;
        }

        #region property
        private ImporterIntValue ScaleFactor {
            get {
                return data.ScaleFactor;
            }
            set {
                data.ScaleFactor = value;
            }
        }
#if UNITY_2017_1_OR_NEWER
        private ImporterBoolValue UseFileScale {
            get {
                return data.UseFileScale;
            }
            set {
                data.UseFileScale = value;
            }
        }
#endif
        private ImporterMeshCompressionValue MeshCompression {
            get {
                return data.MeshCompression;
            }
            set {
                data.MeshCompression = value;
            }
        }

        private ImporterBoolValue ReadWriteEnabled {
            get {
                return data.ReadWriteEnabled;
            }
            set {
                data.ReadWriteEnabled = value;
            }
        }

        private ImporterBoolValue OptimizeMesh {
            get {
                return data.OptimizeMesh;
            }
            set {
                data.OptimizeMesh = value;
            }
        }

        private ImporterBoolValue ImportBlendShapes {
            get {
                return data.ImportBlendShapes;
            }
            set {
                data.ImportBlendShapes = value;
            }
        }

        private ImporterBoolValue GenerateColliders {
            get {
                return data.GenerateColliders;
            }
            set {
                data.GenerateColliders = value;
            }
        }

        private ImporterBoolValue KeepQuads {
            get {
                return data.KeepQuads;
            }
            set {
                data.KeepQuads = value;
            }
        }
#if UNITY_2017_3_OR_NEWER
        private ImporterIndexFormatValue IndexFormat {
            get {
                return data.IndexFormat;
            }
            set {
                data.IndexFormat = value;
            }
        }
#endif
#if UNITY_5_6_OR_NEWER
        private ImporterBoolValue WeldVertics {
            get {
                return data.WeldVertics;
            }
            set {
                data.WeldVertics = value;
            }
        }
#endif
#if UNITY_2017_1_OR_NEWER
        private ImporterBoolValue ImportVisibility {
            get {
                return data.ImportVisibility;
            }
            set {
                data.ImportVisibility = value;
            }
        }

        private ImporterBoolValue ImportCameras {
            get {
                return data.ImportCameras;
            }
            set {
                data.ImportCameras = value;
            }
        }

        private ImporterBoolValue ImportLights {
            get {
                return data.ImportLights;
            }
            set {
                data.ImportLights = value;
            }
        }
#endif
#if UNITY_2017_3_OR_NEWER
        private ImporterBoolValue PreserverHierarchy {
            get {
                return data.PreserverHierarchy;
            }
            set {
                data.PreserverHierarchy = value;
            }
        }
#endif
        private ImporterBoolValue SwapUVs {
            get {
                return data.SwapUVs;
            }
            set {
                data.SwapUVs = value;
            }
        }

        private ImporterBoolValue GenerateLightMapUVs {
            get {
                return data.GenerateLightMapUVs;
            }
            set {
                data.GenerateLightMapUVs = value;
            }
        }

        private ImporterIntValue HardAngle {
            get {
                return data.HardAngle;
            }
            set {
                data.HardAngle = value;
            }
        }

        private ImporterIntValue PackMargin {
            get {
                return data.PackMargin;
            }
            set {
                data.PackMargin = value;
            }
        }

        private ImporterIntValue AngleError {
            get {
                return data.AngleError;
            }
            set {
                data.AngleError = value;
            }
        }

        private ImporterIntValue AreaError {
            get {
                return data.AreaError;
            }
            set {
                data.AreaError = value;
            }
        }

        private ImporterImportNormalsValue Normals {
            get {
                return data.Normals;
            }
            set {
                data.Normals = value;
            }
        }
#if UNITY_2017_1_OR_NEWER
        private ImporterNormalsModeValue NormalsMode {
            get {
                return data.NormalsMode;
            }
            set {
                data.NormalsMode = value;
            }
        }
#endif
        private ImporterIntValue SmoothingAngle {
            get {
                return data.SmoothingAngle;
            }
            set {
                data.SmoothingAngle = value;
            }
        }

        private ImporterImportTangensValue Tangents {
            get {
                return data.Tangents;
            }
            set {
                data.Tangents = value;
            }
        }

        private ImporterBoolValue ImportMaterials {
            get {
                return data.ImportMaterials;
            }
            set {
                data.ImportMaterials = value;
            }
        }
#if UNITY_2017_3_OR_NEWER
        private ImporterMaterialLocationValue MaterialLocation {
            get {
                return data.MaterialLocation;
            }
            set {
                data.MaterialLocation = value;
            }
        }
#endif

        private ImporterMaterialNamingValue MaterialNaming {
            get {
                return data.MaterialNaming;
            }
            set {
                data.MaterialNaming = value;
            }
        }

        private ImporterMaterialSearchValue MaterialSearch {
            get {
                return data.MaterialSearch;
            }
            set {
                data.MaterialSearch = value;
            }
        }

        private ImporterAnimationTypeValue AnimationType {
            get {
                return data.AnimationType;
            }
            set {
                data.AnimationType = value;
            }
        }

        private ImporterBoolValue OptimizeGameObject {
            get {
                return data.OptimizeGameObject;
            }
            set {
                data.OptimizeGameObject = value;
            }
        }

        private ImporterBoolValue ImportAnimation {
            get {
                return data.ImportAnimation;
            }
            set {
                data.ImportAnimation = value;
            }
        }

        private ImporterBoolValue BakeAnimations {
            get {
                return data.BakeAnimations;
            }
            set {
                data.BakeAnimations = value;
            }
        }

        private ImporterBoolValue ResampleCurves {
            get {
                return data.ResampleCurves;
            }
            set {
                data.ResampleCurves = value;
            }
        }

        private ImporterAnimCompressionValue AnimCompression {
            get {
                return data.AnimCompression;
            }
            set {
                data.AnimCompression = value;
            }
        }

        private ImporterFloatValue RotaionError {
            get {
                return data.RotaionError;
            }
            set {
                data.RotaionError = value;
            }
        }

        private ImporterFloatValue PositionError {
            get {
                return data.PositionError;
            }
            set {
                data.PositionError = value;
            }
        }

        private ImporterFloatValue ScaleError {
            get {
                return data.ScaleError;
            }
            set {
                data.ScaleError = value;
            }
        }
#if UNITY_2017_2_OR_NEWER
        private ImporterBoolValue AnimatedCustomProperties {
            get {
                return data.AnimatedCustomProperties;
            }
            set {
                data.AnimatedCustomProperties = value;
            }
        }
#endif
        private ImporterBoolValue LoopTime {
            get {
                return data.LoopTime;
            }
            set {
                data.LoopTime = value;
            }
        }
    
        #endregion

    }

}