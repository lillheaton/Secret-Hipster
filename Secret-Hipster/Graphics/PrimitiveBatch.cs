using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using Secret_Hipster.Primitives;
using System;
using System.Linq;

namespace Secret_Hipster.Graphics
{
    public enum PrimitiveBatchType
    {
        WithTextures,
        RgbColors
    }

    public class PrimitiveBatch : GraphicsBase
    {
        private bool hasBegun;
        private PrimitiveBatchType type;
        private PrimitiveType primitiveType;

        private int program;
        private int fragmentShader;
        private int vertexShader;

        private int attributeVsPosition;
        private int attributeVsColor;
        private int uniformVsModelView;

        private int vboPosition;
        private int vboColor;
        private int vboModelView;

        private int texture;

        public List<IPrimitive> Primitives { get; private set; } 
        public Vector3[] Vertices { get; private set; }
        public Vector3[] Colors { get; private set; }
        public Vector2[] TexturePoints { get; private set; }

        public PrimitiveBatch(PrimitiveBatchType type)
        {
            this.type = type;
            this.Init();
        }

        private void Init()
        {
            this.hasBegun = false;
            this.Primitives = new List<IPrimitive>();
            this.Vertices = new Vector3[0];
            this.TexturePoints = new Vector2[0];

            this.program = GL.CreateProgram();
            this.LoadShaders("fs", ShaderType.FragmentShader, this.program, out fragmentShader);
            this.LoadShaders("vs", ShaderType.VertexShader, this.program, out vertexShader);

            GL.LinkProgram(this.program);
            this.ShaderAttributeMapping();
            this.CreateBuffers();
        }

        private void CreateBuffers()
        {
            GL.GenBuffers(1, out vboPosition);
            GL.GenBuffers(1, out vboColor);
            GL.GenBuffers(1, out vboModelView);
        }

        private void ShaderAttributeMapping()
        {
            attributeVsPosition = GL.GetAttribLocation(this.program, "vPosition");
            attributeVsColor = GL.GetAttribLocation(this.program, "vColor");
            uniformVsModelView = GL.GetUniformLocation(this.program, "modelview");

#if DEBUG
            if (attributeVsPosition == -1 ||
                attributeVsColor == -1 ||
                uniformVsModelView == -1)
            {
                Console.WriteLine("Error binding attributes");
            }
#endif
        }

        private void UpdateBuffers()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboPosition);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(this.Vertices.Length * Vector3.SizeInBytes), this.Vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboColor);
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(this.TexturePoints.Length * Vector2.SizeInBytes), this.TexturePoints, BufferUsageHint.StaticDraw);
        }

        public void AddTexturePrimitive(ITexturePrimitive texturePrimitive)
        {
            Primitives.Add(texturePrimitive);
            this.Vertices = texturePrimitive.GetVertices().ToList().Concat(this.Vertices).ToArray();
            this.TexturePoints = texturePrimitive.GetTexturePoints().ToList().Concat(this.TexturePoints).ToArray();

            // Hmm this will not allow for multiple textures...
            base.GenerateTexture(texturePrimitive.Image, out texture);

            this.UpdateBuffers();
        }

        public void Begin(PrimitiveType primType)
        {
            if (hasBegun)
            {
                throw new Exception("You should not call the begin method twice");
            }
            this.primitiveType = primType;

            hasBegun = true;
        }

        public void End()
        {
            if (!hasBegun)
            {
                throw new Exception("Must call begin before you call end");
            }
            GL.UseProgram(this.program);

            if (type == PrimitiveBatchType.WithTextures)
            {
                GL.EnableVertexAttribArray(attributeVsPosition);
                GL.EnableVertexAttribArray(attributeVsColor);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vboPosition);
                GL.VertexAttribPointer(attributeVsPosition, 3, VertexAttribPointerType.Float, false, 0, 0);

                GL.BindBuffer(BufferTarget.ArrayBuffer, vboColor);
                GL.VertexAttribPointer(attributeVsColor, 2, VertexAttribPointerType.Float, false, 0, 0);
            }

            for (int i = 0; i < this.Primitives.Count; i++)
            {
                Matrix4 transformationMatrix = Primitives[i].TransformationMatrix;
                GL.UniformMatrix4(uniformVsModelView, false, ref transformationMatrix);

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, this.texture);
                GL.Uniform1(GL.GetUniformLocation(this.program, "texUnit"), (int)TextureUnit.Texture0);

                GL.DrawArrays(primitiveType, i, Primitives[i].GetVertices().Count());
            }

            GL.DisableVertexAttribArray(attributeVsPosition);
            GL.DisableVertexAttribArray(attributeVsColor);

            GL.Flush();

            hasBegun = false;
        }
    }
}
