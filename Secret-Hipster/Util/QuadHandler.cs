using OpenTK;
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
            this.CreateCubes();
        }

        private void CreateTexture()
        {
            var bitmap = new Bitmap("Textures/Containers/container001-blue.png");
            Spritebatch.GenerateTexture(bitmap, out this.blueTexture);

            var bitmap2 = new Bitmap("Textures/Containers/container001-green.png");
            Spritebatch.GenerateTexture(bitmap2, out this.greenTexture);
        }

        private void CreateCubes()
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    TextureQuad cube;

                    if (i % 2 == 0)
                    {
                        cube = new TextureQuad(this.blueTexture);
                        cube.Position = new Vector3(3 * i, 0, 3 * j);
                    }
                    else
                    {
                        cube = new TextureQuad(this.greenTexture);
                        cube.Position = new Vector3(3 * i, 0, 3 * j);
                    }

                    this.TextureQuads.Add(cube);
                }
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
