using System.Collections.Generic;
using UnityEngine;
using Vox.Render;

namespace Vox.Terrain
{
    public class Terrain : MonoBehaviour
    {
        class ChunkColumnNode
        {
            public ChunkColumn column;
            public int referenceCount = 0;
        }

        class TerrainUserNode
        {
            public Int3 lastUpdatePosition = Int3.Infinity;
            public TerrainUser user;
        }
        
        private LargeVolume volume;
        private TerrainGenerator generator;
        private VolumeLoader loader;
        private List<TerrainUserNode> userNodes;
        private Dictionary<int, ChunkColumnNode> chunkColumns;
        
        void Awake()
        {
            this.volume = new LargeVolume();
            this.generator = new TerrainGenerator();
            this.chunkColumns = new Dictionary<int, ChunkColumnNode>();
            this.userNodes = new List<TerrainUserNode>();
        }

        public void AddUser(TerrainUser user)
        {
            foreach (var node in userNodes)
            {
                if (node.user == user)
                    return;
            }
            
            var newNode = new TerrainUserNode();
            newNode.user = user;
            userNodes.Add(newNode);
        }

        public void UpdateUserProximities()
        {
            foreach (var pair in chunkColumns)
            {
                pair.Value.referenceCount = 0;
            }
            
            foreach (var node in userNodes)
            {                
                UpdateUserProximity(node);
            }

            var columnsToRemove = new List<ChunkColumn>();
            
            foreach (var pair in chunkColumns)
            {
                if (pair.Value.referenceCount == 0)
                {
                    columnsToRemove.Add(pair.Value.column);                    
                }
            }

            foreach (var column in columnsToRemove)
            {
                UnloadChunkColumn(column);
                chunkColumns.Remove(GetChunkColumnKey(column.position));
            }
        }

        private void UpdateUserProximity(TerrainUserNode node)
        {
            // TODO: @jian 这里使用了引用计数，会清零，所以需要每帧强制重新计算，要考虑有没有更好的算法
//            if (node.lastUpdatePosition == node.user.position)
//                return;

            node.lastUpdatePosition = node.user.position;
            
            // TODO: @jian 这里替换为螺旋矩阵算法
            
            // TODO: @jian 这里改为中心点向四周
            var position = node.user.position;
            for (var x = -node.user.radius; x < node.user.radius; x += 16)
            {
                for (var z = -node.user.radius; z < node.user.radius; z += 16)
                {
                    position.x = node.user.position.x + x;
                    position.z = node.user.position.z + z;

                    var columnNode = GetChunkColumnNode(position);
                    
                    // TODO: @jian 下面逻辑有BUG，考虑合并ChunkColumn & ChunkColumnNode
                    if (columnNode == null)
                        LoadChunkColumn(position);

                    columnNode = GetChunkColumnNode(position);
                    if (columnNode != null)
                        columnNode.referenceCount++;
                }
            }
        }

        public void LoadChunkColumn(Int3 position)
        {
            var chunkColumn = new ChunkColumn();
            // TODO: @jian 下面要封装起来             
            chunkColumn.position = new Vector3(
                unchecked (
                    (int)(position.x & Settings.ChunkPositionMask)), 
                    0, 
                    (int)(position.z & Settings.ChunkPositionMask));
            
            generator.Generate(chunkColumn);
            
            var node = new ChunkColumnNode();
            node.column = chunkColumn;

            var key = GetChunkColumnKey(position);

            chunkColumns[key] = node;
            
            // TODO: @jian 以后这段代码移到 ChunkedVolumeRenderer里面
            
            foreach (var chunk in chunkColumn.chunks)
            {                
                var chunkGO = new GameObject();
                chunk.volume = volume;
                volume.AddChunk(chunk);
                chunkGO.name = chunk.ToString();
                var meshRenderer = chunkGO.AddComponent<MeshRenderer>();
                chunkGO.AddComponent<MeshFilter>();
                var renderer = chunkGO.AddComponent<VolumeRenderer>();
                renderer.volume = chunk;
                var material = new Material(Shader.Find("Standard"));
                meshRenderer.material = material;
                chunkGO.transform.position = chunk.position;
            }
        }

        public void UnloadChunkColumn(ChunkColumn column)
        {
            foreach (var chunk in column.chunks)
            {
                if (chunk != null)
                {
                    chunk.volume = null;
                    volume.RemoveChunk(chunk.position);
                }
            }
            
            column.Destroy();
        }

        public ChunkColumn GetChunkColumn(Int3 position)
        {
            var key = GetChunkColumnKey(position);
            ChunkColumnNode node;

            if (chunkColumns.TryGetValue(key, out node))
            {
                return node.column;
            }

            return null;
        }

        private ChunkColumnNode GetChunkColumnNode(Int3 position)
        {
            var key = GetChunkColumnKey(position);
            ChunkColumnNode node;

            if (chunkColumns.TryGetValue(key, out node))
            {
                return node;
            }

            return null;
        }

        public int GetChunkColumnKey(Int3 position)
        {
            // TODO: x, z 可能为负，需要研究一下
            var x = position.x >> Settings.BlockPositionBitWidth;            
            var z = position.z >> Settings.BlockPositionBitWidth;

            return ((x + 64) << volume.bitWidthZ) | (z  +64);
        }
       
    }
}