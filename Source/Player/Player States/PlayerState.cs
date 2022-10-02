using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using CharacterNS;

namespace PlayerNS.PlayerStateNS {

    public enum PlayerStateType {
        Default,
        Move,
        Attack,
        Placement,
        None
    };

    public abstract class PlayerState
    {
        protected GameMap gameMap;
        protected PlayerStateController gameMapInputController;
        protected Character selectedCharacter;

        // State actions
        public abstract void StartState(Character selectedCharacter);
        public abstract void EndState(PlayerStateType playerStateType);

        // Player actions
        public abstract void LeftClick(Vector3 worldPosition);
        public abstract void RightClick(Vector3 worldPosition);
        public abstract void Hover(Vector3 worldPosition);
        public abstract void PressedButton(KeyCode keyCode);
    }


    // Default state where nothing has been selected
    public class PlayerStateDefault : PlayerState
    {
        private Square startSquare;


        // Constructor
        public PlayerStateDefault(GameMap gameMap, PlayerStateController gameMapInputController) 
        {
            this.gameMap = gameMap;
            this.gameMapInputController = gameMapInputController;
        }


        // Start and end state
        public override void StartState(Character selectedCharacter)
        {
            this.selectedCharacter = selectedCharacter;
        }
        public override void EndState(PlayerStateType playerStateType)
        {

        }


        // Mouse actions
        public override void LeftClick(Vector3 worldPosition)
        {
            Square square = gameMap.GetSquareAtWorldPosition(worldPosition);
            if (square == null || square == startSquare || !square.hasCharacter || !square.character.canMove)
            {
                EndState(PlayerStateType.Default);
                return;
            }

            selectedCharacter = square.character;
            gameMapInputController.SetPlayerState(PlayerStateType.Move, selectedCharacter);
        }
        public override void RightClick(Vector3 worldPosition)
        {
            
        }
        public override void Hover(Vector3 worldPosition)
        {
            
        }
        public override void PressedButton(KeyCode keyCode)
        {
            
        }
    }


    // State where character has been selected
    
}