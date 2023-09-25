using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InGame.ForState.ForUI
{
    public class ProfileView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. 유저 정보")]
        [SerializeField] private TextMeshProUGUI _TMP_UserId      = null;
        [SerializeField] private TextMeshProUGUI _TMP_UserLevel   = null;

        [Header("2. 경험치")]
        [SerializeField] private TextMeshProUGUI _TMP_UserExp     = null;
        [SerializeField] private RectTransform   _RECT_UserExpBar = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const float PARCENT_VALUE = 100f;

        // ----- Private
        private Vector2 _expRectSize = Vector2.zero;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Start()
        {
            _expRectSize.x = _RECT_UserExpBar.rect.width;
            _expRectSize.y = _RECT_UserExpBar.rect.height;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit(string userId, int userLevel, int userExp, int levelUpExp)
        {
            _TMP_UserId   .text = userId;
            _TMP_UserLevel.text = $"Lv.{userLevel}";
            _TMP_UserExp  .text = $"{userExp}/{levelUpExp}({userExp/levelUpExp * PARCENT_VALUE}%)";

            _expRectSize.x             = (userExp * _expRectSize.x) / (float)levelUpExp;
            _RECT_UserExpBar.sizeDelta = _expRectSize;
        }

        public void SetToUserInfo(string userId, int userLevel)
        {
            _TMP_UserId.text    = userId;
            _TMP_UserLevel.text = $"Lv.{userLevel}";
        }

        public void SetToExp()
        {

        }
    }
}