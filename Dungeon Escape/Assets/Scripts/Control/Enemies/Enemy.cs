using UnityEngine;
using Dungeon.Movement.Waypoints;

namespace Dungeon.Control.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float speed;
        [SerializeField] protected int gemsAmount;

        [Header("Patrol path:")]
        [SerializeField] protected PatrolPath patrolPath;
        [SerializeField] protected float toleranceDistance = 0.1f;

        public PatrolPath PatrolPath { get => patrolPath; }
        public float ToleranceDistance { get => toleranceDistance; }
        internal int CurrentWaypointID { get; set; }

        protected virtual void Attack() { }
        protected virtual bool CheckForAggro()
        {
            return true;
        }
    }
}