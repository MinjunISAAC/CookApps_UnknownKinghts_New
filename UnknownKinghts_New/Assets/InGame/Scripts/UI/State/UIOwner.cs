// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForState.ForUI;
using InGame.ForState;
using InGame.ForUI.ForOption;

namespace InGame.ForUI
{
    public class UIOwner : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. State View Group")]
        [SerializeField] private VillageView       _villageView       = null;
        [SerializeField] private ChapterSelectView _chapterSelectView = null;

        [Header("2. Option View Group")]
        [SerializeField] private OptionView  _optionView  = null;



        //[SerializeField] private ChapterSelectView _chapterSelectView = null;
        //[SerializeField] private BuildDeckView _buildDeckView = null;
        //[SerializeField] private BattleView _battleView = null;
        //[SerializeField] private ResultView _resultView = null;

        //[Header("ETC")]
        //[SerializeField] private ToastMessageView _toastMessage = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        //public ToastMessageView ToastMessage => _toastMessage;

        // --------------------------------------------------
        // Fucntions - Nomal
        // --------------------------------------------------
        public StateView GetStateUI()
        {
            var currentState = Game_StateMachine.Instance.CurrentState;

            switch (currentState)
            {
                case EGameState.Village      : return _villageView;
                case EGameState.ChapterSelect: return _chapterSelectView;

                //case EStateType.BuildDeck: return _buildDeckView;
                //case EStateType.Battle: return _battleView;
                //case EStateType.Result: return _resultView;
                default: return null;
            }
        }

        public void SetToOptionUI(EGameState gameState, int coinValue, int gemValue, int breadValue, int maxBreadValue)
        => _optionView.Visiable(gameState, coinValue, gemValue, breadValue, maxBreadValue);
    }
}