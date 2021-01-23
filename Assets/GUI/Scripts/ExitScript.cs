using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour
{
    private Button _quitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        _quitButton = GetComponent<Button>();
        _quitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            Application.Quit(); 
    }
    void QuitGame () {
        Application.Quit ();
        Debug.Log("Game is exiting");
        //Just to make sure its working
    }
}