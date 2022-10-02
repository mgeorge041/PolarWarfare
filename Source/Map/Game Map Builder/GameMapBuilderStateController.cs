using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS.GameMapBuilderNS.GameMapBuilderStateNS;
using System.IO;

namespace GameMapNS.GameMapBuilderNS
{
    public class GameMapBuilderStateController
    {
        public GameMap gameMap;
        public GameMapTileButton selectedTileButton;
        public GameMapBuilderUI mapBuilderUI;
        public GameMapBuilderCamera builderCamera;
        private Dictionary<GameMapBuilderStateType, GameMapBuilderState> builderStates;
        private GameMapBuilderStateType currentState = GameMapBuilderStateType.Paint;
        public bool isDisabled { get; private set; }


        // Instantiate
        public GameMapBuilderStateController() { }
        public GameMapBuilderStateController(GameMap gameMap, GameMapBuilderCamera builderCamera)
        {
            this.gameMap = gameMap;
            this.builderCamera = builderCamera;
            Initialize();
        }


        // Initialize
        public void Initialize()
        {
            GameMapBuilderEventManager.leftClicked += LeftClick;
            GameMapBuilderEventManager.rightClicked += RightClick;
            GameMapBuilderEventManager.hovered += Hover;
            GameMapBuilderEventManager.scrolled += Scroll;
            GameMapBuilderEventManager.centerClicked += CenterClick;
            GameMapBuilderEventManager.releaseCenterClick += ReleaseCenterClick;
            GameMapBuilderEventManager.pressedButton += PressedButton;
            GameMapBuilderEventManager.movedDirection += MoveDirection;
            GameMapBuilderEventManager.changedMapWidth += ChangeMapWidth;
            GameMapBuilderEventManager.changedMapHeight += ChangeMapHeight;
            GameMapBuilderEventManager.changedState += SetGameMapBuilderState;
            GameMapBuilderEventManager.clickedSaveMap += SaveTilemap;

            builderStates = new Dictionary<GameMapBuilderStateType, GameMapBuilderState>()
            {
                { GameMapBuilderStateType.Paint, new GameMapBuilderStatePaint(gameMap, builderCamera) },
                { GameMapBuilderStateType.Character, new GameMapBuilderStateCharacter(gameMap, builderCamera) },
            };

            SetGameMapBuilderState(currentState, Vector3.zero);
        }


        // Set state
        public void SetGameMapBuilderState(GameMapBuilderStateType stateType, Vector3 worldPosition)
        {
            Debug.Log("Setting builder state: " + stateType);
            currentState = stateType;
            builderStates[currentState].StartState(worldPosition);
        }


        // Change map width
        public void ChangeMapWidth(int mapWidth)
        {
            if (!isDisabled)
            {
                gameMap.UpdateMapWidth(mapWidth);
                builderCamera.CalculateBounds();
            }
        }


        // Change map height
        public void ChangeMapHeight(int mapHeight)
        {
            if (!isDisabled)
            {
                gameMap.UpdateMapHeight(mapHeight);
                builderCamera.CalculateBounds();
            }
        }


        // Left click
        public void LeftClick(Vector3 worldPosition)
        {
            if (!isDisabled)
                builderStates[currentState].LeftClick(worldPosition);
        }


        // Right click
        public void RightClick(Vector3 worldPosition)
        {
            if (!isDisabled)
                builderStates[currentState].RightClick(worldPosition);
        }


        // Hover
        public void Hover(Vector3 worldPosition)
        {
            if (!isDisabled)
                builderStates[currentState].Hover(worldPosition);
        }


        // Scroll
        public void Scroll(float scroll)
        {
            if (!isDisabled)
                builderCamera.Scroll(scroll);
        }


        // Center click
        public void CenterClick(Vector3 worldPosition)
        {
            if (!isDisabled)
                builderStates[currentState].CenterClick(worldPosition);
        }


        // Release center click
        public void ReleaseCenterClick()
        {
            if (!isDisabled)
                builderStates[currentState].ReleaseCenterClick();
        }


        // Move direction
        public void MoveDirection(Vector2 direction)
        {
            if (!isDisabled)
                builderCamera.Move(direction);
        }


        // Button pressed
        public void PressedButton(KeyCode keyCode)
        {
            if (!isDisabled)
                builderStates[currentState].PressButton(keyCode);
        }


        // Save tilemap
        public void SaveTilemap(string mapName)
        {
            string filepath = "Project Files/Maps/" + mapName + ".json";
            string squareData = "";
            foreach (Square square in gameMap.squareCoordsDict.Values)
            {
                squareData += JsonUtility.ToJson(new Square.SquareData(square));
            }
            File.WriteAllText(filepath, squareData);
            Debug.Log("Saved tilemap");
        }


        // Load tilemap
        public void LoadTilemap()
        {
            string filepath = "Assets/Resources/Map/map.txt";
            StreamReader reader = new StreamReader(filepath);
            /*
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                line = line.Replace(")", string.Empty);
                line = line.Replace("(", string.Empty);
                string[] lineSplit = line.Split(':');
                string coordsString = lineSplit[0];
                string tileName = lineSplit[1];
                string[] coordsSplit = coordsString.Split(',');
                Vector3Int tileCoords = new Vector3Int(int.Parse(coordsSplit[0]), int.Parse(coordsSplit[1]), int.Parse(coordsSplit[2]));
                Debug.Log("Setting coords: " + tileCoords + " to: " + tileName);
                HexStats hexStats = HexStats.LoadHexStats(tileName);
                hexMap.SetTile(tileCoords, hexStats.tile);
                hexMap.GetHexAtTileCoords(tileCoords).hexStats = hexStats;
            }
            */
        }
    }
}