using UnityEngine;
public class Ragdoll : MonoBehaviour
{
    [SerializeField] Collider2D[] colliderArr;
    [SerializeField] Material unlitMaterial;

    private Animator anim;
    private Collider2D mainCollider;
    private Rigidbody2D mainRigidbody;
    private PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        mainCollider = GetComponent<Collider2D>();
        mainRigidbody = GetComponent<Rigidbody2D>();

        foreach (var collider in colliderArr)
        {
            collider.enabled = false;
        }
    }
    public void ActivateRagdoll()
    {   
        mainRigidbody.velocity = Vector2.zero;
        player.enabled = false;
        Destroy(mainRigidbody);
        mainCollider.enabled = false;
        anim.enabled = false;

        foreach (var collider in colliderArr)
        {
            Rigidbody2D rb = collider.gameObject.AddComponent<Rigidbody2D>();
            if (transform.CompareTag("Player"))
            {
                rb.gravityScale = -1f;
            }
            SpriteRenderer sr = collider.GetComponent<SpriteRenderer>();
            sr.material = unlitMaterial;
            rb.AddForce(transform.up * -10f, ForceMode2D.Impulse);
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            collider.enabled = true;
        }

        Destroy(gameObject, 10f);
    }
}
