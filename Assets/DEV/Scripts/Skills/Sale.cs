using GrowingGems.Controllers;
using GrowingGems.Managers;
using System.Collections;
using UnityEngine;

namespace GrowingGems.Skills
{
    public class Sale : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenSales;
        private GameManager _gameManager;
        private Stacking _stacking;
        private bool _isPlayerInSaleZone;

        void Awake()
        {
            _stacking = GetComponent<Stacking>();
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("SaleZone") && _stacking.StackedGems.Count > 0)
            {
                _isPlayerInSaleZone = true;
                 StartCoroutine(nameof(SailToGems));
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("SaleZone"))
            {
                 _isPlayerInSaleZone = false;
                 StopCoroutine(nameof(SailToGems));
            }
        }

        IEnumerator SailToGems()
        {
            while (_isPlayerInSaleZone && _stacking.StackedGems.Count > 0)
            {
                yield return new WaitForSeconds(_timeBetweenSales);

                if (_stacking.StackedGems.Count > 0)
                {
                    SoundManager.Instance.PlaySaleSound();

                    GemController otherObjectGemController = _stacking.StackedGems[^1].GetComponent<GemController>();
                    _gameManager.Data.Gold += otherObjectGemController.StartingPrice + _stacking.StackedGems[^1].transform.localScale.y * 100;
                    _gameManager.Data.Gold = Mathf.Round(_gameManager.Data.Gold);
                    _gameManager.GoldText.text = _gameManager.Data.Gold.ToString();

                    int index = otherObjectGemController.GemTypeOrder;
                    _gameManager.Data._gemCounts[index]++;
                    for (int i = 0; i < _gameManager.AllRows.Count; i++)
                    {
                        _gameManager.AllRows[i].GetComponent<PopupManager>().PrintSoldStonesToPopup();
                    }
                    Destroy(_stacking.StackedGems[^1]);
                    SaveManager.SaveData(_gameManager.Data);
                }
               
            }
        }
    }
}