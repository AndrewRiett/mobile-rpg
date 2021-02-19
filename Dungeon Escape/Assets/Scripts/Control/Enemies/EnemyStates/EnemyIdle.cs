using Dungeon.FSM;
using Dungeon.Navigation;
using System;
using Cinemachine;

namespace Dungeon.Control.Enemies.EnemyStates
{
    public class EnemyIdle : BaseState
    {
        private readonly Enemy _enemy;
        private readonly NavController _navController;

        public EnemyIdle(Enemy enemy) : base(enemy.gameObject)
        {
            _enemy = enemy;
            _navController = _enemy.NavController;
        }

        public override Enum Tick()
        {
            // TODO check aggro

            if (_navController.ShouldPatrol())
                return EnemyStateType.Patrol;

            return EnemyStateType.Idle;
        }

        public override Enum GetStateType()
        {
            return EnemyStateType.Idle;
        }

        public override void OnStateEnter()
        {
            //Debug.Log("Entered IDLE State");
        }

        public override void OnStateExit()
        {
            //Debug.Log("Exited IDLE State");
        }
    }
}




