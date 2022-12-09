using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{
    private Volume globalVolume;
    private float lensDistortionIntensity;

    private void Start()
    {
        globalVolume = GetComponent<Volume>();
    }

    private void Update()
    {
        LerpLensDistortionIntensity();
    }


    private void LerpLensDistortionIntensity()
    {
        if (globalVolume.profile.TryGet<LensDistortion>(out LensDistortion ld))
        {
            ld.intensity.value = Mathf.Lerp(ld.intensity.value, lensDistortionIntensity, 10 * Time.unscaledDeltaTime);
        }
    }
    public void SetLensDistortionIntensity(float value)
    {
        lensDistortionIntensity = value;
    }
}
