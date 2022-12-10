using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 40f;
    public bool isUltimateBullet = false;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * bulletSpeed;
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
                if(transform.rotation.z == 0f)
                {
                    GameObject bullet = Instantiate(gameObject, transform.position, Quaternion.Euler(0f, 0f, 90f));
                    bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed * 2f;
                }
                else
                {
                    GameObject bullet = Instantiate(gameObject, transform.position, Quaternion.Euler(0f, 0f, 0f));
                }
            }
        }
    }
}
