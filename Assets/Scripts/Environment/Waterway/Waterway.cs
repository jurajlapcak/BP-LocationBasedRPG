using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocationRPG
{
    public class Waterway : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.transform.position += new Vector3(0, 0.01f, 0);
        }
    }
}