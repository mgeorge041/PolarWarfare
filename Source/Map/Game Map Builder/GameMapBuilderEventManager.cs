using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameMapNS.GameMapBuilderNS.GameMapBuilderStateNS;

namespace GameMapNS.GameMapBuilderNS
{
    public static class GameMapBuilderEventManager
    {
        // Mouse and keyboard
        public static event Action<Vector3> leftClicked;
        public static void OnLeftClicked(Vector3 worldPosition) => leftClicked.Invoke(worldPosition);

        public static event Action<Vector3> rightClicked;
        public static void OnRightClicked(Vector3 worldPosition) => rightClicked.Invoke(worldPosition);

        public static event Action releaseRightClick;
        public static void OnReleaseRightClick() => releaseRightClick.Invoke();

        public static event Action<Vector3> hovered;
        public static void OnHovered(Vector3 worldPosition) => hovered.Invoke(worldPosition);

        public static event Action<float> scrolled;
        public static void OnScrolled(float scroll) => scrolled.Invoke(scroll);

        public static event Action<Vector3> centerClicked;
        public static void OnCenterClicked(Vector3 worldPosition) => centerClicked.Invoke(worldPosition);

        public static event Action releaseCenterClick;
        public static void OnReleaseCenterClick() => releaseCenterClick.Invoke();

        public static event Action<KeyCode> pressedButton;
        public static void OnPressedButton(KeyCode keyCode) => pressedButton.Invoke(keyCode);

        public static event Action<Vector2> movedDirection;
        public static void OnMovedDirection(Vector2 direction) => movedDirection.Invoke(direction);


        // UI events
        public static event Action<bool> setDisabled;
        public static void OnSetDisabled(bool disabled) => setDisabled.Invoke(disabled);

        public static event Action<GameMapBuilderStateType, Vector3> changedState;
        public static void OnChangedState(GameMapBuilderStateType stateType, Vector3 worldPosition) => changedState.Invoke(stateType, worldPosition);

        public static event Action<GameMapTileButton> clickedGameMapTileButton;
        public static void OnClickedGameMapTileButton(GameMapTileButton tileButton) => clickedGameMapTileButton.Invoke(tileButton);

        public static event Action<CharacterButton> clickedCharacterButton;
        public static void OnClickedCharacterButton(CharacterButton button) => clickedCharacterButton.Invoke(button);

        public static event Action<int> changedMapWidth;
        public static void OnChangedMapWidth(int mapWidth) => changedMapWidth.Invoke(mapWidth);

        public static event Action<int> changedMapHeight;
        public static void OnChangedMapHeight(int mapHeight) => changedMapHeight.Invoke(mapHeight);

        public static event Action<string> clickedSaveMap;
        public static void OnClickedSaveMap(string mapName) => clickedSaveMap.Invoke(mapName);
    }
}