using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterNS;

namespace GameMapNS.GameMapBuilderNS.GameMapBuilderStateNS {
    public enum GameMapBuilderStateType
    {
        Character,
        Paint,
        Pan,
        None,
    };

    public abstract class GameMapBuilderState
    {
        protected GameMap gameMap;
        protected GameMapBuilderCamera builderCamera;
        protected Vector3 panPosition;
        protected bool isPanning;

        // State events
        public virtual void StartState(Vector3 worldPosition) { }
        public virtual void EndState(GameMapBuilderStateType stateType, Vector3 worldPosition) 
        {
            GameMapBuilderEventManager.OnChangedState(stateType, worldPosition);
        }

        // Mouse actions
        public virtual void LeftClick(Vector3 worldPosition) { }
        public virtual void RightClick(Vector3 worldPosition) { }
        public virtual void CenterClick(Vector3 worldPosition) 
        {
            isPanning = true;
            panPosition = builderCamera.mainCamera.WorldToScreenPoint(worldPosition);
        }
        public virtual void ReleaseCenterClick()
        {
            isPanning = false;
        }
        public virtual void Hover(Vector3 worldPosition) { }
        public virtual void PressButton(KeyCode keyCode) { }

        // Panning
        public virtual void Panning(Vector3 worldPosition)
        {
            Vector3 screenPosition = builderCamera.mainCamera.WorldToScreenPoint(worldPosition);
            if (panPosition == Vector3.zero || screenPosition == panPosition)
                return;

            Vector3 moveDiff = (screenPosition - panPosition) / (100f * builderCamera.scale);
            builderCamera.Move(-moveDiff);
            panPosition = screenPosition;
        }
    }


    public class GameMapBuilderStatePan : GameMapBuilderState
    {
        public GameMapBuilderStatePan(GameMap gameMap, GameMapBuilderCamera builderCamera)
        {
            this.gameMap = gameMap;
            this.builderCamera = builderCamera;
        }

        public override void StartState(Vector3 worldPosition)
        {
            CenterClick(worldPosition);
        }

        public override void LeftClick(Vector3 worldPosition)
        {
            EndState(GameMapBuilderStateType.Paint, worldPosition);
        }
        public override void RightClick(Vector3 worldPosition)
        {
            EndState(GameMapBuilderStateType.Paint, worldPosition);
        }

        public override void CenterClick(Vector3 worldPosition)
        {
            isPanning = true;
            panPosition = builderCamera.mainCamera.WorldToScreenPoint(worldPosition);
        }
        public override void Hover(Vector3 worldPosition)
        {
            Vector3 screenPosition = builderCamera.mainCamera.WorldToScreenPoint(worldPosition);
            if (panPosition == Vector3.zero || screenPosition == panPosition)
                return;

            Vector3 moveDiff = (screenPosition - panPosition) / (100f * builderCamera.scale);
            builderCamera.Move(-moveDiff);
            panPosition = screenPosition;
        }
    } 
}