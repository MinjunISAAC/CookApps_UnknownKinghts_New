// ----- C#
using System;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using InGame.ForUI;
using TMPro;

namespace InGame.ForState.ForUI
{
    public class ChapterSelectStageInfoView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. UI Group")]
        [SerializeField] private TextMeshProUGUI _TMP_ChapterStep = null;
        [SerializeField] private TextMeshProUGUI _TMP_ChpaterName = null;

        [Header("2. Stage Info Group")]
        [SerializeField] private StageInfoView _nomalStageGroup = null;
        [SerializeField] private StageInfoView _hardStageGroup  = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToChapterInfos(int chapterStep, string chapterName, int clearStarCount, int stageCount, Action onClickStage)
        {
            _TMP_ChapterStep.text = $"Chapter {chapterStep}";
            _TMP_ChpaterName.text = $"{chapterName}";

            _nomalStageGroup.SetToChapterInfo(clearStarCount, stageCount, onClickStage);
        }
    }
}