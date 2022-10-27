using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using PlayerNS;
using GamePieceNS;
using GamePieceNS.CharacterNS;
using TMPro;
using UnityEngine.UI;

public class TestScene_Pathfinding : MonoBehaviour
{
    public Camera mainCamera;
    private GameMap gameMap;
    private PlayerStateController gameMapInputController;
    private PlayerInputController playerInputController;
    private List<Character> characters = new List<Character>();
    private Character character;
    private Vector2Int coords;
    private GridArea hoverGridArea;
    private bool canMoveToCoords;
    public GameObject pathfindingLabelPrefab;
    private Dictionary<Square, TextMeshProUGUI> pathfindingLabels;
    private Dictionary<Intersection, TextMeshProUGUI> intersectionLabels;
    public ToggleGroup toggleGroup;


    // Create character
    private void CreateCharacter()
    {
        character = Character.InstantiateCharacter();
        characters.Add(character);
        coords = new Vector2Int(1, 1);
        gameMap.AddCharacterAtCoords(character, coords);

        character = Character.InstantiateCharacter("Test Character 3x3");
        coords = new Vector2Int(4, 4);
        gameMap.AddCharacterAtCoords(character, coords);
        characters.Add(character);
        character = characters[0];
    }


    // Start scene
    public void StartScene()
    {
        gameMap = GameMap.InstantiateGameMap();
        playerInputController = PlayerInputController.InstantiatePlayerInputController();
        playerInputController.SetCamera(mainCamera);
        gameMapInputController = new PlayerStateController(gameMap);
        CreateCharacter();
        pathfindingLabels = new Dictionary<Square, TextMeshProUGUI>();
        intersectionLabels = new Dictionary<Intersection, TextMeshProUGUI>();
        foreach (Square square in gameMap.squareCoordsDict.Values)
        {
            GameObject label = Instantiate(pathfindingLabelPrefab);
            TextMeshProUGUI pathfindingLabel = label.GetComponentInChildren<TextMeshProUGUI>();
            pathfindingLabels[square] = pathfindingLabel;
            pathfindingLabel.text = "(f: " + square.fCost + "; g: " + square.gCost + ")";
            label.transform.position = square.bounds.center;
            label.gameObject.SetActive(false);
        }
        foreach (Intersection intersection in gameMap.intersectionDict.Values)
        {
            GameObject label = Instantiate(pathfindingLabelPrefab);
            TextMeshProUGUI pathfindingLabel = label.GetComponentInChildren<TextMeshProUGUI>();
            intersectionLabels[intersection] = pathfindingLabel;
            pathfindingLabel.text = "(f: " + intersection.fCost + "; g: " + intersection.gCost + ")";
            label.transform.position = intersection.bounds.center;
        }
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


    // Set character size
    public void SetCharacterSize(string size)
    {
        if (size == "2x2")
        {
            foreach (TextMeshProUGUI label in intersectionLabels.Values)
            {
                label.GetComponentInParent<GameObject>().SetActive(true);
            }
            foreach (TextMeshProUGUI label in pathfindingLabels.Values)
            {
                label.GetComponentInParent<GameObject>().SetActive(false);
            }
        }
        else
        {
            foreach (TextMeshProUGUI label in intersectionLabels.Values)
            {
                label.GetComponentInParent<GameObject>().SetActive(false);
            }
            foreach (TextMeshProUGUI label in pathfindingLabels.Values)
            {
                label.GetComponentInParent<GameObject>().SetActive(true);
            }
        }
    }


    // Get move path
    private void GetMovePath()
    {
        
        GridArea gridArea = gameMap.GetGridAreaAtWorldPosition(character.sizeType, mainCamera.ScreenToWorldPoint(Input.mousePosition));
        if (gridArea == null || gridArea == hoverGridArea)
        {
            return;
        }

        Debug.Log("Hovering grid area: " + gridArea.coords);
        hoverGridArea = gridArea;
        List<GridArea> path = Pathfinding.GetPath(gameMap, character, character.gridArea.coords, gridArea.coords);

        if (character.sizeType == GamePieceSizeType.Even)
        {
            foreach (Intersection intersection in intersectionLabels.Keys)
            {
                Debug.Log("Setting label text: " + intersection.coords + "; f: " + intersection.fCost + " ; g: " + intersection.gCost);
                intersectionLabels[intersection].text = "(f: " + intersection.fCost + "; g: " + intersection.gCost + ")";
            }
        }
        else
        {
            foreach (Square square in pathfindingLabels.Keys)
            {
                Debug.Log("Setting label text: " + square.coords + "; f: " + square.fCost + " ; g: " + square.gCost);
                pathfindingLabels[square].text = "(f: " + square.fCost + "; g: " + square.gCost + ")";
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StartScene();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovePath();
    }
}
