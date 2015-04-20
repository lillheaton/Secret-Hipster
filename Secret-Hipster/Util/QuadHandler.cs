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
        private int greenTexture;
        private int blueTexture;

        public QuadHandler()
        {
            this.Init();
        }

        private void Init()
        {
            this.TextureQuads = new List<TextureQuad>();
            this.CreateTexture();
        }

        private void CreateTexture()
        {
            var bitmap = new Bitmap("Textures/Containers/container001-blue.png");
            Spritebatch.GenerateTexture(bitmap, out this.blueTexture);

            var bitmap2 = new Bitmap("Textures/Containers/container001-green.png");
            Spritebatch.GenerateTexture(bitmap2, out this.greenTexture);
        }
        
        public void AddCube()
        {
            if (TextureQuads.Count % 2 == 0)
            {
                this.TextureQuads.Add(new TextureQuad(this.blueTexture));    
            }
            else
            {
                this.TextureQuads.Add(new TextureQuad(this.greenTexture));
            }    
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
