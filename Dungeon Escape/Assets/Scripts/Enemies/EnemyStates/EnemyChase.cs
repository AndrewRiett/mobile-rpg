using System;
using UnityEngine;
using Dungeon.FSM;
using Dungeon.Fighting;
using Dungeon.Movement;
using Dungeon.Navigation;

namespace Dungeon.Enemies.EnemyStates
{
    public class EnemyChase : BaseState
    {
        private readonly EnemyAI _enemy;
        private readonly NavController _navController;
        private readonly float _moveSpeed;

        public EnemyChase(EnemyAI enemy, NavController navController, 
            float moveSpeed) : base(enemy.gameObject)
        {
            _enemy = enemy;

            _navController = navController;
            _moveSpeed = moveSpeed;
        }

        public override Enum Tick()
        {
            // if _enemy.inAttackRange -> Attack
            
            Vector2 destination = _enemy.GetLastTargetPos();

            if (_navController.IsAtPosition(destination))
            {
                _navController.Stop();
                return EnemyStateType.Suspicion; // returns and waits for the suspicion timer 
            }
            
            _navController.MoveTo(destination, _moveSpeed);
            return EnemyStateType.Chase;
        }

        public override Enum GetStateType()
        {
            return EnemyStateType.Chase;
        }

        public override void OnStateEnter()
        {
            // Debug.Log("Chase");
        }
    }
}