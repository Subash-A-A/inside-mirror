using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject floor;
    public Transform spawnPoint;

    private FloorManager floorManager;

    private void Start()
    {
        floorManager = FindObjectOfType<FloorManager>();
    }

    private void Update()
    {
        if(transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            floorManager.SpawnFloor(gameObject);
        }
    }
}
