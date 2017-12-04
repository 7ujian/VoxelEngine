namespace Vox
{
    public class DirtyFlag
    {
        public static int[] NeighboursIndex =
        {
            0x01,    // n
            0x02,    // e
            0x04,    // s
            0x08,    // w
            0x10,    // u
            0x20,    // d
            0x03,    // ne
            0x06,    // es
            0x0C,    // sw
            0x09,    // wn
            0x11,    // un
            0x12,    // ue
            0x14,    // us
            0x18,    // uw
            0x21,    // dn
            0x22,    // de
            0x24,    // dn
            0x28,    // dw
            0x13,    // une
            0x16,    // ues
            0x1C,    // usw
            0x19,    // uwn
            0x23,    // dne
            0x26,    // des
            0x2C,    // dsw
            0x29,    // dwn
        };

        public static Int3[] Neighbours =
        {
            new Int3(0, 0, 1), // n
            new Int3(1, 0, 0), // e
            new Int3(0, 0, -1), // s
            new Int3(-1, 0, 0), // w
            new Int3(0, 1, 0), // u
            new Int3(0, -1, 0), // d
            new Int3(1, 0, 1), // ne
            new Int3(1, 0, -1), // es
            new Int3(-1, 0, -1), // sw
            new Int3(-1, 0, 1), // wn
            new Int3(0, 1, 1), // un
            new Int3(1, 1, 0), // ue
            new Int3(0, 1, -1), // us
            new Int3(-1, 1, 0), // uw
            new Int3(0, -1, 1), // dn
            new Int3(1, -1, 0), // de
            new Int3(0, -1, -1), // dn
            new Int3(-1, -1, 0), // dw
            new Int3(1, 1, 1), // une
            new Int3(1, 1, -1), // ues
            new Int3(-1, 1, -1), // usw
            new Int3(-1, 1, 1), // uwn
            new Int3(1, -1, 1), // dne
            new Int3(1, -1, -1), // des
            new Int3(-1, -1, -1), // dsw
            new Int3(-1, -1, 1), // dwn
        };

        public static int[] DirtyMasks =
        {
            0x00000000,
            0x00000001,
            0x00000002,
            0x00000043,
            0x00000004,
            0x00000005,
            0x00000086,
            0x000000C7,
            0x00000008,
            0x00000209,
            0x0000000A,
            0x0000024B,
            0x0000010C,
            0x0000030D,
            0x0000018E,
            0x000003CF,
            0x00000010,
            0x00000411,
            0x00000812,
            0x00040C53,
            0x00001014,
            0x00001415,
            0x00081896,
            0x000C1CD7,
            0x00002018,
            0x00202619,
            0x0000281A,
            0x00242E5B,
            0x0010311C,
            0x0030371D,
            0x0018399E,
            0x003C3FDF,
            0x00000020,
            0x00004021,
            0x00008022,
            0x0040C063,
            0x00010024,
            0x00014025,
            0x008180A6,
            0x00C1C0E7,
            0x00020028,
            0x02024229,
            0x0002802A,
            0x0242C26B,
            0x0103012C,
            0x0303432D,
            0x018381AE,
            0x03C3C3EF,
            0x00000030,
            0x00004431,
            0x00008832,
            0x0044CC73,
        };

        public static int GetDirtyMask(bool n, bool e, bool s, bool w, bool u, bool d)
        {
            var index = 0;
            
            if (n)
                index |= 0x1;
            if (e)
                index |= 0x2;
            if (s)
                index |= 0x4;
            if (w)
                index |= 0x8;
            if (u)
                index |= 0x10;
            if (d)
                index |= 0x20;

            return DirtyMasks[index];
        }
    }
}