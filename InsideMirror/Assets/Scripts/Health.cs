using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Ragdoll mirrorPlayerRagDoll;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Image healthBarFill;
    [SerializeField] float healAmount = 25f;

    public static bool gameOver = false;

    private float currentHealth;
    private Ragdoll ragdoll;
    private AudioManager am;

    private void Start()
    {
        am = FindObjectOfType<AudioManager>();

        ragdoll = GetComponent<Ragdoll>();
        gameOver = false;

        currentHealth = maxHealth;
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    private void Update()
    {
        HealthLerper();
    }

    public void TakeDamage(float value)
    {
        HitSound();
        currentHealth -= value;
        if (currentHealth <= 0.01)
        {
            mirrorPlayerRagDoll.ActivateRagdoll();
            ragdoll.ActivateRagdoll();
            gameOver = true;
        }
    }

    private void HealthLerper()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        healthBarFill.fillAmount = Mathf.Lerp(healthBarFill.fillAmount, currentHealth/maxHealth, 10 * Time.deltaTime);
    }

    public void HealPlayers()
    {
        currentHealth += healAmount;
        mirrorPlayerRagDoll.GetComponent<Health>().currentHealth += healAmount;
    }

    private void HitSound()
    {
        am.Play("hurt");
        float randVal = Random.value;
        if(randVal <= 0.7f)
        {
            int randVal2 = Random.Range(0, 3);
            switch (randVal2)
            {
                case 0:
                    am.Play("ouch");
                    break;
                case 1:
                    am.Play("notYet");
                    break;
                case 2:
                    am.Play("shit");
                    break;
            }
        }
    }
}
