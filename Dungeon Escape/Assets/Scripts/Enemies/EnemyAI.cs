using System;
using System.Collections.Generic;
using UnityEngine;
using Dungeon.FSM;
using Dungeon.Enemies.EnemyStates;
using Dungeon.Animation;
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

        // general
        internal NavController NavController { get; private set;}
        internal float SuspicionTime => _suspicionTime;
        internal float MoveSpeed  => _moveSpeed;

        // targeting
        public GameObject Target { get; private set; }
        private Vector2 _targetLastPos;
        internal bool isAggravated;
        
        // fsm
        private StateMachine _stateMachine;


        // TODO: aggrevate nearby allies by casting a box,
        // then get components and setTarget

        private void Awake()
        {
            _stateMachine = GetComponent<StateMachine>();
            NavController = GetComponent<NavController>();
        }

        private void OnEnable()
        {
            InitStates();
        }

        internal void RemoveTarget()
        {
            _targetLastPos = this.Target.transform.position;
            Target = null;
        }

        internal void SetTarget(GameObject target)
        {
            Target = target;
            isAggravated = true;
        }

        internal Vector2 GetLastTargetPos()
        {
            // Debug.Log("target removed, lastPos: " + _targetLastPos);
            if (!HasTarget())
                return _targetLastPos;

            return Target.transform.position;
        }

        public bool HasTarget()
        {
            return Target;
        }

        private void InitStates()
        {
            var statesToInit = new Dictionary<Enum, BaseState>
            {
                {EnemyStateType.Idle, new EnemyIdle(this)},
                {EnemyStateType.Patrol, new EnemyPatrol(this)},
                {EnemyStateType.Chase, new EnemyChase(this, NavController, _moveSpeed)},
                {EnemyStateType.Suspicion, new EnemySuspicion(this)},
            };

            _stateMachine.SetStates(statesToInit, EnemyStateType.Idle);
        }
    }
}
