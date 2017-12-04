using UnityEngine;
using System.Collections.Generic;
using Vox.Render;


namespace Vox
{
    public class CubeBuilder : BlockBuilder
    {
        public CubeBuilder()
        {
        }

//        
//        private Int3 ;
//        private Vector3 ;        
        
        public override void Build(MeshData meshData, IVolume volume, Int3 position, byte blockId, VoxelEngineContext context)
        {
            byte neighbourId;
            BlockController neighbourController;
            int index;
            var blockManager = context.blockManager;
            var blockController = blockManager.GetController(blockId);
            var color = blockController.color;

            var localPosition = position - volume.position;

            Int3 neighbourPosition;
            Vector3 vertex;
            // Up
            neighbourPosition.x = position.x;
            neighbourPosition.y = position.y + 1;
            neighbourPosition.z = position.z;

            neighbourId = volume.GetBlockId(ref neighbourPosition);            
            neighbourController = blockManager.GetController(neighbourId);
            
            if (!neighbourController.isSolid)
            {
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z + 0.5f;                
                meshData.vertices.Add(vertex);                
                
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z - 0.5f;
                meshData.vertices.Add(vertex);
                
                index = meshData.vertices.Count;
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 3);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 1);
                
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
            }
            
            // Down
            neighbourPosition.x = position.x;
            neighbourPosition.y = position.y - 1;
            neighbourPosition.z = position.z;
                        
            neighbourId = volume.GetBlockId(ref neighbourPosition);
            neighbourController = blockManager.GetController(neighbourId);

            if (!neighbourController.isSolid)
            {
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z + 0.5f;
                meshData.vertices.Add(vertex);
                
                index = meshData.vertices.Count;
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 3);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 1);
                
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
            }

            // North
            neighbourPosition.x = position.x;
            neighbourPosition.y = position.y;
            neighbourPosition.z = position.z + 1;
            
            
            neighbourId = volume.GetBlockId(ref neighbourPosition);
            neighbourController = blockManager.GetController(neighbourId);

            if (!neighbourController.isSolid)
            {
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z + 0.5f;
                meshData.vertices.Add(vertex);
                
                index = meshData.vertices.Count;
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 3);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 1);
                
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
            }
            
            // East
            neighbourPosition.x = position.x + 1;
            neighbourPosition.y = position.y;
            neighbourPosition.z = position.z;
            
            neighbourId = volume.GetBlockId(ref neighbourPosition);
            neighbourController = blockManager.GetController(neighbourId);

            if (!neighbourController.isSolid)
            {
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z - 0.5f;
                meshData.vertices.Add(vertex);
                
                index = meshData.vertices.Count;
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 3);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 1);
                
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
            }

            // South
            neighbourPosition.x = position.x;
            neighbourPosition.y = position.y;
            neighbourPosition.z = position.z - 1;
            
            neighbourId = volume.GetBlockId(ref neighbourPosition);
            neighbourController = blockManager.GetController(neighbourId);

            if (!neighbourController.isSolid)
            {
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x + 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z - 0.5f;
                meshData.vertices.Add(vertex);
                
                index = meshData.vertices.Count;
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 3);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 1);
                
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
            }
            
            // West
            neighbourPosition.x = position.x - 1;
            neighbourPosition.y = position.y;
            neighbourPosition.z = position.z;
            
            neighbourId = volume.GetBlockId(ref neighbourPosition);
            neighbourController = blockManager.GetController(neighbourId);

            if (!neighbourController.isSolid)
            {
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y + 0.5f;
                vertex.z = localPosition.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = localPosition.x - 0.5f;
                vertex.y = localPosition.y - 0.5f;
                vertex.z = localPosition.z + 0.5f;
                meshData.vertices.Add(vertex);
                
                index = meshData.vertices.Count;
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 3);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 4);
                meshData.triangles.Add(index - 2);
                meshData.triangles.Add(index - 1);
                
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
                meshData.colors.Add(color);
            }
        }
    }
}
