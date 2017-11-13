using System;
using UnityEngine;
using System.Collections;
using MessagePack;

namespace Vox
{
	[MessagePackObject]
	public struct Int3 :IEquatable<Int3> {
		
		public static Int3 Zero = new Int3(0,0,0);
		public static Int3 Infinity = new Int3(Int32.MaxValue, Int32.MaxValue, Int32.MaxValue);
		
		[Key(0)]
		public int x;
		[Key(1)]
		public int y;
		[Key(2)]
		public int z;

		public Int3(int x = 0, int y = 0, int z = 0)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		public static Int3 operator +(Int3 c1, Int3 c2)
		{
			return new Int3(c1.x + c2.x, c1.y + c2.y, c1.z + c2.z);
		}

		public static Int3 operator -(Int3 c1, Int3 c2)
		{
			return new Int3(c1.x - c2.x, c1.y - c2.y, c1.z - c2.z);
		}
		
		public static Int3 operator -(Int3 c)
		{
			return new Int3(-c.x, -c.y, -c.z);
		}
		
		public static bool operator ==(Int3 c1, Int3 c2) 
		{
			return c1.Equals(c2);
		}
		
		public static bool operator !=(Int3 c1, Int3 c2) 
		{
			return !c1.Equals(c2);
		}

		public bool Equals(Int3 other)
		{
			return x == other.x && y == other.y && z == other.z;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Int3 && Equals((Int3) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = x;
				hashCode = (hashCode * 397) ^ y;
				hashCode = (hashCode * 397) ^ z;
				return hashCode;
			}
		}

		public override string ToString()
		{
			return x + "," + y + "," + z;
		}
		
		public static explicit operator Int3(Vector3 v)
		{
			return new Int3(
				Mathf.FloorToInt(v.x), 
				Mathf.FloorToInt(v.y), 
				Mathf.FloorToInt(v.z)
			);
		}
		
		public static explicit operator Vector3(Int3 v)
		{
			return new Vector3(
				(float)v.x,
				(float)v.y,
				(float)v.z
			);
		}
	}
}