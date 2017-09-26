namespace Vox
{
    public class VolumeState
    {
        // 内存已分配
        public bool allocated = false;
        
        // 加载中
        public bool loading = false;
        // 已加载
        public bool loaded = false;
                
        // 渲染中
        public bool rendering = false;
        // 渲染完成
        public bool rendered = false;

        // 已删除
        public bool destroyed = false;
    }
}