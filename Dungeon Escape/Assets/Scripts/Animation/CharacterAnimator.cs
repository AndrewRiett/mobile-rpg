using UnityEngine;

namespace Dungeon.Animation
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private VFXAnimationController attackVFX;

        private bool _facingLeft;

        private SpriteRenderer _sprite;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void AnimateMovement(float horizontalInput, bool isGrounded)
        {
            _animator.SetFloat("horizontal", horizontalInput);
            _animator.SetBool("isGrounded", isGrounded);
            FlipHorizontal(horizontalInput);
        }

        public void AnimateJumping()
        {
            _animator.SetTrigger("shouldJump");
            // FlipHorizontal(horizontalInput);
        }
 
        public void AnimateAttack()
        {
            _animator.SetTrigger("shouldAttack");

            if (attackVFX == null) return;
            
            attackVFX.FlipXAxis(_facingLeft);
            attackVFX.Play();
        }

        public void Stop()
        {
            _animator.SetFloat("horizontal", 0f);
        }

        /// <summary>
        /// Takes the last horizontal input and adjusts flipX sprite renderer value respectively.
        /// </summary>
        /// <param name="horizontalInput"></param>
        private void FlipHorizontal(float horizontalInput)
        {
            // note: the range is: [-1; 0) & (0; 1]
            // 0 is excluded from deliberately the range to freeze the last "lookAt" direction of idleAnimation state
            if (horizontalInput < 0f) // look left
            {
                _sprite.flipX = true;
                _facingLeft = true;
            }
            else if (horizontalInput > 0f) // look right
            {
                _sprite.flipX = false;
                _facingLeft = false;
            }
        }
    }
}