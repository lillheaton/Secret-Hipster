using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace Secret_Hipster.Graphics
{
    public abstract class GraphicsBase
    {
        private const string ShaderFolder = "Shaders";

        /// <summary>
        /// Will load shader to program and compile it
        /// </summary>
        /// <param name="shader"></param>
        /// <param name="type"></param>
        /// <param name="program"></param>
        /// <param name="address"></param>
        protected void LoadShaders(string shader, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (var sr = new StreamReader(string.Format("{0}/{1}.glsl", ShaderFolder, shader)))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);

#if DEBUG 
            // Compilation information 
            Console.WriteLine(GL.GetShaderInfoLog(address));
#endif
        }

        protected void GenerateTexture(Bitmap image, out int textureId)
        {
            textureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            BitmapData data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                data.Width,
                data.Height,
                0,
                PixelFormat.Bgra,
                PixelType.UnsignedByte,
                data.Scan0);

            image.UnlockBits(data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        }
    }
}
