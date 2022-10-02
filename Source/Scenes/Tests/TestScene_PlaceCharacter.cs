using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using CharacterNS;
using PlayerNS;
using PlayerNS.PlayerStateNS;
using UnityEngine.UI;

public class TestScene_PlaceCharacter : MonoBehaviour, ISceneManager
{
    public Camera mainCamera;
    public GameMap gameMap;
    public PlayerInputController playerInputController;
    public PlayerStateController gameMapInputController;
    public Character characterA;
    public Transform charactersPanel;
    public List<CharacterPanel> characterPanels = new List<CharacterPanel>();


    // Create characters
    private void CreateCharacters()
    {
        CharacterStats characterStats = CharacterStats.LoadCharacterStats();
        CharacterPanel characterPanel = CharacterPanel.InstantiateCharacterPanel(characterStats);
        characterPanels.Add(characterPanel);
        characterPanel.transform.SetParent(charactersPanel);

        characterStats = CharacterStats.LoadCharacterStats("Test Character 3x3");
        characterPanel = CharacterPanel.InstantiateCharacterPanel(characterStats);
        characterPanels.Add(characterPanel);
        characterPanel.transform.SetParent(charactersPanel);
    }

    // Start scene
    public void StartScene()
    {
        gameMap = GameMap.InstantiateGameMap();
        gameMapInputController = new PlayerStateController(gameMap);
        playerInputController = PlayerInputController.InstantiatePlayerInputController();
        playerInputController.SetCamera(mainCamera);
        CreateCharacters();
        gameMapInputController.SetPlayerState(PlayerStateType.Placement, characterA);
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
