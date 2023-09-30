using InGame.ForUnit.ForData;
using System;
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
            public EUnitType     UnitType     = EUnitType    .Unknown;
            public EGradeType    GradeType    = EGradeType   .Unknown;
            public EJobType      JobType      = EJobType     .Unknown;
            public ESpecType     SpecType     = ESpecType    .Unknown;
            public EPositionType PositionType = EPositionType.Unknown;
        }

        // --------------------------------------------------
        // Unit Data
        // --------------------------------------------------
        // ----- Basic Data
        public int       Level        = 1;
        public int       MaxLevel     = 1;
        public int       Star         = 1;
        public string    Name         = "";

        // ----- Type Data
        public Type      TypeGroup    = new Type();
        
        // ----- Ability Data
        public Ability   AbilityGroup = new Ability();

        // ----- Skill Data
        public Skill     NomalSkill   = new Skill();
        public Skill     WeaponSkill  = new Skill();

        // --------------------------------------------------
        // Functions - Test
        // --------------------------------------------------
        public void SetUp_Test(UnitDefaultData defaultData)
        {
            Level    = defaultData.Level;
            MaxLevel = defaultData.MaxLevel;
            Star     = defaultData.Star;
            Name     = defaultData.Name;

            TypeGroup.UnitType     = Enum.Parse<EUnitType>    (defaultData.TypeGroup.UnitType    );
            TypeGroup.GradeType    = Enum.Parse<EGradeType>   (defaultData.TypeGroup.GradeType   );
            TypeGroup.JobType      = Enum.Parse<EJobType>     (defaultData.TypeGroup.JobType     );
            TypeGroup.SpecType     = Enum.Parse<ESpecType>    (defaultData.TypeGroup.SpecType    );
            TypeGroup.PositionType = Enum.Parse<EPositionType>(defaultData.TypeGroup.PositionType);

            AbilityGroup.AttackPower    = defaultData.AbilityGroup.AttackPower;
            AbilityGroup.AttackSpeed    = defaultData.AbilityGroup.AttackSpeed;
            AbilityGroup.AttackDistance = defaultData.AbilityGroup.AttackDistance;
            AbilityGroup.Hp             = defaultData.AbilityGroup.Hp;
            AbilityGroup.Defense        = defaultData.AbilityGroup.Defense;
            AbilityGroup.PenetratePower = defaultData.AbilityGroup.PenetratePower;
            AbilityGroup.CriticalRate   = defaultData.AbilityGroup.CriticalRate;

            NomalSkill.Name     = defaultData.NomalSkill.Name;
            NomalSkill.CoolTime = defaultData.NomalSkill.CoolTime;

            WeaponSkill.Name     = defaultData.WeaponSkill.Name;
            WeaponSkill.CoolTime = defaultData.WeaponSkill.CoolTime;
        }
    }
}