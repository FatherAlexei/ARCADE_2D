using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade
{
    public enum QuestType
    {
        Coins
    }

    [CreateAssetMenu(fileName = "QuestCfg", menuName = "Configs / Quest / QuestCfg", order = 1)]
    public class QuestConfig : ScriptableObject
    {
        public int id;
        public QuestType type;
    }
}