using OpenTK;
using OpenTK.Graphics.OpenGL;
using Secret_Hipster.Graphics;

namespace Secret_Hipster.Primitives
{
    public class TextureQuad : IDrawable
    {
        private static Buffer verticesBuffer;
        private static Buffer texturePointBuffer;
        public static Buffer VerticesBuffer { get { return verticesBuffer ?? (verticesBuffer = new Buffer(Graphics.Primitives.CubeVertices)); } }
        public static Buffer TexturePointBuffer { get { return texturePointBuffer ?? (texturePointBuffer = new Buffer(Graphics.Primitives.CubeTexturePoints)); } }

        public Vector3 Position { get; set; }        
        public int Texture { get; private set; }

        private Matrix4 modelMatrix;
        private float rotation;

        public TextureQuad(int texture)
        {
            this.Position = new Vector3(0, 0, 0);
            this.Texture = texture;
        }

        public void Update(double time)
        {
            rotation += (float)time;
            this.modelMatrix = Matrix4.CreateTranslation(0, 0, 1f) * Matrix4.CreateRotationY(rotation) * Matrix4.CreateTranslation(Position);
        }

        public void Draw(Spritebatch spritebatch)
        {
            // Matrix transformation
            Matrix4 transformationMatrix = modelMatrix * spritebatch.Camera.GetViewProjectionMatrix();
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