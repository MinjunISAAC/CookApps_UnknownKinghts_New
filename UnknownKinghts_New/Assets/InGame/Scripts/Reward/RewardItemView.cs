using InGame.ForLevel.ForReward;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForUI
{
    public class RewardItemView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. UI Group")]
        [SerializeField] private Image           _IMG_Star       = null;
        [SerializeField] private Image           _IMG_Frame      = null;
        [SerializeField] private Image           _IMG_Icon       = null;
        [SerializeField] private TextMeshProUGUI _TMP_Value      = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit(StageRewardData data)
        {
            var value       = data.Value;
            var frameSprite = data.FrameSprite;
            var iconSprite  = data.IconSprite;
            var starSprite  = data.StarSprite;

            _IMG_Star .sprite = starSprite;
            _IMG_Frame.sprite = frameSprite;
            _IMG_Icon .sprite = iconSprite;
            _TMP_Value.text   = _Format(value);

            if (starSprite == null)
                _IMG_Star.gameObject.SetActive(false);
        }

        // ----- Private
        private string _Format(int value)
        => string.Format("{0:#,##0}", value);
    }
}