using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DamageVignette : MonoBehaviour
{

    PostProcessVolume m_Volume;
    Vignette m_Vignette;
    public float vignetteIntensityIncrement;

    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(0f);
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    public void IncreaseVignetteIntensity(float value)
    {
        m_Vignette.intensity.value += value;
    }

    private void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
