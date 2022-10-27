using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using PlayerNS;
using GamePieceNS.CharacterNS;

public class TestScene_CharacterTargeting : MonoBehaviour
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
            Character character = gameMap.PlaceCharacterAtCoords(CharacterStats.LoadCharacterStats(), characterCoords[i]);
            characters.Add(character);
        }

        Character character3x3 = gameMap.PlaceCharacterAtCoords(CharacterStats.LoadCharacterStats("Test Character 3x3"), new Vector2Int(6, 6));
        characters.Add(character3x3);
    }


    // Start scene
    public void StartScene()
    {
        gameMap = GameMap.InstantiateGameMap();
        playerCamera = PlayerCamera.InstantiatePlayerCamera(gameMap);
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
