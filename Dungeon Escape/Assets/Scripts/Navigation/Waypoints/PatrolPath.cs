using UnityEngine;

namespace Dungeon.Navigation.Waypoints
{
    public class PatrolPath : MonoBehaviour
    {
        // a bool for cycling the waypoints (0 -> 1 -> 2 -> 0), 
        // used for flying units that might interact with Y axis
        [SerializeField] private bool cycleWaypoints;
        private PatrolOrder _patrolOrder;
        
        private const float WaypointGizmosRadius = 0.1f;
        private int _nextPatrolIndex;
        private float _horizontal;

        public int CalculateNextIndex(int currentIndex)
        {
            _nextPatrolIndex = currentIndex;

            //if (cycleWaypoints)
            //    return GetNextCycledID(currentIndex);

            switch (_patrolOrder)
            {
                case PatrolOrder.Ascending:
                    if (_nextPatrolIndex + 1 == transform.childCount)
                    {
                        _patrolOrder = PatrolOrder.Descending;
                        _nextPatrolIndex--;
                    }
                    else
                    {
                        _nextPatrolIndex++;
                    }
                    break;

                case PatrolOrder.Descending:
                    if (_nextPatrolIndex - 1 < 0)
                    {
                        _patrolOrder = PatrolOrder.Ascending;
                        _nextPatrolIndex++;
                    }
                    else
                    {
                        _nextPatrolIndex--;
                    }
                    break;
            }

            return _nextPatrolIndex;

        }

        internal float DetermineHorizontal(Vector2 position) //Vector2 target)
        {
            //Vector2 direction = position - target;
            //float magnitude = direction.magnitude;
            //direction.Normalize();

            ////if (magnitude > 0)
            ////    magnitude = 1;
            ////else if (magnitude < 0)
            ////    magnitude = -1;

            //horizontal = (magnitude > 0 && magnitude != 0) ? 1 : -1;

            switch (_patrolOrder)
            {
                case PatrolOrder.Ascending:
                    _horizontal = 1;
                    break;
                case PatrolOrder.Descending:
                    _horizontal = -1;
                    break;

            }

            return _horizontal;
        }

        internal int InitWaypointID()
        {
            if (_patrolOrder == PatrolOrder.Descending)
            {
                Debug.Log("Left: " + (transform.childCount - 1));

                _horizontal = -1;
                return transform.childCount - 1;
            }

            Debug.Log("Right: 0");

            return 0;
        }

        // returns vector
        public Vector3 GetCurrentWaypointPos(int currentIndex)
        {
            return transform.GetChild(currentIndex).position;
        }
        
        public Vector3 GetNextWaypointPos()
        {
            return transform.GetChild(_nextPatrolIndex).position;
        }

        //private int GetNextCycledID(int currentIndex)
        //{
        //    int index = currentIndex;

        //    switch (patrolOrder)
        //    {
        //        case PatrolOrder.Ascending:
        //            index = (currentIndex + 1) % transform.childCount; // returns 0 -> 1 -> 2 -> 0
        //            break;
        //        case PatrolOrder.Descending:
        //            index = ((index - 1) != -1) ? index - 1 : transform.childCount; // returns 2 -> 1 -> 0 -> 2
        //            break;
        //    }

        //    return index;
        //}

        // used in the edit mode to draw a path
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector3 currentWaypointPos = GetCurrentWaypointPos(i);
                int cycledIndex = (i + 1) % transform.childCount;
                Vector3 nextWaypointPos = transform.GetChild(cycledIndex).position;

                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(currentWaypointPos, WaypointGizmosRadius);

#if UNITY_EDITOR
                // Labeling a waypoint index with an index value
                UnityEditor.Handles.color = Color.white;

                if (_patrolOrder == PatrolOrder.Ascending)
                    UnityEditor.Handles.Label(currentWaypointPos, i.ToString());
                else
                    UnityEditor.Handles.Label(currentWaypointPos, (transform.childCount - (i+1)).ToString());
                    
#endif
                // drawing line connections
                if (!cycleWaypoints && (i + 1) == transform.childCount)
                    continue;
                else
                    Gizmos.DrawLine(currentWaypointPos, nextWaypointPos);
            }
        }
    }
}