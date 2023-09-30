using Core.ForData.ForUserLevel;
using Core.ForData.ForUserSave;
using InGame.ForLevel.ForChapter;
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
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� �����Ͽ����ϴ�.</color>");

            #region <Manage Group>
            _owner = Owner.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Owner�� Null �����Դϴ�.</color>");
                return;
            }

            _buildDeckView = (BuildDeckView)_owner.GetToStateUI();
            if (_buildDeckView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View�� Null �����Դϴ�.</color>");
                return;
            }
            #endregion

            // 1. ���� Data �ʱ�ȭ
            var data = (SelectStageInfo)startParam;

            // 2. State UI �ʱ�ȭ
            _SetToBuildDeckView(data);

            // 2. Option View �ʱ�ȭ
            var userLevel = UserSaveDataManager.GetToUserLevel();
            var userCoin  = UserSaveDataManager.GetToCoin();
            var userGem   = UserSaveDataManager.GetToGem();
            var userBread = UserSaveDataManager.GetToBread();
            var maxBread  = UserLevelDataHelper.GetToMaxBread(userLevel);

            _owner.SetToOptionView(State, userCoin, userGem, userBread, maxBread);
        }

        protected override void _Finish(EGameState nextStateKey)
        {
            _buildDeckView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� ��Ż�Ͽ����ϴ�.</color>");
        }


        // ----- Only State 
        private void _SetToBuildDeckView(SelectStageInfo infoData)
        {
            _buildDeckView.gameObject.SetActive(true);

            var chapterData = infoData.TargetChapterData;
            var stageData   = infoData.TargetStageData;
            var chapterName = chapterData.Name;
            var chapterStep = chapterData.Step;
            var stageStep   = stageData  .StageStep;

            _buildDeckView.SetToTopView
            (
                chapterName, chapterStep, stageStep,
                () => 
                {

                    var nextData = new SelectStageInfo();

                    nextData.TargetChapterData = chapterData;
                    nextData.TargetStageData   = stageData;

                    Game_StateMachine.Instance.ChangeState(EGameState.ChapterSelect, nextData); 
                }
            );

        }
    }
}