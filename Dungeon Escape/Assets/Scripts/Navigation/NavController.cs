using UnityEngine;
using Dungeon.Movement;
using Dungeon.Animation;
using Dungeon.Navigation.Waypoints;

namespace Dungeon.Navigation
{
    public class NavController : MonoBehaviour
    {
        // TODO: 1) add walls check (ray)
        // 2) add ground to the check (ray)
        // 3) implement a free movement option without PatrolPath's waypoints
        
        [Header("Patrol:")]
        [SerializeField] private float idleTime = 5f;
        
        // Called in custom editor NavControllerEditor via string references,
        // careful with changing the names:
        [HideInInspector][SerializeField] private bool usePath;
        [HideInInspector][SerializeField] private PatrolPath _patrolPath;
        [HideInInspector][SerializeField] private float _toleranceDistance;

        public float IdleTime => idleTime;
        private Vector2 CurrentPos => transform.position;

        private MovementController _movement;
        private SurfaceChecker _surfaceChecker;
        private Vector2 _direction;
        private float _horizontal;
        
        private int _currentWaypointIndex = -1; // is computed after entering IdleState via SetNextWaypoint()
        private float _timer;
        private CharacterAnimator _characterAnimator;

        private void Awake()
        {
            _movement = GetComponent<MovementController>();
            _characterAnimator = GetComponent<CharacterAnimator>();
            
            _surfaceChecker = GetComponentInChildren<SurfaceChecker>();
        }

        public void MoveTo(Vector2 destination, float speed)
        {
            _horizontal = CalculateHorizontal(destination);
            _movement.Move(_horizontal, speed);
        }

        public void LookAt(Transform target)
        {
            _characterAnimator.FlipHorizontal(
                CalculateHorizontal(target.position));
        }

        public bool IsAtPosition(Vector2 position)
        {
            // For Y axis:
            // float distanceToWaypoint = Vector2.Distance(currentPos, nextPos);
            
            float horizontalDistance = Mathf.Abs(CurrentPos.x - position.x); 
            return horizontalDistance < _toleranceDistance;
        }

        private float CalculateHorizontal(Vector2 nextPos)
        {
            _direction = (nextPos - CurrentPos).normalized;
            _horizontal = _direction.x > 0 ? 1 : -1;
            
            return _horizontal;
        }

        public bool ShouldPatrolAfter(float time)
        {
            if (_timer >= time)
            {
                ResetTimer();
                return true;
            }

            _timer += Time.deltaTime;
            // Debug.Log(_timer);
            return false;
        }

        public void ResetTimer()
        {
            _timer = 0f;
        }

        public void Stop()
        {
            _horizontal = 0f;
            _movement.Stop();
            
        }

        #region PatrolPath Waypoints

        public bool HasPatrolPath()
        {
            return _patrolPath;
        }

        public Vector2 GetWaypointPos()
        {
            return _patrolPath.GetWaypointPos();
        }

        public void SetNextWaypoint()
        {
            _currentWaypointIndex = _patrolPath.CalculateNextIndex(_currentWaypointIndex);
        }

        #endregion
    }
}