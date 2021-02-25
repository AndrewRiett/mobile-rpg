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
        private int _index;

        public virtual int CalculateNextIndex(int currentIndex)
        {
            _index = currentIndex;
            
            // int activePoints = 0;
            // for (int i = 0; i < transform.childCount; i++)
            // {
            //     if (transform.GetChild(i).gameObject.activeSelf)
            //         activePoints = i;
            // }

            switch (_patrolOrder)
            {
                case PatrolOrder.Ascending:
                    if (currentIndex + 1 == transform.childCount)
                    {
                        _patrolOrder = PatrolOrder.Descending;
                        _index--;
                    }
                    else
                    {
                        _index++;
                    }
                    break;

                case PatrolOrder.Descending:
                    if (currentIndex - 1 < 0)
                    {
                        _patrolOrder = PatrolOrder.Ascending;
                        _index++;
                    }
                    else
                    {
                        _index--;
                    }
                    break;
            }

            return _index;
        }

        internal Vector2 GetWaypointPos()
        {
            return transform.GetChild(_index).position;
        }

        private Vector2 GetCurrentWaypointPos(int currentIndex)
        {
            return transform.GetChild(currentIndex).position;
        }

        //public override int CalculateNextIndex(int currentIndex)
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
                // for not drawing the line to an inactive waypoint
                if (!transform.GetChild(cycledIndex).gameObject.activeSelf)
                    return; 
                
                // drawing line connections
                if (!cycleWaypoints && (i + 1) == transform.childCount)
                    continue;
                else
                    Gizmos.DrawLine(currentWaypointPos, nextWaypointPos);
            }
        }
    }
}