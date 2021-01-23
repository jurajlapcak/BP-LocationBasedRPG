using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player currentPlayer;
    public Player CurrentPlayer => currentPlayer;

    private void Awake()
    {
        Assert.IsNotNull(currentPlayer);
    }
}
