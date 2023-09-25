using Core.ForData.ForUserLevel;
using Core.ForData.ForUserSave;
using InGame.ForState;
using InGame.ForUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.ForJson;

namespace InGame
{
    public class Owner : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header(". UI Owner")]
        [SerializeField] private UIOwner _uiOwner = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public static Owner NullableInstance
        {
            get;
            private set;
        } = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake() { NullableInstance = this; }

        private IEnumerator Start()
        {
            // [Manage Group Init]
            // 1. Json File Load
            // 2. User Save Data Load
            JsonParser         .LoadJson();
            UserSaveDataManager.Load();
            UserLevelDataHelper.OnInit();

            Game_StateMachine.Instance.ChangeState(EGameState.Village);
            yield return null;
        }

        private void Update()
        {
            Game_StateMachine.Instance.OnUpdate();
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public StateView GetToStateUI() => _uiOwner.GetStateUI();
    }
}