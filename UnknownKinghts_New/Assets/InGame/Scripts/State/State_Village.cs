using Core.ForData.ForUserLevel;
using Core.ForData.ForUserSave;
using InGame.ForState;
using InGame.ForState.ForUI;
using InGame.ForUI.ForOption;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.ForStateMachine;

namespace InGame.ForState
{
    public class State_Village : SimpleState<EGameState>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner _owner = null;

        // ----- UI
        private VillageView _villageView = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public override EGameState State => EGameState.Village;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Protected Game Flow
        protected override void _Start(EGameState preStateKey, object startParam)
        {
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 진입하였습니다.</color>");

            #region <Manage Group>
            _owner = Owner.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Owner가 Null 상태입니다.</color>");
                return;
            }

            _villageView = (VillageView)_owner.GetToStateUI();
            if(_villageView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // 1. State UI 초기화
            _SetToProfileView();
            _SetToBottomView ();
            
            // 2. Option View 초기화
            var userLevel = UserSaveDataManager.GetToUserLevel();
            var userCoin  = UserSaveDataManager.GetToCoin     ();
            var userGem   = UserSaveDataManager.GetToGem      ();
            var userBread = UserSaveDataManager.GetToBread    ();
            var maxBread  = UserLevelDataHelper.GetToMaxBread (userLevel);

            _owner.SetToOptionView(State, userCoin, userGem, userBread, maxBread);
        }

        private void _SetToProfileView()
        {
            var userId       = UserSaveDataManager.GetToUserName ();
            var userLevel    = UserSaveDataManager.GetToUserLevel();
            var userExp      = UserSaveDataManager.GetToUserExp  ();
            var userLevelExp = UserLevelDataHelper.GetToLevelUpExp(userLevel);

            _villageView.SetToProfileView(userId, userLevel, userExp, userLevelExp);
        }

        private void _SetToBottomView()
        {
            _villageView.SetToBottomView((type) => _OnClickToBattleEnter(type));
            _villageView.FocusToBattleItem(EBattleType.Adventure);
        }

        private void _OnClickToBattleEnter(EBattleType battleType)
        {
            switch (battleType)
            {
                case EBattleType.Adventure:
                    Game_StateMachine.Instance.ChangeState(EGameState.ChapterSelect);
                    break;

                default:
                    // [TODO] Toast Message Show
                    break;
            }
        }
    }
}