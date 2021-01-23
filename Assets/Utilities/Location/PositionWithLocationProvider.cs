using System;
using Animations;
using Characters.Player;
using Mapbox.Map;
using Mapbox.Unity.Map;

namespace Utilities.Location
{
    using Mapbox.Unity.Location;
    using UnityEngine;

    public class PositionWithLocationProvider : MonoBehaviour
    {

        private Player _currentPlayer;
        //minimal distance (in metres) for a player position to update 
        [SerializeField] private float minimalDistance = 1.0f;
        private DistanceController _distanceController;

        private LerpingController _lerpingController;

        private Rigidbody _rigidbody;
        
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



        public bool isLerping;

        
        private void Awake()
        {
            _currentPlayer = GameManager.Instance.CurrentPlayer;
            _rigidbody = _currentPlayer.GetComponent<Rigidbody>();
        }

        void Start()
        {
            LocationProviderFactory.Instance.mapManager.OnInitialized += () =>
            {
                _isInitialized = true;
                map =  LocationProviderFactory.Instance.mapManager;
                _distanceController = new DistanceController(LocationProvider.CurrentLocation, 0);
                this._lerpingController = new LerpingController(map.GeoToWorldPosition(LocationProvider.CurrentLocation.LatitudeLongitude));
            };
        }

        private void Update()
        {
            if (_isInitialized)
            {
                Location newLocation = LocationProvider.CurrentLocation;
                float distance = (float)this._distanceController.DistanceUpdate(newLocation);
                if (distance == 0)
                {
                    _distanceController.SetTimePassed(1.0f);
                    if (!this._lerpingController.IsLerping)
                    {
                        _currentPlayer.Animation.ChangeAnimation(AnimationConstants.PLAYER_IDLE);
                    }
                }
                else
                {
                    if (distance >= this.minimalDistance)
                    {
                        this._distanceController.Apply(newLocation, distance);
                        this._lerpingController.StartLerping(transform.position, map.GeoToWorldPosition(newLocation.LatitudeLongitude),1.0f);
                        this._distanceController.SetTimePassed(Time.deltaTime);
                        _currentPlayer.Animation.ChangeAnimation(AnimationConstants.PLAYER_WALKING);
                        Debug.Log("Wanted position: "+map.GeoToWorldPosition(newLocation.LatitudeLongitude));
                    }
                    else
                    {
                        this._distanceController.Deny(distance, Time.deltaTime);
                        if(!this._lerpingController.IsLerping){
                            _currentPlayer.Animation.ChangeAnimation(AnimationConstants.PLAYER_IDLE);
                        }
                    }
                }
            }

        }

        private void FixedUpdate()
        {
            if(_isInitialized){
                if (this._lerpingController.IsLerping)
                {
                    _rigidbody.MovePosition(_lerpingController.Lerp());
                    Debug.Log("Actualposition "+transform.position);
                }
            }
        }


    }
}