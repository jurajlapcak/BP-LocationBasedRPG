using UnityEngine;

namespace LocationRPG
{
    public class LerpingController
    {
        private Vector3 _startPosition;

        private Vector3 _endPosition;

        //time to move from start to end
        private float _time;
        private float _timeStarted;
        private bool _isLerping;

        public bool IsLerping => _isLerping;


        public LerpingController(Vector3 startPosition)
        {
            _startPosition = startPosition;
            _endPosition = startPosition;
            _time = 1;
            _isLerping = false;
        }

        public LerpingController(float time)
        {
            _time = time;
            _isLerping = false;
        }

        public void StartLerping(Vector3 startPosition, Vector3 endPosition)
        {
            _isLerping = true;
            _startPosition = startPosition;
            _endPosition = endPosition;
            _timeStarted = Time.time;
        }

        public void StopLerping()
        {
            _isLerping = false;
        }

        public Vector3 Lerp()
        {
            float timeSinceStarted = Time.time - _timeStarted;
            float percentageCompleted = timeSinceStarted / _time;

            Vector3 position = Vector3.Lerp(_startPosition, _endPosition, percentageCompleted);

            if (percentageCompleted >= 1.0f)
            {
                StopLerping();
            }

            return position;
        }
    }
}