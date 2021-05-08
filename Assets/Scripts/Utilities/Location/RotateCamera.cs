using Mapbox.Unity.Location;
using UnityEngine;

namespace LocationRPG
{
    /// <summary>
    /// Modified version on Mapbox class RotateWithLocationProvider.cs without unwanted functionality 
    /// </summary>
    public class RotateCamera : MonoBehaviour
    {
        /// The rate at which the transform's rotation tries catch up to the provided heading.  
        [SerializeField]
        [Tooltip("The rate at which the transform's rotation tries catch up to the provided heading. ")]
        float rotationFollowFactor = 1;

        Quaternion _targetRotation;

        ILocationProvider _locationProvider;

        public ILocationProvider LocationProvider
        {
            private get
            {
                if (_locationProvider == null)
                {
                    _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
                }

                return _locationProvider;
            }
            set
            {
                if (_locationProvider != null)
                {
                    _locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
                }

                _locationProvider = value;
                _locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
            }
        }

        void Start()
        {
            LocationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
        }

        void OnDestroy()
        {
            if (LocationProvider != null)
            {
                LocationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
            }
        }

        void LocationProvider_OnLocationUpdated(Location location)
        {
            float rotationAngle = location.DeviceOrientation;

            // 'Orientation' changes all the time, pass through immediately
            if (rotationAngle > location.UserHeading)
            {
                rotationAngle = 360 - (rotationAngle - location.UserHeading);
            }
            else
            {
                rotationAngle = location.UserHeading - rotationAngle + 360;
            }

            if (rotationAngle < 0)
            {
                rotationAngle += 360;
            }

            if (rotationAngle >= 360)
            {
                rotationAngle -= 360;
            }

            _targetRotation = Quaternion.Euler(NewEulerAngles(rotationAngle));
        }

        private Vector3 NewEulerAngles(float newAngle)
        {
            var localRotation = transform.localRotation;
            var currentEuler = localRotation.eulerAngles;
            var euler = Mapbox.Unity.Constants.Math.Vector3Zero;

            euler.y = -newAngle;
            euler.x = currentEuler.x;
            euler.z = currentEuler.z;

            return euler;
        }

        void FixedUpdate()
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _targetRotation,
                Time.deltaTime * rotationFollowFactor);
        }
    }
}