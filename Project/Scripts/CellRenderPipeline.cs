using IcarianEngine;
using IcarianEngine.Maths;
using IcarianEngine.Rendering;
using IcarianEngine.Rendering.Lighting;
using System;

namespace Summoned
{
    public class CellRenderPipeline : RenderPipeline, IDisposable
    {
        uint               m_width;
        uint               m_height;

        MultiRenderTexture m_drawRenderTexture;
        RenderTexture      m_lightingRenderTexture;

        TextureSampler     m_colorSampler;
        TextureSampler     m_normalSampler;
        TextureSampler     m_specularSampler;
        TextureSampler     m_depthSampler;

        TextureSampler     m_lightColorSampler;

        Material           m_ambientLightMaterial;
        Material           m_directionalLightMaterial;

        Material           m_outlineMaterial;

        VertexShader       m_quadVert;

        void SetTexture(Material a_material)
        {
            a_material.SetTexture(0, m_colorSampler);
            a_material.SetTexture(1, m_normalSampler);
            a_material.SetTexture(2, m_specularSampler);
            a_material.SetTexture(3, m_depthSampler);
        }

        public CellRenderPipeline()
        {
            m_width = 1280;
            m_height = 720;

            m_drawRenderTexture = new MultiRenderTexture(3, m_width, m_height, true, true);
            m_lightingRenderTexture = new RenderTexture(m_width, m_height, false, true);

            m_colorSampler = TextureSampler.GenerateRenderTextureSampler(m_drawRenderTexture, 0);
            m_normalSampler = TextureSampler.GenerateRenderTextureSampler(m_drawRenderTexture, 1);
            m_specularSampler = TextureSampler.GenerateRenderTextureSampler(m_drawRenderTexture, 2);
            m_depthSampler = TextureSampler.GenerateRenderTextureDepthSampler(m_drawRenderTexture);

            m_lightColorSampler = TextureSampler.GenerateRenderTextureSampler(m_lightingRenderTexture);

            m_quadVert = VertexShader.LoadVertexShader("[INTERNAL]Quad");

            PixelShader ambientPix = AssetLibrary.LoadPixelShader("Shaders/RenderPipeline/AmbientLight.fpix");
            PixelShader directionalPix = AssetLibrary.LoadPixelShader("Shaders/RenderPipeline/DirectionalLight.fpix");

            MaterialBuilder ambientLightBuilder = new MaterialBuilder()
            {
                VertexShader = m_quadVert,
                PixelShader = ambientPix,
                PrimitiveMode = PrimitiveMode.TriangleStrip,
                EnableColorBlending = true,
                ShaderInputs = new ShaderBufferInput[]
                {
                    new ShaderBufferInput()
                    {
                        Slot = 0,
                        BufferType = ShaderBufferType.Texture,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 0
                    },
                    new ShaderBufferInput()
                    {
                        Slot = 1,
                        BufferType = ShaderBufferType.Texture,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 0
                    },
                    new ShaderBufferInput()
                    {
                        Slot = 2,
                        BufferType = ShaderBufferType.Texture,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 0
                    },
                    new ShaderBufferInput()
                    {
                        Slot = 3,
                        BufferType = ShaderBufferType.Texture,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 0
                    },
                    new ShaderBufferInput()
                    {
                        Slot = 5,
                        BufferType = ShaderBufferType.AmbientLightBuffer,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 1
                    }
                }  
            };

            MaterialBuilder directionalLightBuilder = new MaterialBuilder()
            {
                VertexShader = m_quadVert,
                PixelShader = directionalPix,
                PrimitiveMode = PrimitiveMode.TriangleStrip,
                EnableColorBlending = true,
                ShaderInputs = new ShaderBufferInput[]
                {
                    new ShaderBufferInput()
                    {
                        Slot = 0,
                        BufferType = ShaderBufferType.Texture,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 0
                    },
                    new ShaderBufferInput()
                    {
                        Slot = 1,
                        BufferType = ShaderBufferType.Texture,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 0
                    },
                    new ShaderBufferInput()
                    {
                        Slot = 2,
                        BufferType = ShaderBufferType.Texture,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 0
                    },
                    new ShaderBufferInput()
                    {
                        Slot = 3,
                        BufferType = ShaderBufferType.Texture,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 0
                    },
                    new ShaderBufferInput()
                    {
                        Slot = 5,
                        BufferType = ShaderBufferType.CameraBuffer,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 1
                    },
                    new ShaderBufferInput()
                    {
                        Slot = 6,
                        BufferType = ShaderBufferType.SSDirectionalLightBuffer,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 2
                    }
                }
            };

            m_ambientLightMaterial = Material.CreateMaterial(ambientLightBuilder);
            m_directionalLightMaterial = Material.CreateMaterial(directionalLightBuilder);

            SetTexture(m_ambientLightMaterial);
            SetTexture(m_directionalLightMaterial);

            PixelShader outlinePix = AssetLibrary.LoadPixelShader("Shaders/RenderPipeline/Outline.fpix");

            MaterialBuilder outlineBuilder = new MaterialBuilder()
            {
                VertexShader = m_quadVert,
                PixelShader = outlinePix,
                PrimitiveMode = PrimitiveMode.TriangleStrip,
                EnableColorBlending = false,
                ShaderInputs = new ShaderBufferInput[]
                {
                    new ShaderBufferInput()
                    {
                        Slot = 0,
                        BufferType = ShaderBufferType.Texture,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 0
                    },
                    new ShaderBufferInput()
                    {
                        Slot = 1,
                        BufferType = ShaderBufferType.Texture,
                        ShaderSlot = ShaderSlot.Pixel,
                        Set = 0
                    }
                }
            };

            m_outlineMaterial = Material.CreateMaterial(outlineBuilder);

            m_outlineMaterial.SetTexture(0, m_lightColorSampler);
            m_outlineMaterial.SetTexture(1, m_depthSampler);
        }

