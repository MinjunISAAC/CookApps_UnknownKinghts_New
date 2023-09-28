using Core.ForData.ForUserSave;
using InGame.ForLevel.ForReward;
using InGame.ForLevel.ForStage;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForUI
{
    public class ChapterSelectStageEnterView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Top Group")]
        [SerializeField] private List<Image> _starImgGroup = null;
        [SerializeField] private Button      _BTN_Close    = null;

        [Header("Center Group")]
        [SerializeField] private Button          _BTN_PrevStage = null;
        [SerializeField] private Button          _BTN_NextStage = null;
        [SerializeField] private TextMeshProUGUI _TMP_Difficult = null;
        [SerializeField] private TextMeshProUGUI _TMP_StageInfo = null;

        [Header("Bottom Group")]
        [SerializeField] private ChapterSelectRewardView _rewardView     = null;
        [SerializeField] private Button                  _BTN_BuildDeck = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Action _onClickCloseView  = null;
        private Action _onClickPrevStage  = null;
        private Action _onClickNextStage  = null;
        private Action _onClickBuildDeck = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToCloseView(Action onClickCloseView)
        {
            if (_onClickCloseView == null)
            {
                _onClickCloseView = onClickCloseView;
                _BTN_Close.onClick.AddListener(() => { _onClickCloseView(); });
            }
        }

        public void SetToCenter(Action onClickPrevStage, Action onClickNextStage )
        {
            if (_onClickPrevStage == null && _onClickNextStage == null)
            {
                _onClickPrevStage = onClickPrevStage;
                _onClickNextStage = onClickNextStage;

                _BTN_PrevStage.onClick.AddListener(() => { _onClickPrevStage(); });
                _BTN_NextStage.onClick.AddListener(() => { _onClickNextStage(); });
            }
        }

        public void SetToRewardView(List<StageRewardData> rewardDataList)
        => _rewardView.SetToRewardItems(rewardDataList);

        public void ResetToRewardView()
        => _rewardView.ResetToRewardItems();

        public void SetToClearStar(UserSaveData.ClearData clearData)
        {
            if (clearData == null)
            {
                ResetToClearStar();
                return;
            }

            var clearStar = clearData.Star;
            for (int i = 0; i < clearStar; i++)
            {
                var starImg = _starImgGroup[i];
                starImg.gameObject.SetActive(true);
            }

            for (int i = clearStar - 1; i < _starImgGroup.Count; i++)
            {
                var starImg = _starImgGroup[i];
                starImg.gameObject.SetActive(false);
            }
        }

        public void ResetToClearStar()
        {
            for (int i = 0; i < _starImgGroup.Count; i++)
            {
                var starImg = _starImgGroup[i];
                starImg.gameObject.SetActive(false);
            }
        }

        public void SetToStageInfo(EDifficultType difficultType, int chapterStep, int stageStep)
        {
            if (difficultType == EDifficultType.Nomal)
                _TMP_Difficult.text = $"보통";
            else
                _TMP_Difficult.text = $"어려움";

            _TMP_StageInfo.text = $"{chapterStep}-{stageStep}";
        }

        public void SetToOnClickBuildDeck(Action onClickBuildDeck)
        {
            if (_onClickBuildDeck == null)
            {
                _onClickBuildDeck = onClickBuildDeck;

                _BTN_BuildDeck.onClick.AddListener
                (
                    () => { _onClickBuildDeck(); }
                );
            }
        }

        public void ResetToSelectStageEnterView()
        {
            _onClickBuildDeck = null;
            _BTN_BuildDeck.onClick.RemoveAllListeners();
        }
    }
}