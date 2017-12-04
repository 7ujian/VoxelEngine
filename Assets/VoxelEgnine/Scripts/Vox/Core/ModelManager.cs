using System.Collections.Generic;
using UnityEngine;

namespace Vox
{
    public class ModelManager
    {
        // TODO: @jian 先静态将模型都注册在这里，以后应当动态加载和释放
        private Dictionary<byte, GameObject> prefabRegistry = new Dictionary<byte, GameObject>();

        public void RegisterPrefabs(ModelEntry[] entries)
        {
            foreach (var entry in entries)
            {
                RegisterPrefab((byte)entry.blockId, entry.modelPrefab);
            }
        }
        
        public void RegisterPrefab(byte blockId, GameObject prefab)
        {
            prefabRegistry[blockId] = prefab;
        }

        public GameObject GetPrefab(byte blockId)
        {
            GameObject prefab;
            if (!prefabRegistry.TryGetValue(blockId, out prefab))
                return null;
            
            return prefab;
        }
        
        
        public GameObject GetModel(byte blockId)
        {
            // TODO: @jian，以后这里做对象池

            var prefab = GetPrefab(blockId);
            if (prefab == null)
            {
                Debug.Log("Missing model prefab " + blockId);
                return null;
            }

            return GameObject.Instantiate(prefab);
        }
    }
}