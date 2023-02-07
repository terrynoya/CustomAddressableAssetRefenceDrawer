using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Yaojz
{
    public class CustomAssetReferenceDrawer
    {
        public void OnGUILayout<T>(T value,out T newValue,Func<T> createAssetCallBack) where T:AssetReference
        {
            if (value != null)
            {
                float iconHeight = EditorGUIUtility.singleLineHeight - EditorGUIUtility.standardVerticalSpacing * 3;
                Vector2 iconSize = EditorGUIUtility.GetIconSize();
                EditorGUIUtility.SetIconSize(new Vector2(iconHeight, iconHeight));
                string assetPath = AssetDatabase.GUIDToAssetPath(value.AssetGUID);
                Texture2D assetIcon = AssetDatabase.GetCachedIcon(assetPath) as Texture2D;
                if (EditorGUILayout.DropdownButton(new GUIContent(value.editorAsset.name, assetIcon),
                    FocusType.Keyboard, EditorStyles.objectField))
                {
                    
                }
                EditorGUIUtility.SetIconSize(iconSize);
            }
            else
            {
                if (EditorGUILayout.DropdownButton(new GUIContent("Drag Addressable"), FocusType.Keyboard,
                    EditorStyles.objectField))
                {
                
                }    
            }
            var position = GUILayoutUtility.GetLastRect();
            bool isDragging = Event.current.type == EventType.DragUpdated && position.Contains(Event.current.mousePosition);
            bool isDropping = Event.current.type == EventType.DragPerform && position.Contains(Event.current.mousePosition);
            
            newValue = HandleDragAndDrop(value,isDragging, isDropping,createAssetCallBack);
        }
        
        private T HandleDragAndDrop<T>(T value,bool isDragging, bool isDropping,Func<T> createAssetCallBack) where T:AssetReference
        {
            var newValue = value;
            if (isDragging)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            }
            var paths = DragAndDrop.paths;
            if (paths.Length == 1)
            {
                if (isDropping)
                {
                    string path = paths[0];   
                    Object obj;
                    if (DragAndDrop.objectReferences != null && DragAndDrop.objectReferences.Length == 1)
                        obj = DragAndDrop.objectReferences[0];
                    else
                        obj = AssetDatabase.LoadAssetAtPath<Object>(path);
                    if (value == null)
                    {
                        //newValue = new T();
                        newValue = createAssetCallBack.Invoke();
                    }
                    var success = newValue.SetEditorAsset(obj);
                    Debug.Log("success!!");
                }    
            }
            return newValue;
        }
    }
}