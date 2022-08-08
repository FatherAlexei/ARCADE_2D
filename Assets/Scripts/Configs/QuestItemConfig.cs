using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Arcade
{
    [CreateAssetMenu(fileName = "QuestItemCfg", menuName = "Configs / Quest / QuestItemCfg", order = 1)]
    public class QuestItemConfig : ScriptableObject
    {
        public int questID;
        public List<int> questItemCollection;
    }
}