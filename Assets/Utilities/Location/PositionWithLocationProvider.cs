using Animations;
using Characters.Player;
using Mapbox.CheapRulerCs;
using Mapbox.Utils;

namespace Utilities.Location
{
    using Mapbox.Unity.Location;
    using UnityEngine;

    public class PositionWithLocationProvider : MonoBehaviour
    {
        bool _isInitialized;
        private static int i = 0;

        private Player _currentPlayer;

        //minimal distance (in metres) for a player position to update 
        [SerializeField] private double minimalDistance = 1.0f;
        private DistanceCounter _distanceCounter;

        [SerializeField] private double speed = 1f;
        private Rigidbody _rigidBody;
        ILocationProvider _locationProvider;

        ILocationProvider LocationProvider
        {
            get
            {
                //If not yet initialised initialise
                if (_locationProvider == null)
                {
                    _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
                }

                return _locationProvider;
            }
        }


        private void Awake()
        {
            _currentPlayer = GameManager.Instance.CurrentPlayer;
        }

        void Start()
        {
            LocationProviderFactory.Instance.mapManager.OnInitialized += () =>
            {
                _isInitialized = true;
                _distanceCounter = new DistanceCounter(LocationProvider.CurrentLocation, 0);
            };
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_isInitialized)
            {
                var map = LocationProviderFactory.Instance.mapManager;
                Location newLocation = LocationProvider.CurrentLocation;
                Vector3 newPosition = map.GeoToWorldPosition(newLocation.LatitudeLongitude);
                double distance = this._distanceCounter.Distance(newLocation);
                
                Debug.Log(i + " Distance: " + distance);
                
                if (distance >= this.minimalDistance)
                {
                    this._distanceCounter.SwapAndAdd(newLocation, distance);
                    if (newPosition != transform.position)
                    {
                        Vector3 direction = (newPosition - transform.position).normalized;
                        PositionTransition(direction);
                        i++;
                        //DEBUG
                        //STUFF
                        Debug.Log(i + " Direction: " + direction);
                        Debug.Log(i + " Wanted Position: " + newPosition);
                        Debug.Log(i + " Actual Position: " + transform.position);
                    }
                }
            }
        }

        private void PositionTransition(Vector3 direction)
        {
            _rigidBody.isKinematic = true;
            _currentPlayer.Animation.ChangeAnimation(AnimationConstants.PLAYER_WALKING);
            Debug.Log("New Position: " + (transform.position + (direction * ((float)speed * Time.deltaTime))));
            _rigidBody.MovePosition(transform.position + (direction * ((float)speed * Time.deltaTime)));
        }
    }
}