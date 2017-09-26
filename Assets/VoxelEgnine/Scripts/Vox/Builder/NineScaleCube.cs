using UnityEngine;
using System.Collections;

public static class NineScaleCube {
//    public Vector3 size = Vector3.one;
//    public Vector3 pivot = Vector3.zero;
//
//    private Vector3 _size;
//    private Vector3 _pivot;

//    private static float cutout = 0.4f;
//    private float _cutout;

    //定义顶点数组， 
//    private static Vector3[] vertices;

    public static Mesh GenerateMesh(Vector3 pivot, Vector3 size, float edge)
    {
        Mesh mesh = new Mesh ();
        Vector3[] vertices = {
            //front
            new Vector3(0, 0, 0),
            new Vector3(edge, 0, 0),
            new Vector3(size.x-edge, 0, 0),
            new Vector3(size.x, 0, 0),
            new Vector3(size.x, edge, 0),
            new Vector3(size.x, size.y-edge, 0),
            new Vector3(size.x, size.y, 0),
            new Vector3(size.x-edge, size.y, 0),
            new Vector3(edge, size.y, 0),
            new Vector3(0, size.y, 0),
            new Vector3(0, size.y-edge, 0),
            new Vector3(0, edge, 0),
            new Vector3(edge, edge, 0),
            new Vector3(size.x-edge, edge, 0),
            new Vector3(size.x-edge, size.y-edge, 0),
            new Vector3(edge, size.y-edge, 0),
            //up
            new Vector3(0, size.y, 0),
            new Vector3(edge,size.y, 0),
            new Vector3(size.x-edge,size.y, 0),
            new Vector3(size.x, size.y, 0),
            new Vector3(size.x, size.y, edge),
            new Vector3(size.x, size.y, size.z-edge),
            new Vector3(size.x, size.y, size.z),
            new Vector3(size.x-edge, size.y, size.z),
            new Vector3(edge, size.y, size.z),
            new Vector3(0, size.y, size.z),
            new Vector3(0, size.y, size.z-edge),
            new Vector3(0, size.y, edge),
            new Vector3(edge, size.y, edge),
            new Vector3(size.x-edge, size.y, edge),
            new Vector3(size.x-edge, size.y, size.z-edge),
            new Vector3(edge, size.y, size.z-edge),
            //back
            new Vector3(size.x, 0, size.z),
            new Vector3(size.x-edge, 0, size.z),
            new Vector3(edge, 0, size.z),
            new Vector3(0, 0, size.z),

            new Vector3(0, edge, size.z),
            new Vector3(0, size.y-edge, size.z),
            new Vector3(0, size.y, size.z),
            new Vector3(edge, size.y, size.z),

            new Vector3(size.x-edge, size.y, size.z),
            new Vector3(size.x, size.y, size.z),
            new Vector3(size.x, size.y-edge, size.z),
            new Vector3(size.x, edge, size.z),

            new Vector3(size.x-edge, edge, size.z),
            new Vector3(edge, edge, size.z),
            new Vector3(edge, size.y-edge, size.z),
            new Vector3(size.x-edge, size.y-edge, size.z),
            //bottom
            new Vector3(0, 0, size.z),
            new Vector3(edge, 0, size.z),
            new Vector3(size.x-edge, 0, size.z),
            new Vector3(size.x, 0, size.z),
            new Vector3(size.x, 0, size.z-edge),
            new Vector3(size.x, 0, edge),
            new Vector3(size.x,0, 0),
            new Vector3(size.x-edge, 0, 0),
            new Vector3(edge, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, edge),
            new Vector3(0, 0, size.z-edge),
            new Vector3(edge, 0, size.z-edge),
            new Vector3(size.x-edge, 0, size.z-edge),
            new Vector3(size.x-edge, 0, edge),
            new Vector3(edge, 0, edge),
            //left
            new Vector3(0, 0, size.z),
            new Vector3(0, 0, size.z-edge),
            new Vector3(0, 0, edge),
            new Vector3(0, 0, 0),
            new Vector3(0, edge, 0),
            new Vector3(0, size.y-edge, 0),
            new Vector3(0, size.y, 0),
            new Vector3(0, size.y, edge),
            new Vector3(0, size.y, size.z-edge),
            new Vector3(0, size.y, size.z),
            new Vector3(0, size.y-edge, size.z),
            new Vector3(0, edge, size.z),
            new Vector3(0, edge, size.z-edge),
            new Vector3(0, edge, edge), 
            new Vector3(0, size.y-edge, edge),
            new Vector3(0, size.y-edge, size.z-edge),
            //right
            new Vector3(size.x, 0, 0),
            new Vector3(size.x, 0, edge),
            new Vector3(size.x, 0, size.z-edge),
            new Vector3(size.x, 0, size.z),
            new Vector3(size.x, edge, size.z),
            new Vector3(size.x, size.y-edge, size.z),
            new Vector3(size.x, size.y, size.z),
            new Vector3(size.x, size.y, size.z-edge),
            new Vector3(size.x, size.y, edge),
            new Vector3(size.x, size.y, 0),
            new Vector3(size.x, size.y-edge, 0),
            new Vector3(size.x, edge, 0),
            new Vector3(size.x, edge, edge),
            new Vector3(size.x, edge, size.z-edge),
            new Vector3(size.x, size.y-edge, size.z-edge),
            new Vector3(size.x, size.y-edge, edge),
        };

        //pivot
        CorrectPivot (ref vertices, pivot);
        //vertices
        mesh.vertices = vertices;
        //triangles
        mesh.triangles = GetTriangles();
        //uv
        mesh.uv = GetUV(edge);
        //normals
        mesh.normals = GetNormals(vertices);
        return mesh;
    }
    public static void CorrectPivot(ref Vector3[] vert, Vector3 p)
    {
        for (int i = 0; i < vert.Length; i++) {
            vert [i].x -= p.x;
            vert [i].y -= p.y;
            vert [i].z -= p.z;
        }
    }

