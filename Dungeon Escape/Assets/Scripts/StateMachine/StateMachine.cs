using UnityEngine;
using System.Collections.Generic;
using System;

namespace Dungeon.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private StateType defaultState = StateType.Idle;

        private Dictionary<StateType, BaseState> availableStates;
        public BaseState CurrentState { get; private set; }
        public event Action<BaseState> OnStateChanged;

        public void SetStates(Dictionary<StateType, BaseState> statesDict)
        {
            availableStates = statesDict;
        }

        private void Update()
        {
            if (CurrentState == null)
            {
                CurrentState = availableStates[defaultState];
                return;
            }

            StateType nextStateType = CurrentState.Tick();

            if (nextStateType != CurrentState.GetStateType())
            {
                SwitchToNewState(nextStateType);
            }
        }

        private void SwitchToNewState(StateType nextState)
        {
            CurrentState = availableStates[nextState];
            OnStateChanged?.Invoke(CurrentState);
        }
    }
}