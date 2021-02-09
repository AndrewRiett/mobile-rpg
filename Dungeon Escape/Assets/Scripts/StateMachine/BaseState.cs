using UnityEngine;

namespace Dungeon.StateMachine
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

        public abstract StateType Tick();
        public abstract StateType GetStateType();
    }
}