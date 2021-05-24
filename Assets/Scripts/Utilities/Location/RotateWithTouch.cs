using UnityEngine;

namespace LocationRPG
{
    public class RotateWithTouch : MonoBehaviour
    {
        private float _rotatespeed = 100f;
        private Vector2 _startingPosition;

        
        private Touch _touch;
        
        void Update()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);

                switch (_touch.phase)
                {
                    case TouchPhase.Began:
                        _startingPosition = _touch.position;
                        break;
                    case TouchPhase.Moved:
                        float diffX = Mathf.Abs(_startingPosition.x - _touch.position.x);
                        float diffY = Mathf.Abs(_startingPosition.y - _touch.position.y);

                        float startingFloat;
                        float touchFloat;
                        
                        if (diffX > diffY)
                        {
                            startingFloat = _startingPosition.x;
                            touchFloat = _touch.position.x;
                        }
                        else
                        {
                            startingFloat = _startingPosition.y;
                            touchFloat = _touch.position.y;
                        }
                        
                        if (startingFloat > touchFloat)
                        {
                            transform.Rotate(Vector3.down, _rotatespeed * Time.deltaTime);
                        }
                        else if (startingFloat < touchFloat)
                        {
                            transform.Rotate(Vector3.down, -_rotatespeed * Time.deltaTime);
                        }
                
                        break;
                }
            }
        }
    }
}