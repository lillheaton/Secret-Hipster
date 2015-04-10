using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Secret_Hipster.Graphics;
using Secret_Hipster.Primitives;

using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Secret_Hipster
{
    public class Game : GameWindow
    {
        private PrimitiveBatch primitiveBatch;
        private TextureQuad textureQuad;


        //private int programId;
        //private int vsId;
        //private int fsId;

        //private int attribute_vcol;
        //private int attribute_vpos;
        //private int uniform_mview;

        //private int vbo_position;
        //private int vbo_color;
        //private int vbo_mview;

        //private Vector3[] vertdata;
        //private Vector2[] coldata;
        //private Matrix4[] mviewdata;

        public Game()
        {
        }

        //private void LoadShaders(string shader, ShaderType type, int program, out int address)
        //{
        //    address = GL.CreateShader(type);
        //    using (var sr = new StreamReader(string.Format("Shaders/{0}.glsl", shader)))
        //    {
        //        GL.ShaderSource(address, sr.ReadToEnd());
        //    }
        //    GL.CompileShader(address);
        //    GL.AttachShader(program, address);

        //    // Compilation information LOG
        //    Console.WriteLine(GL.GetShaderInfoLog(address));
        //}

        //private void ShaderAttributeMapping()
        //{
        //    attribute_vpos = GL.GetAttribLocation(programId, "vPosition");
        //    attribute_vcol = GL.GetAttribLocation(programId, "vColor");
        //    uniform_mview = GL.GetUniformLocation(programId, "modelview");

        //    if (attribute_vpos == -1 ||
        //        attribute_vcol == -1 || 
        //        uniform_mview == -1)
        //    {
        //        Console.WriteLine("Error binding attributes");
        //    }
        //}

        //private void CreateBuffers()
        //{
        //    GL.GenBuffers(1, out vbo_position);
        //    GL.GenBuffers(1, out vbo_color);
        //    GL.GenBuffers(1, out vbo_mview);
        //}

        // Settings, load textures, sounds
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            VSync = VSyncMode.On;
            GL.Viewport(0, 0, Width, Height);



            //programId = GL.CreateProgram();
            //this.LoadShaders("fs", ShaderType.FragmentShader, programId, out fsId);
            //this.LoadShaders("vs", ShaderType.VertexShader, programId, out vsId);

            //// ===============================

            //// Load bitmap
            //var bmpTextures = new Bitmap("Textures/Containers/container001-green.png");

            //// Create texture
            //var textureId = GL.GenTexture();
            //GL.BindTexture(TextureTarget.Texture2D, textureId);

            //BitmapData data = bmpTextures.LockBits(
            //    new Rectangle(0, 0, bmpTextures.Width, bmpTextures.Height),
            //    ImageLockMode.ReadOnly,
            //    PixelFormat.Format32bppArgb);

            //GL.TexImage2D(
            //    TextureTarget.Texture2D,
            //    0,
            //    PixelInternalFormat.Rgba,
            //    data.Width,
            //    data.Height,
            //    0,
            //    OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
            //    PixelType.UnsignedByte,
            //    data.Scan0);

            //bmpTextures.UnlockBits(data);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            //// Bind texture
            //GL.ActiveTexture(TextureUnit.Texture0);
            //GL.BindTexture(TextureTarget.Texture2D, textureId);
            //GL.Uniform1(GL.GetUniformLocation(programId, "texUnit"), TextureUnit.Texture0 - TextureUnit.Texture0);


            //// ===============================

            //GL.LinkProgram(programId);
            //this.ShaderAttributeMapping();
            //this.CreateBuffers();

            ////vertdata = new Vector3[] { 
            ////    new Vector3(-1f, -1f, 0f),
            ////    new Vector3( 1f, -1f, 0f),
            ////    new Vector3( 0f,  1f, -1f),

            ////    new Vector3( 1f,  -1f, 0f),
            ////    new Vector3( 1f,  -1f, -2f),
            ////    new Vector3( 0f,  1f, -1f),

            ////    new Vector3( -1f,  -1f, 0f),
            ////    new Vector3( -1f,  -1f, -2f),
            ////    new Vector3( 0f,  1f, -1f),

            ////    new Vector3(-1f, -1f, -2f),
            ////    new Vector3( 1f, -1f, -2f),
            ////    new Vector3( 0f,  1f, -1f),
            ////};

            //vertdata = new Vector3[] { 
            //    new Vector3(-1f, 1f, 0f), // Top Left
            //    new Vector3( 1f, 1f, 0f), // Top Right
            //    new Vector3( 1f,  -1f, 0f), // Bottom right
            //    new Vector3( -1f,  -1f, 0f), // bottom left

            //    new Vector3(-1f, 1f, -2f), // Top Left
            //    new Vector3( 1f, 1f, -2f), // Top Right
            //    new Vector3( 1f,  -1f, -2f), // Bottom right
            //    new Vector3( -1f,  -1f, -2f), // bottom left

            //    new Vector3( -1f,  1f, 0f),
            //    new Vector3( -1f,  -1f, 0f),
            //    new Vector3( -1f,  -1f, -2f),
            //    new Vector3( -1f,  1f, -2f),

            //    new Vector3(1f, 1f, 0f),
            //    new Vector3( 1f, -1f, 0f),
            //    new Vector3(1f, -1f, -2f),
            //    new Vector3( 1f, 1f, -2f),
            //};


            //coldata = new Vector2[] { 
            //    new Vector2(0f, 0f),
            //    new Vector2( 1f, 0f),
            //    new Vector2( 1f,  1f),
            //    new Vector2( 0f,  1f),

            //    new Vector2(0f, 0f),
            //    new Vector2( 1f, 0f),
            //    new Vector2( 1f,  1f),
            //    new Vector2( 0f,  1f),

            //    new Vector2(0f, 0f),
            //    new Vector2( 0f, 1f),
            //    new Vector2( 1f,  1f),
            //    new Vector2( 1f,  0f),

            //    new Vector2(0f, 0f),
            //    new Vector2( 0f, 1f),
            //    new Vector2( 1f,  1f),
            //    new Vector2( 1f,  0f),
            //};


            //mviewdata = new Matrix4[]{
            //    Matrix4.CreateScale(0.5f),
            //    Matrix4.CreateRotationY(30)
            //};


            //GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            //GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);

            //GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
            //GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector2.SizeInBytes), coldata, BufferUsageHint.StaticDraw);


            primitiveBatch = new PrimitiveBatch(PrimitiveBatchType.WithTextures);
            textureQuad = new TextureQuad(new Bitmap("Textures/Containers/container001-green.png"));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.ClearColor(Color.CornflowerBlue);
        }

        // Game logic, input handling
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
            {
                Exit();
            }
        }

        // Render graphics

        private float cool = 0;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);

            primitiveBatch.Begin(PrimitiveType.Quads);

            textureQuad.TransformationMatrix = Matrix4.CreateTranslation(0f, 0f, 1f) * Matrix4.CreateScale(0.5f) * Matrix4.CreateRotationY(cool += 0.01f);
            primitiveBatch.AddTexturePrimitive(textureQuad);

            primitiveBatch.End();


            //GL.Enable(EnableCap.CullFace);

            //GL.UseProgram(programId);
            //GL.EnableVertexAttribArray(attribute_vpos);
            //GL.EnableVertexAttribArray(attribute_vcol);



            //GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            //GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

            //GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
            //GL.VertexAttribPointer(attribute_vcol, 2, VertexAttribPointerType.Float, false, 0, 0);

            //var test = Matrix4.CreateTranslation(0f, 0f, 1f) * Matrix4.CreateScale(0.5f) * Matrix4.CreateRotationY(cool += 0.01f);            
            ////var test = Matrix4.CreateScale(0.5f) * Matrix4.CreateRotationY(90);
            //GL.UniformMatrix4(uniform_mview, false, ref test);   



            //GL.DrawArrays(PrimitiveType.Quads, 0, 4 * 4);

            //GL.DisableVertexAttribArray(attribute_vpos);
            //GL.DisableVertexAttribArray(attribute_vcol);

            //GL.Flush();

            SwapBuffers();
        }
    }
}
