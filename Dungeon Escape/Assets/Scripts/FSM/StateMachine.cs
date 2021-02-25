using UnityEngine;
using System.Collections.Generic;
using System;

namespace Dungeon.FSM
{
    public class StateMachine : MonoBehaviour
    {
        private Enum _defaultState;
        private Dictionary<Enum, BaseState> _availableStates;
        
        public BaseState CurrentState { get; private set; }
        public event Action<BaseState> OnStateChanged;

        public void SetStates(Dictionary<Enum, BaseState> statesDict, Enum defaultState)
        {
            _defaultState = defaultState;
            _availableStates = statesDict;
        }

        private void Update()
        {
            if (CurrentState is null)
            {
                CurrentState = _availableStates[_defaultState];
                return;
            }

            Enum nextStateType = CurrentState.Tick();

            if (!nextStateType.Equals(CurrentState.GetStateType()))
            {
                SwitchToNewState(nextStateType);
            }
        }

        private void SwitchToNewState(Enum nextState)
        {
            CurrentState.OnStateExit();
            CurrentState = _availableStates[nextState];
            CurrentState.OnStateEnter();

            OnStateChanged?.Invoke(CurrentState);
        }
    }
}