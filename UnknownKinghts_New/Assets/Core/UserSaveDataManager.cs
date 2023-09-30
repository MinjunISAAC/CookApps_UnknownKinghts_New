using InGame.ForUnit;
using InGame.ForUnit.ForData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Util.ForJson;
using static Core.ForData.ForUserSave.UserSaveData;

namespace Core.ForData.ForUserSave
{
    public static class UserSaveDataManager
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const string FILE_NAME = "UserSaveData.json";

        // ----- Private
        private static UserSaveData                                _userSaveData = new UserSaveData();
        private static Dictionary<int, Dictionary<int, ClearData>> _clearDataSet = new Dictionary<int, Dictionary<int, ClearData>>();
        private static Dictionary<EUnitType, UnitData>             _ownedUnitSet = new Dictionary<EUnitType, UnitData>();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public static void OnInit()
        {
            Load();

            // 테스트를 위한 Unit 생성
            var defaultUnitDatas = UnitDefaultDataHelper.GetToDefaultUnitDataSet();
            foreach (KeyValuePair<EUnitType, UnitDefaultData> key in defaultUnitDatas)
            {
                if (!_ownedUnitSet.TryGetValue(key.Key, out var unitData))
                {
                    if (defaultUnitDatas.TryGetValue(key.Key, out var defaultData)) 
                    {
                        UnitData newUnitData = new UnitData();
                        newUnitData.SetUp_Test(defaultData);
                        _ownedUnitSet.Add(key.Key, newUnitData);
                        SetToUnit(newUnitData);
                    }
                }
            }

            var clearDatas     = _userSaveData.ClearDatas;

            if (clearDatas.Count == 0)
            {
                var clearData = new ClearData(1, 1, 0);
                clearDatas.Add(clearData);
                return;
            }

            for (int i = 0; i < clearDatas.Count; i++)
            {
                var newDic      = new Dictionary<int, ClearData>();
                var clearData   = clearDatas[i];
                var chapterStep = clearData.Chapter;
                var stageStep   = clearData.Stage;

                newDic.Add(stageStep, clearData);
                _clearDataSet.Add(chapterStep, newDic);
            }

        }

        #region<기본 정보>
        public static int    GetToUserLevel() => _userSaveData.Level;
        public static int    GetToUserExp  () => _userSaveData.Experience;
        public static string GetToUserName () => _userSaveData.UserName;
        #endregion

        #region<플레이 정보>
        public static int GetToLastChapter() => _userSaveData.LastChapter;
        public static int GetToLastStage  () => _userSaveData.LastStage;

        public static void SetToLastChapter(int chapter)
        {
            _userSaveData.LastChapter = chapter;
            Save();
        }

        public static void SetToLastStage(int stage)
        {
            _userSaveData.LastStage = stage;
            Save();
        }

        public static void SetToClearData(int chapter, int stage, int star)
        {
            if (_clearDataSet.TryGetValue(chapter, out var stageClearData))
            {
                if (stageClearData.TryGetValue(stage, out var clearData))
                {
                    clearData.Chapter = chapter;
                    clearData.Stage   = stage;
                    clearData.Star    = star;
                }
                else
                {
                    var data = new ClearData(chapter, stage, star);
                    stageClearData.Add(stage, data);
                }
                
                Save();
                return;
            }
            else
            {
                Debug.LogError($"<color=red>[UserSaveDataManager.SetToClearData] Chapter {chapter}에 대한 정보가 존재하지 않습니다. 초기화 되어있는지 확인해야합니다.</color>");
                return;
            }
        }

        public static ClearData GetToClearData(int chapter, int stage)
        {
            if (_clearDataSet.TryGetValue(chapter, out var stageClearData))
            {
                if (stageClearData.TryGetValue(stage, out var clearData)) return clearData;
                else 
                {
                    Debug.LogError($"<color=red>[UserSaveDataManager.GetToClearData] Chapter{chapter}-Stage{stage}에 대한 정보가 존재하지 않습니다. 초기화 되어있는지 확인해야합니다.</color>");
                    return null;
                }
            }
            else
            {
                //Debug.LogError($"<color=red>[UserSaveDataManager.GetToClearData] Chapter {chapter}에 대한 정보가 존재하지 않습니다. 초기화 되어있는지 확인해야합니다.</color>");
                return null;
            }
        }

        public static Dictionary<int, ClearData> GetToClearData(int chapter)
        {
            if (_clearDataSet.TryGetValue(chapter, out var stageClearDatas))
                return stageClearDatas;
            else
                return null;
        }

        #endregion

