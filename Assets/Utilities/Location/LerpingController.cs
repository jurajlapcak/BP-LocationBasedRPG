namespace Utilities.Location
{
    using Mapbox.Unity.Location;
    using UnityEngine;
    public class LerpingController
    {
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        //time to move from start to end
        private float _time;
        private float _timeStarted;
        private float _distance;
        private bool _isLerping;
        
        public bool IsLerping => _isLerping;

        public float TimeStarted => _timeStarted;

        public LerpingController(Vector3 startPosition)
        {
            this._startPosition = startPosition;
            this._endPosition = startPosition;
            this._time = 1;
            this._distance = 1;
            this._isLerping = false;
        }


        public void StartLerping(Vector3 startPosition, Vector3 endPosition, float time)
        {
            this._isLerping = true;
            this._startPosition = startPosition;
            this._endPosition = endPosition;
            this._timeStarted = Time.time;
        }
        
        public void StopLerping()
        {
            this._isLerping = false;
            
        }

        public Vector3 Lerp()
        {
            float timeSinceStarted = Time.time - _timeStarted;
            float percentageCompleted = timeSinceStarted / _time;

            Vector3 position = Vector3.Lerp(_startPosition, _endPosition, percentageCompleted);

            if (percentageCompleted >= 1.0f)
            {
                this.StopLerping();
            }

            return position;
        }
    }
}