using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using PlayerNS;
using GamePieceNS.CharacterNS;

public class TestScene_AddCharacter : MonoBehaviour
{
    public Camera mainCamera;
    private GameMap gameMap;
    private PlayerCamera playerCamera;
    private PlayerInputController playerInputController;
    private PlayerStateController playerStateController;
    private List<Character> characters = new List<Character>();


    // Create characters
    private void CreateCharacters()
    {
        // 2x2 characters
        Vector2Int[] characterCoords = new Vector2Int[]
        {
            new Vector2Int(1, 1),
            new Vector2Int(3, 4),
            new Vector2Int(4, 1),
            new Vector2Int(2, 7)
        };
        for (int i = 0; i < characterCoords.Length; i++)
        {
            Character character = gameMap.PlaceCharacterAtCoords(CharacterStats.LoadCharacterStats(), characterCoords[i]);
            characters.Add(character);
        }

        // 3x3 character
        Vector2Int coords = new Vector2Int(7, 5);
        Character character3x3 = gameMap.PlaceCharacterAtCoords(CharacterStats.LoadCharacterStats("Test Character 3x3"), coords);
        characters.Add(character3x3);
    }


    // Start scene
    public void StartScene()
    {
        gameMap = GameMap.InstantiateGameMap();
        playerCamera = PlayerCamera.InstantiatePlayerCamera(gameMap);
        playerInputController = PlayerInputController.InstantiatePlayerInputController();
        playerInputController.SetCamera(playerCamera.mainCamera);
        playerInputController.SetHover(false);
        playerStateController = new PlayerStateController(gameMap);
        CreateCharacters();
    }


    // Reset scene
    public void ResetScene()
    {
        foreach (Character character in characters)
        {
            C.Destroy(character);
        }
        characters.Clear();
        gameMap.ResetGameMap();
        C.Destroy(playerCamera);
        C.Destroy(playerInputController);
        StartScene();
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
