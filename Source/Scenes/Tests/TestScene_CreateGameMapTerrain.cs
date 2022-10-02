using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using GameMapNS.GameMapBuilderNS;

public class TestScene_CreateGameMapTerrain : MonoBehaviour, ISceneManager
{
    public Camera mainCamera;
    public GameMapBuilderStateController stateController;
    public GameMapBuilderUI mapBuilderUI;
    public GameMapBuilderInputController inputController;
    public GameMapBuilderCamera builderCamera;
    public GameMap gameMap;


    // Start scene
    public void StartScene()
    {
        gameMap = GameMap.InstantiateGameMap();
        mapBuilderUI = GameMapBuilderUI.InstantiateGameMapBuilderUI();
        builderCamera = GameMapBuilderCamera.InstantiateGameMapBuilderCamera();
        builderCamera.SetGameMap(gameMap);
        builderCamera.SetUIPixelWidth(mapBuilderUI.pixelWidth);
        inputController = GameMapBuilderInputController.InstantiateGameMapBuilderInputController();
        inputController.SetCamera(builderCamera.mainCamera);
        
        stateController = new GameMapBuilderStateController(gameMap, builderCamera);
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
