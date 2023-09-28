using InGame.ForLevel.ForReward;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForState.ForUI
{
    public class ChapterSelectRewardView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Reward Item View Group")]
        [SerializeField] private RewardItemView _originRewardItem = null;
        [SerializeField] private Transform      _itemParents      = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private List<RewardItemView> _rewardItemViewGroup = new List<RewardItemView>();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToRewardItems(List<StageRewardData> rewardDataList)
        {
            ResetToRewardItems();

            for (int i = 0; i < rewardDataList.Count; i++)
            {
                var itemData = rewardDataList[i];
                var item     = Instantiate(_originRewardItem, _itemParents);

                item.OnInit(itemData);

                _rewardItemViewGroup.Add(item);
            }

            Debug.Log($"Item View Group 1 {_rewardItemViewGroup.Count}");

        }

        public void ResetToRewardItems()
        {
            Debug.Log($"Item View Group 2 {_rewardItemViewGroup.Count}");
            for (int i = _rewardItemViewGroup.Count - 1; i >= 0; i--)
            {
                Debug.Log($"Item View Group 3 {_rewardItemViewGroup.Count}");
                var itemView = _rewardItemViewGroup[i];
                Destroy(itemView.gameObject);
            }
            _rewardItemViewGroup.Clear();
        }
    }
}