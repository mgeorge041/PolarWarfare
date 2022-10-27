using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GamePieceNS.CharacterNS;

namespace GameMapNS.GameMapBuilderNS
{
    public class GameMapBuilderUI : MonoBehaviour
    {
        public int pixelWidth { get; private set; } = 200;
        public TMP_InputField mapWidthInput;
        public TMP_InputField mapHeightInput;
        public Transform tileButtonContainer;
        private List<GameMapTileButton> tileButtons = new List<GameMapTileButton>();
        private GameMapTileButton selectedTileButton;
        public Transform characterButtonContainer;
        private List<CharacterButton> characterButtons = new List<CharacterButton>();
        private CharacterButton selectedCharacterButton;

        // Save map window
        public Transform saveMapWindow;
        public TMP_InputField mapNameInput;
        public Button saveMapButton;
        public TextMeshProUGUI savedLabel;


        // Instantiate
        public static GameMapBuilderUI InstantiateGameMapBuilderUI()
        {
            GameMapBuilderUI mapBuilderUI = Instantiate(Resources.Load<GameMapBuilderUI>(ENV.GAMEMAPBUILDER_RESOURCE_PREFAB_PATH + "Game Map Builder UI")).GetComponent<GameMapBuilderUI>();
            mapBuilderUI.Initialize();
            return mapBuilderUI;
        }


        // Initialize
        public void Initialize()
        {
            CreateTileButtons();
            CreateCharacterButtons();
            mapWidthInput.text = 10.ToString();
            mapHeightInput.text = 10.ToString();
            GameMapBuilderEventManager.clickedGameMapTileButton += ClickedTileButton;
            GameMapBuilderEventManager.clickedCharacterButton += ClickedCharacterButton;
        }


        // Unsubscribe on destroy
        public void OnDestroy()
        {
            GameMapBuilderEventManager.clickedGameMapTileButton -= ClickedTileButton;
            GameMapBuilderEventManager.clickedCharacterButton -= ClickedCharacterButton;
        }


        // Clear tile button
        private void ClearTileButton()
        {
            if (selectedTileButton != null)
            {
                selectedTileButton.SetButtonBackground(false);
            }
        }


        // Create tile button
        private void CreateTileButton(string tileName)
        {
            TileInfo tileInfo = TileInfo.LoadTileInfo(tileName);
            GameMapTileButton tileButton = GameMapTileButton.InstantiateGameMapTileButton(tileInfo);
            tileButton.transform.SetParent(tileButtonContainer);
            tileButtons.Add(tileButton);
        }


        // Create terrain tiles
        public void CreateTileButtons()
        {
            CreateTileButton("Snow");
            CreateTileButton("Water");
            CreateTileButton("Rock");
        }


        // Clicked tile button
        public void ClickedTileButton(GameMapTileButton tileButton)
        {
            if (selectedTileButton != tileButton)
            {
                ClearTileButton();
            }
            selectedTileButton = tileButton;
            selectedTileButton.SetButtonBackground(true);
            ClearCharacterButton();
        }


        // Clear character button
        private void ClearCharacterButton()
        {
            if (selectedCharacterButton != null)
            {
                selectedCharacterButton.SetButtonBackground(false);
            }
        }


        // Create character button
        private void CreateCharacterButton(string characterName)
        {
            CharacterStats stats = CharacterStats.LoadCharacterStats(characterName);
            CharacterButton button = CharacterButton.InstantiateCharacterButton(stats);
            button.transform.SetParent(characterButtonContainer);
            characterButtons.Add(button);
        }


        // Create character buttons
        public void CreateCharacterButtons()
        {
            CharacterStats stats = CharacterStats.LoadCharacterStats();
            CharacterButton button = CharacterButton.InstantiateCharacterButton(stats);
            button.transform.SetParent(characterButtonContainer);
            characterButtons.Add(button);
            CreateCharacterButton("Test Character 3x3");
        }


        // Clicked character button
        public void ClickedCharacterButton(CharacterButton characterButton)
        {
            if (selectedCharacterButton != characterButton)
            {
                ClearCharacterButton();
            }
            selectedCharacterButton = characterButton;
            selectedCharacterButton.SetButtonBackground(true);
            ClearTileButton();
        }


        // Change map width input
        public void ChangeMapWidthInput()
        {
            int mapWidth;
            if (int.TryParse(mapWidthInput.text, out mapWidth))
                GameMapBuilderEventManager.OnChangedMapWidth(mapWidth);
        }


        // Change map height input
        public void ChangeMapHeightInput()
        {
            int mapHeight;
            if (int.TryParse(mapHeightInput.text, out mapHeight))
                GameMapBuilderEventManager.OnChangedMapHeight(mapHeight);
        }


        // Open save map window
        public void OpenSaveMapWindow()
        {
            GameMapBuilderEventManager.OnSetDisabled(true);
            saveMapWindow.gameObject.SetActive(true);
        }


        // Clicked save map button
        public void ClickedSaveMapButton()
        {
            GameMapBuilderEventManager.OnClickedSaveMap(mapNameInput.text);
            mapNameInput.gameObject.SetActive(false);
            saveMapButton.gameObject.SetActive(false);
            savedLabel.gameObject.SetActive(true);
        }


        // Close saved map window
        public void CloseSaveMapWindow()
        {
            saveMapWindow.gameObject.SetActive(false);
            GameMapBuilderEventManager.OnSetDisabled(false);
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}