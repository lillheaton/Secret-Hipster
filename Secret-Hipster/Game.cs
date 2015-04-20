using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Secret_Hipster.Graphics;
using Secret_Hipster.OpenGLPrograms;
using Secret_Hipster.Util;
using System;
using System.Drawing;

namespace Secret_Hipster
{
    public class Game : GameWindow
    {
        private Camera camera;
        private Spritebatch spritebatch;
        private QuadHandler quadHandler;

        // Settings, load textures, sounds
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            VSync = VSyncMode.On;
            GL.Viewport(0, 0, Width, Height);

            camera = new Camera(Width, Height);
            spritebatch = new Spritebatch(camera);
            quadHandler = new QuadHandler();
            quadHandler.AddCube();
            quadHandler.AddCube();

            quadHandler.TextureQuads[1].ScaleMatric = Matrix4.CreateScale(0.4f);
            quadHandler.TextureQuads[1].TranslationMatrix = Matrix4.CreateTranslation(-0.5f, 0, 0);
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

            quadHandler.Update(e.Time);

            if (Keyboard[Key.Escape])
            {
                Exit();
            }
        }

        // Render graphics
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);

            spritebatch.Begin<TextureProgram>();
            quadHandler.Draw(spritebatch);
            spritebatch.End();

            SwapBuffers();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.W:
                    camera.Move(0f, 0.1f, 0f);
                    break;
                case Key.A:
                    camera.Move(-0.1f, 0f, 0f);
                    break;
                case Key.S:
                    camera.Move(0f, -0.1f, 0f);
                    break;
                case Key.D:
                    camera.Move(0.1f, 0f, 0f);
                    break;
                case Key.Up:
                    camera.Move(0f, 0f, 0.1f);
                    break;
                case Key.Down:
                    camera.Move(0f, 0f, -0.1f);
                    break;
            }
        }
    }
}
