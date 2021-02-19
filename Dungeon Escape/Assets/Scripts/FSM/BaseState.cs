using System;
using UnityEngine;

namespace Dungeon.FSM
{
    public abstract class BaseState
    {
        public BaseState(GameObject gameObject)
        {
            this.gameObject = gameObject;
            this.transform = gameObject.transform;
        }

        protected GameObject gameObject;
        protected Transform transform;

        public abstract Enum Tick();
        public abstract Enum GetStateType();

        public virtual void OnStateEnter() { }
        public virtual void OnStateExit() { }

    }
}