using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : RPGSceneManager
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void monsterInterract(GameObject monster)
    {
        SceneManager.LoadScene(SceneTypes.FightScene.ToString());
    }
}
