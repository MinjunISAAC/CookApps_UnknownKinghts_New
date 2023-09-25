using Core.ForData.ForUserLevel;
using Core.ForData.ForUserSave;
using InGame.ForState;
using InGame.ForState.ForUI;
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
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� �����Ͽ����ϴ�.</color>");

            #region <Manage Group>
            _owner = Owner.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Owner�� Null �����Դϴ�.</color>");
                return;
            }

            _villageView = (VillageView)_owner.GetToStateUI();
            if(_villageView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View�� Null �����Դϴ�.</color>");
                return;
            }
            #endregion

            // 1. UI �ʱ�ȭ
            _SetToProfileView();
        }

        private void _SetToProfileView()
        {
            var userId       = UserSaveDataManager.GetToUserName ();
            var userLevel    = UserSaveDataManager.GetToUserLevel();
            var userExp      = UserSaveDataManager.GetToUserExp  ();
            var userLevelExp = UserLevelDataHelper.GetToLevelUpExp(userLevel);

            _villageView.SetToProfileView(userId, userLevel, userExp, userLevelExp);
        }
    }
}