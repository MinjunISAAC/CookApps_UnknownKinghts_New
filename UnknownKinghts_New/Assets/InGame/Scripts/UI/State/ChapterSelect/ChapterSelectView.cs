// ----- C#
using System;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using InGame.ForUI;
using InGame.ForLevel.ForChapter;
using Core.ForData.ForUserSave;
using System.Collections.Generic;
using InGame.ForLevel.ForReward;
using InGame.ForLevel.ForStage;

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
        [SerializeField] private ChapterSelectStageInfoView _stageInfoView = null;

        [Header("3. Stage Enter Group")]
        [SerializeField] private ChapterSelectStageEnterView _stageEnterView = null;

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
            _stageEnterView.SetToStageInfo (stageData.DifficultType, chapterData.Step, stageData.StageStep);
            
            _stageEnterView.SetToCloseView
            (
                () => { _stageEnterView.gameObject.SetActive(false); }
            );
        }
    }
}