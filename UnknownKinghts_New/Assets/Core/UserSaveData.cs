// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit;

namespace Core.ForData.ForUserSave
{
    [System.Serializable]
    public class UserSaveData
    {
        // --------------------------------------------------
        // Clear Data (Chapter, Stage, Star)
        // --------------------------------------------------
        [System.Serializable]
        public class ClearData
        {
            public int Chapter = 1;
            public int Stage   = 1;
            public int Star    = 0;

            public ClearData(int chapter, int stage, int star)
            {
                Chapter = chapter;
                Stage   = stage;  
                Star    = star;
            }
        }

        // --------------------------------------------------
        // User Basic Save Data
        // --------------------------------------------------
        // [�⺻ ����]
        [SerializeField] private int    _level      = 1;
        [SerializeField] private int    _experience = 0;
        [SerializeField] private int    _bread      = 0;
        [SerializeField] private string _userName   = "������";

        // [�÷��� ����]
        [SerializeField] private int             _lastChapter = 1;
        [SerializeField] private int             _lastStage   = 1;
        [SerializeField] private List<ClearData> _clearDatas  = new List<ClearData>();

        // [��ȭ ����]
        [SerializeField] private int _currencyBread = 100;
        [SerializeField] private int _currencyCoin  = 0;
        [SerializeField] private int _currencyGem   = 0;

        // [ĳ���� ����]
        [SerializeField] private List<UnitData> _ownedUnits = new List<UnitData>();

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        #region <�⺻ ����>
        public int Level
        {
            get => _level;
            set => _level = value;
        }

        public int Experience
        {
            get => _experience;
            set => _experience = value;
        }

        public int Bread
        {
            get => _bread;
            set => _bread = value;
        }

        public string UserName
        {
            get => _userName;
            set => _userName = value;
        }
        #endregion

        #region<�÷��� ����>
        public int LastChapter
        {
            get => _lastChapter;
            set => _lastChapter = value;
        }

        public int LastStage
        {
            get => _lastStage;
            set => _lastStage = value;  
        }

        public List<ClearData> ClearDatas
        {
            get => _clearDatas;
            set => _clearDatas = value;
        }
        #endregion

        #region<��ȭ����>
        public int CurrencyBread
        {
            get => _currencyBread;
            set => _currencyBread = value;
        }

        public int CurrencyCoin
        {
            get => _currencyCoin;
            set => _currencyCoin = value;
        }

        public int CurrencyGem
        {
            get => _currencyGem;
            set => _currencyGem = value;
        }
        #endregion

        #region <���� ����>
        public List<UnitData> OwnedUnits
        {
            get => _ownedUnits;
            set => _ownedUnits = value;
        }
        #endregion
    }
}