    
using System;
using System.Collections.Generic;
using Assets.Scripts.SerializedObject;
using Enum;
using UnityEngine;

namespace Assets.Scripts.Sound
{

    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _bgAudioSource;
        [SerializeField] private AudioSource[] _sfxAudioSource;
        [SerializeField] private List<AudioTarget> _audioTargetList =  new List<AudioTarget>();
        private Dictionary<SfxSounds, AudioClip> _sfxSoundDictionary = new Dictionary<SfxSounds, AudioClip>();
        private int _index = 0;
        
        #region Instance

        private static SoundController _instance;

        public static SoundController Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("SoundController");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<SoundController>();
                    DontDestroyOnLoad(_instance);
                }
           
                return _instance;
            }
        }

        #endregion

#if UNITY_EDITOR

        public void UdpateNames()
        {
            for (int i = 0; i < _audioTargetList.Count; i++)
            {
                _audioTargetList[i].name = _audioTargetList[i].SfxSounds.ToString();
            }
        }
#endif
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                PrepareSfxDictionary();
            }
            else
                Destroy(gameObject);
        }


        private void Start()
        {
            _bgAudioSource.Play(3);
            _bgAudioSource.loop = true;
            UpdateVolumeMusic();
            UpdateVolumeSfx();
        }

        
        public void OnMusicSettingsChange()
        {
            UpdateVolumeMusic();
            UpdateVolumeSfx();
        }

        public void UpdateVolumeMusic()
        {
            _bgAudioSource.volume = PlayerPrefs.GetInt("MusicEnable", PlayerPrefs.GetInt("MusicEnable", 1) == 1 ? 1 : 0);
        }

        public void UpdateVolumeSfx()
        {
            foreach (var sfxAudio in _sfxAudioSource)
            {
                sfxAudio.volume = PlayerPrefs.GetInt("SFXEnable", PlayerPrefs.GetInt("SFXEnable", 1) == 1 ? 1 : 0);
            }
        }

        public void PlaySfxSound(SfxSounds sfxSound)
        {
            AudioClip targetAudioClip = null;
            if (GetAudioClip(sfxSound, out targetAudioClip))
            {
                AudioSource targetSfxAudioSource = GetSfxAudioSource();
                targetSfxAudioSource.clip = targetAudioClip;
                targetSfxAudioSource.loop = false;
                targetSfxAudioSource.Play();

               // Debug.LogFormat("{0} : {1}", targetSfxAudioSource.gameObject.name, targetAudioClip.name);
            } 
        }

        public void GameArenaBattle()
        {
            _bgAudioSource.Pause();
        }

        public void GameArenaEndBattle()
        {
            _bgAudioSource.Play();
        }
        
        #region getAudioSourceForPlay

        private AudioSource GetSfxAudioSource()
        {
            return _sfxAudioSource[GetIndex()];
        }

        private int GetIndex()
        {
            if (++_index % _sfxAudioSource.Length == 0)
                _index = 0;
            return _index;
        }

        #endregion

        #region Get Audio Clip

        private bool GetAudioClip(SfxSounds sfxSound, out AudioClip audioClip)
        {
            if (_sfxSoundDictionary.ContainsKey(sfxSound))
            {
                audioClip = _sfxSoundDictionary[sfxSound];
                return true;
            }

            audioClip = null;
            return false;
        }

        private void PrepareSfxDictionary()
        {
            foreach (var audioTarget in _audioTargetList)
            {
                if (_sfxSoundDictionary.ContainsKey(audioTarget.SfxSounds))
                {
                    _sfxSoundDictionary[audioTarget.SfxSounds] = audioTarget.AudioClip;
                }
                else
                {
                    _sfxSoundDictionary.Add(audioTarget.SfxSounds, audioTarget.AudioClip);
                }
            }
        }

        #endregion      

    }  
}