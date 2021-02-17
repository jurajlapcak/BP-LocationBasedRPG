using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocationRPG
{
    public class MenuPanelHandler : MonoBehaviour
    {
        public GameObject Panel;

        public void OpenPanel()
        {
            if (Panel != null)
            {
                if (!Panel.activeSelf)
                {
                    Panel.SetActive(true);
                }
                else
                {
                    ClosePanel();
                }
            }
        }

        public void ClosePanel()
        {
            if (Panel != null)
            {
                Panel.SetActive(false);
            }
        }
    }
}