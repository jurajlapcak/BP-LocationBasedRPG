using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;

namespace LocationRPG
{
    public class RotateCameraWithGyroscope : MonoBehaviour
    {
        private bool _gyroscopeEnabled;
        private Gyroscope _gyroscope;

        private float _rotationToNorth;

        private float _headingVelocity = 0f;

        private void Start()
        {
            _gyroscopeEnabled = EnableGyroscope();

            EnableCompass();

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

        private void EnableCompass()
        {
            if (Input.location.status != LocationServiceStatus.Running)
            {
                Input.location.Start();
            }

            Input.compass.enabled = true;
            UpdateCompass();
        }

        private void UpdateCompass()
        {
            _rotationToNorth = Input.compass.trueHeading;
            transform.rotation = Quaternion.Euler(0, _rotationToNorth, 0);
            GameObject.Find("rotation").GetComponent<Text>().text = _rotationToNorth.ToString("F6");
        }
        
        // private void OnApplicationFocus(bool hasFocus)
        // {
        //     if (hasFocus)
        //     {
        //         UpdateCompass();
        //     }
        // }

        private void FixedUpdate()
        {
            if (_gyroscopeEnabled)
            {
                // float deltaY = (Mathf.Rad2Deg * -Input.gyro.rotationRateUnbiased.y) * Time.deltaTime;
                //
                // Vector3 currentRot = transform.rotation.eulerAngles;
                //
                // currentRot += new Vector3(0, deltaY, 0);
                //
                // transform.rotation = Quaternion.Euler(currentRot);
            }
            
            if (Input.location.status == LocationServiceStatus.Running && Input.compass.enabled)
            {
                UpdateCompass();
                // float trueHeading = Input.compass.trueHeading;
                // float newRotationToNorth = Mathf.LerpAngle(_rotationToNorth, trueHeading, 0.1f);
                // newRotationToNorth = Mathf.SmoothDampAngle(_rotationToNorth, trueHeading,
                //     ref _headingVelocity, 0.1f);
                // float rotationDiff = newRotationToNorth - _rotationToNorth;
                // if (rotationDiff > 5f || rotationDiff < -5f)
                // {
                //     _rotationToNorth = newRotationToNorth;
                //     GameObject.Find("rotation").GetComponent<Text>().text = trueHeading.ToString("F6");
                //     transform.rotation = Quaternion.Euler(0, _rotationToNorth, 0);
                // }
            }
            else
            {
                GameObject.Find("rotation").GetComponent<Text>().text =
                    "Location not running: " + Input.location.status + " Input.compass.enabled:" +
                    Input.compass.enabled;
            
                Debug.Log("Location not running: " + Input.location.status);
            }
        }
    }
}