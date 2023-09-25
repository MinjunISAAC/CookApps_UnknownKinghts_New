using InGame.ForState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForUI.ForOption
{
    public class OptionView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Currency Group")]
        [SerializeField] private CurrencyView _coinView  = null;
        [SerializeField] private CurrencyView _gemView   = null;
        [SerializeField] private CurrencyView _breadView = null;

        [Header("2. Setting Group")]
        [SerializeField] private Button _BTN_Friend  = null;
        [SerializeField] private Button _BTN_Alarm   = null;
        [SerializeField] private Button _BTN_Mail    = null;
        [SerializeField] private Button _BTN_Setting = null;

        [Header("3. Empty Group")]
        [SerializeField] private GameObject _empty_0 = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void Visiable(EGameState gameState, int coinValue, int gemValue, int breadValue, int maxBreadValue)
        {
            _HideToAll();

            switch (gameState)
            {
                case EGameState.Village:
                    _SetToCoinView (true, coinValue                );
                    _SetToGemView  (true, gemValue                 );
                    _SetToBreadView(true, breadValue, maxBreadValue);
                    _BTN_Friend .gameObject.SetActive(true);
                    _BTN_Alarm  .gameObject.SetActive(true);
                    _BTN_Mail   .gameObject.SetActive(true);
                    _BTN_Setting.gameObject.SetActive(true);
                    break;

                case EGameState.ChapterSelect:
                    _SetToCoinView (true, coinValue                );
                    _SetToGemView  (true, gemValue                 );
                    _SetToBreadView(true, breadValue, maxBreadValue);
                    _BTN_Friend .gameObject.SetActive(true);
                    _BTN_Alarm  .gameObject.SetActive(true);
                    _BTN_Mail   .gameObject.SetActive(true);
                    break;

                case EGameState.BuildDeck:
                    _SetToBreadView(true, breadValue, maxBreadValue);
                    _empty_0.gameObject.SetActive(true);
                    break;

                default: break;
            }
        }

        // ----- Private
        private void _SetToCoinView(bool isShow, int value = 0)
        {
            _coinView.gameObject.SetActive(isShow);
            
            if (isShow)
                _coinView.RefreshToCurreny(value);
        }

        private void _SetToGemView(bool isShow, int value = 0)
        {
            _gemView.gameObject.SetActive(isShow);

            if (isShow)
                _gemView.RefreshToCurreny(value);
        }

        private void _SetToBreadView(bool isShow, int value = 0, int maxValue = 0)
        {
            _breadView.gameObject.SetActive(isShow);

            if (isShow)
                _breadView.RefreshToCurreny(value, maxValue);
        }

        private void _HideToAll()
        {
            _SetToCoinView(false);
            _SetToGemView(false);
            _SetToBreadView(false);

            _BTN_Friend .gameObject.SetActive(false);
            _BTN_Alarm  .gameObject.SetActive(false);
            _BTN_Mail   .gameObject.SetActive(false);
            _BTN_Setting.gameObject.SetActive(false);

            _empty_0.gameObject.SetActive(false);
        }
    }
}