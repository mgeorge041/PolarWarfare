using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePieceNS.CharacterNS;

namespace GameMapNS.GameMapBuilderNS.GameMapBuilderStateNS
{
    public class GameMapBuilderStatePaint : GameMapBuilderState
    {
        private GameMapTileButton selectedTileButton;
        private bool isDragging;


        public GameMapBuilderStatePaint(GameMap gameMap, GameMapBuilderCamera builderCamera)
        {
            this.gameMap = gameMap;
            this.builderCamera = builderCamera;
            GameMapBuilderEventManager.clickedGameMapTileButton += SetTileButton;
            GameMapBuilderEventManager.releaseRightClick += ReleaseRightClick;
        }


        // Right click
        public override void RightClick(Vector3 worldPosition)
        {
            Square square = gameMap.GetSquareAtWorldPosition(worldPosition);
            if (square == null || selectedTileButton == null)
                return;

            isDragging = true;
            PaintTerrainSquare(square);
        }


        // Release right click
        public void ReleaseRightClick()
        {
            isDragging = false;
        }


        // Hover
        public override void Hover(Vector3 worldPosition)
        {
            // Panning camera
            if (isPanning)
            {
                Panning(worldPosition);
                return;
            }

            if (!isDragging)
                return;

            Square square = gameMap.GetSquareAtWorldPosition(worldPosition);
            if (square == null || selectedTileButton == null)
                return;

            PaintTerrainSquare(square);
        }


        // Paint square at location
        private void PaintTerrainSquare(Square square)
        {
            gameMap.SetSquareTile(square, selectedTileButton.tileInfo);
        }


        // Set tile button
        public void SetTileButton(GameMapTileButton tileButton)
        {
            Debug.Log("Setting tile button: " + tileButton.tileInfo.tileName);
            selectedTileButton = tileButton;
            GameMapBuilderEventManager.OnChangedState(GameMapBuilderStateType.Paint, Vector3.zero);
        }
    }
}