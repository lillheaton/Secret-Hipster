using OpenTK.Graphics.OpenGL;
using Secret_Hipster.Graphics;
using Secret_Hipster.Primitives;
using System.Collections.Generic;
using System.Drawing;

namespace Secret_Hipster.Util
{
    public class QuadHandler
    {
        public List<TextureQuad> TextureQuads { get; private set; }
        private int texture;

        private int vertexBuffer;
        private int texturePointBuffer;

        public QuadHandler()
        {
            this.Init();
        }

        private void Init()
        {
            this.TextureQuads = new List<TextureQuad>();
            this.CreateTexture();

            GL.GenBuffers(1, out vertexBuffer);
            GL.GenBuffers(1, out texturePointBuffer);
        }

        private void CreateTexture()
        {
            var bitmap = new Bitmap("Textures/Containers/container001-green.png");
            Spritebatch.GenerateTexture(bitmap, out this.texture);
        }
        
        public void AddCube()
        {
            this.TextureQuads.Add(new TextureQuad(this.texture));
        }

        public void Update(double time)
        {
            foreach (var textureQuad in TextureQuads)
            {
                textureQuad.Update(time);
            }
        }

        public void Draw(Spritebatch spritebatch)
        {
            for (int i = 0; i < TextureQuads.Count; i++)
            {
                TextureQuads[i].Draw(spritebatch);
            }
        }
    }
}
