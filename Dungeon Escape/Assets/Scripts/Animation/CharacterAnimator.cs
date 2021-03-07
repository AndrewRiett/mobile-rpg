using System;
using UnityEngine;

namespace Dungeon.Animation
{
    public class CharacterAnimator : MonoBehaviour
    {
        // [SerializeField] private VFXAnimationController attackVFX;

        public static event Action<bool> OnFlip; 

        private SpriteRenderer _sprite;
        private Animator _animator;
        
        private bool _facingLeft;

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
            // attackVFX?.Play();
        }

        public void Stop()
        {
            _animator.SetFloat("horizontal", 0f);
        }

        /// <summary>
        /// Takes the last horizontal input and adjusts flipX sprite renderer value respectively.
        /// </summary>
        /// <param name="horizontalInput"></param>
        public void FlipHorizontal(float horizontalInput)
        {
            // the range is: [-1; 0) & (0; 1]
            // 0 is excluded from deliberately the range to freeze the last "lookAt" direction of idleAnimation state
            if (horizontalInput < 0f) // look left
            {
                _sprite.flipX = true;
                _facingLeft = true;
                OnFlip?.Invoke(_facingLeft);
                // attackVFX?.FlipXAxis(_facingLeft);
            }
            else if (horizontalInput > 0f) // look right
            {
                _sprite.flipX = false;
                _facingLeft = false;
                OnFlip?.Invoke(_facingLeft);
                // attackVFX?.FlipXAxis(_facingLeft);
            }
        }
    }
}