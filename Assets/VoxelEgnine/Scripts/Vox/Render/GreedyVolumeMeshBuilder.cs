using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace Vox.Render
{
    public class GreedyVolumeMeshBuilder : VolumeBuilder
    {
        public override void BuildMeshData(VolumeBuildTask task)
        {
            BuildGreedyMeshData(task.meshData, task.volume, task.context);
        }

        public void BuildGreedyMeshData(MeshData meshData, IVolume volume, VoxelEngineContext context)
        {
            var x = new int[3];
            var q = new int[3];
            var du = new int[3];
            var dv = new int[3];

            var dims = new[] {volume.size.x, volume.size.y, volume.size.z};

            Int3 blockPosition;
            Int3 neighbourPosition;


            //Sweep over 3-axes            
            for (var d = 0; d < 3; ++d)
            {
                //var i, j, k, l, w, h, 
                int w;
                int h;

                var u = (d + 1) % 3;
                var v = (d + 2) % 3;
                x[0] = 0;
                x[1] = 0;
                x[2] = 0;

                q[0] = 0;
                q[1] = 0;
                q[2] = 0;

                du[0] = 0;
                du[1] = 0;
                du[2] = 0;

                dv[0] = 0;
                dv[1] = 0;
                dv[2] = 0;


                var mask = new int[dims[u] * dims[v]];
                q[d] = 1;
                for (x[d] = -1; x[d] < dims[d];)
                {
                    //Compute mask
                    var n = 0;
                    for (x[v] = 0; x[v] < dims[v]; ++x[v])
                    {
                        for (x[u] = 0; x[u] < dims[u]; ++x[u])
                        {
                            blockPosition.x = volume.position.x + x[0];
                            blockPosition.y = volume.position.y + x[1];
                            blockPosition.z = volume.position.z + x[2];

                            neighbourPosition.x = volume.position.x + x[0] + q[0];
                            neighbourPosition.y = volume.position.y + x[1] + q[1];
                            neighbourPosition.z = volume.position.z + x[2] + q[2];

                            var block = volume.GetBlockId(ref blockPosition);
                            var neighbour = volume.GetBlockId(ref neighbourPosition);

                            if (block == 0 && neighbour != 0)
                                mask[n] = 0x1 << 8 | neighbour;
                            if (block != 0 && neighbour == 0)
                                mask[n] = block;

                            n++;

                            //                        mask[n++] =
                            //                            (0 <= x[d] ? volume.GetBlockId(ref blockPosition) : 0) !=
                            //                            (x[d] < dims[d] - 1 ? volume.GetBlockId(ref neighbourPosition) : 0);
                        }
                    }
                    //Increment x[d]
                    ++x[d];
                    //Generate mesh for mask using lexicographic ordering
                    n = 0;
                    for (var j = 0; j < dims[v]; ++j)
                    {
                        for (var i = 0; i < dims[u];)
                        {
                            if (mask[n] != 0)
                            {
                                //Compute width

                                for (w = 1; i + w < dims[u] && mask[n + w] == mask[n]; ++w)
                                {
                                }
                                //Compute height (this is slightly awkward
                                var done = false;
                                for (h = 1; j + h < dims[v]; ++h)
                                {
                                    for (var k = 0; k < w; ++k)
                                    {
                                        if (mask[n + k + h * dims[u]] != mask[n])
                                        {
                                            done = true;
                                            break;
                                        }
                                    }
                                    if (done)
                                    {
                                        break;
                                    }
                                }
                                //Add quad
                                x[u] = i;
                                x[v] = j;

                                du[u] = w;
                                dv[v] = h;

                                var block = (byte)(mask[n] & 0xFF);
                                var direction = (mask[n] >> 8) & 0x1;

                                var color = context.blockManager.GetController(block).color;
                                // TODO: @jian 先临时都用 一种颜色
                                if (direction == 0)
                                    meshData.AddQuad(
                                        new Vector3(x[0] - 0.5f, x[1] - 0.5f, x[2] - 0.5f),
                                        new Vector3(x[0] + du[0] - 0.5f, x[1] + du[1] - 0.5f, x[2] + du[2] - 0.5f),
                                        new Vector3(x[0] + du[0] + dv[0] - 0.5f, x[1] + du[1] + dv[1] - 0.5f, x[2] + du[2] + dv[2] - 0.5f),
                                        new Vector3(x[0] + dv[0] - 0.5f, x[1] + dv[1] - 0.5f, x[2] + dv[2] - 0.5f),
                                        color);
                                else
                                    meshData.AddQuad(
                                        new Vector3(x[0] - 0.5f, x[1] - 0.5f, x[2] - 0.5f),
                                        new Vector3(x[0] + dv[0] - 0.5f, x[1] + dv[1] - 0.5f, x[2] + dv[2] - 0.5f),
                                        new Vector3(x[0] + du[0] + dv[0] - 0.5f, x[1] + du[1] + dv[1] - 0.5f, x[2] + du[2] + dv[2] - 0.5f),
                                        new Vector3(x[0] + du[0] - 0.5f, x[1] + du[1] - 0.5f, x[2] + du[2] - 0.5f),
                                        
                                        color);
                                    
                                    
                                

                                //Zero-out mask
                                for (var l = 0; l < h; ++l)
                                {
                                    for (var k = 0; k < w; ++k)
                                    {
                                        mask[n + k + l * dims[u]] = 0;
                                    }
                                }

                                //Increment counters and continue
                                i += w;
                                n += w;
                            }
                            else
                            {
                                ++i;
                                ++n;
                            }
                        }
                    }
                }
            }
        }
    }
}