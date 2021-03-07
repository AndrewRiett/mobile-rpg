using System;
using Dungeon.Animation;
using UnityEngine;
using UnityEngine.Events;

namespace Dungeon.Fighting
{
    public class FightingController : MonoBehaviour
    {
        
        [SerializeField] protected float attackDelay = 0.9f; // 0.9 sec is a min animation value
        
        protected CharacterAnimator _characterAnimator;
        protected Collider2D _collider;
        
        public UnityEvent onAttack;
        
        private float _attackDelayCounter = Mathf.Infinity;

        private void Awake()
        {
            _characterAnimator = GetComponent<CharacterAnimator>();
            _collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            UpdateTimer();
        }
        
        // TODO: onHit() animator event
        // vfx would have a collider which checks ITarget
        // and will reduce health from the health class

        public void Hit(bool shouldHit)
        {
            if (shouldHit && IsTime())
            {
                _characterAnimator.AnimateAttack();
                onAttack.Invoke();

                _attackDelayCounter = 0f;
            }
        }

        protected bool IsTime()
        {
            return (_attackDelayCounter > attackDelay);
        }

        private void UpdateTimer()
        {
            _attackDelayCounter += Time.deltaTime;
        }

        
    }
}