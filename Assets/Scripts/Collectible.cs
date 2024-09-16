using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField,Range(1,1000)] int score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.CollectedPoint.Invoke(score);
            Destroy(this.gameObject);
        }
    }

}
