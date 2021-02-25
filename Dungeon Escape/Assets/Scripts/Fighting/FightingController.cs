using Dungeon.Animation;
using UnityEngine;
using UnityEngine.Events;

namespace Dungeon.Fighting
{
    public class FightingController : MonoBehaviour
    {
        [SerializeField] private float attackDelay = 0.9f; // 0.9 sec is a min animation value
        private float _attackDelayCounter = Mathf.Infinity;
        private CharacterAnimator _characterAnimator;
        
        public UnityEvent onAttack;

        private void Awake()
        {
            _characterAnimator = GetComponent<CharacterAnimator>();
        }

        private void Update()
        {
            UpdateTimer();
        }

        public void Attack(bool shouldAttack)
        {
            if (shouldAttack && CanAttack())
            {
                _characterAnimator.AnimateAttack();
                onAttack.Invoke();

                _attackDelayCounter = 0f;
            }
        }

        private bool CanAttack()
        {
            if (_attackDelayCounter < attackDelay)
                return false;

            return true;
        }

        private void UpdateTimer()
        {
            _attackDelayCounter += Time.deltaTime;
        }
    }
}