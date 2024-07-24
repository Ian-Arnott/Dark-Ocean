using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkParticleController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float sensitivity = 100f;
    private ParticleSystem.EmissionModule _emissionModule;
    private float[] _spectrumData = new float[256];
    void Start()
    {
        if (_audioSource == null || _particleSystem == null)
        {
            Debug.LogError("AudioSource or ParticleSystem is not assigned.");
            return;
        }
        _emissionModule = _particleSystem.emission;
    }

    void Update()
    {
        _audioSource.GetSpectrumData(_spectrumData,0,FFTWindow.BlackmanHarris);

        float avgSpectrumValue = 0f;
        for (int i = 0; i < _spectrumData.Length; i++)
        {
            avgSpectrumValue += _spectrumData[i];
        }
        avgSpectrumValue /= _spectrumData.Length;

        _emissionModule.rateOverTime = avgSpectrumValue * sensitivity;
    }
}
