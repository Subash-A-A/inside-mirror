using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 40f;
    [SerializeField] bool isUltimateBullet = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            if (!isUltimateBullet)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
