using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScaleFromAudioClip : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private Vector3 minScale;

    [SerializeField]
    private Vector3 maxScale;
    
    [SerializeField, ValueDropdown("MicrophoneDevices")]
    private string mic;
    private string lastMic;

    private static IEnumerable MicrophoneDevices()
    {
        return Microphone.devices;
    }

    [SerializeField]
    private float threshold = 0.01f;
    private AudioClip micClip;

    private void Start()
    {
        SetMic();
    }

    private void Update()
    {
        float spectrum = AudioSpectrum.GetSpectrumValue(Microphone.GetPosition(mic), micClip);

        if (spectrum < threshold)
            spectrum = 0f;

        transform.localScale = Vector3.Lerp(minScale, maxScale, spectrum);
    }

    private void OnValidate()
    {
        if (mic != lastMic && Application.isPlaying)
        {
            if (Microphone.IsRecording(lastMic))
                Microphone.End(lastMic);

            SetMic();
        }
    }

    private void SetMic()
    {
        // test try catch
        try
        {
            micClip = Microphone.Start(mic, true, 20, AudioSettings.outputSampleRate);
            source.clip = micClip;
            lastMic = mic;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
