using UnityEngine;

namespace Dungeon.Controller
{
    public class AnimationController : MonoBehaviour
    {
        private Animator animator;
        private SpriteRenderer sprite;

        private void Awake()
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponentInChildren<Animator>();
        }

        public void AnimateMovement(float horizontalInput)
        {
            animator.SetFloat("Horizontal", horizontalInput);

            FlipHorizontal(horizontalInput);
        }

        /// <summary>
        /// Takes the last horizontal input and adjusts flipX sprite renderer value respectively.
        /// </summary>
        /// <param name="horizontalInput"></param>
        private void FlipHorizontal(float horizontalInput)
        {
            // note: the range is: [-1; 0) & (0; 1]
            // 0 is excluded from the range to freeze the last "lookAt" direction of idleAnimation state
            if (horizontalInput < 0f) // look left
                sprite.flipX = true;
            else if (horizontalInput > 0f) // look right
                sprite.flipX = false;
        }
    }
}