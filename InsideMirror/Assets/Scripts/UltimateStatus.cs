using UnityEngine;
using UnityEngine.UI;

public class UltimateStatus : MonoBehaviour
{
    private PlayerController playerController;
    private AnimationEventManager aem;

    [SerializeField] Image lMouseIcon;
    [SerializeField] Image lSlider;
    [SerializeField] Image rMouseIcon;
    [SerializeField] Image rSlider;

    [SerializeField] Color available;
    [SerializeField] Color unAvailable;

    private float lFillAmount = 1f;
    private float rFillAmount = 1f;

    private bool rFilled = true;
    private bool lFilled = true;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        aem = GetComponent<AnimationEventManager>();
    }

    private void Update()
    {
        if (playerController._ultimateReady)
        {
            rMouseIcon.color = available;
            rFillAmount = 1f;
            rFilled = true;
        }
        else if(rFilled && !playerController._ultimateReady) {
            rMouseIcon.color = unAvailable;
            rFillAmount = 0f;
            rFilled = false;
        }

        if(playerController._goodEvil == 0f)
        {
            if (!rFilled)
            {
                rFillAmount += Time.unscaledDeltaTime / (aem.ultimateShieldDuration + aem.shieldDownTime + 0.05f);
            }
        }
        else
        {
            if (!rFilled)
            {
                rFillAmount += Time.unscaledDeltaTime / (aem.fireDownTime);
            }
        }
        

        if (playerController._goodEvil == 0f)
        {
            if (playerController._canShield && !playerController._isShielded)
            {
                lMouseIcon.color = available;
                lFillAmount = 1f;
                lFilled = true;
            }
            else if(playerController._selfAbilityInUse && lFilled)
            {
                lMouseIcon.color = unAvailable;
                lFillAmount = 0f;
                lFilled = false;
            }

            if (!lFilled)
            {
                lFillAmount += Time.unscaledDeltaTime / (aem.shieldDuration + 0.05f);
            }
        }
        else
        {
            if (playerController._canShoot)
            {
                lMouseIcon.color = available;
                lFillAmount = 1f;
            }
            else
            {
                lMouseIcon.color = unAvailable;
                lFillAmount = 0f;
            }
        }

        rSlider.fillAmount = Mathf.Lerp(rSlider.fillAmount, rFillAmount, 10 * Time.unscaledDeltaTime);
        lSlider.fillAmount = Mathf.Lerp(lSlider.fillAmount, lFillAmount, 10 * Time.unscaledDeltaTime);
    }
}
