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

            // [TODO] �׽�Ʈ �� �ּ� ó��
            Debug.Log($"<color=yellow>[UserLevelDataHelper.OnInit] User Level Sheet �����ʹ� �� {_dataSet.Count}�� �Դϴ�.</color>");
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
                Debug.LogError($"<color=red>[UserLevelDataHelper.GetToLevelUpExp] ���� ����(Lv.{nextLevel})�� ���� ������ �������� �ʽ��ϴ�.</color>");
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
                Debug.LogError($"<color=red>[UserLevelDataHelper.GetToMaxBread] ���� ����(Lv.{userLevel})�� ���� ������ �������� �ʽ��ϴ�.</color>");
                return -1;
            }
        }
    }
}