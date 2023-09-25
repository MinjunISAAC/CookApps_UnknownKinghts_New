using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForState.ForUI
{
    public class VillageBottomView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private List<BattleItem> _battleItemList = new List<BattleItem>();

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<EBattleType, BattleItem> _battleItemSet = new Dictionary<EBattleType, BattleItem>();

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake()
        {
            if (_battleItemSet.Count != 0)
                return;

            for (int i = 0; i < _battleItemList.Count; i++)
            {
                var battleItem = _battleItemList[i];
                _battleItemSet.Add(battleItem.BattleType, battleItem);
            }
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToBattleItemOnClick(Action<EBattleType> onClickBtn)
        {
            for (int i = 0; i < _battleItemList.Count; i++)
            {
                var battleItem = _battleItemList[i];
                battleItem.SetToEnterButton(onClickBtn);
            }
        }

        public void FocusToBattleItem(EBattleType type)
        {
            for (int i = 0; i < _battleItemList.Count; i++)
            {
                var battleItem = _battleItemList[i];
                battleItem.Focus(false);
            }
            
            if (_battleItemSet.TryGetValue(type, out var targetItem))
            {
                targetItem.Focus(true);
            }
        }
    }
}