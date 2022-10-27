using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using PlayerNS;
using GamePieceNS.CharacterNS;

public class TestScene_CharacterPathfinding : MonoBehaviour
{
    public Camera mainCamera;
    private GameMap gameMap;
    private PlayerStateController gameMapInputController;
    private PlayerInputController playerInputController;
    private List<Character> characters = new List<Character>();
    private Character character;
    private Vector2Int coords;


    // Create character
    private void CreateCharacter()
    {
        character = Character.InstantiateCharacter();
        coords = new Vector2Int(1, 1);
        gameMap.AddCharacterAtCoords(character, coords);
        characters.Add(character);

        character = Character.InstantiateCharacter();
        coords = new Vector2Int(1, 4);
        gameMap.AddCharacterAtCoords(character, coords);
        characters.Add(character);


        character = Character.InstantiateCharacter("Test Character 3x3");
        coords = new Vector2Int(4, 4);
        gameMap.AddCharacterAtCoords(character, coords);
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
