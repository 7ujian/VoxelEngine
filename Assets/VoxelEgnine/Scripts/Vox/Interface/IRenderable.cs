namespace Vox
{
    public interface IRenderable
    {
        bool isRenderDirty { get; }        
        void Render();
    }
}