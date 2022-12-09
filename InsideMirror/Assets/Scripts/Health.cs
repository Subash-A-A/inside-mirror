using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Image healthBarFill;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    private void Update()
    {
        HealthLerper();
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
    }

    private void HealthLerper()
    {
        healthBarFill.fillAmount = Mathf.Lerp(healthBarFill.fillAmount, currentHealth/maxHealth, 10 * Time.deltaTime);
    }
}
