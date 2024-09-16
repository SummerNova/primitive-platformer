using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private Transform LeftSide;
    [SerializeField] private Transform RightSide;
    [SerializeField] private float ForceStrength = 100;
    [SerializeField,Range(-1,1)] private float direction = 1;
    



    // Update is called once per frame
    void Update()
    {
        if (ball.transform.position.x > RightSide.position.x)
        {
            ball.transform.position = new(RightSide.position.x, ball.transform.position.y);
            ball.velocity = Vector2.zero; ball.angularVelocity = 0;
            direction = -1;
        }
        else if (ball.transform.position.x < LeftSide.position.x)
        {
            ball.transform.position = new(LeftSide.position.x, ball.transform.position.y);
            ball.velocity = Vector2.zero; ball.angularVelocity = 0;
            direction = 1;
        }
        ball.AddForce(Vector2.right * direction * ForceStrength);

    }

    
}
