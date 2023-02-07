using System;
using UnityEngine.AddressableAssets;

namespace Yaojz
{
    [Serializable]
    public class CustomAssetGameObjectData:CustomAssetDataBase
    {
        public AssetReferenceGameObject Prefab;
    }
}