using System;
using UnityEngine;

namespace Vox
{
    public class DirtyFlagGenerator
    {
        
            
        public int[] Generate()
        {
            var masks = new int[64];
            
            for (var i = 0; i < 64; i++)
            {
                var duwsen = i;
                var mask = duwsen;
                
                var n = duwsen & 0x1;
                var e = (duwsen >> 1) & 0x1;
                var s = (duwsen >> 2) & 0x1;
                var w = (duwsen >> 3) & 0x1;
                var u = (duwsen >> 4) & 0x1;
                var d = (duwsen >> 5) & 0x1;

                var wsen = duwsen & 0xF;
                
                var ne_es_sw_wn = (n & e) |
                                  ((e & s) << 1) |
                                  ((s & w) << 2) |
                                  ((w & n) << 3);
                
                mask |= ne_es_sw_wn << 6;

                if (u != 0)
                {
                    mask |= wsen << 10;
                    mask |= ne_es_sw_wn << 18;
                }
                
                if (d != 0)
                {
                    mask |= wsen << 14;
                    mask |= ne_es_sw_wn << 22;
                }

                masks[i] = mask;
                
            }

            return masks;
        }
    }


}