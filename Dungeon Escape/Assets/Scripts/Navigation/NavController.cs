using System;
using UnityEngine;
using Dungeon.Movement;
using Dungeon.Navigation.Waypoints;

namespace Dungeon.Navigation
{
    public class NavController : MonoBehaviour
    {
        // TODO: 1) add walls check (ray)
        // 2) add ground to walk check (ray)
        // 3) implement movement without PatrolPath
        // 4) make a custom editor for choosing either free movement or PatrolPath
        
        [Header("Patrol path:")]
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float toleranceDistance = 0.1f;
        [SerializeField] private float waypointIdleTime = 5f;

        public float WaypointIdleTime => waypointIdleTime;
        private Vector2 CurrentPos => transform.position;

        private MovementController _movement;
        private Vector2 _direction;
        private float _horizontal;
        
        private int _currentWaypointIndex = -1; // is computed in IdleState 
        private float _timer;

        private void Awake()
        {
            _movement = GetComponent<MovementController>();
        }

        public void MoveTo(Vector2 destination, float speed)
        {
            _horizontal = CalculateHorizontal(destination);
            _movement.Move(_horizontal, speed);
        }

        private float CalculateHorizontal(Vector2 nextPos)
        {
            _direction = (nextPos - CurrentPos).normalized;
            _horizontal = _direction.x > 0 ? 1 : -1;
            
            return _horizontal;
        }

        public bool IsAtPosition(Vector2 nextPos)
        {
            // For Y axis:
            // float distanceToWaypoint = Vector2.Distance(currentPos, nextPos);
            
            float horizontalDistance = Mathf.Abs(CurrentPos.x - nextPos.x); 
            return horizontalDistance < toleranceDistance;
        }

        public Vector2 GetWaypointPos()
        {
            return patrolPath.GetWaypointPos();
        }

        public void SetNextWaypoint()
        {
            _currentWaypointIndex = patrolPath.CalculateNextIndex(_currentWaypointIndex);
        }

        public bool ShouldPatrolAfter(float time)
        {
            if (_timer >= time)
            {
                _timer = 0f;
                return true;
            }

            _timer += Time.deltaTime;
            return false;
        }

        public void Stop()
        {
            _horizontal = 0f;
            _movement.Stop();
            
        }

        public bool HasPatrolPath()
        {
            return patrolPath;
        }
    }
}