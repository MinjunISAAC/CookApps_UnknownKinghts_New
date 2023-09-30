using Core.ForData.ForUserLevel;
using Core.ForData.ForUserSave;
using InGame.ForLevel.ForChapter;
using InGame.ForLevel.ForStage;
using InGame.ForState.ForData;
using InGame.ForState.ForUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.ForStateMachine;

namespace InGame.ForState
{
    public class State_BuilidDeck : SimpleState<EGameState>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner _owner = null;

        // ----- UI
        private BuildDeckView _buildDeckView = null;

        // ----- Target Level
        private Chapter _targetChapter = null;
        private Stage   _targetStage   = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public override EGameState State => EGameState.BuildDeck;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Protected Game Flow
        protected override void _Start(EGameState preStateKey, object startParam)
        {
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 진입하였습니다.</color>");

            #region <Manage Group>
            _owner = Owner.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Owner가 Null 상태입니다.</color>");
                return;
            }

            _buildDeckView = (BuildDeckView)_owner.GetToStateUI();
            if (_buildDeckView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // 1. 전달 Data 초기화
            var data = (SelectStageInfo)startParam;
            _targetChapter = data.TargetChapterData;
            _targetStage   = data.TargetStageData;

            // 2. State UI 초기화
            _SetToBuildDeckView(data);

            // 2. Option View 초기화
            var userLevel = UserSaveDataManager.GetToUserLevel();
            var userCoin  = UserSaveDataManager.GetToCoin();
            var userGem   = UserSaveDataManager.GetToGem();
            var userBread = UserSaveDataManager.GetToBread();
            var maxBread  = UserLevelDataHelper.GetToMaxBread(userLevel);

            _owner.SetToOptionView(State, userCoin, userGem, userBread, maxBread);
        }

        protected override void _Finish(EGameState nextStateKey)
        {
            _targetChapter = null;
            _targetStage   = null;
            _buildDeckView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 이탈하였습니다.</color>");
        }


        // ----- Only State 
        private void _SetToBuildDeckView(SelectStageInfo infoData)
        {
            _buildDeckView.gameObject.SetActive(true);

            var chapterData = infoData.TargetChapterData;
            var stageData   = infoData.TargetStageData;
            var chapterName = chapterData.Name;
            var chapterStep = chapterData.Step;
            var stageStep   = stageData  .Step;

            _buildDeckView.SetToTopView
            (
                chapterName, chapterStep, stageStep,
                () => 
                {
                    var nextData = new SelectStageInfo();

                    nextData.TargetChapterData = _targetChapter;
                    nextData.TargetStageData   = _targetStage;

                    Game_StateMachine.Instance.ChangeState(EGameState.ChapterSelect, nextData); 
                }
            );

        }
    }
}