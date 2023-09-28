using Core.ForData.ForUserLevel;
using Core.ForData.ForUserSave;
using InGame.ForLevel.ForChapter;
using InGame.ForLevel.ForStage;
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

        // ----- Target Level
        private Chapter           _targetChapter     = null;
        private Stage             _targetStage       = null;

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

            var chapterStep = UserSaveDataManager.GetToLastChapter();
            var stageStep   = UserSaveDataManager.GetToLastStage  ();
            _targetChapter  = _owner.GetToChapterData(chapterStep);
            _targetStage    = _owner.GetToStageData(chapterStep, stageStep);

            Debug.Log($"Target Stage {_targetStage.StageStep}");
            // 1. State UI 초기화
            _SetToChapterSelectView(_targetChapter, _targetStage);

            // 2. Option View 초기화
            _SetToOptionView();
        }

        protected override void _Finish(EGameState nextStateKey)
        {
            _chapterSelectView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 이탈하였습니다.</color>");
        }

        // ----- Only State 
        private void _SetToChapterSelectView(Chapter chapterData, Stage stageData)
        {
            _chapterSelectView.gameObject.SetActive(true);

            // Stage Info Init
            _chapterSelectView.SetToStageInfoView(chapterData, null);
            _chapterSelectView.SetToOnClickReturn
            (
                () => { Game_StateMachine.Instance.ChangeState(EGameState.Village); }
            );

            // Stage Enter Init
            var clearData = UserSaveDataManager.GetToClearData(chapterData.Step, stageData.StageStep);
            _chapterSelectView.SetToStageEnterView(clearData, chapterData, stageData);
            _chapterSelectView.SetToStageMoveBtn
            (
                () => 
                {
                    if (_targetStage.StageStep > 1)
                    {
                        _chapterSelectView.SetToFocusToStage(false, _targetStage.StageStep, _targetStage.StageStep - 1);
                        _targetStage = _owner.GetToStageData(_targetChapter.Step, _targetStage.StageStep - 1);

                        var clearDatas = UserSaveDataManager.GetToClearData(_targetChapter.Step, _targetStage.StageStep);
                        _chapterSelectView.SetToStageEnterView(clearDatas, _targetChapter, _targetStage);
                    }
                },
                () =>
                {
                    if (_targetStage.StageStep < _targetChapter.StageQuantity)
                    {
                        _chapterSelectView.SetToFocusToStage(false, _targetStage.StageStep, _targetStage.StageStep + 1);
                        _targetStage = _owner.GetToStageData(_targetChapter.Step, _targetStage.StageStep + 1);

                        var clearDatas = UserSaveDataManager.GetToClearData(_targetChapter.Step, _targetStage.StageStep);
                        _chapterSelectView.SetToStageEnterView(clearDatas, _targetChapter, _targetStage);
                    }
                }
            );

            // Stage Map Init
            var clearDataSet = UserSaveDataManager.GetToClearData(chapterData.Step);
            _chapterSelectView.SetToStageMapView
            (
                chapterData, clearDataSet, 
                (chapterStep, stageStep) =>
                {
                    if (_targetStage.StageStep == stageStep)
                        return;

                    _chapterSelectView.SetToFocusToStage(false, _targetStage.StageStep, stageStep);
                    _targetStage = _owner.GetToStageData(_targetChapter.Step, stageStep);

                    var clearDatas = UserSaveDataManager.GetToClearData(_targetChapter.Step, _targetStage.StageStep);
                    _chapterSelectView.SetToStageEnterView(clearDatas, _targetChapter, _targetStage);
                }
            );
            _chapterSelectView.SetToFocusToStage(true, _targetStage.StageStep, _targetStage.StageStep);
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