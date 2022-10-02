using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using PlayerNS;
using CharacterNS;

public class TestScene_Penguin : MonoBehaviour
{
    private GameMap gameMap;
    private PlayerCamera playerCamera;
    private PlayerInputController playerInputController;
    private PlayerStateController playerStateController;
    private List<Character> characters = new List<Character>();


    // Create characters
    private void CreateCharacters()
    {
        Vector2Int[] characterCoords = new Vector2Int[]
        {
            new Vector2Int(1, 1),
            new Vector2Int(3, 3),
            new Vector2Int(3, 1),
            new Vector2Int(2, 6)
        };
        for (int i = 0; i < characterCoords.Length; i++)
        {
            Character character = gameMap.PlaceCharacterAtCoords(CharacterStats.LoadCharacterStats(CharacterType.Penguin, "Penguin"), characterCoords[i]);
            characters.Add(character);
        }
    }


    // Start scene
    public void StartScene()
    {
        gameMap = GameMap.InstantiateGameMap();
        playerCamera = PlayerCamera.InstantiatePlayerCamera(gameMap);
        playerCamera.Scroll(1);
        playerInputController = PlayerInputController.InstantiatePlayerInputController();
        playerInputController.SetCamera(playerCamera.mainCamera);
        playerStateController = new PlayerStateController(gameMap);
        CreateCharacters();
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
