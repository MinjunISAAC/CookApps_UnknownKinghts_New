// ----- C#
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Util.ForJson;

namespace InGame.ForUnit.ForData
{
    public static class UnitDefaultDataHelper
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private static Dictionary<EUnitType, UnitDefaultData> _dataSet = new Dictionary<EUnitType, UnitDefaultData>();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public static void OnInit()
        {
            var unitDefaultDatas = JsonParser.GetToUnitDefaultData();

            for (int i = 0; i < unitDefaultDatas.Count; i++)
            {
                var unitData = unitDefaultDatas[i];

                if (Enum.TryParse<EUnitType>(unitData.TypeGroup.UnitType, true, out var type))
                    _dataSet.Add(type, unitData);
                else
                    Debug.Log($"<color=yellow>[UnitDefaultDataHelper.OnInit] {unitData.TypeGroup.UnitType}에 해당하는 데이터의 상태가 정상이지 않습니다.</color>");
            }

            // [TODO] 테스트 후 주석 처리
            Debug.Log($"<color=yellow>[UnitDefaultDataHelper.OnInit] Unit Default Data는 총 {_dataSet.Count}개 입니다.</color>");
        }

        public static Dictionary<EUnitType, UnitDefaultData> GetToDefaultUnitDataSet()
        => _dataSet;
    }
}