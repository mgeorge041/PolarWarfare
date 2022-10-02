using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;

public class TestScene_CreateGameMap : MonoBehaviour, ISceneManager
{
    private GameMap gameMap;


    // Start scene
    public void StartScene()
    {
        gameMap = GameMap.InstantiateGameMap();
    }


    // Reset scene
    public void ResetScene()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        StartScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
