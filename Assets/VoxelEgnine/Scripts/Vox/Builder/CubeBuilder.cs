using UnityEngine;
using System.Collections.Generic;


namespace Vox
{
    public class CubeBuilder : BlockBuilder
    {
        public CubeBuilder()
        {
        }

//        
        private Int3 neighbourPosition;
        private Vector3 vertex;        
        
        public override void Build(MeshData meshData, IVolume volume, Int3 position, byte blockId, VoxelEngineContext context)
        {
            byte neighbourId;
            BlockController neighbourController;
            int index;
            var blockManager = context.blockManager;
            var blockController = blockManager.GetController(blockId);
            var color = blockController.color;
                                    
            // Up
            neighbourPosition.x = volume.position.x + position.x;
            neighbourPosition.y = volume.position.y + position.y + 1;
            neighbourPosition.z = volume.position.z + position.z;

            neighbourId = volume.GetBlockId(ref neighbourPosition);            
            neighbourController = blockManager.GetController(neighbourId);
            
            if (!neighbourController.isSolid)
            {
                vertex.x = position.x - 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z + 0.5f;                
                meshData.vertices.Add(vertex);                
                
                vertex.x = position.x + 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x + 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x - 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z - 0.5f;
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
            neighbourPosition.x = volume.position.x + position.x;
            neighbourPosition.y = volume.position.y + position.y - 1;
            neighbourPosition.z = volume.position.z + position.z;
                        
            neighbourId = volume.GetBlockId(ref neighbourPosition);
            neighbourController = blockManager.GetController(neighbourId);

            if (!neighbourController.isSolid)
            {
                vertex.x = position.x - 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x + 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x + 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x - 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z + 0.5f;
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
            neighbourPosition.x = volume.position.x + position.x;
            neighbourPosition.y = volume.position.y + position.y;
            neighbourPosition.z = volume.position.z + position.z + 1;
            
            
            neighbourId = volume.GetBlockId(ref neighbourPosition);
            neighbourController = blockManager.GetController(neighbourId);

            if (!neighbourController.isSolid)
            {
                vertex.x = position.x + 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x - 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x - 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x + 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z + 0.5f;
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
            neighbourPosition.x = volume.position.x + position.x + 1;
            neighbourPosition.y = volume.position.y + position.y;
            neighbourPosition.z = volume.position.z + position.z;
            
            neighbourId = volume.GetBlockId(ref neighbourPosition);
            neighbourController = blockManager.GetController(neighbourId);

            if (!neighbourController.isSolid)
            {
                vertex.x = position.x + 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x + 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x + 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x + 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z - 0.5f;
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
            neighbourPosition.x = volume.position.x + position.x;
            neighbourPosition.y = volume.position.y + position.y;
            neighbourPosition.z = volume.position.z + position.z - 1;
            
            neighbourId = volume.GetBlockId(ref neighbourPosition);
            neighbourController = blockManager.GetController(neighbourId);

            if (!neighbourController.isSolid)
            {
                vertex.x = position.x - 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x + 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x + 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x - 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z - 0.5f;
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
            neighbourPosition.x = volume.position.x + position.x - 1;
            neighbourPosition.y = volume.position.y + position.y;
            neighbourPosition.z = volume.position.z + position.z;
            
            neighbourId = volume.GetBlockId(ref neighbourPosition);
            neighbourController = blockManager.GetController(neighbourId);

            if (!neighbourController.isSolid)
            {
                vertex.x = position.x - 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z + 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x - 0.5f;
                vertex.y = position.y + 0.5f;
                vertex.z = position.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x - 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z - 0.5f;                
                meshData.vertices.Add(vertex);
                
                vertex.x = position.x - 0.5f;
                vertex.y = position.y - 0.5f;
                vertex.z = position.z + 0.5f;
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
