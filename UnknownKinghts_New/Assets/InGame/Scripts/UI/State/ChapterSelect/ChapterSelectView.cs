// ----- C#
using System;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using InGame.ForUI;
using InGame.ForLevel.ForChapter;
using Core.ForData.ForUserSave;

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
        [SerializeField] private ChapterSelectStageInfoView _stageInfoView;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Action _onClickReturn = null;

        // --------------------------------------------------
        // Functions
        // --------------------------------------------------
        public override void OnInit  () { }
        public override void OnFinish() { }

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
    }
}