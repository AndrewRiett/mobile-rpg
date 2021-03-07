using System;
using UnityEngine;

namespace Dungeon.Fighting
{
    public class FightingAI : FightingController
    {
        [SerializeField] protected float attackRange = 1f;
        
        public void Attack(GameObject target) 
        {
            if (CanAttack(target))
                Hit(true);
        }
        
        public bool CanAttack(GameObject target)
        {
            if (!IsInRange(target))
                return false;

            return true;
        }
        
        public bool IsInRange(GameObject target)
        {
            if (target is null) return false;
            
            // Debug.Log($"{Mathf.Abs(distance)}, result: {Mathf.Abs(distance) < attackRange}");
            float distance = (target.transform.position.x - _collider.bounds.center.x);
            return Mathf.Abs(distance) < attackRange;
        }

        private void OnDrawGizmosSelected()
        {
            Vector2 center = GetComponent<Collider2D>().bounds.center;
            Vector2 size = new Vector2(attackRange, GetComponent<Collider2D>().bounds.size.y);
            
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(center, size);
        }
    }
}