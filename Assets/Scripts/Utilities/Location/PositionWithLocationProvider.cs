using System;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.UI;

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

        //testing
        private Text latText;
        private Text lngText;

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
            latText = GameObject.Find("lat").GetComponent<Text>();
            lngText = GameObject.Find("lng").GetComponent<Text>();
        }

        void Start()
        {
            LocationProviderFactory.Instance.mapManager.OnInitialized += () =>
            {
                map = LocationProviderFactory.Instance.mapManager;
                
                Location newLocation = LocationProvider.CurrentLocation;
                if (_isInitialized == false)
                {
                    latText.text = newLocation.LatitudeLongitude.x.ToString("F6");
                    lngText.text = newLocation.LatitudeLongitude.y.ToString("F6");
                    transform.position = map.GeoToWorldPosition(newLocation.LatitudeLongitude);
                }
                
                _distanceController = new DistanceController(LocationProvider.CurrentLocation, 0);
                _lerpingController =
                    new LerpingController(map.GeoToWorldPosition(newLocation.LatitudeLongitude));
                _isInitialized = true;
            };
        }

        private void Update()
        {
            if (_isInitialized)
            {
                Location newLocation = LocationProvider.CurrentLocation;

                latText.text = newLocation.LatitudeLongitude.x.ToString("F6");
                lngText.text = newLocation.LatitudeLongitude.y.ToString("F6");

                float distance = (float) _distanceController.DistanceUpdate(newLocation);
                if (distance >= minimalDistance)
                {
                    _distanceController.AddNewDistance(newLocation, distance);
                    _lerpingController.StartLerping(transform.position,
                        map.GeoToWorldPosition(newLocation.LatitudeLongitude));

                    //change animation
                    _currentPlayer.AnimationController.ToggleWalking();
                }
                else
                {
                    if (!_lerpingController.IsLerping)
                    {
                        //change animation
                        _currentPlayer.AnimationController.ToggleIdle();
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