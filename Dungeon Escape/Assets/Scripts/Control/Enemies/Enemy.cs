using System.Collections.Generic;
using UnityEngine;
using Dungeon.Animation;
using Dungeon.FSM;
using Dungeon.Control.Enemies.EnemyStates;
using Dungeon.Movement;
using System;
using Dungeon.Navigation;

namespace Dungeon.Control.Enemies
{
    [RequireComponent(typeof(CharacterAnimator))]
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(NavController))]
    public abstract class Enemy : MonoBehaviour
    {
        // Basic
        [SerializeField] protected float _maxHealth;
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected int _gemsAmount;

        protected StateMachine _stateMachine;
        protected MovementController _movement;
        protected NavController _navController;

        #region Properties:
        internal float MoveSpeed { get => _moveSpeed; }
        internal MovementController Movement { get => _movement; }
        internal NavController NavController { get => _navController; }
        #endregion

        private void Awake()
        {
            _stateMachine = GetComponent<StateMachine>();
            _movement = GetComponent<MovementController>();
            _navController = GetComponent<NavController>();
        }

        private void OnEnable()
        {
            InitStates();
        }
        protected virtual void InitStates()
        {
            var statesToInit = new Dictionary<Enum, BaseState>
            {
                {EnemyStateType.Idle, new EnemyIdle(this)},
                {EnemyStateType.Patrol, new EnemyPatrol(this) }
            };

            _stateMachine.SetStates(statesToInit, EnemyStateType.Idle);
        }
    }
}