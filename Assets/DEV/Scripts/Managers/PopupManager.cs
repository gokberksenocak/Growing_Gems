using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GrowingGems.ScriptableObjects;

namespace GrowingGems.Managers
{
    public class PopupManager : MonoBehaviour
    {
        [Header("ASSIGNMENTS")]
        [SerializeField] private GameData _data;
        [SerializeField] private Image _icon;
        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
        [SerializeField] private TextMeshProUGUI _gemNameText;
        public TextMeshProUGUI GemNameText
        {
            get { return _gemNameText; }
            set { _gemNameText= value; }
        }
        [SerializeField] private TextMeshProUGUI _gemCountText;
        public TextMeshProUGUI GemCountText
        {
            get { return _gemCountText; }
            set { _gemCountText = value; }
        }
        private int _indexOfPopupRow;

        private void Start()
        {
            FindIndexOfPopupRow();
            PrintSoldStonesToPopup();
        }

        public void FindIndexOfPopupRow()
        {
            for (int i = 0; i < _data._gemIcons.Count; i++)
            {    
                if (_icon.sprite == _data._gemIcons[i])
                {
                    _indexOfPopupRow = i;
                    break;
                }
            }
        }

        public void PrintSoldStonesToPopup()
        {
            _gemCountText.text = "Sold: " + _data._gemCounts[_indexOfPopupRow].ToString();
        }
    }
}