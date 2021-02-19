using UnityEngine;
using Dungeon.Navigation.Waypoints;

namespace Dungeon.Navigation
{
    public class NavController : MonoBehaviour
    {
        private int CurrentWaypointID { get; set; }
        public float WaypointIdleTime => waypointIdleTime;

        [Header("Patrol path:")]
        [SerializeField] protected PatrolPath patrolPath;
        [SerializeField] private float toleranceDistance = 0.1f;
        [SerializeField] private float waypointIdleTime = 5f;

        [Header("Chasing:")] 
        [SerializeField] protected float suspicionTime;

        private float _timer;

        public bool IsAtWaypoint()
        {
            // For Y axis:
            // float distanceToWaypoint = Vector2.Distance(currentPos, nextPos);
            
            Vector3 currentPos = new Vector3(transform.position.x, transform.position.y);
            Vector3 nextPos = patrolPath.GetNextWaypointPos();
            float horizontalDistance = Mathf.Abs(currentPos.x - nextPos.x); 

            return horizontalDistance < toleranceDistance;
        }

        public float CalculateHorizontal()
        {
            float horizontal = 0f;

            Vector2 direction = (patrolPath.GetNextWaypointPos() - transform.position).normalized;
            horizontal = direction.x;

            return horizontal;
        }

        public void SetNextWaypoint()
        {
            CurrentWaypointID = patrolPath.CalculateNextIndex(CurrentWaypointID);
        }

        public bool ShouldPatrol()
        {
            if (_timer >= waypointIdleTime)
            {
                _timer = 0f;
                return true;
            }

            _timer += Time.deltaTime;
            return false;
        }
    }
}