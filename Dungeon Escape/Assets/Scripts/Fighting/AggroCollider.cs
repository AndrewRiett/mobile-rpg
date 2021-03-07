using Dungeon.Enemies;
using UnityEngine;

namespace Dungeon.Fighting
{
    public class AggroCollider : MonoBehaviour
    {
        // private ITarget player;
        // private ITarget characterToInteract;

        private EnemyAI _enemy;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;

            _enemy = other.GetComponent<EnemyAI>();
            _enemy.SetTarget(gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;

            _enemy = other.GetComponent<EnemyAI>();
            _enemy.RemoveTarget();
        }
    }
}