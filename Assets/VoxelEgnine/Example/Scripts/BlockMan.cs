using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MessagePack;
using MessagePack.Resolvers;
using UnityEditor.VersionControl;
using UnityEngine;
using Vox;
using Vox.Render;
using FileMode = System.IO.FileMode;

public class BlockMan : MonoBehaviour
{
    public ToolBar toolBar;
    public BlueprintListView blueprintList;

//	public VolumeRenderer volumeRenerer;
    public VolumeAccessorProxy volumeAccessorProxy;
    
//    public ChunkedVolumeRenderer chunkedVolumeRenderer;
//	private LargeVolume volume;

    public GameObject cursor;

    public Vector2 clickedPosition;
    public bool isClicked = false;

    private VoxelEngineContext context;
    public Item itemInHand;

    public Item[] items;
    private Camera camera;
    public Camera uiCamera;
    public GameObject player;
    public ModelEntry[] modelPrefabs;

    void Start()
    {
        VoxelEngineContext.Default.modelManager.RegisterPrefabs(modelPrefabs);
        var volumeAccesor = VolumeFactory.Instance.CreateLargeVolume();
        
        volumeAccesor.volume = new LargeVolume();
        volumeAccesor.transform.position = new Vector3(0.5f, 0.5f, 0.5f);
                
        volumeAccessorProxy.volumeAccessor = volumeAccesor;

        toolBar.items = items;
        camera = uiCamera;

        context = VoxelEngineContext.Default;

        MessagePack.Resolvers.CompositeResolver.RegisterAndSetAsDefault(
            // enable extension packages first
            MessagePack.Unity.UnityResolver.Instance,

            // finaly use standard(default) resolver
            StandardResolver.Instance);

        blueprintList.onCellClick.AddListener(OnBlueprintCellClick);
        LoadBlueprints();
    }


    void Update()
    {
        // TODO @jian 这里猥琐一下，以后统一走输入控制
        if (Input.GetButtonUp("Fire1"))
        {
            isClicked = true;
            clickedPosition = Input.mousePosition;
        }
        
        // 移动选择
        Vector3 touchPosition;
        
        

        if (isClicked)
            touchPosition = clickedPosition;
        else if (Input.touchCount <= 1)
            touchPosition = Input.mousePosition;
        else
            touchPosition = Input.touches[0].position;

        var ray = camera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // TODO: @jian 正确处理BlockHand，不要每次都New
            var itemId = (itemInHand != null) ? itemInHand.id : BlockId.Hand;
            var itemController = context.blockManager.GetController((byte) itemInHand.id);

            var volumeAccessor = FindVolumeRenderer(hit.collider.gameObject);
            if (volumeAccessor != null && volumeAccessor.volume != null)
            {
                var volume = volumeAccessor.volume;

                var localPos = hit.point - volumeAccessor.transform.position;
                // TODO: @jian 下面这里要整理出operator
                var targetLocalPos = (localPos - hit.normal * 0.5f);
                var targetPos = new Int3(
                    Mathf.RoundToInt(targetLocalPos.x),
                    Mathf.RoundToInt(targetLocalPos.y),
                    Mathf.RoundToInt(targetLocalPos.z)
                );

                var targetBlockId = volume.GetBlockId(ref targetPos);
                var targetBlockController = context.blockManager.GetController(targetBlockId);
                
                    
                if (itemController.canDig || itemController.canUseOnBlock || targetBlockController.canUseOnScene)
                    localPos -= hit.normal * 0.5f;
                else
                    localPos += hit.normal * 0.5f;

                
                
                Int3 blockPos = new Int3(
                    Mathf.RoundToInt(localPos.x),
                    Mathf.RoundToInt(localPos.y),
                    Mathf.RoundToInt(localPos.z)
                );

                if (cursor != null)
                {
                    cursor.transform.position = (Vector3) blockPos + volumeAccessor.transform.position;
                }

                if (isClicked)
                {
                    if (volume is Volume)
                    {
                        var largeVolume = VolumeFactory.Instance.CreateLargeVolume();
                        largeVolume.volume = new LargeVolume();
                        volume.CopyTo(Int3.Zero, volume.size, largeVolume.volume, Int3.Zero);
                        
                        volume = largeVolume.volume;
                        GameObject.Destroy(volumeAccessor.gameObject);
                    }
                    if (itemController.canDig)
                    {
                        volume.SetBlock(ref blockPos, Block.Air);
                    }
                    else if (itemController.canUseOnBlock)
                    {
                        var blockController = context.blockManager.GetController((byte) itemInHand.id);
                        blockController.UseOnBlock(volume, ref blockPos);
                    }
                    // TODO: @jian 手持物品在场景上使用，和 场景上可以互动的物品被点击，这两个接口要分开
                    else if (targetBlockController.canUseOnScene)
                    {
                        targetBlockController.UseOnScene(volume, ref blockPos);
                    }
                    else
                    {                        
                        // TODO: @jian 这里要给物品配置blockId，将物品与block分离
                        if (itemInHand == null)
                        {
                            Debug.LogError("Item in Hand is Null");
                        }

                        var blockController = context.blockManager.GetController((byte) itemInHand.id);
                        var block = new Block((byte) itemInHand.id);
                        blockController.BeforePlace(volume, ref blockPos, ref block);
                        volume.SetBlock(ref blockPos, block);
                        blockController.AfterPlace(volume, ref blockPos, ref block);
                        //					cursor.GetComponent<Cursor>().FadeOutAndIn();
                    }
                }
            }
        }