    public static int[] GetTriangles()
    {
        int[] triangles = {
            //front
            0, 11, 12,
            0, 12, 1,
            1, 12, 13, 
            1, 13, 2,
            2, 13, 4,
            2, 4, 3,

            11, 10, 15,
            11, 15, 12,
            12, 15, 14,
            12, 14, 13,
            13, 14, 5,
            13, 5, 4,

            10, 9, 8,
            10, 8, 15,
            15, 8, 7, 
            15, 7, 14,
            14, 7, 6,
            14, 6, 5,

            //up
            16, 27, 28,
            16, 28, 17, 
            17, 28, 29,
            17, 29, 18,
            18, 29, 20,
            18, 20, 19,

            27, 26, 31,
            27, 31, 28,
            28, 31, 30,
            28, 30, 29,
            29, 30, 21,
            29, 21, 20,

            26, 25, 24,
            26, 24, 31,
            31, 24, 23,
            31, 23, 30,
            30, 23, 22,
            30, 22, 21,
            //back
            32, 43, 44,
            32, 44, 33,
            33, 44, 45, 
            33, 45, 34,
            34, 45, 36,
            34, 36, 35,

            43, 42, 47, 
            43, 47, 44,
            44, 47, 46, 
            44, 46, 45,
            45, 46, 37,
            45, 37, 36,

            42, 41, 40,
            42, 40, 47, 
            47, 40, 39,
            47, 39, 46,
            46, 39, 38,
            46, 38, 37,
            //bottom
            48, 59, 60,
            48, 60, 49,
            49, 60, 61,
            49, 61, 50,
            50, 61, 52,
            50, 52, 51,
            59, 58, 63,
            59, 63, 60,
            60, 63, 62,
            60, 62, 61,
            61, 62, 53,
            61, 53, 52,
            58, 57, 56,
            58, 56, 63,
            63, 56, 55,
            63, 55, 62,
            62, 55, 54,
            62, 54, 53,
            //left
            64, 75, 76,
            64, 76, 65,
            65, 76, 77,
            65, 77, 66,
            66, 77, 68, 
            66, 68, 67,
            75, 74, 79,
            75, 79, 76,
            76, 79, 78,
            76, 78, 77,
            77, 78, 69,
            77, 69, 68,
            74, 73, 72,
            74, 72, 79,
            79, 72, 71,
            79, 71, 78,
            78, 71, 70,
            78, 70, 69,
            //right
            80, 91, 92,
            80, 92, 81,
            81, 92, 93,
            81, 93, 82,
            82, 93, 84,
            82, 84, 83,
            91, 90, 95, 
            91, 95, 92,
            92, 95, 94,
            92, 94, 93,
            93, 94, 85,
            93, 85, 84,
            90, 89, 88,
            90, 88, 95,
            95, 88, 87,
            95, 87, 94,
            94, 87, 86, 
            94, 86, 85

        };
        return triangles;
    }
    public static Vector2[] GetUV(float edge)
    {
        Vector2[] uv = {
            //front
            new Vector2(0, 1),
            new Vector2(edge, 1),
            new Vector2(1-edge, 1),
            new Vector2(1, 1),

            new Vector2(1, 1-edge),
            new Vector2(1, edge),
            new Vector2(1, 0),
            new Vector2(1-edge, 0),

            new Vector2(edge, 0),
            new Vector2(0, 0),
            new Vector2(0, edge),
            new Vector2(0, 1-edge),

            new Vector2(edge, 1-edge),
            new Vector2(1-edge, 1-edge),
            new Vector2(1-edge, edge),
            new Vector2(edge, edge),
            //up
            new Vector2(0,1),
            new Vector2(edge, 1),
            new Vector2(1-edge, 1),
            new Vector2(1, 1),

            new Vector2(1, 1-edge),
            new Vector2(1, edge),
            new Vector2(1, 0),
            new Vector2(1-edge, 0),
            new Vector2(edge, 0),
            new Vector2(0, 0),
            new Vector2(0, edge),
            new Vector2(0, 1-edge),

            new Vector2(edge, 1-edge),
            new Vector2(1-edge, 1-edge),
            new Vector2(1-edge, edge),
            new Vector2(edge, edge),
            //back
            new Vector2(0,1),
            new Vector2(edge, 1),
            new Vector2(1-edge, 1),
            new Vector2(1, 1),

            new Vector2(1, 1-edge),
            new Vector2(1, edge),
            new Vector2(1, 0),
            new Vector2(1-edge, 0),

            new Vector2(edge, 0),
            new Vector2(0, 0),
            new Vector2(0, edge),
            new Vector2(0, 1-edge),

            new Vector2(edge, 1-edge),
            new Vector2(1-edge, 1-edge),
            new Vector2(1-edge, edge),
            new Vector2(edge, edge),
            //bottom
            new Vector2(0,1),
            new Vector2(edge, 1),
            new Vector2(1-edge, 1),
            new Vector2(1, 1),

            new Vector2(1, 1-edge),
            new Vector2(1, edge),
            new Vector2(1, 0),
            new Vector2(1-edge, 0),

            new Vector2(edge, 0),
            new Vector2(0, 0),
            new Vector2(0, edge),
            new Vector2(0, 1-edge),

            new Vector2(edge, 1-edge),
            new Vector2(1-edge, 1-edge),
            new Vector2(1-edge, edge),
            new Vector2(edge, edge),

            //left
            new Vector2(0,1),
            new Vector2(edge, 1),
            new Vector2(1-edge, 1),
            new Vector2(1, 1),

            new Vector2(1, 1-edge),
            new Vector2(1, edge),
            new Vector2(1, 0),
            new Vector2(1-edge, 0),

            new Vector2(edge, 0),
            new Vector2(0, 0),
            new Vector2(0, edge),
            new Vector2(0, 1-edge),

            new Vector2(edge, 1-edge),
            new Vector2(1-edge, 1-edge),
            new Vector2(1-edge, edge),
            new Vector2(edge, edge),
            //right
            new Vector2(0,1),
            new Vector2(edge, 1),
            new Vector2(1-edge, 1),
            new Vector2(1, 1),

            new Vector2(1, 1-edge),
            new Vector2(1, edge),
            new Vector2(1, 0),
            new Vector2(1-edge, 0),

            new Vector2(edge, 0),
            new Vector2(0, 0),
            new Vector2(0, edge),
            new Vector2(0, 1-edge),

            new Vector2(edge, 1-edge),
            new Vector2(1-edge, 1-edge),
            new Vector2(1-edge, edge),
            new Vector2(edge, edge)

        };
        return uv;
    }
    public static Vector3[] GetNormals(Vector3[] vertices)
    {
        //normals
        Vector3 front =  (vertices[80]-vertices[81] ).normalized;
        Vector3 up =     (vertices[89]-vertices[90] ).normalized;
        Vector3 back =   (vertices[83]-vertices[82] ).normalized;
        Vector3 bottom = (vertices[80]-vertices[91] ).normalized;
        Vector3 left =   (vertices[0]-vertices[1]   ).normalized;
        Vector3 right =  (vertices[3]-vertices[2]   ).normalized;

        Vector3[] normals = new Vector3[96];
        for (int i = 0; i < vertices.Length; i++) {
            switch (i/16) {
                case 0:
                    normals [i] = front;
                    break;
                case 1:
                    normals [i] = up;
                    break;
                case 2:
                    normals [i] = back;
                    break;
                case 3:
                    normals [i] = bottom;
                    break;
                case 4:
                    normals [i] = left;
                    break;
                case 5:
                    normals [i] = right;
                    break;
                default:
                    break;
            }
        }
        return normals;
    }

}
