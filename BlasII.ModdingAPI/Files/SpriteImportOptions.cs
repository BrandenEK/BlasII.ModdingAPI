using UnityEngine;

namespace BlasII.ModdingAPI.Files
{
    public class SpriteImportOptions
    {
        public int PixelsPerUnit { get; init; } = 32;
        public bool UsePointFilter { get; init; } = true;
        public Vector2 Pivot { get; init; } = new Vector2(0.5f, 0.5f);
        public Vector4 Border { get; init; } = Vector4.zero;
        public SpriteMeshType MeshType { get; init; } = SpriteMeshType.Tight;
    }
}
