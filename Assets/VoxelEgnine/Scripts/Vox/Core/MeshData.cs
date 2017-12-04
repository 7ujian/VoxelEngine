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

        public void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Color32 color)
        {
            var index = vertices.Count;
            
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            vertices.Add(v4);
            
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);
            
            triangles.Add(index+0);
            triangles.Add(index+1);
            triangles.Add(index+2);
            triangles.Add(index+0);
            triangles.Add(index+2);
            triangles.Add(index+3);
        }
    }
}