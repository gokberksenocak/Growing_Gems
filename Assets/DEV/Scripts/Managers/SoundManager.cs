using UnityEngine;

namespace GrowingGems.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [Header("ASSIGNMENTS")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private AudioClip _pickSound;
        [SerializeField] private AudioClip _saleSound;
        public static SoundManager Instance { get; set; }
        void Awake()
        {
            SingletonThisObject();
        }

        public void PlayClickSound(){ _audioSource.PlayOneShot(_clickSound); }
        public void PlayPickSound() { _audioSource.PlayOneShot(_pickSound); }
        public void PlaySaleSound() {_audioSource.PlayOneShot(_saleSound);}

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