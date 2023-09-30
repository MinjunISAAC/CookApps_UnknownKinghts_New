using Core.ForData.ForUserLevel;
using Core.ForData.ForUserSave;
using InGame.ForChapterGroup;
using InGame.ForLevel.ForChapter;
using InGame.ForLevel.ForStage;
using InGame.ForState;
using InGame.ForUI;
using InGame.ForUI.ForOption;
using InGame.ForUnit.ForData;
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
        [Header("1. Chapter Data")]
        [SerializeField] private ChapterGroup _chapterGroup = null;

        [Header("2. UI Group")]
        [SerializeField] private UIOwner      _uiOwner      = null;

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
            // 3. Chapter Data Load
            JsonParser           .LoadJson();
            UnitDefaultDataHelper.OnInit();
            UserSaveDataManager  .Load();
            UserSaveDataManager  .OnInit();
            UserLevelDataHelper  .OnInit();
            _chapterGroup        .OnInit();

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
        public StateView GetToStateUI() 
        => _uiOwner.GetStateUI();
        
        public void SetToOptionView(EGameState gameState, int coinValue, int gemValue, int breadValue, int maxBreadValue)
        => _uiOwner.SetToOptionUI(gameState, coinValue, gemValue, breadValue, maxBreadValue);

        public Chapter GetToChapterData(int chapterStep)
        => _chapterGroup.GetToChapter(chapterStep);

        public Stage GetToStageData(int chapterStep, int stageStep)
        => _chapterGroup.GetToStage(chapterStep, stageStep);
    }
}