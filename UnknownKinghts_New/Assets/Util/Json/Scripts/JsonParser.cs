// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Core.ForData.ForUserLevel;
using System.IO;

namespace Util.ForJson
{
    public static class JsonParser
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const string JSONFILE_NAME = "UserLevelSheet";

        // ----- Private
        private static UserLevelData _userLevelDataSet = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public static void LoadJson()
        {
            //var filePath_UserLevelData = Path.Combine(Application.persistentDataPath, JSONFILE_NAME);
            var loadedJsonFile = Resources.Load<TextAsset>($"{JSONFILE_NAME}");

            if (loadedJsonFile == null)
            {
                Debug.LogError("<color=red>[JsonParser.LoadJson] Json ����(UserLevel)�� �������� �ʽ��ϴ�.</color>");
                return;
            }

            _userLevelDataSet = JsonUtility.FromJson<UserLevelData>(loadedJsonFile.text);

            if (_userLevelDataSet == null)
            {
                Debug.LogError("<color=red>[JsonParser.LoadJson] Json ����(UserLevel) �Ľ̿� �����Ͽ����ϴ�.</color>");
            }
        }

        public static List<LevelInfo> GetToUserLevelData()
        {
            if (_userLevelDataSet == null)
                return null;

            return _userLevelDataSet.DataSet;
        }
    }
}