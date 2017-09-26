using System.Collections.Generic;
using UnityEngine;

namespace Vox
{
    public class MeshData
    {
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> triangles = new List<int>();
        public List<Color32> colors = new List<Color32>();

        public bool vertexColorEnabled = true;

        public void Clear()
        {
            vertices.Clear();
            triangles.Clear();
            
            if (vertexColorEnabled)
                colors.Clear();
        }
    }
}