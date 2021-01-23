using Mapbox.CheapRulerCs;

namespace Mapbox.Unity.Location
{
    public class DistanceCounter
    {
        private CheapRuler _cheapRuler;
        private Location _oldLocation;
        private double _distanceCovered;

        public DistanceCounter(Location oldLocation, double distanceCovered)
        {
            _cheapRuler = new CheapRuler(oldLocation.LatitudeLongitude.x
                , CheapRulerUnits.Meters);
            this._oldLocation = oldLocation;
            this._distanceCovered = distanceCovered;
        }

        //count distance 
        public double Distance(Location newLocation)
        {
            double[] oldLatlong = {_oldLocation.LatitudeLongitude.x, _oldLocation.LatitudeLongitude.y};
            double[] newLatlong = {newLocation.LatitudeLongitude.x, newLocation.LatitudeLongitude.y};
            double distance = _cheapRuler.Distance(oldLatlong,newLatlong);
            return distance;
        }

        //remembers newLocation as location that player is at at the moment
        //Also adds covered distance to distance counter
        public void SwapAndAdd(Location newLocation, double distance)
        {
            this._distanceCovered += distance;
            this._oldLocation = newLocation;
        }   
    }
}