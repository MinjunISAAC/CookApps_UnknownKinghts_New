// ----- C#
using UnityEngine;

namespace InGame.ForLevel.ForReward
{
    [System.Serializable]
    public class StageRewardData
    {
        public ERewardType Type;
        public int         Value;
        public Sprite      StarSprite;
        public Sprite      FrameSprite;
        public Sprite      IconSprite;
    }
}