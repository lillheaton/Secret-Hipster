using System.Drawing;

using OpenTK;

namespace Secret_Hipster.Primitives
{
    public interface ITexturePrimitive : IPrimitive
    {
        Vector2[] GetTexturePoints();
        Bitmap Image { get; }
    }
}