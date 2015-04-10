using OpenTK;
using System.Drawing;
namespace Secret_Hipster.Primitives
{
    public class TextureQuad : ITexturePrimitive
    {
        public Matrix4 TransformationMatrix { get; set; }
        public Vector3[] Vertices { get; private set; }
        public Vector2[] TexturePoints { get; private set; }
        public Bitmap Image { get; private set; }

        public TextureQuad(Bitmap image)
        {
            this.CreateSampleCube();
            this.Image = image;
            this.TransformationMatrix = Matrix4.Identity;
        }

        public TextureQuad(Bitmap image, Vector3[] vertices, Vector2[] texturePoints)
        {
            this.Vertices = vertices;
            this.TexturePoints = texturePoints;
            this.Image = image;
            this.TransformationMatrix = Matrix4.Identity;
        }

        

        public Vector3[] GetVertices()
        {
            return Vertices;
        }

        public Vector2[] GetTexturePoints()
        {
            return TexturePoints;
        }

        private void CreateSampleCube()
        {
            // Creates a cube
            Vertices = new[]
            {
                new Vector3(-1f, 1f, 0f), // Top Left
                new Vector3(1f, 1f, 0f), // Top Right
                new Vector3(1f, -1f, 0f), // Bottom right
                new Vector3(-1f, -1f, 0f), // bottom left

                new Vector3(-1f, 1f, -2f), // Top Left
                new Vector3(1f, 1f, -2f), // Top Right
                new Vector3(1f, -1f, -2f), // Bottom right
                new Vector3(-1f, -1f, -2f), // bottom left

                new Vector3(-1f, 1f, 0f), 
                new Vector3(-1f, -1f, 0f), 
                new Vector3(-1f, -1f, -2f),
                new Vector3(-1f, 1f, -2f), 

                new Vector3(1f, 1f, 0f), 
                new Vector3(1f, -1f, 0f),
                new Vector3(1f, -1f, -2f), 
                new Vector3(1f, 1f, -2f),
            };

            TexturePoints = new[] { 
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
                new Vector2(0f, 1f),

                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
                new Vector2(0f, 1f),

                new Vector2(0f, 0f),
                new Vector2(0f, 1f),
                new Vector2(1f, 1f),
                new Vector2(1f, 0f),

                new Vector2(0f, 0f),
                new Vector2(0f, 1f),
                new Vector2(1f, 1f),
                new Vector2(1f, 0f),
            };
        }
    }
}