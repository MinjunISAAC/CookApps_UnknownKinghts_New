// ----- C#
using System;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace InGame.ForState.ForUI
{
    public class BuildDeckTopView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private TextMeshProUGUI _TMP_StageName = null;
        [SerializeField] private Button          _BTN_Return    = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Action _onClickReturn = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToTopView(string chapterName, int chapterStep, int stageStep, Action onClickReturn)
        {
            if (_onClickReturn == null)
            {
                _onClickReturn = onClickReturn;
                _BTN_Return.onClick.AddListener(() => { _onClickReturn(); });
            }

            _TMP_StageName.text = $"{chapterName} {chapterStep}-{stageStep}";
        }

    }
}