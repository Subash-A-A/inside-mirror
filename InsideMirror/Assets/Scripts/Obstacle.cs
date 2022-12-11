using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] bool isRed = false;
    [SerializeField] GameObject RedParticle;
    [SerializeField] GameObject BlueParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("MirrorPlayer"))
        {
            collision.GetComponent<Health>().TakeDamage(25f);
        }
    }

    private void OnDestroy()
    {
        if (isRed)
        {
            Instantiate(RedParticle, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(BlueParticle, transform.position, Quaternion.identity);
        }
    }
}
