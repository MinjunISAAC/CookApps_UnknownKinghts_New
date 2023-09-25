using InGame.ForCurrency;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InGame.ForUI.ForOption
{
    public class CurrencyView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Currency Type")]
        [SerializeField] private ECurrencyType _currencyType = ECurrencyType.Unknown;

        [Header("2. UI Components")]
        [SerializeField] private TextMeshProUGUI _TMP_Currency = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        // ----- Public
        public void RefreshToCurreny(int value)
        {
            if (_TMP_Currency == null)
            {
                Debug.LogError($"<color=red>[CurrencyView.RefreshToCurreny] {_currencyType}의 View가 존재하지 않습니다.</color>");
            }

            _TMP_Currency.text = _Format(value);
        }

        public void RefreshToCurreny(int value, int maxValue)
        {
            if (_TMP_Currency == null)
            {
                Debug.LogError($"<color=red>[CurrencyView.RefreshToCurreny] {_currencyType}의 View가 존재하지 않습니다.</color>");
            }

            _TMP_Currency.text = _Format(value, maxValue);
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public

        // ----- Private
        private string _Format(int value) => string.Format("{0:n0}", value);
        private string _Format(int value, int maxValue) => $"{value}/{maxValue}";
    }
}