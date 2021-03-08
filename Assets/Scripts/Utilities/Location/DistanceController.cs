using Mapbox.CheapRulerCs;
using Mapbox.Unity.Location;

namespace LocationRPG
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
            _timePassed = 1;
            _oldLocation = oldLocation;
            _distanceCovered = distanceCovered;
        }

        //count distance 
        public double DistanceUpdate(Location newLocation)
        {
            double[] oldLatlong = {_oldLocation.LatitudeLongitude.x, _oldLocation.LatitudeLongitude.y};
            double[] newLatlong = {newLocation.LatitudeLongitude.x, newLocation.LatitudeLongitude.y};
            double distance = _cheapRuler.Distance(oldLatlong,newLatlong);
            _distance = distance;
            return distance;
        }

        //remembers newLocation as location that player is at at the moment
        //Also adds covered distance to distance counter
        public void Apply(Location newLocation, double distance)
        {
            _distanceCovered += distance;
            _oldLocation = newLocation;
        }   
        public void Deny(double distance, double time)
        {
            _timePassed += time;
        }
    }
}