using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Mapbox.CheapRulerCs;
using UnityEngine;

namespace Mapbox.Unity.Location
{
    public class DistanceController
    {
        private CheapRuler _cheapRuler;
        private Location _oldLocation;
        
        //total session's distance covered in metres [m]
        private double _distanceCovered;

        //distance that device covered since last update
        private double _distance;
        
        //speed that passed since the last transform of position [s]
        private double _timePassed;
        
        //speed that device is traveling at [m/s]
        private double _currentSpeed;

        public double Distance => _distance;

        public void SetTimePassed(double time)
        {
            this._timePassed = time;
        }

        public double TimePassed => _timePassed;

        public double CurrentSpeed => this._distance / this._timePassed;

        public DistanceController(Location oldLocation, double distanceCovered)
        {
            _cheapRuler = new CheapRuler(oldLocation.LatitudeLongitude.x
                , CheapRulerUnits.Meters);
            this._timePassed = 1;
            this._oldLocation = oldLocation;
            this._distanceCovered = distanceCovered;
            this._currentSpeed = 1;
        }

        //count distance 
        public double DistanceUpdate(Location newLocation)
        {
            double[] oldLatlong = {_oldLocation.LatitudeLongitude.x, _oldLocation.LatitudeLongitude.y};
            double[] newLatlong = {newLocation.LatitudeLongitude.x, newLocation.LatitudeLongitude.y};
            double distance = _cheapRuler.Distance(oldLatlong,newLatlong);
            this._distance = distance;
            return distance;
        }

        //remembers newLocation as location that player is at at the moment
        //Also adds covered distance to distance counter
        public double Apply(Location newLocation, double distance)
        {
            this._distanceCovered += distance;
            this._oldLocation = newLocation;
            return this.CurrentSpeed;
        }   
        public void Deny(double distance, double time)
        {
            this._timePassed += time;
        }
    }
}