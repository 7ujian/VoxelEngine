using UnityEngine;

namespace Vox.Render
{
    public class VolumeBuildTask
    {
        public MeshData meshData = new MeshData();
        public IVolume volume;
        public VoxelEngineContext context;
        
        public void SetVolume(IVolume volume, VoxelEngineContext context)
        {
            this.volume = volume;
            this.context = context;            
        }

        public Mesh ToMesh(Mesh mesh = null)
        {
            if (mesh == null)
                mesh = new Mesh();            
            
            mesh.SetVertices(meshData.vertices);
            mesh.SetTriangles(meshData.triangles, 0);
            mesh.RecalculateNormals();
            if (meshData.vertexColorEnabled)
                mesh.SetColors(meshData.colors);
            // TODO: @jian Collision用的话需要allow access，否则就是true
            mesh.UploadMeshData(false);            

            return mesh;
        }

        public void Clear()
        {
            meshData.Clear();
            volume = null;
            context = null;            
        }
    }
}