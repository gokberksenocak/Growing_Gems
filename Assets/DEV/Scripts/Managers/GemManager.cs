using System.Collections.Generic;
using UnityEngine;
using GrowingGems.ScriptableObjects;

namespace GrowingGems.Managers
{
    [System.Serializable]
    public class GemTypes
    {
        public string _gemName;
        public int _startingPrice;
        public Sprite _gemIcon;
        public GameObject _gemModel;
    }

    public class GemManager : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        public List<GemTypes> _gemTypes = new();
        public static GemManager Instance { get; set; }

        private void Awake()
        {
            SingletonThisObject();
        }

        void SingletonThisObject()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}