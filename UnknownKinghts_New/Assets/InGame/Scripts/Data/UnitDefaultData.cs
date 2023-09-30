using InGame.ForUnit.ForData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit.ForData
{
    [System.Serializable]
    public class UnitDefaultData
    {
        // --------------------------------------------------
        // Data Class
        // --------------------------------------------------
        [System.Serializable]
        public class Ability
        {
            public int   AttackPower    = 0;
            public float AttackSpeed    = 0f;
            public float AttackDistance = 0f;
            public int   Hp             = 0;
            public int   Defense        = 0;
            public int   PenetratePower = 0;
            public int   CriticalDamage = 0;
            public int   CriticalRate   = 0;
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
            public string UnitType     = "";
            public string GradeType    = "";
            public string JobType      = "";
            public string SpecType     = "";
            public string PositionType = "";
        }

        // --------------------------------------------------
        // Unit Data
        // --------------------------------------------------
        // ----- Basic Data
        public int     Level        = 1;
        public int     MaxLevel     = 1;
        public int     Star         = 1;
        public string  Name         = "";

        // ----- Type Data
        public Type    TypeGroup    = new Type();

        // ----- Ability Data
        public Ability AbilityGroup = new Ability();

        // ----- Skill Data
        public Skill  NomalSkill    = new Skill();
        public Skill  WeaponSkill   = new Skill();
    }
}