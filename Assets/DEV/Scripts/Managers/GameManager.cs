using GrowingGems.ScriptableObjects;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GrowingGems.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("ASSIGNMENTS")]
        [SerializeField] private GemManager _gemManager;
        [SerializeField] private GameObject _popupPanel;
        [SerializeField] private GameObject _popupRowPrefab;
        [SerializeField] private GameObject _content;
        [SerializeField] private GameData _data;
        public GameData Data
        {
            get { return _data; }
            set { _data = value; }
        }

        [SerializeField] private TextMeshProUGUI _goldText;
        public TextMeshProUGUI GoldText
        {
            get { return _goldText; }
            set { _goldText = value; }
        }
        private List<GameObject> _allRows = new();
        public List<GameObject> AllRows
        {
            get { return _allRows; }
            set { _allRows = value; }
        }
        private Vector2 _scrollHandleFirstPos;
        public static GameManager Instance { get; set; }
        void Awake()
        {
            SingletonThisObject();
            CheckGemCountsDatas();
            CheckGemIconsDatas();
#if !UNITY_EDITOR
        SaveManager.LoadData(_data);
#endif
        }
        private void Start()
        {
            CreatePopupRows();
            _goldText.text = _data.Gold.ToString();
        }

        public void CreatePopupRows()
        {
            for (int i = 0; i < _gemManager._gemTypes.Count; i++)
            {
                GameObject popupRow= Instantiate(_popupRowPrefab, _content.transform);
                PopupManager popup = popupRow.GetComponent<PopupManager>();
                popup.Icon.sprite = _gemManager._gemTypes[i]._gemIcon;
                popup.GemNameText.text = _gemManager._gemTypes[i]._gemName.ToString();
                _allRows.Add(popupRow);
            }      
        }

        public void OpenOrClosePopup(string action)
        {
            if (action=="open")
            {
                _scrollHandleFirstPos = _content.GetComponent<RectTransform>().anchoredPosition;
                SoundManager.Instance.PlayClickSound();
                _popupPanel.SetActive(true);
            }
            else if (action=="close")
            {
                _content.GetComponent<RectTransform>().anchoredPosition = _scrollHandleFirstPos;
                SoundManager.Instance.PlayClickSound();
                _popupPanel.SetActive(false);
            }
        }

        public void CheckGemCountsDatas()//gem menagerdan yeni gem veya gemler eklenirse count save islemi icin game datada yer acmak amaciyla yazildi
        {
            if (_data._gemCounts.Count < _gemManager._gemTypes.Count)
            {
                int difference = _gemManager._gemTypes.Count - _data._gemCounts.Count;
                for (int i = 0; i < difference; i++)
                {
                    _data._gemCounts.Add(0);
                }
            }
        }

        public void CheckGemIconsDatas()//Gem ikonlarina bakip yeni gem eklenir veya mevcut gem silinirse count verilerinin dogru eslestirilmesi icin yazimi gerekliydi
        {
            if (_data._gemIcons.Count==0)// modellerin ilk kayit dosyasini olusturdugumuz yer
            {
                for (int i = 0; i < _gemManager._gemTypes.Count; i++)
                {
                    _data._gemIcons.Add(_gemManager._gemTypes[i]._gemIcon);
                }
            }
            else
            {
                if (_data._gemIcons.Count < _gemManager._gemTypes.Count)//yeni gem modeli eklendigi anlamýna gelir
                {
                    for (int i = 0; i < _gemManager._gemTypes.Count; i++)
                    {
                        if (_data._gemIcons.Contains(_gemManager._gemTypes[i]._gemIcon) == false)
                        {
                            _data._gemIcons.Add(_gemManager._gemTypes[i]._gemIcon);
                        }
                    }
                }
            }        
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