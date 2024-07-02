using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_CSharp
{
    class GroundedState : BaseState
    {
        public override PlayerController player { get; set; }
        private JumpingState jumpingState;
        private DuckedState duckedState;

        public void Init(PlayerController Player,JumpingState jump, DuckedState duck)
        {
            Player = player;
            jumpingState = jump;
            duckedState = duck;
        }

        public override void HorizontalMove(float input)
        {
            player.rb.linearVelocity = 
        }

        public override BaseState Jump()
        {
            throw new NotImplementedException();
        }

        public override BaseState Landed()
        {
            throw new NotImplementedException();
        }

        public override BaseState ToggleDuck()
        {
            throw new NotImplementedException();
        }
    }

    class DuckedState : BaseState
    {
        public override PlayerController player { get; set; }
        private JumpingState jumpingState;
        private GroundedState GroundedState;

        public void Init(PlayerController Player, JumpingState jump, GroundedState ground)
        {
            Player = player;
            jumpingState = jump;
            GroundedState = ground;
        }

        public override void HorizontalMove(float input)
        {
            throw new NotImplementedException();
        }

        public override BaseState Jump()
        {
            throw new NotImplementedException();
        }

        public override BaseState Landed()
        {
            throw new NotImplementedException();
        }

        public override BaseState ToggleDuck()
        {
            throw new NotImplementedException();
        }
    }

    class JumpingState : BaseState
    {
        public override PlayerController player { get; set; }
        private GroundedState groundedState;
        private DuckedState duckedState;

        public void Init(PlayerController Player, GroundedState ground, DuckedState duck)
        {
            Player = player;
            groundedState = ground;
            duckedState = duck;
        }
        public override void HorizontalMove(float input)
        {
            throw new NotImplementedException();
        }

        public override BaseState Jump()
        {
            throw new NotImplementedException();
        }

        public override BaseState Landed()
        {
            throw new NotImplementedException();
        }

        public override BaseState ToggleDuck()
        {
            throw new NotImplementedException();
        }
    }
}
