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
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� �����Ͽ����ϴ�.</color>");

            #region <Manage Group>
            _owner = Owner.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Owner�� Null �����Դϴ�.</color>");
                return;
            }

            _chapterSelectView = (ChapterSelectView)_owner.GetToStateUI();
            if (_chapterSelectView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View�� Null �����Դϴ�.</color>");
                return;
            }
            #endregion

            // Start Param ���� Ȯ��
            if (startParam != null)
            {
                var data       = (SelectStageInfo)startParam;
                _targetChapter = data.TargetChapterData;
                _targetStage   = data.TargetStageData;
                Debug.Log($"�Ĺ� üũ 2 {_targetChapter.Step}-{_targetStage.Step}");
            }
            else
            {
                var chapterStep = UserSaveDataManager.GetToLastChapter();
                var stageStep   = UserSaveDataManager.GetToLastStage  ();
                _targetChapter  = _owner.GetToChapterData(chapterStep);
                _targetStage    = _owner.GetToStageData(chapterStep, stageStep);
                Debug.Log($"�Ĺ� üũ 3 {_targetChapter.Step}-{_targetStage.Step}");
            }

            // 1. State UI �ʱ�ȭ
            _SetToChapterSelectView(_targetChapter, _targetStage);

            // 2. Option View �ʱ�ȭ
            _SetToOptionView();
        }

        protected override void _Finish(EGameState nextStateKey)
        {
            _chapterSelectView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� ��Ż�Ͽ����ϴ�.</color>");
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
            var clearData = UserSaveDataManager.GetToClearData(chapterData.Step, stageData.Step);
            _chapterSelectView.VisiableStageEnterView(true);
            _chapterSelectView.SetToStageEnterView(clearData, chapterData, stageData);
            _chapterSelectView.SetToStageMoveBtn
            (
                () => 
                {
                    if (_targetStage.Step > 1)
                    {
                        _chapterSelectView.VisiableStageEnterView(true);

                        _chapterSelectView.SetToFocusToStage(false, _targetStage.Step - 1);
                        _targetStage = _owner.GetToStageData(_targetChapter.Step, _targetStage.Step - 1);

                        var clearDatas = UserSaveDataManager.GetToClearData(_targetChapter.Step, _targetStage.Step);
                        _chapterSelectView.SetToStageEnterView(clearDatas, _targetChapter, _targetStage);
                        
                        Debug.Log($"���� üũ {_targetChapter.Step}-{_targetStage.Step}");
                    }
                },
                () =>
                {
                    if (_targetStage.Step < _targetChapter.StageQuantity)
                    {
                        _chapterSelectView.VisiableStageEnterView(true);

                        _chapterSelectView.SetToFocusToStage(false, _targetStage.Step + 1);
                        _targetStage = _owner.GetToStageData(_targetChapter.Step, _targetStage.Step + 1);

                        var clearDatas = UserSaveDataManager.GetToClearData(_targetChapter.Step, _targetStage.Step);
                        _chapterSelectView.SetToStageEnterView(clearDatas, _targetChapter, _targetStage);

                        Debug.Log($"������ üũ {_targetChapter.Step}-{_targetStage.Step}");
                    }
                }
            );
            _EnterToBuildDeck();

            // Stage Map Init
            var clearDataSet = UserSaveDataManager.GetToClearData(chapterData.Step);
            _chapterSelectView.SetToStageMapView
            (
                chapterData, clearDataSet, 
                (chapterStep, stageStep) =>
                {
                    if (_targetStage.Step == stageStep)
                        return;

                    _chapterSelectView.VisiableStageEnterView(true);

                    _chapterSelectView.SetToFocusToStage(false, stageStep);
                    _targetStage = _owner.GetToStageData(_targetChapter.Step, stageStep);

                    var clearDatas = UserSaveDataManager.GetToClearData(_targetChapter.Step, _targetStage.Step);
                    _chapterSelectView.SetToStageEnterView(clearDatas, _targetChapter, _targetStage);
                }
            );
            _chapterSelectView.SetToFocusToStage(true, _targetStage.Step);
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
        
        private void _EnterToBuildDeck()
        {
            _chapterSelectView.SetToOnClickBuildDeck
            (
                () => 
                {
                    var nextData = new SelectStageInfo();
                    
                    nextData.TargetChapterData = _targetChapter;
                    nextData.TargetStageData   = _targetStage;

                    Debug.Log($"Stage Info {_targetChapter.Step}-{_targetStage.Step}");
                    Game_StateMachine.Instance.ChangeState(EGameState.BuildDeck, nextData);  
                }
            );
        }
    }
}