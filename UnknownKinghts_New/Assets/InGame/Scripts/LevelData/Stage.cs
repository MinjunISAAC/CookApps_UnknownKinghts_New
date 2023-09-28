// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForLevel.ForReward;
using System.Collections.Generic;

namespace InGame.ForLevel.ForStage
{
    [CreateAssetMenu(menuName = "Chapter Group/Create To Stage", fileName = "Stage")]
    public class Stage : ScriptableObject
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Difficulty Type")]
        [SerializeField] private EDifficultType        _difficultType    = EDifficultType.Unknown;

        [Header("2. Step Number")]
        [SerializeField] private int                   _stageStep        = 0;

        [Header("3. Reward Group")]
        [SerializeField] private List<StageRewardData> _stageRewardDatas = null;
        /*
        [Header("3. Unit Group")]
        [SerializeField] private List<UnitData> _unitList = null;

        [Header("4. Play Option")]
        [SerializeField] private float _playTime = 0f;
        */

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EDifficultType        DifficultType    => _difficultType;
        public int                   StageStep        => _stageStep;
        public List<StageRewardData> StageRewardDatas => _stageRewardDatas;
        /*
        public List<UnitData> UnitList => _unitList;
        public float PlayTime => _playTime;
        */
        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        /*
        public int GetToCoinReward() => _stageReward.CoinReward; 
        public int GetToUserExp   () => _stageReward.UserExp; 
        public int GetToKnightsExp() => _stageReward.KnightsExp;
        public int GetToExpScroll () => _stageReward.ExpScroll;
        public int GetToGemReward (int starCount)
        {
            var gemRewards = _stageReward.GemRewards;
            var gemReward  = gemRewards[starCount - 1];
            return gemReward;
        }
        */
    }
}