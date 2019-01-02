using UnityEngine;
using UnityEditor;
using System;

namespace charcolle.Utility.CustomAssetImporter {

    internal abstract class EditorWindowItem<T> {

        protected T data;

        public EditorWindowItem( T data ) {
            this.data = data;
        }

        public T Data {
            get {
                return data;
            }
        }

        public void OnGUI() {
            if( data == null ) {
                DrawIfDataIsNull();
                return;
            }

            EditorGUI.BeginChangeCheck();
            Draw();
            if( EditorGUI.EndChangeCheck() )
                GUI.changed = true;
        }

        public void OnGUI( ref Rect rect ) {
            if( data == null ) {
                DrawIfDataIsNull();
                return;
            }

            EditorGUI.BeginChangeCheck();
            Draw( ref rect );
            if( EditorGUI.EndChangeCheck() )
                GUI.changed = true;
        }

        protected abstract void Draw();
        protected virtual void Draw( ref Rect rect ) { }
        protected virtual void DrawIfDataIsNull() { }

    }

}