using UnityEngine;

namespace Assembly_CSharp
{
    public abstract class BaseState
    {
        public abstract PlayerController player { get; set; }
        public abstract BaseState Jump();

        public abstract BaseState ToggleDuck();

        public abstract BaseState Landed();
        public abstract BaseState LeftGround();

        public abstract void HorizontalMove(float input, float time);

        public abstract void update(float time);
    }
}
