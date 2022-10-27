using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePieceNS.CharacterNS;

namespace GameMapNS.GameMapBuilderNS.GameMapBuilderStateNS
{
    public class GameMapBuilderStateCharacter : GameMapBuilderState
    {
        private Character selectedCharacter;
        private List<Square> placementSquares = new List<Square>();
        private GridArea hoverGridArea;
        private CharacterStats characterStats;
        private CharacterButton characterButton;


        public GameMapBuilderStateCharacter(GameMap gameMap, GameMapBuilderCamera builderCamera)
        {
            this.gameMap = gameMap;
            this.builderCamera = builderCamera;
            GameMapBuilderEventManager.clickedCharacterButton += SetCharacterButton;
        }

        public override void StartState(Vector3 worldPosition)
        {

        }
        public override void EndState(GameMapBuilderStateType stateType, Vector3 worldPosition)
        {
            base.EndState(stateType, worldPosition);
            ClearState();
        }


        // Clear state
        private void ClearState()
        {
            ClearGameMap();
            selectedCharacter = null;
            placementSquares.Clear();
            hoverGridArea = null;
            characterButton = null;
        }


        public override void LeftClick(Vector3 worldPosition)
        {
            Square square = gameMap.GetSquareAtWorldPosition(worldPosition);
            if (square == null || !square.hasCharacter)
            {
                ClearGameMap();

                // Reset to selected character button
                if (characterButton != null)
                {
                    selectedCharacter = null;
                    characterStats = characterButton.characterStats;
                    placementSquares.Clear();
                }
                return;
            }

            selectedCharacter = square.character;
            characterStats = selectedCharacter.characterStats;
            ClearGameMap();
            gameMap.PaintCharacterSelectSquares(selectedCharacter);
        }


        // Right click
        public override void RightClick(Vector3 worldPosition)
        {
            Square square = gameMap.GetSquareAtWorldPosition(worldPosition);
            if (square == null || !placementSquares.Contains(square))
            {
                return;
            }

            if (selectedCharacter == null)
            {
                gameMap.PlaceCharacterAtCoords(characterStats, hoverGridArea.coords);
            }
            else
            {
                gameMap.MoveCharacterToCoords(selectedCharacter, hoverGridArea.coords);
                selectedCharacter.SetPosition(hoverGridArea.bounds.center);
            }
            ClearGameMap();
            placementSquares.Clear();
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

            if (characterButton == null)
            {
                ClearGameMap();
                return;
            }

            // Get grid area
            GridArea area = gameMap.GetGridAreaAtWorldPosition(characterStats.sizeType, worldPosition);
            if (area == null || area == hoverGridArea)
            {
                return;
            }

            ClearGameMap();
            placementSquares.Clear();
            gameMap.PaintCharacterSelectSquares(selectedCharacter);
            hoverGridArea = area;

            // Check whether can place character
            bool canMoveToCoords;
            if (selectedCharacter == null)
                canMoveToCoords = gameMap.CanPlaceAtCoords(characterStats, area.coords);
            else
            {
                canMoveToCoords = gameMap.CanMoveToCoords(selectedCharacter, area.coords);
                gameMap.PaintCharacterSelectSquares(selectedCharacter);
            }
            if (canMoveToCoords)
            {
                placementSquares = gameMap.GetSquaresAtSquareCoords(area.GetSquareCoords(characterStats.radius));
                PaintCharacterPlacementTiles();
            }
        }


        // Press button
        public override void PressButton(KeyCode keyCode)
        {
            if (keyCode == KeyCode.Delete)
            {
                if (selectedCharacter != null)
                {
                    gameMap.RemoveCharacter(selectedCharacter);
                    selectedCharacter = null;
                    ClearGameMap();
                    placementSquares.Clear();
                }
            }
        }


        // Set character stats
        public void SetCharacterButton(CharacterButton characterButton)
        {
            this.characterButton = characterButton;
            characterStats = characterButton.characterStats;
            GameMapBuilderEventManager.OnChangedState(GameMapBuilderStateType.Character, Vector3.zero);
        }


        // Clear game map tiles
        private void ClearGameMap()
        {
            gameMap.PaintGameMap();
        }


        // Paint tiles for character placement
        private void PaintCharacterPlacementTiles()
        {
            foreach (Square square in placementSquares)
            {
                gameMap.PaintSquare(square, GameTiles.placementTile);
            }
        }
    }
}