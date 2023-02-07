using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Yaojz
{
    public class CustomAssetReferenceEditorWindow:EditorWindow
    {
        [MenuItem("yaojz/custom asset reference")]
        public static void Open()
        {
            var win = CreateInstance<CustomAssetReferenceEditorWindow>();
            win.Show();
        }

        private CustomAssetReferenceDrawer _goDrawer =
            new CustomAssetReferenceDrawer();
        
        private void OnGUI()
        {
            if (GUILayout.Button("add asset"))
            {
                CustomAssetEditorManager.Instance.AddAsset(new CustomAssetGameObjectData());
            }
            var table = CustomAssetEditorManager.Instance.Table;
            foreach (var data in table.Datas)
            {
                var goData = data as CustomAssetGameObjectData;
                _goDrawer.OnGUILayout(goData.Prefab,out var newP,OnCreate);
                if (newP != null)
                {
                    //Debug.Log($"new p!{newP}");
                    goData.Prefab = newP;
                }
            }
        }

        private AssetReferenceGameObject OnCreate()
        {
            return new AssetReferenceGameObject("");
        }
    }
}