        public override void Resize(uint a_width, uint a_height)
        {
            if (a_width == m_width && a_height == m_height)
            {
                return;
            }

            m_width = a_width;
            m_height = a_height;

            m_drawRenderTexture.Resize(m_width, m_height);
            m_lightingRenderTexture.Resize(m_width, m_height);

            SetTexture(m_ambientLightMaterial);
            SetTexture(m_directionalLightMaterial);

            m_outlineMaterial.SetTexture(0, m_lightColorSampler);
            m_outlineMaterial.SetTexture(1, m_depthSampler);
        }

        public override void ShadowSetup(LightType a_lightType, Camera a_camera)
        {
            
        }

        public override LightShadowSplit PreShadow(Light a_light, Camera a_camera, uint a_textureSlot)
        {
            return new LightShadowSplit()
            {
                LVP = Matrix4.Identity,
                Split = 0.0f
            };
        }
        public override void PostShadow(Light a_light, Camera a_camera, uint a_textureSlot)
        {
            
        }

        public override void PreRender(Camera a_camera)
        {
            RenderCommand.BindRenderTexture(m_drawRenderTexture);
        }
        public override void PostRender(Camera a_camera)
        {
            
        }

        public override void LightSetup(Camera a_camera)
        {
            RenderCommand.BindRenderTexture(m_lightingRenderTexture);
        }
        public override LightShadowPass PreShadowLight(Light a_light, Camera a_camera)
        {
            LightShadowPass shadowPass = new LightShadowPass();

            return shadowPass;
        }
        public override void PostShadowLight(Light a_light, Camera a_camera)
        {
            
        }
        public override Material PreLight(LightType a_lightType, Camera a_camera)
        {
            switch (a_lightType)
            {
            case LightType.Ambient:
            {
                return m_ambientLightMaterial;
            }
            case LightType.Directional:
            {
                return m_directionalLightMaterial;
            }
            }

            return null;
        }
        public override void PostLight(LightType a_lightType, Camera a_camera)
        {
            
        }

        public override void PostProcess(Camera a_camera)
        {
            RenderCommand.BindRenderTexture(a_camera.RenderTexture);

            RenderCommand.BindMaterial(m_outlineMaterial);
            RenderCommand.DrawMaterial();
        }

        public void Dispose()
        {
            m_drawRenderTexture.Dispose();
            m_lightingRenderTexture.Dispose();
            
            m_colorSampler.Dispose();
            m_normalSampler.Dispose();
            m_specularSampler.Dispose();
            m_depthSampler.Dispose();

            m_lightColorSampler.Dispose();

            m_quadVert.Dispose();

            m_ambientLightMaterial.Dispose();
            m_directionalLightMaterial.Dispose();

            m_outlineMaterial.Dispose();
        }
    }
}