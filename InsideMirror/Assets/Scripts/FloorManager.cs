using UnityEngine;

public class FloorManager : MonoBehaviour
{   
    [SerializeField] private Floor[] floors;
    [SerializeField] private Transform currentSpawnPoint;

    public void SpawnFloor(GameObject prevFloor)
    {
        Floor floor = GetRandomFloor();
        GameObject floorObj =  Instantiate(floor.floor, currentSpawnPoint);
        floorObj.transform.parent = null;
        currentSpawnPoint = floorObj.transform.GetChild(0);

        prevFloor.AddComponent<Rigidbody2D>();
    }

    private Floor GetRandomFloor()
    {
        return floors[Random.Range(0, floors.Length)];
    }
}
