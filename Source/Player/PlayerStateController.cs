using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using PlayerNS;
using PlayerNS.PlayerStateNS;
using GamePieceNS.CharacterNS;

public class PlayerStateController
{
    public GameMap gameMap;
    public PlayerInputController inputController;
    private Dictionary<PlayerStateType, PlayerState> playerStates;
    private PlayerStateType currentPlayerState = PlayerStateType.Default;


    // Constructor
    public PlayerStateController() { }
    public PlayerStateController(GameMap gameMap) 
    {
        this.gameMap = gameMap;
        GameMapEventManager.squareLeftClicked += LeftClickAtWorldPosition;
        GameMapEventManager.squareRightClicked += RightClickAtWorldPosition;
        GameMapEventManager.squareHovered += HoverAtWorldPosition;
        GameMapEventManager.pressedButton += PressedButton;

        playerStates = new Dictionary<PlayerStateType, PlayerState>() 
        {
            { PlayerStateType.Default, new PlayerStateDefault(gameMap, this) },
            { PlayerStateType.Move, new PlayerStateMove(gameMap, this) },
            { PlayerStateType.Attack, new PlayerStateAttack(gameMap, this) },
            { PlayerStateType.Placement, new PlayerStatePlacement(gameMap, this) }
        };

        SetPlayerState(currentPlayerState, null);
    }


    // Set player state
    public void SetPlayerState(PlayerStateType playerStateType, Character selectedCharacter)
    {
        currentPlayerState = playerStateType;
        playerStates[currentPlayerState].StartState(selectedCharacter);
    }


    // Get current player state
    public T GetCurrentPlayerState<T>() where T : PlayerState
    {
        if (playerStates[currentPlayerState].GetType() == typeof(T))
            return (T)playerStates[currentPlayerState];
        return null;
    }


    // Left click
    public void LeftClickAtWorldPosition(Vector3 worldPosition)
    {
        playerStates[currentPlayerState].LeftClick(worldPosition);
    }


    // Right click
    public void RightClickAtWorldPosition(Vector3 worldPosition)
    {
        playerStates[currentPlayerState].RightClick(worldPosition);
    }


    // Hover
    public void HoverAtWorldPosition(Vector3 worldPosition)
    {
        playerStates[currentPlayerState].Hover(worldPosition);
    }


    // Press A button
    public void PressedButton(KeyCode keyCode)
    {
        Debug.Log("Pressed key: " + keyCode);
        playerStates[currentPlayerState].PressedButton(keyCode);
    }

}
