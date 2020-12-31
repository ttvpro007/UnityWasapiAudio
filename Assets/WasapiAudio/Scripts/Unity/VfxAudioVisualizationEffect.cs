﻿using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Assets.WasapiAudio.Scripts.Unity
{
    public abstract class VfxAudioVisualizationEffect : VFXBinderBase
    {
        // Inspector Properties
        public WasapiAudioSource WasapiAudioSource;

        protected int SpectrumSize { get; private set; }

        public override bool IsValid(VisualEffect component)
        {
            return WasapiAudioSource != null;
        }

        public override void UpdateBinding(VisualEffect component)
        {
            SpectrumSize = WasapiAudioSource.SpectrumSize;
        }

        protected float[] GetSpectrumData()
        {
            return WasapiAudioSource.GetSpectrumData();
        }
    }
}
