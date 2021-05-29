using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace LocationRPG
{
    public class RotateWithTouch : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private Camera camera;
        private float _rotatespeed = 100f;
        private Vector2 _lastPosition;

        private Vector2 _playerPosition;

        private Touch _touch;

        private void Awake()
        {
            Assert.IsNotNull(player);
            Assert.IsNotNull(camera);
        }

        private void Start()
        {
            _lastPosition = Vector2.zero;
            _playerPosition = camera.WorldToScreenPoint(player.transform.position);
        }

        void Update()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);

                //bottom-right corner is position (0,0)
                switch (_touch.phase)
                {
                    case TouchPhase.Began:
                        if (_touch.position.y < Screen.height / 5f || InteractionManager.Instance.InteractionLock)
                        {
                            _lastPosition = Vector2.zero;
                            break;
                        }

                        _lastPosition = _touch.position;
                        break;
                    case TouchPhase.Moved:
                        if (_lastPosition == Vector2.zero)
                        {
                            break;
                        }

                        InteractionManager.Instance.Lock();
                        float direction = 1;
                        
                        // swipe is bellow player object
                        if (_lastPosition.y < _playerPosition.y)
                        {
                            //swipe to right => rotate right
                            if (_lastPosition.x < _touch.position.x)
                            {
                                direction = -1;
                            }
                        }
                        // swipe is above player object
                        else
                        {
                            // left to left => rotate right
                            if (_lastPosition.x > _touch.position.x)
                            {
                                direction = -1;
                            }
                        }

                        transform.Rotate(Vector3.down, direction * _rotatespeed * Time.deltaTime);

                        _lastPosition = _touch.position;
                        break;
                    case TouchPhase.Ended:
                        Debug.Log("Ended");
                        if (_lastPosition == Vector2.zero)
                        {
                            break;
                        }

                        InteractionManager.Instance.Unlock();
                        _lastPosition = Vector2.zero;
                        break;
                }
            }
        }
    }
}