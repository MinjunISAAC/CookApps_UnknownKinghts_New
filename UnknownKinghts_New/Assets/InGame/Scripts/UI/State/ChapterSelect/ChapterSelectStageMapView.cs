using Core.ForData.ForUserSave;
using InGame.ForLevel.ForChapter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForState.ForUI
{
    public class ChapterSelectStageMapView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private List<StageButton> _stageButtonGroup = null;
        [SerializeField] private RectTransform     _RECT_Focus       = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToStageMapView(Chapter chapterData, Dictionary<int, UserSaveData.ClearData> clearDataSet, Action<int, int> onClickStage)
        {
            for (int i = 0; i < chapterData.StageQuantity; i++)
            {
                var chapterStep = chapterData.Step;
                var stageStep   = i + 1;

                if (clearDataSet == null)
                    _stageButtonGroup[i].OnInit(null, chapterStep, stageStep, onClickStage);
                else
                {
                    if (clearDataSet.TryGetValue(stageStep, out var clearData))
                        _stageButtonGroup[i].OnInit(clearData, chapterStep, stageStep, onClickStage);
                    else
                        _stageButtonGroup[i].OnInit(null, chapterStep, stageStep, onClickStage);
                }
            }
        }

        public Vector2 GetToTargetChapterItemPos(int stageStep)
        {
            var prevPos = _stageButtonGroup[stageStep - 1].RectTrans.anchoredPosition - _RECT_Focus.anchoredPosition;
            return -1f * prevPos;
        }

        public void VisiableToFocus(int stageStep)
        {
            for (int i = 0; i < _stageButtonGroup.Count; i++)
            {
                var stageButton = _stageButtonGroup[i];
                stageButton.VisiableToFocusFx(false);
            }

            _stageButtonGroup[stageStep - 1].VisiableToFocusFx(true);
        }
    }
}