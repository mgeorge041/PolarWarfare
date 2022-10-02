using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMapNS.GameMapBuilderNS
{
    public class GameMapBuilderInputController : MonoBehaviour
    {
        private Camera mainCamera;
        public float moveSpeed = 1;
        public bool isDisabled;
        

        // Instantiate
        public static GameMapBuilderInputController InstantiateGameMapBuilderInputController()
        {
            GameMapBuilderInputController inputController = Instantiate(Resources.Load<GameMapBuilderInputController>(ENV.GAMEMAPBUILDER_RESOURCE_PREFAB_PATH + "Game Map Builder Input Controller")).GetComponent<GameMapBuilderInputController>();
            inputController.Initialize();
            return inputController;
        }


        // Initialize
        public void Initialize()
        {
            GameMapBuilderEventManager.setDisabled += SetDisabled;
        }


        // Disable
        public void SetDisabled(bool disabled)
        {
            isDisabled = disabled;
        }


        // Set camera
        public void SetCamera(Camera camera)
        {
            mainCamera = camera;
        }



        // Get world position for mouse click
        private Vector3 GetMouseWorldPosition(Vector3 mousePosition)
        {
            return mainCamera.ScreenToWorldPoint(mousePosition);
        }


        // Left click
        private void LeftClick(Vector3 worldPosition)
        {
            GameMapBuilderEventManager.OnLeftClicked(worldPosition);
        }


        // Right click
        private void RightClick(Vector3 worldPosition)
        {
            GameMapBuilderEventManager.OnRightClicked(worldPosition);
        }


        // Release right click
        private void ReleaseRightClick()
        {
            GameMapBuilderEventManager.OnReleaseRightClick();
        }


        // Hover
        private void Hover(Vector3 worldPosition)
        {
            GameMapBuilderEventManager.OnHovered(worldPosition);
        }


        // Get directional movement
        private void GetDirectionMovement()
        {
            float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            float moveY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
            if (moveX != 0 || moveY != 0)
            {
                GameMapBuilderEventManager.OnMovedDirection(new Vector2(moveX, moveY));
            }
        }


        // Scroll
        private void Scroll(float scroll)
        {
            GameMapBuilderEventManager.OnScrolled(scroll);
        }


        // Center click
        private void CenterClick(Vector3 worldPosition)
        {
            GameMapBuilderEventManager.OnCenterClicked(worldPosition);
        }


        // Release center click
        private void ReleaseCenterClick()
        {
            GameMapBuilderEventManager.OnReleaseCenterClick();
        }


        // Pressed button
        public void PressedButton(KeyCode keyCode)
        {
            GameMapBuilderEventManager.OnPressedButton(keyCode);
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isDisabled)
                return;

            // Left click
            if (Input.GetMouseButtonDown(0))
            {
                LeftClick(GetMouseWorldPosition(Input.mousePosition));
            }

            // Right click
            if (Input.GetMouseButtonDown(1))
            {
                RightClick(GetMouseWorldPosition(Input.mousePosition));
            }
            if (Input.GetMouseButtonUp(1))
            {
                ReleaseRightClick();
            }

            // Center click
            if (Input.GetMouseButtonDown(2))
            {
                Debug.Log("Center clicked");
                CenterClick(GetMouseWorldPosition(Input.mousePosition));
            }
            if (Input.GetMouseButtonUp(2))
            {
                ReleaseCenterClick();
            }

            // Hover
            Hover(GetMouseWorldPosition(Input.mousePosition));

            // Move camera
            GetDirectionMovement();

            // Zoom camera
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                Scroll(Input.GetAxis("Mouse ScrollWheel"));
            }

            // Press delete
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                PressedButton(KeyCode.Delete);
            }
        }
    }
}