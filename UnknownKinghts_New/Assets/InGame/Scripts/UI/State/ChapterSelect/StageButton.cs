using Core.ForData.ForUserSave;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForUI
{
    public class StageButton : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private RectTransform   _RECT_Button   = null;
        [SerializeField] private TextMeshProUGUI _TMP_StageStep = null;
        [SerializeField] private List<Image>     _starImgGroup  = null;
        [SerializeField] private Button          _BTN_Stage     = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private int              _chapterStep  = 0;
        private int              _stageStep    = 0;
        private Action<int, int> _onClickStage = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public RectTransform RectTrans => _RECT_Button;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit(UserSaveData.ClearData clearData, int chapterStep, int stageStep, Action<int, int> onClickStage)
        {
            if (_onClickStage == null)
            {
                _chapterStep  = chapterStep;
                _stageStep    = stageStep;
                _onClickStage = onClickStage;

                _TMP_StageStep.text = $"{chapterStep}-{stageStep}";
                _BTN_Stage.onClick.AddListener(() => 
                {
                    Debug.Log($"Check {_chapterStep} - {_stageStep}");
                    _onClickStage(_chapterStep, _stageStep); 
                });
            }

            if (clearData == null)
            {
                for (int i = 0; i < _starImgGroup.Count; i++)
                {
                    var star = _starImgGroup[i];
                    star.gameObject.SetActive(false);
                }
            }
            else
            {
                var clearStar = clearData.Star;
                for (int i = 0; i < clearData.Star; i++)
                {
                    var star = _starImgGroup[i];
                    star.gameObject.SetActive(true);
                }
                for (int i = clearData.Star - 1; i < _starImgGroup.Count; i++)
                {
                    var star = _starImgGroup[i];
                    star.gameObject.SetActive(false);
                }
            }
        }
    }
}