using Dungeon.Animation;
using UnityEngine;
using UnityEngine.Events;

namespace Dungeon.Fighting
{
    public class FightingController : MonoBehaviour
    {
        [SerializeField] private float attackDelay = 0.9f; // 0.9 sec is a min animation value
        private float attackDelayCounter = Mathf.Infinity;
        private CharacterAnimator characterAnimator;

        public UnityEvent onAttack;

        private void Awake()
        {
            characterAnimator = GetComponent<CharacterAnimator>();
        }

        private void Update()
        {
            UpdateTimer();
        }

        public void Attack(bool shouldAttack)
        {
            if (shouldAttack && CanAttack())
            {
                characterAnimator.AnimateAttack();
                onAttack.Invoke();

                attackDelayCounter = 0f;
            }
        }

        private bool CanAttack()
        {
            if (attackDelayCounter < attackDelay)
                return false;

            return true;
        }

        private void UpdateTimer()
        {
            attackDelayCounter += Time.deltaTime;
        }
    }
}