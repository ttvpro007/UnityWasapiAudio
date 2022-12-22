using UnityEngine;

public static class AudioSpectrum
{
    private static int sampleWindow = 64;
    private static float max = 1f;

    public static float GetSpectrumValue(int clipPosition, AudioClip clip)
    {
        int start = Mathf.Max(0, clipPosition - sampleWindow);
        float[] waveData = new float[sampleWindow];

        if (clip && clip.GetData(waveData, start))
        {
            float spectrum = 0f;

            for (int i = 0; i < sampleWindow; i++)
            {
                spectrum += Mathf.Abs(waveData[i]);
            }

            spectrum /= sampleWindow;

            if (max < spectrum) max = spectrum;

            return spectrum / max;
        }

        return 0f;
    }
}
