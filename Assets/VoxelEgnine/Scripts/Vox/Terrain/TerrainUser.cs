namespace Vox.Terrain
{
    public class TerrainUser
    {
        public Int3 position { get; set; }
        
        private Terrain terrain;

        public int radius = 64;
        
        public TerrainUser(Terrain terrain)
        {
            this.terrain = terrain;
        }
        
        public void Update(Int3 position)
        {
            
        }
    }
}