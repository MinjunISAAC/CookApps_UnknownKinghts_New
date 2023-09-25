using Core.ForData.ForUserLevel;
using Core.ForData.ForUserSave;
using InGame.ForState.ForUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.ForStateMachine;

namespace InGame.ForState
{
    public class State_ChapterSelect : SimpleState<EGameState>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner             _owner             = null;

        // ----- UI
        private ChapterSelectView _chapterSelectView = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public override EGameState State => EGameState.ChapterSelect;

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

            _chapterSelectView = (ChapterSelectView)_owner.GetToStateUI();
            if (_chapterSelectView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // 1. State UI 초기화
            _SetToChapterSelectView();

            // 2. Option View 초기화
            _SetToOptionView();
        }

        protected override void _Finish(EGameState nextStateKey)
        {
            _chapterSelectView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 이탈하였습니다.</color>");
        }

        // ----- Only State 
        private void _SetToChapterSelectView()
        {
            _chapterSelectView.gameObject.SetActive(true);
        }

        private void _SetToOptionView()
        {
            var userLevel = UserSaveDataManager.GetToUserLevel();
            var userCoin  = UserSaveDataManager.GetToCoin();
            var userGem   = UserSaveDataManager.GetToGem();
            var userBread = UserSaveDataManager.GetToBread();
            var maxBread  = UserLevelDataHelper.GetToMaxBread(userLevel);

            _owner.SetToOptionView(State, userCoin, userGem, userBread, maxBread);
        }
    }
}