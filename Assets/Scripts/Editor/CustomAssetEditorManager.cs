using UnityEditor;
using UnityEngine;

namespace Yaojz
{
    public class CustomAssetEditorManager
    {
        public const string RESOURCE_TABLE_PATH = "Assets/_Resources/CustomAssetDataTable.asset";
        
        private static CustomAssetEditorManager _instance;

        private CustomAssetDataTable _table;
        
        public static CustomAssetEditorManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomAssetEditorManager();
                }
                return _instance;
            }
        }
        
        public CustomAssetDataTable Table
        {
            get
            {
                _table =
                    AssetDatabase.LoadAssetAtPath<CustomAssetDataTable>(RESOURCE_TABLE_PATH);
                if (_table == null)
                {
                    _table = ScriptableObject.CreateInstance<CustomAssetDataTable>();
                    AssetDatabase.CreateAsset(_table, RESOURCE_TABLE_PATH);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
                return _table;
            }
        }

        public void AddAsset(CustomAssetDataBase data)
        {
            // if (Table.Datas.ContainsKey(data.GetGUID()))
            // {
            //     return;
            // }
            //Table.Datas.Add(data.GetGUID(),data);
            Table.Datas.Add(data);
            EditorUtility.SetDirty(_table);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}