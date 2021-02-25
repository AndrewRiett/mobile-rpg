using System;
using UnityEngine;
using Dungeon.FSM;
using Dungeon.Navigation;

namespace Dungeon.Enemies.EnemyStates
{
    public class EnemyIdle : BaseState
    {
        private readonly EnemyAI _enemy;
        private readonly NavController _navController;

        public EnemyIdle(EnemyAI enemy) : base(enemy.gameObject)
        {
            _enemy = enemy;
            _navController = _enemy.NavController;
        }

        public override Enum Tick()
        {
            if (_enemy.isAggravated)
            {
                return EnemyStateType.Chase;
            }

            if (_navController.ShouldPatrolAfter(_navController.WaypointIdleTime))
            {
                _navController.SetNextWaypoint();
                return EnemyStateType.Patrol;
            }

            return EnemyStateType.Idle;
        }

        public override Enum GetStateType()
        {
            return EnemyStateType.Idle;
        }
    }
}




