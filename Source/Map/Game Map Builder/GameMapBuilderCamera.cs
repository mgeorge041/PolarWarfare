using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMapNS.GameMapBuilderNS {
    public class GameMapBuilderCamera : MonoBehaviour
    {
        public Camera mainCamera;
        public GameMap gameMap { get; private set; }
        public int uiWidth { get; private set; }
        public const int PIXELS_PER_UNIT = 100;
        public int scale { get; private set; } = 1;
        private float minXPos;
        private float minYPos;
        private float maxXPos;
        private float maxYPos;


        // Instantiate
        public static GameMapBuilderCamera InstantiateGameMapBuilderCamera()
        {
            GameMapBuilderCamera builderCamera = Instantiate(Resources.Load<GameMapBuilderCamera>(ENV.GAMEMAPBUILDER_RESOURCE_PREFAB_PATH + "Game Map Builder Camera")).GetComponent<GameMapBuilderCamera>();
            builderCamera.Initialize();
            return builderCamera;
        }


        // Initialize
        public void Initialize()
        {
            CalculateOrthographicSize();
        }


        // Set game map
        public void SetGameMap(GameMap gameMap)
        {
            this.gameMap = gameMap;
            CalculateBounds();
            MoveToPosition(new Vector3(gameMap.mapPhysicalCenter.x, gameMap.mapPhysicalCenter.y));
        }


        // Set UI pixel width
        public void SetUIPixelWidth(int pixelWidth)
        {
            uiWidth = pixelWidth;
            CalculateBounds();
        }


        // Scroll
        public void Scroll(float scroll)
        {
            if (scroll > 0)
                scale = Mathf.Min(scale + 1, 5);
            else
                scale = Mathf.Max(scale - 1, 1);

            CalculateOrthographicSize();
            CalculateBounds();
            MoveToPosition(transform.position);
        }


        // Move to position
        public void MoveToPosition(Vector3 worldPosition)
        {
            transform.position = new Vector3(
                Mathf.Clamp(worldPosition.x, minXPos, maxXPos), 
                Mathf.Clamp(worldPosition.y, minYPos, maxYPos), 
                -10
            );
        }


        // Move
        public void Move(Vector2 direction)
        {
            MoveToPosition(new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, -10));
        }


        // Pan camera
        public void Pan(Vector3 worldPosition)
        {

        }


        // Set orthographic size
        public void CalculateOrthographicSize()
        {
            float orthoSize = (float)Screen.height / (2 * PIXELS_PER_UNIT * scale);
            mainCamera.orthographicSize = orthoSize;
        }


        // Calculate bounds
        public void CalculateBounds()
        {
            if (gameMap == null)
                return;

            Debug.Log("Map height: " + gameMap.mapPixelHeight);
            Debug.Log("Camera height: " + (mainCamera.orthographicSize * 2 * PIXELS_PER_UNIT));
            float heightDiff = gameMap.mapPixelHeight - (mainCamera.orthographicSize * 2 * PIXELS_PER_UNIT);
            if (heightDiff > 0)
            {
                float yOffset = heightDiff / 100 / 2;
                minYPos = -yOffset + gameMap.mapPhysicalCenter.y - GridArea.SQUARE_HEIGHT;
                maxYPos = yOffset + gameMap.mapPhysicalCenter.y + GridArea.SQUARE_HEIGHT;
            }
            else
            {
                minYPos = gameMap.mapPhysicalCenter.y - GridArea.SQUARE_HEIGHT;
                maxYPos = gameMap.mapPhysicalCenter.y + GridArea.SQUARE_HEIGHT;
            }

            Debug.Log("Map width: " + gameMap.mapPixelWidth);
            Debug.Log("Camera width: " + (Screen.width / scale));
            float widthDiff = gameMap.mapPixelWidth - (Screen.width / scale);
            if (widthDiff > 0)
            {
                float xOffset = widthDiff / 100 / 2;
                minXPos = -xOffset + gameMap.mapPhysicalCenter.x - GridArea.SQUARE_WIDTH;
                maxXPos = xOffset + gameMap.mapPhysicalCenter.x + GridArea.SQUARE_WIDTH + uiWidth / (100f * scale);
            }
            else
            {
                minXPos = gameMap.mapPhysicalCenter.x - GridArea.SQUARE_WIDTH;
                maxXPos = gameMap.mapPhysicalCenter.x + GridArea.SQUARE_WIDTH + uiWidth / (100f * scale);
            }
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