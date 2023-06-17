using System.Collections.Generic;
using UnityEngine;

namespace GrowingGems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private float _gold;
        public float Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }

        public List<int> _gemCounts;
        public List<Sprite> _gemIcons;
    }
}