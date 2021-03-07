using System;
using UnityEngine;
using Dungeon.FSM;
using Dungeon.Navigation;

namespace Dungeon.Enemies.EnemyStates
{
    public class EnemyPatrol : BaseState
    {
        private Vector2 _destination = Vector2.zero;
        private bool _hasPath;

        // from enemy:
        private readonly EnemyAI _enemy;
        private readonly NavController _navController;
        private readonly float _moveSpeed;

        public EnemyPatrol(EnemyAI enemy) : base(enemy.gameObject)
        {
            _enemy = enemy;

            _navController = _enemy.NavController;
            _moveSpeed = _enemy.MoveSpeed;

            _hasPath = _navController.HasPatrolPath();
        }

        public override Enum GetStateType()
        {
            return EnemyStateType.Patrol;
        }

        public override Enum Tick()
        {
            if (_enemy.isAggravated)
            {
                return EnemyStateType.Chase;
            }

            if (_navController.IsAtPosition(_destination))
            {
                _navController.Stop();
                return EnemyStateType.Idle;
            }
            
            _navController.MoveTo(_destination, _moveSpeed);
            return EnemyStateType.Patrol;
        }

        public override void OnStateEnter()
        {
            Debug.Log("Patrol");
            _destination = _navController.GetWaypointPos(); 
        }
    }
}