using UnityEngine;

namespace Extensions
{
    public static class SpriteRendererExtensions
    {
        public static Vector3 GetSize(this SpriteRenderer spriteRenderer)
        {
            //size in Units
            var size = spriteRenderer.bounds.size;
            var pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
            size.y *= pixelsPerUnit;
            size.x *= pixelsPerUnit;

            return size;
        }
    }
}
