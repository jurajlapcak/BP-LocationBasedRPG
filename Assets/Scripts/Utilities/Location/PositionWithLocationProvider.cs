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

                latText.text = newLocation.LatitudeLongitude.x.ToString("F6");
                lngText.text = newLocation.LatitudeLongitude.y.ToString("F6");

                float distance = (float) _distanceController.DistanceUpdate(newLocation);
                if (distance == 0)
                {
                    _distanceController.SetTimePassed(1.0f);
                    if (!_lerpingController.IsLerping)
                    {
                        //change animation
                        _currentPlayer.AnimationController.ToggleIdle();
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

                        //change animation
                        _currentPlayer.AnimationController.ToggleWalking();
                    }
                    else
                    {
                        _distanceController.Deny(distance, Time.deltaTime);
                        if (!_lerpingController.IsLerping)
                        {
                            //change animation
                            _currentPlayer.AnimationController.ToggleIdle();
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