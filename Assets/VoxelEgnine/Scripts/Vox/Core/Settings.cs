using UnityEngine;
using System.Collections;

public class Settings {

	public const int BlockPositionBitWidth = 4;
	public const int BlockPositionBitWidthX2 = 8;
	public const uint BlockPositionMask = 0x0000000F;
	public const uint ChunkPositionMask = 0xFFFFFFF0;
	public const int ChunkSize = 16;
}
