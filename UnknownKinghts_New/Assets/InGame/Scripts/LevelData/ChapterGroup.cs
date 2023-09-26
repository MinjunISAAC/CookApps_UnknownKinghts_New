// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForLevel.ForChapter;
using InGame.ForLevel.ForStage;

namespace InGame.ForChapterGroup
{
    [CreateAssetMenu(menuName = "Chapter Group/Create To Chapter Group", fileName = "ChapterGroup")]
    public class ChapterGroup : ScriptableObject
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private List<Chapter> _chapterList = new List<Chapter>();

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<int, Chapter> _chapterSet = new Dictionary<int, Chapter>();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit()
        {
            for (int i = 0; i < _chapterList.Count; i++)
            {
                var chapter = _chapterList[i];
                var chapterStep = chapter.Step;
                chapter.OnInit();

                _chapterSet.Add(chapterStep, chapter);
            }

            // [TODO] Test ���� ��, �ּ�ó�� ����
            Debug.Log($"<color=orange>[ChaterGroup.OnInit] ���� ������ {_chapterSet.Count}���� Chapter�� �ʱ�ȭ�Ǿ����ϴ�.</color>");
        }

        public Chapter GetToChapter(int chapterNum)
        {
            if (_chapterSet.TryGetValue(chapterNum, out var chapter))
                return chapter;
            else
            {
                Debug.LogError($"<color=red>[ChapterGroup.GetToChapter] Chapter ������ [Chapter {chapterNum}]�� ������ �������� �ʽ��ϴ�.</color>");
                return null;
            }
        }

        public Stage GetToStage(int chapterNum, int stageNum)
        {
            var chapter = GetToChapter(chapterNum);
            var stage = chapter.GetToStage(stageNum);

            return stage;
        }
    }
}