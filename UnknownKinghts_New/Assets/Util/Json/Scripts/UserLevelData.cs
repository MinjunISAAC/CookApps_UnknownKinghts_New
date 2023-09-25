using System.Collections.Generic;

namespace Core.ForData.ForUserLevel
{
    [System.Serializable]
    public class LevelInfo
    {
        public int Level;
        public int Exp;
        public int Bread;
    }

    [System.Serializable]
    public class UserLevelData
    {
        public List<LevelInfo> DataSet = new List<LevelInfo>();
    }
}