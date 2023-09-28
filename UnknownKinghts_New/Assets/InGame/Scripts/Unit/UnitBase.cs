using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit
{
    public class UnitBase : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Unit Data")]
        [SerializeField] private UnitData _defaultUnitData = null;

        [Header("2. Animate Group")]
        [SerializeField] private Animator _animator        = null;
    }
}