using System;
using System.Collections.Generic;
using UnityEngine;
using Dungeon.FSM;
using Dungeon.Enemies.EnemyStates;
using Dungeon.Animation;
using Dungeon.Fighting;
using Dungeon.Navigation;

namespace Dungeon.Enemies
{
    [RequireComponent(typeof(CharacterAnimator))]
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(NavController))]
    public class EnemyAI : MonoBehaviour
    {
        // Serialized
        [SerializeField] protected float _maxHealth;
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected int _gemsAmount;
        [SerializeField] protected float _suspicionTime;

        // properties
        internal NavController NavController { get; private set;}
        internal FightingAI Fighting { get; private set; }
        
        internal float SuspicionTime => _suspicionTime;
        internal float MoveSpeed  => _moveSpeed; // patrolSpeed => get { hasPatrolSpeed ? _patrolSpeed : _moveSpeed} 

        // targeting
        public GameObject Target { get; private set; }
        private Vector2 _targetLastPos;
        internal bool isAggravated;
        
        // fsm
        private StateMachine _stateMachine;
        
        // TODO: aggravate nearby allies by casting a box,
        // then get components and setTarget

        private void Awake()
        {
            _stateMachine = GetComponent<StateMachine>();
            NavController = GetComponent<NavController>();
            Fighting = GetComponent<FightingAI>();
        }

        private void OnEnable()
        {
            InitStates();
        }

        internal void SetTarget(GameObject target)
        {
            Target = target;
            isAggravated = true;
        }

        internal void RemoveTarget()
        {
            _targetLastPos = this.Target.transform.position;
            Target = null;
        }

        internal Vector2 GetLastTargetPos()
        {
            // Debug.Log("target removed, lastPos: " + _targetLastPos);
            if (!HasTarget())
                return _targetLastPos;

            return Target.transform.position;
        }

        internal bool HasTarget()
        {
            return Target;
        }

        private void InitStates()
        {
            var statesToInit = new Dictionary<Enum, BaseState>
            {
                {EnemyStateType.Idle, new EnemyIdle(this)},
                {EnemyStateType.Patrol, new EnemyPatrol(this)},
                {EnemyStateType.Chase, new EnemyChase(this)},
                {EnemyStateType.Suspicion, new EnemySuspicion(this)},
                {EnemyStateType.Attack, new EnemyAttack(this)},
            };

            _stateMachine.SetStates(statesToInit, defaultState: EnemyStateType.Idle);
        }
    }
}
