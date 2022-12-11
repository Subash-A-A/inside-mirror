using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            FindObjectOfType<AudioManager>().Play("explosion", Random.Range(0.9f, 1f));
            FindObjectOfType<ScoreManager>().IncrementScore();
            Destroy(collision.gameObject);
        }
    }
}
