using OpenTK;

namespace Secret_Hipster.Primitives
{
    public interface IPrimitive
    {
        Matrix4 TransformationMatrix { get; set; }
        Vector3[] GetVertices();
    }
}