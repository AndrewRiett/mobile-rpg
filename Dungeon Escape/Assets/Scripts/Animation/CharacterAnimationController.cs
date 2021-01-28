using UnityEngine;

namespace Dungeon.Animation
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField] private VFXAnimationController attackVFX;

        private bool facingLeft;

        private SpriteRenderer sprite;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            sprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void AnimateMovement(float horizontalInput, bool isGrounded, bool shouldJump)
        {
            animator.SetFloat("horizontal", horizontalInput);
            animator.SetBool("isGrounded", isGrounded);
            animator.SetBool("shouldJump", shouldJump);

            FlipHorizontal(horizontalInput, sprite);
        }

        public void AnimateAttack()
        {
            animator.SetTrigger("shouldAttack");

            if (attackVFX != null)
            {
                attackVFX.FlipXAxis(facingLeft);
                attackVFX.Play();
            }
        }

        /// <summary>
        /// Takes the last horizontal input and adjusts flipX sprite renderer value respectively.
        /// </summary>
        /// <param name="horizontalInput"></param>
        protected void FlipHorizontal(float horizontalInput, SpriteRenderer sprite)
        {
            // note: the range is: [-1; 0) & (0; 1]
            // 0 is excluded from deliberately the range to freeze the last "lookAt" direction of idleAnimation state
            if (horizontalInput < 0f) // look left
            {
                sprite.flipX = true;
                facingLeft = true;
            }
            else if (horizontalInput > 0f) // look right
            {
                sprite.flipX = false;
                facingLeft = false;
            }
        }
    }
}