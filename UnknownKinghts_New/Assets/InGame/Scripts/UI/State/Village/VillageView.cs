using InGame.ForUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForUI
{
    public class VillageView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Profile")]
        [SerializeField] private ProfileView       _profileView       = null;

        [Header("2. Bottom")]
        [SerializeField] private VillageBottomView _villageBottomView = null;
        
        // --------------------------------------------------
        // Functions
        // --------------------------------------------------
        public override void OnFinish() {}
        public override void OnInit  () {}

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToProfileView(string userId, int userLevel, int userExp, int levelUpExp)
        => _profileView.OnInit(userId, userLevel, userExp, levelUpExp);

        public void SetToBottomView(Action<EBattleType> onClickBattleEnter)
        => _villageBottomView.SetToBattleItemOnClick(onClickBattleEnter);

        public void FocusToBattleItem(EBattleType battleType)
        => _villageBottomView.FocusToBattleItem(battleType);

        public void RefreshToProfileView(int userLevel, int userExp, int levelUpExp)
        => _profileView.RefreshToUserInfo(userLevel, userExp, levelUpExp);

    }
}