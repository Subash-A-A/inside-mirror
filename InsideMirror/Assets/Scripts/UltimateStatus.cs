using UnityEngine;
using UnityEngine.UI;

public class UltimateStatus : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] Image lMouseIcon;
    [SerializeField] Image lSlider;
    [SerializeField] Image rMouseIcon;
    [SerializeField] Image rSlider;

    [SerializeField] Color available;
    [SerializeField] Color unAvailable;

    private float lFillAmount = 1f;
    private float rFillAmount = 1f;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerController._ultimateReady)
        {
            rMouseIcon.color = available;
            rFillAmount = 1f;
        }
        else
        {
            rMouseIcon.color = unAvailable;
            rFillAmount = 0f;
        }

        rSlider.fillAmount = Mathf.Lerp(rSlider.fillAmount, rFillAmount, 10 * Time.deltaTime);
    }
}
