using UnityEngine;
using System.Collections;

public class AnimationEventManager : MonoBehaviour
{
    [SerializeField] private GameObject currentPlayerBox;
    [SerializeField] private GameObject mirrorPlayerBox;
    [SerializeField] private PlayerController mirrorPlayer;
    [SerializeField] private Animator camAnim;
    [SerializeField] private Animator mirrorPlayerAnimator;
    [SerializeField] private TimeManager tm;
    [SerializeField] private EffectManager em;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject UltimateBullet;
    [SerializeField] private GameObject Shield;
    [SerializeField] private GameObject UltimateShield;
    [SerializeField] private GameObject MirrorShield;

    [SerializeField] ParticleSystem currentParticle;

    public float shieldDownTime = 20f;
    public float fireDownTime = 10f;
    public float ultimateShieldDuration = 10f;
    public float shieldDuration = 5f;
    public float fireDelay = 1f;

    private PlayerController currentPlayer;
    private Animator currentAnimator;
    private AudioManager am;

    private void Start()
    {
        currentPlayer = GetComponent<PlayerController>();
        currentAnimator = GetComponent<Animator>();
        am = FindObjectOfType<AudioManager>();
    }

    public void SwitchInit()
    {
        currentParticle.Play();
        am.Play("charge", Random.Range(1.5f, 2f));
        currentAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        tm.StartSlowMotion();
        camAnim.SetBool("shake", true);
        currentPlayer._canSwitch = false;
        mirrorPlayer._canSwitch = false;

        currentPlayerBox.SetActive(true);
        mirrorPlayerBox.SetActive(false);
    }

    public void SwitchEnd()
    {
        camAnim.SetBool("shake", false);

        currentPlayer._canSwitch = true;
        mirrorPlayer._canSwitch = true;

        currentPlayerBox.SetActive(false);
        mirrorPlayerBox.SetActive(true);
        currentAnimator.updateMode = AnimatorUpdateMode.Normal;
        tm.StopSlowMotion();
        currentParticle.Stop();
    }

    public void FireInit()
    {
        am.Play("powerUp", Random.Range(.95f, 1f));
        currentAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        tm.StartSlowMotion();
        camAnim.SetBool("shake", true);
        em.SetLensDistortionIntensity(-0.6f);
    }

    public void FireBullet()
    {
        Instantiate(BulletPrefab, currentPlayerBox.transform.position, Quaternion.identity);
        currentPlayer._canShoot = true;
        am.Play("shoot", Random.Range(0.7f, 0.8f));
    }

    public void FireUltimateBullet()
    {
        Instantiate(UltimateBullet, currentPlayerBox.transform.position, Quaternion.identity);
        currentPlayer._canShoot = true;
        am.Play("fire", Random.Range(1f, 1.2f));
        StartCoroutine(UltimateDowntime(fireDownTime));
    }

    public void FireEnd()
    {
        tm.StopSlowMotion();
        camAnim.SetBool("shake", false);
        em.SetLensDistortionIntensity(0f);
        currentAnimator.updateMode = AnimatorUpdateMode.Normal;
    }

    public void ShieldInit()
    {
        currentPlayer._isShielded = true;
        ShieldDeactivate();
        currentAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        tm.StartSlowMotion();
    }

    public void ShieldActivate()
    {
        TrailRenderer tr = Shield.GetComponent<TrailRenderer>();
        tr.emitting = true;
        am.Play("shield");
        Shield.SetActive(true);
        tm.StopSlowMotion();
        currentAnimator.updateMode = AnimatorUpdateMode.Normal;
        StartCoroutine(ShieldDuration());
    }

    public void UltimateShieldActivate()
    {
        currentPlayer.GetComponent<Health>().HealPlayers();
        am.Play("shield");
        TrailRenderer tr = UltimateShield.GetComponent<TrailRenderer>();
        tr.emitting = true;
        UltimateShield.SetActive(true);

        am.Play("shield");
        TrailRenderer tr1 = MirrorShield.GetComponent<TrailRenderer>();
        tr1.emitting = true;
        MirrorShield.SetActive(true);

        tm.StopSlowMotion();
        currentAnimator.updateMode = AnimatorUpdateMode.Normal;
        StartCoroutine(UltimateShieldDuration());
    }

    IEnumerator UltimateShieldDuration()
    {
        TrailRenderer tr = UltimateShield.GetComponent<TrailRenderer>();
        TrailRenderer tr1 = MirrorShield.GetComponent<TrailRenderer>();
        yield return new WaitForSecondsRealtime(ultimateShieldDuration);
        tr.emitting = false;
        tr1.emitting = false;
        yield return new WaitForSecondsRealtime(0.05f);
        UltimateShield.SetActive(false);
        MirrorShield.SetActive(false);
        currentPlayer._isShielded = false;
        currentPlayer._canShield = true;
        currentPlayer._ultimateInUse = false;
        StartCoroutine(UltimateDowntime(shieldDownTime));
    }

    IEnumerator UltimateDowntime(float downTime)
    {
        yield return new WaitForSecondsRealtime(downTime);
        currentPlayer._ultimateReady = true;
    }

    IEnumerator ShieldDuration()
    {
        TrailRenderer tr = Shield.GetComponent<TrailRenderer>();
        yield return new WaitForSecondsRealtime(shieldDuration);
        tr.emitting = false;
        yield return new WaitForSecondsRealtime(0.05f);
        Shield.SetActive(false);
        currentPlayer._isShielded = false;
        currentPlayer._selfAbilityInUse = false;
    }

    public void ShieldDeactivate()
    {
        TrailRenderer tr = Shield.GetComponent<TrailRenderer>();
        tr.emitting = false;
        Shield.SetActive(false);

        TrailRenderer tr1 = UltimateShield.GetComponent<TrailRenderer>();
        tr1.emitting = false;
        UltimateShield.SetActive(false);

        TrailRenderer tr2 = MirrorShield.GetComponent<TrailRenderer>();
        tr2.emitting = false;
        MirrorShield.SetActive(false);
    }

    public void Hit()
    {
        tm.StopSlowMotion();
        camAnim.SetBool("shake", false);
        em.SetLensDistortionIntensity(0f);
        currentAnimator.updateMode = AnimatorUpdateMode.Normal;
    }

}
