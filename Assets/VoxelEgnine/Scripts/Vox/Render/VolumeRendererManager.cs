using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vox.Render
{
    // TODO: @jian 合理解决多线程情况下Context的问题
    public class VolumeRendererManager : MonoBehaviour
    {
        private static VolumeRendererManager __Instance;
        
        public static VolumeRendererManager Instance
        {
            get
            {
                if (__Instance == null)
                {
                    __Instance = FindObjectOfType<VolumeRendererManager>();
                    if (__Instance == null)
                    {
                        var go = new GameObject("VolumeRendererManager");
                        __Instance = go.AddComponent<VolumeRendererManager>();                        
                    }
                }

                return __Instance;
            }
        }

        public Material defaultVolumeMaterial;

        class Task
        {
            public VolumeBuildTask task;
            public Action onComplete;
        }

        private Queue<Task> taskQueue = new Queue<Task>();
        private float lastFrameTime = Mathf.Infinity;

        public void QueueTask(VolumeBuildTask task, System.Action onComplete)
        {            
            taskQueue.Enqueue(new Task{task = task, onComplete = onComplete});
        }

        private bool hasCpuTime
        {
            get
            {
                if (lastFrameTime > Time.realtimeSinceStartup)
                    return false;
                return Time.realtimeSinceStartup - lastFrameTime <= 0.03f;
            }
        }

        private void LateUpdate()
        {
            while (taskQueue.Count > 0 && hasCpuTime)
            {
                RunNextTask();
            }

            lastFrameTime = Time.realtimeSinceStartup;
        }

        private void RunNextTask()
        {
            Task task = taskQueue.Dequeue();
            if (task != null)
            {
                if (task.onComplete != null)
                {
                    task.onComplete();
                }
            }
        }
        
        
       
    }
}