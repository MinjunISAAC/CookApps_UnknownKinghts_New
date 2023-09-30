// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Core.ForData.ForUserLevel;
using System.IO;
using InGame.ForUnit;
using InGame.ForUnit.ForData;

namespace Util.ForJson
{
    public static class JsonParser
    {
        // --------------------------------------------------
        // Variables        
        // --------------------------------------------------
        // ----- Const
        private const string JSON_USERLEVELSHEET_NAME  = "UserLevelSheet";
        private const string JSON_UNITDEFAULTDATA_NAME = "UnitDefaultData";

        // ----- Private
        private static UserLevelData    _userLevelDataSet = null;
        private static UnitDefaultDatas _unitDefaultData  = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public static void LoadJson()
        {
            var loadedUserLevelSheetData = Resources.Load<TextAsset>($"{JSON_USERLEVELSHEET_NAME}");
            var loadedUnitDefaultData    = Resources.Load<TextAsset>($"{JSON_UNITDEFAULTDATA_NAME}");

            if (loadedUserLevelSheetData == null)
            {
                Debug.LogError($"<color=red>[JsonParser.LoadJson] Json ����({JSON_USERLEVELSHEET_NAME})�� �������� �ʽ��ϴ�.</color>");
                return;
            }

            if (loadedUnitDefaultData == null)
            {
                Debug.LogError($"<color=red>[JsonParser.LoadJson] Json ����({JSON_UNITDEFAULTDATA_NAME})�� �������� �ʽ��ϴ�.</color>");
                return;
            }

            _userLevelDataSet = JsonUtility.FromJson<UserLevelData>   (loadedUserLevelSheetData.text);
            _unitDefaultData  = JsonUtility.FromJson<UnitDefaultDatas>(loadedUnitDefaultData   .text);

            if (_userLevelDataSet == null)
            {
                Debug.LogError($"<color=red>[JsonParser.LoadJson] Json ����({JSON_USERLEVELSHEET_NAME}) �Ľ̿� �����Ͽ����ϴ�.</color>");
            }

            if (_unitDefaultData == null)
            {
                Debug.LogError($"<color=red>[JsonParser.LoadJson] Json ����({JSON_UNITDEFAULTDATA_NAME}) �Ľ̿� �����Ͽ����ϴ�.</color>");
            }
        }

        public static List<LevelInfo> GetToUserLevelData()
        {
            if (_userLevelDataSet == null)
                return null;

            return _userLevelDataSet.DataSet;
        }

        public static List<UnitDefaultData> GetToUnitDefaultData()
        {
            if (_unitDefaultData == null)
                return null;

            return _unitDefaultData.DataSet;
        }
    }
}