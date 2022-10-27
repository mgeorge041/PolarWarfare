using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using GamePieceNS.CharacterNS;
using PlayerNS;

public class TestScene_CharacterMovement : MonoBehaviour, ISceneManager
{
    public Camera mainCamera;
    private GameMap gameMap;
    private PlayerStateController gameMapInputController;
    private PlayerInputController playerInputController;
    private List<Character> characters = new List<Character>();
    private Character character;
    private CharacterStats characterStats;
    private Vector2Int coords;


    // Create character
    private void CreateCharacter()
    {
        characterStats = CharacterStats.LoadCharacterStats();
        coords = new Vector2Int(1, 1);
        character = gameMap.PlaceCharacterAtCoords(characterStats, coords);
        characters.Add(character);

        characterStats = CharacterStats.LoadCharacterStats("Test Character 3x3");
        coords = new Vector2Int(4, 4);
        character = gameMap.PlaceCharacterAtCoords(characterStats, coords);
        characters.Add(character);
    }


    // Start scene
    public void StartScene()
    {
        gameMap = GameMap.InstantiateGameMap();
        playerInputController = PlayerInputController.InstantiatePlayerInputController();
        playerInputController.SetCamera(mainCamera);
        gameMapInputController = new PlayerStateController(gameMap);
        CreateCharacter();
    }


    // Reset scene
    public void ResetScene()
    {
        gameMap.ResetGameMap();
        foreach (Character character in characters)
            Destroy(character.gameObject);
        characters.Clear();
        CreateCharacter();
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
