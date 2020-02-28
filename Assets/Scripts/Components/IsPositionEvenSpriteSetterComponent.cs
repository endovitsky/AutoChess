using Extensions;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class IsPositionEvenSpriteSetterComponent : MonoBehaviour
    {
        [SerializeField]
        public Sprite _evenSprite;
        [SerializeField]
        public Sprite _oddSprite;

        private void Start()
        {
            var spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

            var x = (int) this.gameObject.transform.localPosition.x + 1;
            var y = (int) this.gameObject.transform.localPosition.y + 1;
            if (x.IsEven() ||
                y.IsEven())
            {
                spriteRenderer.sprite = _evenSprite;
            }
            else
            {
                spriteRenderer.sprite = _oddSprite;
            }

            if (x.IsEven() &&
                y.IsEven())
            {
                spriteRenderer.sprite = _oddSprite;
            }
        }
    }
}