        if (isClicked)
            ClearClickEvent();
    }

    public void OnClicked(Vector2 position)
    {
        clickedPosition = position;
        isClicked = true;
    }

    public void ClearClickEvent()
    {
        isClicked = false;
    }


    public void Save()
    {
//		Debug.Log("Saving " + Application.persistentDataPath + "/volume1.bin");
//		var bytes = MessagePackSerializer.Serialize(volume);
//		File.WriteAllBytes(Application.persistentDataPath + "/volume1.bin", bytes);
    }

    public void Load()
    {
//		Debug.Log("Loading " + Application.persistentDataPath + "/volume1.bin");
//		
//		var fs = File.Open(Application.persistentDataPath + "/volume1.bin", FileMode.Open);
//		volume = MessagePackSerializer.Deserialize<LargeVolume>(fs);
//		fs.Close();
//
//		chunkedVolumeRenderer.volume = volume;
//		chunkedVolumeRenderer.GetComponent<VolumeBehaviour>().volume = volume;
    }

    public void TogglePlayer()
    {
        if (player.gameObject.activeSelf)
        {
            player.gameObject.SetActive(false);
            camera = uiCamera;
            uiCamera.enabled = true;
        }
        else
        {
            player.gameObject.SetActive(true);
            camera = player.GetComponentInChildren<Camera>();
            uiCamera.enabled = false;
        }
    }

    private void LoadBlueprints()
    {
        var blueprintPath = Application.persistentDataPath + "/blueprints";
        var blueprints = Directory.GetFiles(blueprintPath, "*.blueprint")
            .Select(file => System.IO.Path.GetFileNameWithoutExtension(file)).ToList();
        blueprintList.items = blueprints;
    }

    private void OnBlueprintCellClick(string filename)
    {
        var filepath = Application.persistentDataPath + "/blueprints/" + filename + ".blueprint";


        var fs = File.Open(filepath, FileMode.Open);
        var volume = MessagePackSerializer.Deserialize<Volume>(fs);
        fs.Close();

        VolumeFactory.Instance.CreateSimpleVolume().volume = volume;
    }

    public void SaveBlueprint(Volume volume, string filename)
    {
        var filepath = Application.persistentDataPath + "/blueprints/" + filename + ".blueprint";
        Debug.Log("Saving " + filepath);

        var bytes = MessagePackSerializer.Serialize(volume);
        File.WriteAllBytes(filepath, bytes);
        
        // TODO: @jian 自动Reload，这里耦合很厉害
        LoadBlueprints();
    }

    private VolumeAccessor FindVolumeRenderer(GameObject go)
    {
        var volumeAccesor = go.GetComponentInParent<VolumeAccessor>();
        if ( volumeAccesor == null)
        {
            var proxy = go.GetComponentInParent<VolumeAccessorProxy>();
            if (proxy != null)
                volumeAccesor = proxy.volumeAccessor;
        }
        // TODO: 临时hack 一下应对Chunk和Volume的local、global坐标不一致的情况
        else if (volumeAccesor.volume is Chunk)
            return volumeAccesor.transform.parent.GetComponentInParent<VolumeAccessor>();

        return volumeAccesor;
    }
}