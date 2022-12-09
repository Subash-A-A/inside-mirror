using UnityEngine;
using System.Collections;

public class AnimationEventManager : MonoBehaviour
{
    [SerializeField] private GameObject currentPlayerBox;
    [SerializeField] private GameObject mirrorPlayerBox;
    [SerializeField] private PlayerController mirrorPlayer;
    [SerializeField] private Animator camAnim;
    [SerializeField] private TimeManager tm;
    [SerializeField] private EffectManager em;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject Shield;

    private PlayerController currentPlayer;

    private void Start()
    {
        currentPlayer = GetComponent<PlayerController>();
    }

    public void SwitchInit()
    {
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
    }

    public void FireInit()
    {
        tm.StartSlowMotion();
        camAnim.SetBool("shake", true);
        em.SetLensDistortionIntensity(-0.6f);
    }

    public void FireBullet()
    {
        Instantiate(BulletPrefab, currentPlayerBox.transform.position, Quaternion.identity);
    }

    public void FireEnd()
    {
        tm.StopSlowMotion();
        camAnim.SetBool("shake", false);
        em.SetLensDistortionIntensity(0f);
    }

    public void ShieldInit()
    {
        currentPlayer._isShielded = true;
    }

    public void ShieldActivate()
    {
        TrailRenderer tr = Shield.GetComponent<TrailRenderer>();
        tr.emitting = true;
        Shield.SetActive(true);
        StartCoroutine(ShieldDuration());
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

}
