using System;
using UnityEngine;

namespace Dungeon.Movement.Waypoints
{
    public class PatrolPath : MonoBehaviour
    {
        // a bool for cycling the waypoints 0 -> 1 -> 2 -> 0, 
        // used for flying units that might interact with Y axis
        [SerializeField] private bool cycleAxisY;

        private const float waypointGizmosRadius = 0.1f;

        public int GetNextIndex(int currentIndex)
        {
            // OR: (i + 1 == transform.childCount) ? return 0 : return i + 1
            return (currentIndex + 1) % transform.childCount;
        }

        // returns transform
        public Vector3 GetCurrentWaypointPos(int index)
        {
            return transform.GetChild(index).position;
        }

        // used in the edit mode to draw a path
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Vector3 currentWaypointPos = GetCurrentWaypointPos(i);
                Vector3 nextWaypointPos = transform.GetChild(GetNextIndex(i)).position;

                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(currentWaypointPos, waypointGizmosRadius);

#if UNITY_EDITOR
                // for drawing a waypoint's index
                UnityEditor.Handles.color = Color.white;
                UnityEditor.Handles.Label(currentWaypointPos, (i + 1).ToString());
#endif

                if (!cycleAxisY && i + 1 == transform.childCount)
                    continue;
                else
                    Gizmos.DrawLine(currentWaypointPos, nextWaypointPos);
            }
        }
    }
}