using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("MirrorPlayer"))
        {
            collision.GetComponent<Health>().TakeDamage(25f);
        }
    }
}
