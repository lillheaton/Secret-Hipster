using OpenTK;
using OpenTK.Graphics.OpenGL;
using Secret_Hipster.Graphics;

namespace Secret_Hipster.Primitives
{
    public class TextureQuad
    {
        private static Buffer verticesBuffer;
        private static Buffer texturePointBuffer;
        public static Buffer VerticesBuffer { get { return verticesBuffer ?? (verticesBuffer = new Buffer(Graphics.Primitives.CubeVertices)); } }
        public static Buffer TexturePointBuffer { get { return texturePointBuffer ?? (texturePointBuffer = new Buffer(Graphics.Primitives.CubeTexturePoints)); } }

        public Matrix4 TranslationMatrix { get; set; }
        public Matrix4 ScaleMatric { get; set; }
        //public Matrix4 RotationMatrix { get; set; }

        public int Texture { get; private set; }

        private Matrix4 modelMatrix;
        private float rotation;

        public TextureQuad(int texture)
        {
            this.TranslationMatrix = Matrix4.CreateTranslation(0, 0, 1f);
            this.ScaleMatric = Matrix4.CreateScale(0.5f);
            //this.RotationMatrix = Matrix4.Identity;

            this.Texture = texture;
        }

        public void Update(double time)
        {
            rotation += (float)time;
            this.modelMatrix = TranslationMatrix * ScaleMatric * Matrix4.CreateRotationY(rotation);
        }

        public void Draw(Spritebatch spritebatch)
        {
            // Matrix transformation
            Matrix4 transformationMatrix = modelMatrix;
            GL.UniformMatrix4(spritebatch.TextureProgram.ModelViewUniform, false, ref transformationMatrix);

            // Use texture
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, this.Texture);
            GL.Uniform1(spritebatch.TextureProgram.TextureUniform, (int)TextureUnit.Texture0);

            // Attribute pointers
            GL.BindBuffer(BufferTarget.ArrayBuffer, VerticesBuffer.MemoryLocation);
            GL.VertexAttribPointer(spritebatch.TextureProgram.PositionAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TexturePointBuffer.MemoryLocation);
            GL.VertexAttribPointer(spritebatch.TextureProgram.TexturePointsAttribute, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.DrawArrays(PrimitiveType.Quads, 0, VerticesBuffer.Length);
        }
    }
}