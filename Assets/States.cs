using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assembly_CSharp
{
    class GroundedState : BaseState
    {
        public override PlayerController player { get; set; }
        private JumpingState jumpingState;
        private DuckedState duckedState;
        private float dampingVal;

        public void Init(PlayerController Player,JumpingState jump, DuckedState duck, float damping)
        {
            player = Player;
            jumpingState = jump;
            duckedState = duck;
            dampingVal = damping;
        }

        public override void HorizontalMove(float input,float time)
        {
            
            player.rb.velocityX = Utility.Dampning(player.rb.velocityX, input, dampingVal, time);
        }

        public override BaseState Jump()
        {
            player.rb.velocityY = 3*player.JumpStrength;
            return jumpingState;
        }

        public override BaseState Landed()
        {
            return this;
        }

        public override BaseState ToggleDuck()
        {
            player.targetY = 0.5f;
            return duckedState;
        }

        public override void update(float time)
        {
            Debug.Log("on ground");
            return;
        }

        public override BaseState LeftGround()
        {
            return jumpingState;
        }
    }

    class DuckedState : BaseState
    {
        public override PlayerController player { get; set; }
        private JumpingState jumpingState;
        private GroundedState GroundedState;
        private float dampingVal;

        public void Init(PlayerController Player, JumpingState jump, GroundedState ground,float damping)
        {
            player = Player;
            jumpingState = jump;
            GroundedState = ground;
            dampingVal=damping;
        }

        public override void HorizontalMove(float input, float time)
        {
            player.rb.velocityX = Utility.Dampning(player.rb.velocityX, input/2, dampingVal, time);
        }

        public override BaseState Jump()
        {
            player.targetY = 1;
            player.rb.velocityY = 5*player.JumpStrength;
            return jumpingState;
        }

        public override BaseState Landed()
        {
            return this;
        }

        public override BaseState ToggleDuck()
        {
            player.targetY = 1;
            return GroundedState;
        }

        public override void update(float t)
        {
            Debug.Log("ducking");
            return;
        }

        public override BaseState LeftGround()
        {
            player.targetY = 1;
            return jumpingState;
        }
    }

    class JumpingState : BaseState
    {
        public override PlayerController player { get; set; }
        private GroundedState groundedState;
        private DuckedState duckedState;
        private JumpingState doublejumpingState;
        private bool notDoubleJump = true;
        private float dampingVal;

        public void Init(PlayerController Player, GroundedState ground, DuckedState duck, float damping)
        {
            player = Player;
            groundedState = ground;
            duckedState = duck;
            dampingVal = damping;
            notDoubleJump = false;
        }
        public void Init(PlayerController Player, GroundedState ground, DuckedState duck, JumpingState doubleJump, float damping)
        {
            player = Player;
            groundedState = ground;
            duckedState = duck;
            dampingVal = damping;
            doublejumpingState = doubleJump;
        }

        public override void HorizontalMove(float input, float time)
        {
            player.rb.velocityX = Utility.Dampning(player.rb.velocityX , input * 0.75f, dampingVal, time);
        }

        public override BaseState Jump()
        {
            if (notDoubleJump)
            {
                player.rb.velocityY = 2 * player.JumpStrength;
                return doublejumpingState;
            }
            return this;
        }

        public override BaseState Landed()
        {
            if (Input.GetKey(KeyCode.S))
            {
                player.targetY = 0.5f;
                return duckedState;
            } 
            else
            {
                return groundedState;
            }    
        }


        public override BaseState ToggleDuck()
        {
            return this;
        }

        public override void update(float t)
        {
            Debug.Log("jumping");
            return;
        }

        public override BaseState LeftGround()
        {
            return this;
        }
    }
}
