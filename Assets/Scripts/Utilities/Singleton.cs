using UnityEngine;

namespace LocationRPG
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }

                if (Application.isPlaying)
                {
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }
    }
}