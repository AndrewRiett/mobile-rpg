using Dungeon.Movement;
using Dungeon.FSM;
using System;
using Dungeon.Navigation;

namespace Dungeon.Control.Enemies.EnemyStates
{
    public class EnemyPatrol : BaseState
    {
        private float _horizontal;

        // from enemy:
        private readonly Enemy _enemy;
        private readonly MovementController _movement;
        private readonly NavController _navController;
        private readonly float _moveSpeed;

        public EnemyPatrol(Enemy enemy) : base(enemy.gameObject)
        {
            _enemy = enemy;

            _movement = _enemy.Movement;
            _navController = _enemy.NavController;

            _moveSpeed = enemy.MoveSpeed;
            _horizontal = _navController.CalculateHorizontal();
        }

        public override Enum GetStateType()
        {
            return EnemyStateType.Patrol;
        }

        public override Enum Tick()
        {
            // TODO check agro

            if (_navController.IsAtWaypoint())
            {
                _movement.Stop();
                
                _navController.SetNextWaypoint();
                _horizontal = _navController.CalculateHorizontal();
                
                return EnemyStateType.Idle;
            }
            else
            {
                _movement.Move(_horizontal, _moveSpeed);
            }

            return EnemyStateType.Patrol;
        }
    }
}