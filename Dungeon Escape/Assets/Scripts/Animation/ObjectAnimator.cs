using UnityEngine;
using System.Collections;

namespace Dungeon.Animation
{
    public abstract class ObjectAnimator : MonoBehaviour
    {
        internal SpriteRenderer sprite;
        protected Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        public abstract void Animate();

        internal void FlipXAxis(bool shouldFlipX)
        {
            if (shouldFlipX)
                sprite.flipX = true;
            else
                sprite.flipX = false;
        }
        
        internal void FlipYAxis(bool shouldFlipY)
        {
            if (shouldFlipY)
                sprite.flipY = true;
            else
                sprite.flipY = false;
        }

        internal void FlipBothAxis(bool shouldFlip)
        {
            if (shouldFlip)
            {
                sprite.flipX = true;
                sprite.flipY = true;
            }
            else
            {
                sprite.flipX = false;
                sprite.flipY = false;
            }
        }
    }
}