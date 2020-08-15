using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Experiemntal.Rendering.Universal
{
    public class CameraBlit : ScriptableRendererFeature
    {
        class CameraBlitPass : ScriptableRenderPass
        {
            private Material _material { get; set; }
            private RenderTargetIdentifier _source { get; set; }
            private RenderTargetIdentifier _destination { get; set; }

            public CameraBlitPass(Material material)
            {
                this._material = material;
            }

            public void Setup(RenderTargetIdentifier source, RenderTargetIdentifier destination)
            {
                this._source = source;
                this._destination = destination;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                CommandBuffer cmd = CommandBufferPool.Get();
                Blit(cmd, _source, _destination, _material);
                cmd.SetRenderTarget(_destination);
                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
            }
        }

        [System.Serializable]
        public class CameraBlitSettings
        {
            public RenderPassEvent _passEvent = RenderPassEvent.AfterRendering;
            public Material _material = null;
        }

        public CameraBlitSettings settings = new CameraBlitSettings();

        CameraBlitPass blitPass;

        public override void Create()
        {
            blitPass = new CameraBlitPass(settings._material);
            blitPass.renderPassEvent = settings._passEvent;
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            var src = renderer.cameraColorTarget;
            var dest = RenderTargetHandle.CameraTarget.Identifier();
            if (settings._material == null)
            {
                Debug.LogWarningFormat("Missing Blit Material. {0} blit pass will not execute. Check for missing reference in the assigned renderer.", GetType().Name);
                return;
            }
            blitPass.Setup(src, dest);
            renderer.EnqueuePass(blitPass);
        }
    }
}