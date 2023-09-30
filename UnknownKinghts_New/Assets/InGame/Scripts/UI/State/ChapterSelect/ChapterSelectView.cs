// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using Core.ForData.ForUserSave;
using InGame.ForUI;
using InGame.ForLevel.ForChapter;
using InGame.ForLevel.ForReward;
using InGame.ForLevel.ForStage;
using InGame.ForMap;

namespace InGame.ForState.ForUI
{
    public class ChapterSelectView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. UI Group")]
        [SerializeField] private Button _BTN_Return = null;
        
        [Header("2. Stage Info Group")]
        [SerializeField] private ChapterSelectStageInfoView  _stageInfoView     = null;

        [Header("3. Stage Enter Group")]
        [SerializeField] private ChapterSelectStageEnterView _stageEnterView    = null;

        [Header("4. Stage Map Group")]
        [SerializeField] private ChapterSelectStageMapView   _stageMapView      = null;

        [Header("5. Map Move Controller")]
        [SerializeField] private MapMoveController           _mapMoveController = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Action _onClickReturn = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        public override void OnInit  () { }
        public override void OnFinish() { }

        private void Start()
        {
            _BTN_Return    .gameObject.SetActive(true);
            _stageInfoView .gameObject.SetActive(true);
            _stageEnterView.gameObject.SetActive(true);
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToOnClickReturn(Action onClickReturn)
        {
            if (_onClickReturn == null)
            {
                _onClickReturn = onClickReturn;
                _BTN_Return.onClick.AddListener(() => { _onClickReturn(); });
            }
        }

        public void SetToStageInfoView(Chapter chapterData, Action onClickStage)
        {
            var chapterStep         = chapterData.Step;
            var chapterName         = chapterData.Name;
            var stageCount          = chapterData.StageQuantity;
            var totalClearStageStar = 0;

            for (int i = 0; i < stageCount; i++)
            {
                var data = UserSaveDataManager.GetToClearData(chapterStep, i + 1);
                
                if (data == null)
                    totalClearStageStar += 0;
                else
                    totalClearStageStar += data.Star;
            }

            _stageInfoView.SetToChapterInfos(chapterStep, chapterName, totalClearStageStar, stageCount, onClickStage);
        }

        public void SetToStageEnterView(UserSaveData.ClearData clearData, Chapter chapterData, Stage stageData)
        {
            List<StageRewardData> rewardDatas = stageData.StageRewardDatas;

            _stageEnterView.SetToRewardView(rewardDatas);
            _stageEnterView.SetToClearStar (clearData);
            _stageEnterView.SetToStageInfo (stageData.DifficultType, chapterData.Step, stageData.Step);
            
            _stageEnterView.SetToCloseView
            (
                () => { VisiableStageEnterView(false); }
            );
        }

        public void SetToStageMoveBtn(Action onClickPrevStage, Action onClickNextStage)
        => _stageEnterView.SetToCenter(onClickPrevStage, onClickNextStage);

        public void SetToStageMapView(Chapter chapterData, Dictionary<int, UserSaveData.ClearData> clearDataSet, Action<int, int> onClickStage)
        => _stageMapView.SetToStageMapView(chapterData, clearDataSet, onClickStage);

        public void SetToFocusToStage(bool immediately, int nextStageStep)
        {
            var endPos   = _stageMapView.GetToTargetChapterItemPos(nextStageStep);

            if (immediately) _mapMoveController.MoveToMap(endPos);
            else             _mapMoveController.MoveToMap(endPos, 0.25f, null);

            _stageMapView.VisiableToFocus(nextStageStep);
        }

        public void SetToOnClickBuildDeck(Action onClickBuildDeck)
        => _stageEnterView.SetToOnClickBuildDeck(onClickBuildDeck);

        public void ResetToSelectStageEnterView()
        => _stageEnterView.ResetToClearStar();

        public void VisiableStageEnterView(bool isShow)
        => _stageEnterView.gameObject.SetActive(isShow);
    }
}