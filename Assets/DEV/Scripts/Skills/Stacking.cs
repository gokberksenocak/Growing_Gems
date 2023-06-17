using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

namespace GrowingGems.Skills
{
    public class Stacking : MonoBehaviour
    {
        [Header("STACK SETTINGS")]
        [SerializeField] private Transform _stackPointParent;
        [SerializeField] private ParticleSystem _stackParticle;
        [SerializeField] private float _stackDuration;
        [SerializeField] private Ease _easeType;
        private List<GameObject> _stackedGems = new();
        public List<GameObject> StackedGems
        {
            get { return _stackedGems; }
            set { _stackedGems = value; }
        }

        public void GemStacking(Collider other)
        {
            _stackedGems.Add(other.gameObject);
            other.transform.DOLocalMove(new Vector3(0, .75f * (_stackedGems.Count - 1), 0), _stackDuration).SetEase(_easeType).OnComplete(()=>
            {
                _stackParticle.transform.position = _stackedGems[^1].transform.position;
                _stackParticle.Play();
                other.transform.localRotation = Quaternion.identity;
            }
              );
            other.transform.SetParent(_stackPointParent);
        }

        private void Update()
        {
            _stackedGems = _stackedGems.Where(item => item != null).ToList();
        }
    }
}