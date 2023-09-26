using UnityEngine;

namespace InGame.ForLevel.ForStage
{
    [CreateAssetMenu(menuName = "Chapter Group/Create To Stage", fileName = "Stage")]
    public class Stage : ScriptableObject
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Step Number")]
        [SerializeField] private int _stageStep = 0;

        /*
        [Header("2. Reward Group")]
        [SerializeField] private List<RewardItemData> _rewardList = null;

        [Header("3. Unit Group")]
        [SerializeField] private List<UnitData> _unitList = null;

        [Header("4. Play Option")]
        [SerializeField] private float _playTime = 0f;
        */

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public int StageStep => _stageStep;
        /*
        public List<RewardItemData> RewardList => _rewardList;
        public List<UnitData> UnitList => _unitList;
        public float PlayTime => _playTime;
        */
        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        /*
        public int GetToCoin()
        {
            for (int i = 0; i < _rewardList.Count; i++)
            {
                var item = _rewardList[i];
                if (item.Type == ERewardType.Coin)
                    return item.Value;
            }
            return 0;
        }

        public int GetToGem()
        {
            for (int i = 0; i < _rewardList.Count; i++)
            {
                var item = _rewardList[i];
                if (item.Type == ERewardType.Gem)
                    return item.Value;
            }
            return 0;
        }

        public int GetToExp()
        {
            for (int i = 0; i < _rewardList.Count; i++)
            {
                var item = _rewardList[i];
                if (item.Type == ERewardType.Exp)
                    return item.Value;
            }
            return 0;
        }
        */
    }
}