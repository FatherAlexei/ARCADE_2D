using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Arcade
{
    public enum QuestStoryType
    {
        Common,
        Resettable
    }
    [CreateAssetMenu(fileName = "QuestStoryCfg", menuName = "Configs / Quest / QuestStoryCfg", order = 1)]
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] quests;
        public QuestStoryType type;
    }
}