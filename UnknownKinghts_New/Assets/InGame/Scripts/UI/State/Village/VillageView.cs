using InGame.ForUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForState.ForUI
{
    public class VillageView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. ÇÁ·ÎÇÊ")]
        [SerializeField] private ProfileView _profileView = null;

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
    }
}