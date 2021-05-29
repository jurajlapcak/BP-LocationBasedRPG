using System;

namespace LocationRPG
{
    [Serializable]
    public class DistanceData
    {
        private double _distanceCovered;

        public double DistanceCovered => _distanceCovered;

        public DistanceData(double distanceCovered)
        {
            this._distanceCovered = distanceCovered;
        }
    }
}