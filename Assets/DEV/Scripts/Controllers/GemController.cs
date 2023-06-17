using GrowingGems.Managers;
using GrowingGems.Skills;
using UnityEngine;
using DG.Tweening;
using GrowingGems.ScriptableObjects;

namespace GrowingGems.Controllers
{
    public class GemController : MonoBehaviour
    {
        [Header("ASSIGNMENTS")]
        [SerializeField] private GameData _data;
        [SerializeField] private Sprite _gemIcon;
        private GridMaker _gridMaker;
        private CapsuleCollider _capsuleCollider;
        private Tweener _scaleTween;
        private Tweener _rotateTween;
        private Vector3 _gemPositionBeforeStack;
        private bool _isItTouchBefore;
        private int _startingPrice;
        public int StartingPrice
        {
            get { return _startingPrice; }
            set { _startingPrice = value; }
        }
        
        private int _gemTypeOrder;
        public int GemTypeOrder
        {
            get { return _gemTypeOrder; }
            set { _gemTypeOrder = value; }
        }
        [SerializeField] private float _rotationSpeed;
        private void Awake()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _gridMaker = GetComponentInParent<GridMaker>();         
        }

        private void Start()
        {
            CheckForDesignMistake();
            FindSavedIconDataOfGem();
            _gemPositionBeforeStack = transform.position;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<PlayerController>() != null && transform.localScale.y > .25f && !_isItTouchBefore)
            {
                _scaleTween.Kill();
                _rotateTween.Kill();
                SoundManager.Instance.PlayPickSound();
                Stacking stacking = other.GetComponent<Stacking>();
                stacking.GemStacking(_capsuleCollider);
                _gridMaker.ReplaceGem(_gemPositionBeforeStack);
                _isItTouchBefore = true;
            }
        }

        void GivePriceToGem()
        {
            for (int i = 0; i < GemManager.Instance._gemTypes.Count; i++)
            {
                if (GemManager.Instance._gemTypes[i]._gemModel.GetComponent<MeshRenderer>().sharedMaterial == gameObject.GetComponent<MeshRenderer>().sharedMaterial)
                {
                    _startingPrice = GemManager.Instance._gemTypes[i]._startingPrice;
                    break;
                }
            }
        }

        void FindSavedIconDataOfGem()
        {
            for (int i = 0; i < _data._gemIcons.Count; i++)
            {
                if (_gemIcon == _data._gemIcons[i])
                {
                    _gemTypeOrder = i;
                }
            }
        }

        void CheckForDesignMistake()//eger ekranda olan bir gem, gemManagerin gem listesinden cikarilir ve sahnede kalmasina ragmen oyun calistirilirsa yerine listede bulunan gemlerden biri olussun diye yazildi
        {
            int counter = 0;
            for (int i = 0; i < GemManager.Instance._gemTypes.Count; i++)
            {
                if (gameObject.GetComponent<MeshFilter>().sharedMesh == GemManager.Instance._gemTypes[i]._gemModel.GetComponent<MeshFilter>().sharedMesh)
                {
                    counter++;
                }
            }
            
            if (counter == 0)
            {
                _gridMaker.ReplaceGem(transform.position);
                Destroy(gameObject);
            }
            else
            {
                GivePriceToGem();
                transform.localScale = Vector3.zero;
                _scaleTween = transform.DOScale(Vector3.one, 5f);
                _rotateTween = transform.DORotate(new Vector3(0f, 360f, 0f), _rotationSpeed, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            }
        }
    }
}