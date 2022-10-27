using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GamePieceNS.CharacterNS;

public static class GameMapEventManager
{
    public static event Action<Vector3> squareLeftClicked;
    public static void OnSquareLeftClicked(Vector3 worldPosition) => squareLeftClicked.Invoke(worldPosition);

    public static event Action<Vector3> squareRightClicked;
    public static void OnSquareRightClicked(Vector3 worldPosition) => squareRightClicked.Invoke(worldPosition);

    public static event Action<Vector3> squareHovered;
    public static void OnSquareHovered(Vector3 worldPosition) => squareHovered.Invoke(worldPosition);

    public static event Action<KeyCode> pressedButton;
    public static void OnPressedButton(KeyCode keyCode) => pressedButton.Invoke(keyCode);

    public static event Action<CharacterPanel> clickedCharacterPanel;
    public static void OnClickedCharacterPanel(CharacterPanel stats) => clickedCharacterPanel.Invoke(stats);

    public static void Unsubscribe()
    {
        squareLeftClicked = null;
        squareRightClicked = null;
        squareHovered = null;
        pressedButton = null;
        clickedCharacterPanel = null;
    }
}
