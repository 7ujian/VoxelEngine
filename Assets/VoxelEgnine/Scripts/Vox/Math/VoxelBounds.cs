namespace Vox
{
    public struct VoxelBounds
    {
        public Int3 min;

        public Int3 max;

        public Int3 size
        {
            get { return max - min; }
        }

        public void Intersect(Int3 position)
        {
            if (position.x < min.x)            
                min.x = position.x;            
            else if (position.x > max.x)            
                max.x = position.x;            

            if (position.y < min.y)            
                min.y = position.y;            
            else if (position.y > max.y)            
                max.y = position.y;
            
            if (position.z < min.z)            
                min.z = position.z;            
            else if (position.z > max.z)            
                max.z = position.z;
        }
    }
}