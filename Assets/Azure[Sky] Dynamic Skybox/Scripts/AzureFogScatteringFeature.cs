using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AzureFogScatteringFeature : ScriptableRendererFeature
{
    private class AzureFogScatteringPass : ScriptableRenderPass
    {
        public readonly Material blitMaterial = null;
        public FilterMode filterMode { get; set; }

        private RenderTargetIdentifier m_source { get; set; }
        private RenderTargetHandle m_destination { get; set; }
        private RenderTargetHandle m_temporaryColorTexture;
        private readonly string m_profilerTag;
        
        private Camera m_camera;
        private Vector3[] m_frustumCorners = new Vector3[4];
        private Transform m_cameraTransform = null;
        private Rect m_rect = new Rect(0, 0, 1, 1);
        private Matrix4x4 m_frustumCornersArray;
        private Vector3 m_bottomLeft, m_topLeft, m_bottomRight;
        
        public AzureFogScatteringPass(RenderPassEvent renderPassEvent, Material blitMaterial, string tag)
        {
            this.renderPassEvent = renderPassEvent;
            this.blitMaterial = blitMaterial;
            m_profilerTag = tag;
            m_temporaryColorTexture.Init("_TemporaryColorTexture");
        }
        
        public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
        {
            m_source = source;
            m_destination = destination;
        }
        
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(m_profilerTag);
            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0;
            
            m_camera = renderingData.cameraData.camera;
            m_cameraTransform = m_camera.transform;
        
            m_camera.CalculateFrustumCorners(m_rect, m_camera.farClipPlane, m_camera.stereoActiveEye, m_frustumCorners);
            m_frustumCornersArray = Matrix4x4.identity;
            m_frustumCornersArray.SetRow(0, m_cameraTransform.TransformVector(m_frustumCorners[0]));  // bottom left
            m_frustumCornersArray.SetRow(2, m_cameraTransform.TransformVector(m_frustumCorners[1]));  // top left
            m_frustumCornersArray.SetRow(3, m_cameraTransform.TransformVector(m_frustumCorners[2]));  // top right
            m_frustumCornersArray.SetRow(1, m_cameraTransform.TransformVector(m_frustumCorners[3]));  // bottom right
            blitMaterial.SetMatrix("_FrustumCorners", m_frustumCornersArray);

            // Can't read and write to same color target, create a temp render target to blit. 
            if (m_destination == RenderTargetHandle.CameraTarget)
            {
                cmd.GetTemporaryRT(m_temporaryColorTexture.id, opaqueDesc, filterMode);
                Blit(cmd, m_source, m_temporaryColorTexture.Identifier(), blitMaterial, 0);
                Blit(cmd, m_temporaryColorTexture.Identifier(), m_source);
            }
            else
            {
                Blit(cmd, m_source, m_destination.Identifier(), blitMaterial, 0);
            }
            
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
        
        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (m_destination == RenderTargetHandle.CameraTarget)
                cmd.ReleaseTemporaryRT(m_temporaryColorTexture.id);
        }
    }
    
    public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingSkybox;
    public Material blitMaterial = null;
    private AzureFogScatteringPass m_fogScatteringPass;

    public override void Create()
    {
        m_fogScatteringPass = new AzureFogScatteringPass(renderPassEvent, blitMaterial, name);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (blitMaterial == null) return;
        
        var src = renderer.cameraColorTarget;
        var dest = RenderTargetHandle.CameraTarget;
        
        m_fogScatteringPass.Setup(src, dest);
        renderer.EnqueuePass(m_fogScatteringPass);
    }
}