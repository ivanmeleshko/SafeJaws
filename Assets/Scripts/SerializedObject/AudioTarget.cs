
using System;
using Enum;
using UnityEngine;

namespace Assets.Scripts.SerializedObject
{
    [Serializable]
    public class AudioTarget
    {
        [SerializeField] public SfxSounds SfxSounds;
        [SerializeField] public AudioClip AudioClip;
        public string name = "Test";
    }
}
