using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForUI
{
    public class StageInfoView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. UI Group")]
        [SerializeField] private Button          _BTN_Chapter     = null;
        [SerializeField] private TextMeshProUGUI _TMP_ChapterInfo = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const int MAX_STAR = 3;

        // ----- Private
        private Action _onClickStage = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void OnDestroy()
        {
            _BTN_Chapter.onClick.RemoveAllListeners();
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToChapterInfo(int clearStar, int stageCount, Action onClickStage)
        {
            _TMP_ChapterInfo.text = $"{clearStar}/{stageCount * MAX_STAR}";

            if (_onClickStage == null)
            {
                _onClickStage = onClickStage;
                _BTN_Chapter.onClick.AddListener(() => { _onClickStage(); });
            }
        }
    }
}