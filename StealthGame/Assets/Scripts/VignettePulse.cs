using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignettePulse : MonoBehaviour
{

    PostProcessVolume m_Volume;
    Vignette m_Vignette;
    public float vignetteIntensityIncrement;

    // Start is called before the first frame update
    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(0f);
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    // Update is called once per frame
    void Update()
    {
        //m_Vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
    }

    public void IncreaseVignetteIntensity(float value)
    {
        m_Vignette.intensity.value += value;
        //m_Vignette.intensity.value += Mathf.Sin(Time.realtimeSinceStartup);
    }

    private void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(m_Volume, true, true);
    }
}
