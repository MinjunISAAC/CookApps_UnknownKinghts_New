using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForUI
{
    public class BattleItem : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Battle Type")]
        [SerializeField] private EBattleType   _battleType       = EBattleType.Unknown;

        [Header("2. UI Group")]
        [SerializeField] private Button        _BTN_BattleEnter  = null;

        [Header("3. Rect Transform Group")]
        [SerializeField] private RectTransform _RECT_Frame       = null;
        [SerializeField] private RectTransform _RECT_BottomFrame = null;
        [SerializeField] private RectTransform _RECT_Icon        = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Vector2 _originFrameSize       = Vector2.zero;
        private Vector2 _originBottomFrameSize = Vector2.zero;
        private Vector2 _originIcon            = Vector2.zero;

        private Action<EBattleType> _battleEnterOnClick = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EBattleType BattleType => _battleType;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Start()
        {
            _originFrameSize       = _RECT_Frame      .rect.size;
            _originBottomFrameSize = _RECT_BottomFrame.sizeDelta;
            _originIcon            = _RECT_Icon       .rect.size;
            Debug.Log($"A {_originFrameSize} / B {_originBottomFrameSize} / C {_originIcon}");
        }

        private void OnDestroy()
        {
            _BTN_BattleEnter.onClick.RemoveAllListeners();
            _battleEnterOnClick = null;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToEnterButton(Action<EBattleType> battleEnterOnClick)
        {
            if (_battleEnterOnClick == null)
            {
                _battleEnterOnClick = battleEnterOnClick;
                _BTN_BattleEnter.onClick.AddListener(() => { _battleEnterOnClick(_battleType); });
            }
        }

        public void Focus(bool isFocus)
        {
            if (isFocus)
            {
                var frameSize   = _originFrameSize;
                var bottomFrame = _originBottomFrameSize;
                
                frameSize  .x = frameSize  .x * 1.25f;

                bottomFrame.x = _RECT_BottomFrame.sizeDelta.x;
                bottomFrame.y = bottomFrame.y * 1.25f;
                
                _RECT_Frame      .sizeDelta = frameSize;
                _RECT_BottomFrame.sizeDelta = bottomFrame;
                _RECT_Icon.sizeDelta        = _originIcon * 1.25f;
            }
            else
            {
                _RECT_Frame      .sizeDelta = _originFrameSize;
                _RECT_BottomFrame.sizeDelta = _originBottomFrameSize;
                _RECT_Icon       .sizeDelta = _originIcon;
            }
        }
    }
}