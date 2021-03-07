using System;
using Dungeon.Fighting;
using UnityEngine;
using Dungeon.FSM;
using Dungeon.Navigation;

namespace Dungeon.Enemies.EnemyStates
{
    public class EnemyChase : BaseState
    {
        private readonly EnemyAI _enemy;
        private readonly NavController _navController;
        private readonly FightingAI _fighting;
        private readonly float _moveSpeed;

        public EnemyChase(EnemyAI enemy) : base(enemy.gameObject)
        {
            _enemy = enemy;

            _fighting = _enemy.GetComponent<FightingAI>();
            _navController = _enemy.NavController;
            _moveSpeed = _enemy.MoveSpeed;
        }

        public override Enum Tick()
        {
            if (_fighting.IsInRange(_enemy.Target))
            {
                _navController.Stop(); 
                _navController.LookAt(_enemy.Target.transform);
                _fighting.Attack(_enemy.Target);
                
                return EnemyStateType.Chase; // early return
            }
            
            Vector2 lastTargetPos = _enemy.GetLastTargetPos();

            // if a target has left and the enemy has reached its last position
            if (_navController.IsAtPosition(lastTargetPos)) 
            {
                _navController.Stop();
                return EnemyStateType.Suspicion; // stops and waits for the suspicion timer 
            }
            
            _navController.MoveTo(lastTargetPos, _moveSpeed); // otherwise move to the last target's position
            return EnemyStateType.Chase; 
        }

        public override Enum GetStateType()
        {
            return EnemyStateType.Chase;
        }

        public override void OnStateEnter()
        {
            Debug.Log("Chase");
        }
    }
}