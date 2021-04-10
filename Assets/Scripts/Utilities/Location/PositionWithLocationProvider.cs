using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using UnityEngine;

namespace LocationRPG
{
    public class PositionWithLocationProvider : MonoBehaviour
    {
        private PlayerController _currentPlayer;

        //minimal distance (in metres) for a player position to update
        //distance counter for counting moved distance
        [SerializeField] private float minimalDistance = 1.0f;
        private DistanceController _distanceController;

        //lerping controller for smooth transition between positions
        private LerpingController _lerpingController;


        bool _isInitialized;
        private AbstractMap map;

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
                map = LocationProviderFactory.Instance.mapManager;
                _distanceController = new DistanceController(LocationProvider.CurrentLocation, 0);
                _lerpingController =
                    new LerpingController(map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude));
            };
        }

        private void Update()
        {
            if (_isInitialized)
            {
                Location newLocation = LocationProvider.CurrentLocation;

                float distance = (float) _distanceController.DistanceUpdate(newLocation);
                if (distance == 0)
                {
                    _distanceController.SetTimePassed(1.0f);
                    if (!_lerpingController.IsLerping)
                    {
                        _currentPlayer.Animation.ToggleIdle();
                    }
                }
                else
                {
                    if (distance >= minimalDistance)
                    {
                        _distanceController.Apply(newLocation, distance);
                        _lerpingController.StartLerping(transform.position,
                            map.GeoToWorldPosition(newLocation.LatitudeLongitude));
                        _distanceController.SetTimePassed(Time.deltaTime);
                        _currentPlayer.Animation.ToggleWalking();
                    }
                    else
                    {
                        _distanceController.Deny(distance, Time.deltaTime);
                        if (!_lerpingController.IsLerping)
                        {
                            _currentPlayer.Animation.ToggleIdle();
                        }
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (_isInitialized)
            {
                if (_lerpingController.IsLerping)
                {
                    transform.position = _lerpingController.Lerp();
                }
            }
        }
    }
}