using UnityEngine;
using NaughtyAttributes;
using GrowingGems.Managers;

namespace GrowingGems.Skills
{
    public class GridMaker : MonoBehaviour
    {
        [Header("ASSIGMENTS")]
        [SerializeField] private GemManager _gemManager;
        [Header("GRID SETTINGS")]
        [Min(1)]
        [SerializeField] private int _rowNumber;
        [Min(1)]
        [SerializeField] private int _columnNumber;
        private int _randomIndex;

        [Button]
        public void GenerateRandomGemsOnGrid()
        {
            ClearThisGrid();//grid doluyken tekrar butona basilirsa ic ice gem olusumunu engellemek icin
          
            for (int i = 0; i < _columnNumber; i++)
            {
                for (int j = 0; j < _rowNumber; j++)
                {
                    _randomIndex = Random.Range(0, _gemManager._gemTypes.Count);
                    Instantiate(_gemManager._gemTypes[_randomIndex]._gemModel, transform.position + new Vector3(i * 2, 1, j * 2), Quaternion.identity, gameObject.transform);
                }
            }        
        }
        [Button]
        public void ClearThisGrid()
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        public void ReplaceGem(Vector3 previousGemPosition)
        {
            _randomIndex = Random.Range(0, _gemManager._gemTypes.Count);
            Instantiate(_gemManager._gemTypes[_randomIndex]._gemModel, previousGemPosition, Quaternion.identity, gameObject.transform);
        }
    }
}