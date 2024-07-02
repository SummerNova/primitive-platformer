using UnityEngine;

namespace Assembly_CSharp
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public Rigidbody2D rb;
        [SerializeField] Camera cam;
        [SerializeField] public float JumpStrength = 1;
        [SerializeField] float cameraReactivity = 20;
        public float targetY;
        public float targetX;

        private float timer = 0.2f;

        private GroundedState ground = new();
        private JumpingState jump = new();
        private DuckedState ducked = new();
        private JumpingState doublejump = new();
        private BaseState baseState;

        private void Awake()
        {
            ground.Init(this, jump, ducked, 10);
            jump.Init(this,ground,ducked,doublejump, 10);
            doublejump.Init(this, ground, ducked, 10);
            ducked.Init(this,jump,ground,10);
            baseState = jump;
        }


        // Update is called once per frame
        void Update()
        {
            float y = Utility.Dampning(transform.localScale.y, targetY, 10, Time.deltaTime);
            this.transform.localScale = new Vector3(1+Mathf.Pow((1/y-1),2), y, 1);
            targetX = 0;
            if (timer < 0.2f) timer += Time.deltaTime;

            //baseState.update(Time.deltaTime);

            if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
            {
                targetX -= 1;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                targetX += 1;
            }

            baseState.HorizontalMove(targetX*5, Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                baseState = baseState.Jump();
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                baseState = baseState.ToggleDuck();
            }
            else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                baseState = baseState.ToggleDuck();
            }

            

        }

        private void FixedUpdate()
        {
            cam.orthographicSize = Utility.Dampning(cam.orthographicSize,5 + Mathf.Log(rb.velocity.magnitude+1,2)/2,cameraReactivity,Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

            baseState = baseState.Landed();



        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if(timer >= 0.2)
            {
                timer = 0;

                baseState = baseState.LeftGround();
            }
        }
    }
}
