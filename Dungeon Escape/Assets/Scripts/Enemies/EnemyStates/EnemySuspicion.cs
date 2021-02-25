using System;
using Dungeon.FSM;
using Dungeon.Navigation;
using UnityEngine;

namespace Dungeon.Enemies.EnemyStates
{
    public class EnemySuspicion : BaseState
    {
        private readonly EnemyAI _enemy;
        private readonly NavController _navController;

        public EnemySuspicion(EnemyAI enemy) : base(enemy.gameObject)
        {
            _enemy = enemy;
            _navController = _enemy.NavController;
        }

        public override Enum Tick()
        {
            if (_navController.ShouldPatrolAfter(_enemy.SuspicionTime))
            {
                return EnemyStateType.Patrol;
            }

            return EnemyStateType.Suspicion;
        }

        public override Enum GetStateType()
        {
            return EnemyStateType.Suspicion;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _enemy.isAggravated = false;
        }
    }
}