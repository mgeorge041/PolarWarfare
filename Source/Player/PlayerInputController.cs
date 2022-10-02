using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerNS
{
    public class PlayerInputController : MonoBehaviour
    {
        private Camera playerCamera;
        public bool canLeftClick { get; private set; } = true;
        public bool canRightClick { get; private set; } = true;
        public bool canHover { get; private set; } = true;


        // Create player input controller
        public static PlayerInputController InstantiatePlayerInputController()
        {
            PlayerInputController inputController = Instantiate(Resources.Load<GameObject>("Prefabs/PlayerInputController")).GetComponent<PlayerInputController>();
            inputController.Initialize();
            return inputController;
        }


        // Initialize
        public void Initialize()
        {

        }


        // Set camera
        public void SetCamera(Camera camera)
        {
            playerCamera = camera;
        }


        // Get world position
        public Vector3 GetWorldPosition(Vector3 mousePosition)
        {
            return playerCamera.ScreenToWorldPoint(mousePosition);
        }


        // Enable/disable left click
        public void SetLeftClick(bool canLeftClick)
        {
            this.canLeftClick = canLeftClick;
        }


        // Enable/disable right click
        public void SetRightClick(bool canRightClick)
        {
            this.canRightClick = canRightClick;
        }


        // Enable/disable hover
        public void SetHover(bool canHover)
        {
            this.canHover = canHover;
        }


        // Player left clicked
        public void PlayerLeftClicked(Vector3 worldPosition)
        {
            GameMapEventManager.OnSquareLeftClicked(worldPosition);
        }


        // Player right clicked
        public void PlayerRightClicked(Vector3 worldPosition)
        {
            GameMapEventManager.OnSquareRightClicked(worldPosition);
        }


        // Player hovered
        public void PlayerHovered(Vector3 worldPosition)
        {
            GameMapEventManager.OnSquareHovered(worldPosition);
        }


        // Player pressed a button
        public void PlayerPressedButton(KeyCode keyCode)
        {
            GameMapEventManager.OnPressedButton(keyCode);
        }


        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && canLeftClick)
            {
                PlayerLeftClicked(GetWorldPosition(Input.mousePosition));
            }

            if (Input.GetMouseButtonDown(1) && canRightClick)
            {
                PlayerRightClicked(GetWorldPosition(Input.mousePosition));
            }

            if (canHover)
            {
                PlayerHovered(GetWorldPosition(Input.mousePosition));
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                PlayerPressedButton(KeyCode.A);
            }
        }
    }
}