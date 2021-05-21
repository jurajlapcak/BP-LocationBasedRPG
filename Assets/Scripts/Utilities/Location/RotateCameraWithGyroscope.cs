using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace LocationRPG
{
    public class RotateCameraWithGyroscope : MonoBehaviour
    {
        private bool _gyroscopeEnabled;
        private Gyroscope _gyroscope;

        // private List<Vector3> compassHeadings = new List<Vector3>();
        // public const float MaxHeadings = 24;
        private void Start()
        {
            _gyroscopeEnabled = EnableGyroscope();
            

            // UpdateCompassHeading(true);
        }

        private bool EnableGyroscope()
        {
            if (!SystemInfo.supportsGyroscope)
            {
                return false;
            }

            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;
            
            return true;
        }

        private void Update()
        {
            if (_gyroscopeEnabled)
            {
                float deltaY = (Mathf.Rad2Deg * -Input.gyro.rotationRateUnbiased.y) * Time.deltaTime;

                Vector3 currentRot = transform.rotation.eulerAngles;

                currentRot += new Vector3(0, deltaY, 0);

                transform.rotation = Quaternion.Euler(currentRot);
            }
        }
        
        // private void UpdateCompassHeading(bool rotatePlayer = false)
        // {
        //     compassHeadings.Add(new Vector3(0f, Input.compass.trueHeading, 0f));
        //
        //     if(compassHeadings.Count > MaxHeadings)
        //     {
        //         compassHeadings.RemoveAt(0);
        //     }
        //
        //
        //     float meanCurrentRotation = compassHeadings.Sum(vector => vector.y);
        //     float meanAverage = meanCurrentRotation / compassHeadings.Count;
        //     float northOffset = (365 - meanAverage);
        //
        //     if (rotatePlayer)
        //     {
        //         transform.rotation = Quaternion.Euler(new Vector3(0f, meanAverage, 0f));
        //     }
        //  
        // }

    }
}