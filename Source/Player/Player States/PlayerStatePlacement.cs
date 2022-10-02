using CharacterNS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;

namespace PlayerNS.PlayerStateNS
{
    public class PlayerStatePlacement : PlayerState
    {
        public List<Square> placementSquares = new List<Square>();
        private Square hoverSquare;
        private GridArea hoverGridArea;
        private CharacterPanel characterPanel;
        private CharacterStats characterStats;


        public PlayerStatePlacement(GameMap gameMap, PlayerStateController gameMapInputController)
        {
            this.gameMap = gameMap;
            this.gameMapInputController = gameMapInputController;
        }

        public override void StartState(Character selectedCharacter)
        {
            ClearGameMap();
            this.selectedCharacter = selectedCharacter;
            GameMapEventManager.clickedCharacterPanel += SetCharacterStats;
        }
        public override void EndState(PlayerStateType playerStateType)
        {
            ClearGameMap();
            selectedCharacter = null;
            placementSquares.Clear();
            hoverSquare = null;
            hoverGridArea = null;
            gameMapInputController.SetPlayerState(PlayerStateType.Default, selectedCharacter);
            GameMapEventManager.clickedCharacterPanel -= SetCharacterStats;
        }


        // Clear state
        private void ClearState()
        {
            ClearGameMap();
            selectedCharacter = null;
            placementSquares.Clear();
            hoverSquare = null;
            hoverGridArea = null;
            characterStats = null;
            characterPanel = null;
        }


        // Left click
        public override void LeftClick(Vector3 worldPosition)
        {
            Square square = gameMap.GetSquareAtWorldPosition(worldPosition);
            if (square == null || !square.hasCharacter)
            {
                ClearState();
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
                Character character = Character.InstantiateCharacter(characterStats);
                gameMap.AddCharacterAtCoords(character, hoverGridArea.coords);
                C.Destroy(characterPanel);
            }
            else
            {
                gameMap.MoveCharacterToCoords(selectedCharacter, hoverGridArea.coords);
                selectedCharacter.SetPosition(hoverGridArea.bounds.center);
            }
            ClearState();
            //EndState(PlayerStateType.Default);
        }


        // Hover
        public override void Hover(Vector3 worldPosition)
        {
            if (characterStats == null)
            {
                ClearGameMap();
                return;
            }

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


        // Pressed button
        public override void PressedButton(KeyCode keyCode)
        {
            /* Do nothing
             */
        }


        // Set character stats
        public void SetCharacterStats(CharacterPanel panel)
        {
            characterPanel = panel;
            characterStats = panel.characterStats;
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