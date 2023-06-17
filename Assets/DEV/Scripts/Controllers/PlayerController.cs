using UnityEngine;

namespace GrowingGems.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [Header("MOVEMENT SETTINGS")]
        [SerializeField] private FloatingJoystick _floatingJoystick;
        [SerializeField] private GameObject _joystickBackground;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _turnSpeed;
        private Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            Movement();
            MovementAnimations();
        }

        void Movement()
        {
            if (_joystickBackground.activeInHierarchy)
            {
                float horizontal = _floatingJoystick.Horizontal;
                float vertical = _floatingJoystick.Vertical;
                Vector3 newPos = new(horizontal * _moveSpeed * Time.fixedDeltaTime, 0, vertical * _moveSpeed * Time.fixedDeltaTime);
                transform.position += newPos;

                Vector3 direction = Vector3.forward * _floatingJoystick.Vertical + Vector3.right * _floatingJoystick.Horizontal;
                if (direction != Vector3.zero)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _turnSpeed * Time.fixedDeltaTime);
            }                      
        }

        void MovementAnimations()
        {
            if (_floatingJoystick.Horizontal != 0 || _floatingJoystick.Vertical != 0)
            {
                _animator.SetBool("isMove", true);
            }
            else _animator.SetBool("isMove", false);
        }
    }
}