
using System;
using System.Collections.Generic;
using Assets.Scripts.SerializedObject;
using UnityEngine;

namespace Assets.Scripts.Settings
{
    public class GameSettingsManager : MonoBehaviour
    {

        private static GameSettingsManager _instace;

        public static GameSettingsManager Instanse
        {
            get
            {
                if (_instace == null)
                {
                    GameObject go = new GameObject("GameSettingsManager");
                    _instace = go.AddComponent<GameSettingsManager>();
                    DontDestroyOnLoad(go);
                }

                return _instace;
            }
        }


        private void Start()
        {
            LoadSettingsFromResources();
        }

        private void LoadSettingsFromResources()
        {
            //todo load resources 
            this.LoadGameSettings();
        }

        #region GameSettingValue

        private GameSettings _gameSettings;
        public int AfterRoundWaiting
        {
            get
            {
                if (_gameSettings == null)
                {
                    LoadGameSettings();
                }

                return _gameSettings.AfterRoundWaiting;
            }
        }

        public int RematchWaitingButton
        {
            get
            {
                if (_gameSettings == null)
                {
                    LoadGameSettings();
                }

                return _gameSettings.RematchWaiting;
            }
        }

        public float AttackTime
        {
            get
            {
                if (_gameSettings == null)
                {
                    LoadGameSettings();
                }

                return _gameSettings.AttackTime;
            }
        }

        public float SubmitExpFriendFight
        {
            get
            {
                if (_gameSettings == null)
                {
                    LoadGameSettings();
                }

                return _gameSettings.SubmitExpFriendFight;
            }
        }
        public float SubmitExpBot
        {
            get
            {
                if (_gameSettings == null)
                {
                    LoadGameSettings();
                }

                return _gameSettings.SubmitExpBot;
            }
        }

        private void LoadGameSettings()
        {
            GameSettings gameSettings = Resources.Load<GameSettings>("Settings/GameSettings");

            if (gameSettings == null)
            {
                throw new Exception("RaceImage in resources is null");
            }
            else
            {
                _gameSettings = gameSettings;
            }
        }

        #endregion
    }
}