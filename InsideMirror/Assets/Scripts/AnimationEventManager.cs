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
    [SerializeField] private float shieldDownTime = 20f;
    [SerializeField] private float fireDownTime = 10f;

    private PlayerController currentPlayer;
    private Animator currentAnimator;

    private void Start()
    {
        currentPlayer = GetComponent<PlayerController>();
        currentAnimator = GetComponent<Animator>();
    }

    public void SwitchInit()
    {
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
    }

    public void FireInit()
    {
        currentAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        tm.StartSlowMotion();
        camAnim.SetBool("shake", true);
        em.SetLensDistortionIntensity(-0.6f);
    }

    public void FireBullet()
    {
        Instantiate(BulletPrefab, currentPlayerBox.transform.position, Quaternion.identity);
    }

    public void FireUltimateBullet()
    {
        Instantiate(UltimateBullet, currentPlayerBox.transform.position, Quaternion.identity);
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
        Shield.SetActive(true);

        tm.StopSlowMotion();
        currentAnimator.updateMode = AnimatorUpdateMode.Normal;
        StartCoroutine(ShieldDuration());
    }

    public void UltimateShieldActivate()
    {
        TrailRenderer tr = UltimateShield.GetComponent<TrailRenderer>();
        tr.emitting = true;
        UltimateShield.SetActive(true);

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
        yield return new WaitForSeconds(10f);
        tr.emitting = false;
        tr1.emitting = false;
        yield return new WaitForSeconds(0.05f);
        UltimateShield.SetActive(false);
        MirrorShield.SetActive(false);
        currentPlayer._isShielded = false;
        currentPlayer._canShield = true;
        StartCoroutine(UltimateDowntime(shieldDownTime));
    }

    IEnumerator UltimateDowntime(float downTime)
    {
        yield return new WaitForSeconds(downTime);
        currentPlayer._ultimateReady = true;
    }

    IEnumerator ShieldDuration()
    {
        TrailRenderer tr = Shield.GetComponent<TrailRenderer>();
        yield return new WaitForSeconds(5f);
        tr.emitting = false;
        yield return new WaitForSeconds(0.05f);
        Shield.SetActive(false);
        currentPlayer._isShielded = false;
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

}
