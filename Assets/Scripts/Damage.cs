using UnityEngine;

public class Damage : MonoBehaviour
{

    [SerializeField, Range(1, 1000)] private int _DamageValue;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.DamageTaken.Invoke(_DamageValue);
        }
    }
}
