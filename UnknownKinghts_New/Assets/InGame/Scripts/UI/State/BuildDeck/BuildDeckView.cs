using InGame.ForUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForState.ForUI
{
    public class BuildDeckView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Top View")]
        [SerializeField] private BuildDeckTopView _topView = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        public override void OnInit  (){ }
        public override void OnFinish(){ }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToTopView(string chapterName, int chapterStep, int stageStep, Action onClickReturn)
        => _topView.SetToTopView(chapterName, chapterStep, stageStep, onClickReturn);
    }
}