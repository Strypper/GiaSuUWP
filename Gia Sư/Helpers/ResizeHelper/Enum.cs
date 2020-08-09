using System;

namespace Gia_Sư.Helpers.ResizeHelper
{
    public enum AnimationAxis
    {
        X,
        Y,
        Z
    }
    public enum FlickDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    [Flags]
    public enum VisualPropertyType
    {
        None = 0,
        Opacity = 1 << 0,
        Offset = 1 << 1,
        Scale = 1 << 2,
        Size = 1 << 3,
        RotationAngleInDegrees = 1 << 4,
        All = ~0
    }
}
