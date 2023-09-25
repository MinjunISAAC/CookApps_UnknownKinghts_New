// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Core.ForData.ForUserLevel;
using Util.ForJson;

namespace Core.ForData.ForUserLevel 
{ 
    public static class UserLevelDataHelper
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private static Dictionary<int, LevelInfo> _dataSet = new Dictionary<int, LevelInfo>();

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public static int MaxLevel => _dataSet.Count;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public static void OnInit()
        {
            var userLevelData = JsonParser.GetToUserLevelData();
            
            for (int i = 0; i < userLevelData.Count; i++)
            {
                var levelData = userLevelData[i];

                _dataSet.Add(levelData.Level, levelData);
            }

            // [TODO] 테스트 후 주석 처리
            Debug.Log($"<color=yellow>[UserLevelDataHelper.OnInit] User Level Sheet 데이터는 총 {_dataSet.Count}개 입니다.</color>");
        }

        public static int GetToLevelUpExp(int userLevel)
        {
            var nextLevel = userLevel + 1;

            if (nextLevel >= _dataSet.Count)
                return _dataSet.Count;

            if (_dataSet.TryGetValue(nextLevel, out var targetLevelInfo))
            {
                var needExp = targetLevelInfo.Exp;
                return needExp;
            }
            else 
            {
                Debug.LogError($"<color=red>[UserLevelDataHelper.GetToLevelUpExp] 다음 레벨(Lv.{nextLevel})에 대한 정보가 존재하지 않습니다.</color>");
                return -1;
            }
        }

        public static int GetToMaxBread(int userLevel)
        {
            if (_dataSet.TryGetValue(userLevel, out var targetLevelInfo))
            {
                var maxBread = targetLevelInfo.Bread;
                return maxBread;
            }
            else
            {
                Debug.LogError($"<color=red>[UserLevelDataHelper.GetToMaxBread] 현재 레벨(Lv.{userLevel})에 대한 정보가 존재하지 않습니다.</color>");
                return -1;
            }
        }
    }
}