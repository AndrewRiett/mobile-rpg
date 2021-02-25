using Dungeon.Enemies;
using UnityEngine;

namespace Dungeon.Fighting
{
    public class AggroCollider : MonoBehaviour
    {
        // private ITarget player;
        // private ITarget characterToInteract;

        private EnemyAI enemy;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;

            Debug.Log("set target");
            enemy = other.GetComponent<EnemyAI>();
            enemy.SetTarget(gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;

            enemy = other.GetComponent<EnemyAI>();
            enemy.RemoveTarget();
        }
    }
}