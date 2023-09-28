using InGame.ForUnit.ForData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit
{
    [System.Serializable]
    public class UnitData
    {
        // --------------------------------------------------
        // Data Class
        // --------------------------------------------------
        [System.Serializable]
        public class Ability
        {
            public int   AttackPower    = 0;
            public int   Hp             = 0;
            public int   Defense        = 0;
            public int   PenetratePower = 0;
            public int   CriticalDamage = 0;
            public int   CriticalRate   = 0;
            public float AttackSpeed    = 0f;
            public float AttackDistane  = 0f;
        }

        [System.Serializable]
        public class Skill
        {
            public string Name     = "";
            public float  CoolTime = 0f;
        }

        [System.Serializable]
        public class Type
        {
            public EUnitType     UnitType     = EUnitType    .Unknown;
            public EGradeType    GradeType    = EGradeType   .Unknown;
            public EJobType      JobType      = EJobType     .Unknown;
            public ESpecType     SpecType     = ESpecType    .Unknown;
            public EPositionType PositionType = EPositionType.Unknown;
        }

        // --------------------------------------------------
        // Unit Data
        // --------------------------------------------------
        // ----- Type Data
        public Type    TypeGroup    = new Type();
        
        // ----- Basic Data
        public int     Level        = 1;
        public int     MaxLevel     = 1;
        public int     Star         = 1;
        public string  Name         = "";

        // ----- Ability Data
        public Ability AbilityGroup = new Ability();

        // ----- Skill Data
        public Skill   FirstSkill   = new Skill();
        public Skill   SecondSkill  = new Skill();
    }
}