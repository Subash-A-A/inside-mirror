using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] float _xOffset = 7.5f;
    private void Update()
    {
        transform.position = new Vector3(_followTarget.position.x + _xOffset, 0f, 0f);
    }
}