        #region<재화 정보>
        public static int GetToCoin () => _userSaveData.CurrencyCoin;
        public static int GetToGem  () => _userSaveData.CurrencyGem;
        public static int GetToBread() => _userSaveData.CurrencyBread;

        public static void ConsumeToCoin (int coin , Action failCallBack) => _TryToConsumeCoin (coin , failCallBack);
        public static void ConsumeToGem  (int gem  , Action failCallBack) => _TryToConsumeGem  (gem  , failCallBack);
        public static void ConsumeToBread(int bread, Action failCallBack) => _TryToConsumeBread(bread, failCallBack);

        public static void AddToCoin(int coin) 
        {
            _userSaveData.CurrencyCoin += coin;
            Save();
        }

        public static void AddToGem(int gem) 
        {
            _userSaveData.CurrencyGem += gem;
            Save();
        }

        public static void AddToBread(int bread) 
        {
            _userSaveData.CurrencyBread += bread;
            Save();
        } 
        
        private static void _TryToConsumeCoin(int coin, Action failCallBack = null)
        {
            if (_userSaveData.CurrencyCoin >= coin)
            {
                _userSaveData.CurrencyCoin -= coin;
                Save();
                return;
            }

            failCallBack?.Invoke();
        }

        private static void _TryToConsumeGem(int gem, Action failCallBack = null)
        {
            if (_userSaveData.CurrencyGem >= gem)
            {
                _userSaveData.CurrencyGem -= gem;
                Save();
                return;
            }

            failCallBack?.Invoke();
        }

        private static void _TryToConsumeBread(int bread, Action failCallBack = null)
        {
            if (_userSaveData.CurrencyBread >= bread)
            {
                _userSaveData.CurrencyBread -= bread;
                Save();
                return;
            }
            
            failCallBack?.Invoke();
        }
        #endregion

        #region <보유 유닛>
        public static void SetToUnit(UnitData newUnitData)
        {
            _userSaveData.OwnedUnits.Add(newUnitData);
            Save();
        }
        #endregion

        // --------------------------------------------------
        // Functions - Load & Save
        // --------------------------------------------------
        public static void Load()
        {
            if (!_TryLoad(FILE_NAME, out string fileContents))
            {
                _userSaveData = new UserSaveData();
                return;
            }

            try
            {
                var pendData = JsonUtility.FromJson<UserSaveData>(fileContents);
                if (pendData == null)
                {
                    Debug.LogError($"[UserSaveDataManager.Load] {FILE_NAME} 파일을 로드하는데 실패했습니다.");
                    return;
                }

                _userSaveData = pendData;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return;
            }

            return;
        }

        public static void Save()
        {
            var jsonContents = JsonUtility.ToJson(_userSaveData);

            if (!_TrySave(FILE_NAME, jsonContents))
            {
                Debug.LogError($"[UserSaveDataManager.Save] File을 저장하지 못했습니다.");
                return;
            }
        }

        // ----- Private
        private static bool _TryLoad(string fileName, out string fileContents)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileContents = string.Empty;
                return false;
            }

            var filePath = $"{Application.persistentDataPath}/{fileName}";

            if (!File.Exists(filePath))
            {
                fileContents = string.Empty;
                return false;
            }

            try
            {
                fileContents = File.ReadAllText(filePath);
                return true;
            }
            catch (Exception e)
            {
                fileContents = $"{e}";
                return false;
            }
        }

        private static bool _TrySave(string fileName, string saveDataContents)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Debug.LogError("UserSaveDataManager.Save] 파일명이 비어있습니다.");
                return false;
            }

            if (_userSaveData == null)
            {
                Debug.LogError($"[UserSaveDataManager.Save] User Save Data가 생성되지 않았습니다.");
                return false;
            }

            if (string.IsNullOrEmpty(saveDataContents))
            {
                Debug.LogWarning("[UserSaveDataManager.Save] 저장할 컨텐츠가 비어있습니다.");
                return false;
            }

            try
            {
                var fileContents = JsonUtility.ToJson(_userSaveData);

                var filePath = $"{Application.persistentDataPath}/{fileName}";

                try
                {
                    fileContents = saveDataContents;
                    File.WriteAllText(filePath, fileContents);
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogError($"<color=red>[UserSaveDataManager._TrySave] {e}</color>");
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"<color=red>[UserSaveDataManager._TrySave] {e}</color>");
                return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("UserData/Delete User Save Data")]
        private static void ClearUserSaveData()
        {
            string filePath = $"{Application.persistentDataPath}/{FILE_NAME}";

            if (File.Exists(filePath)) File.Delete(filePath);
        }
#endif
    }
}