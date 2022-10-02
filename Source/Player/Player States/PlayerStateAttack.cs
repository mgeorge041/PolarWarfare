using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using CharacterNS;

namespace PlayerNS.PlayerStateNS
{
    public class PlayerStateAttack : PlayerState
    {
        private List<LineRenderer> projectileLines;
        private List<Vector2> endPoints;
        private Vector3 hoverPosition;
        private List<Rectangle> projectileColliders;
        private List<CircleCollider2D> projectileDestroyers;
        private Dictionary<Vector2, CircleCollider2D> projectileTrajectories;
        private bool liveProjectiles = false;
        private bool createdProjectileItems = false;


        // Constructor
        public PlayerStateAttack(GameMap gameMap, PlayerStateController gameMapInputController)
        {
            this.gameMap = gameMap;
            this.gameMapInputController = gameMapInputController;
        }


        // Start and end state
        public override void StartState(Character selectedCharacter)
        {
            Debug.Log("Starting attack state");
            this.selectedCharacter = selectedCharacter;
            ResetGameMap();
            projectileColliders = new List<Rectangle>();
            projectileDestroyers = new List<CircleCollider2D>();
            projectileLines = new List<LineRenderer>();
            endPoints = new List<Vector2>();
            projectileTrajectories = new Dictionary<Vector2, CircleCollider2D>();
            Hover(Input.mousePosition);
            liveProjectiles = false;

            selectedCharacter.PrepareAttack();

        }
        public override void EndState(PlayerStateType playerStateType)
        {
            projectileColliders.Clear();
            endPoints.Clear();
            foreach (LineRenderer line in projectileLines)
                C.DestroyGameObject(line.gameObject);
            projectileLines.Clear();

            if (!liveProjectiles)
            {
                foreach (CircleCollider2D destroyer in projectileDestroyers)
                    C.DestroyGameObject(destroyer.gameObject);
                projectileDestroyers.Clear();
                selectedCharacter.EndPrepareAttack();
                projectileTrajectories.Clear();
            }
            
            gameMap.PaintGameMap();
            gameMapInputController.SetPlayerState(playerStateType, selectedCharacter);
            createdProjectileItems = false;

            
        }


        // Reset game map
        private void ResetGameMap()
        {
            gameMap.PaintGameMap();
            gameMap.PaintCharacterSelectSquares(selectedCharacter);
        }


        // Clear items
        private void ClearItems()
        {
            projectileColliders.Clear();
            endPoints.Clear();
            projectileTrajectories.Clear();
        }


        // Left click
        public override void LeftClick(Vector3 worldPosition)
        {
            Square square = gameMap.GetSquareAtWorldPosition(worldPosition);
            if (square == null || !square.HasSelectableCharacter() || square.character == selectedCharacter)
            {
                EndState(PlayerStateType.Default);
                return;
            }

            selectedCharacter = square.character;
            EndState(PlayerStateType.Move);
        }


        // Right click
        public override void RightClick(Vector3 worldPosition)
        {
            if (selectedCharacter == null)
                return;

            // Send projectile
            selectedCharacter.SetProjectiles(projectileTrajectories);
            liveProjectiles = true;
            EndState(PlayerStateType.Default);
        }


        // Hover
        public override void Hover(Vector3 worldPosition)
        {
            if (selectedCharacter == null || worldPosition == hoverPosition)
                return;

            hoverPosition = worldPosition;
            Vector2 cwp = new Vector2(selectedCharacter.worldPosition.x, selectedCharacter.worldPosition.y);

            // Get projectile information
            ProjectileInfo projInfo = selectedCharacter.projectileInfo;
            float projectileWidth = selectedCharacter.projectileInfo.pixelRadius / 100f;
            float projHalfWidth = projectileWidth / 2;

            // Clear prior lines
            ClearItems();

            // Initialize projectile game objects
            List<Vector2> trajectories = Projectile.CalculateSpread(projInfo, worldPosition, cwp);
            if (!createdProjectileItems)
                CreateProjectileItems(trajectories.Count);

            // Create projectile items
            foreach (Vector2 trajectory in trajectories)
            {
                int index = trajectories.IndexOf(trajectory);
                Rectangle collider = CreateProjectileCollider(index, trajectory, cwp, projHalfWidth);
                SetProjectileLine(index, collider);
            }

            // Show projectile targets
            ResetGameMap();
            foreach (Rectangle collider in projectileColliders)
            {
                List<Character> targetCharacters = new List<Character>();
                foreach (Square square in gameMap.squareCoordsDict.Values)
                {
                    if (square.hasCharacter && square.character != selectedCharacter
                        && collider.Overlaps(square.collider))
                    {
                        if (!targetCharacters.Contains(square.character))
                            targetCharacters.Add(square.character);

                    }
                }

                // Only hit closest character
                float minDist = 0;
                Character targetCharacter = null;
                foreach (Character character in targetCharacters)
                {
                    Vector3 characterDist = selectedCharacter.worldPosition - character.worldPosition;
                    if (minDist == 0 || characterDist.magnitude < minDist)
                    {
                        targetCharacter = character;
                        minDist = characterDist.magnitude;
                    }
                }

                if (targetCharacter != null)
                    gameMap.PaintCharacterAttackSquares(targetCharacter);
            }
        }


        // Press button
        public override void PressedButton(KeyCode keyCode)
        {
            if (keyCode == KeyCode.A)
                EndState(PlayerStateType.Move);
        }


        // Create projectile items
        private void CreateProjectileItems(int numProjectiles)
        {
            for (int i = 0; i < numProjectiles; i++)
            {
                LineRenderer line = GameObject.Instantiate(Resources.Load<LineRenderer>("Prefabs/Attack Line")).GetComponent<LineRenderer>();
                projectileLines.Add(line);
                CircleCollider2D endProjectileCollider = GameObject.Instantiate(Resources.Load<CircleCollider2D>("Prefabs/Projectile Destroyer")).GetComponent<CircleCollider2D>();
                projectileDestroyers.Add(endProjectileCollider);
            }
            createdProjectileItems = true;
        }


        // Create projectile colliders
        private Rectangle CreateProjectileCollider(int projectileNum, Vector2 trajectory, Vector2 characterPosition, float projHalfWidth)
        {
            float rangeDist = selectedCharacter.range * GridArea.SQUARE_WIDTH;
            Vector2 projectile = trajectory * rangeDist;
            Vector2 endPoint = characterPosition + projectile;
            endPoints.Add(endPoint);
            projectileTrajectories[endPoint] = projectileDestroyers[projectileNum];

            // Create projectile hit box
            Vector2 tangent = new Vector3(trajectory.y, -trajectory.x, 0) * projHalfWidth;
            Vector2 rightPoint = characterPosition + tangent;
            Vector2 leftPoint = characterPosition - tangent;
            Vector2 rightEndPoint = rightPoint + projectile;
            Vector2 leftEndPoint = leftPoint + projectile;

            Rectangle projectileCollider = new Rectangle(new Vector2[] { rightPoint, rightEndPoint, leftEndPoint, leftPoint });
            projectileColliders.Add(projectileCollider);

            SetProjectileDestroyer(projectileNum, trajectory, projHalfWidth, endPoint);
            return projectileCollider;
        }


        // Set projectile lines
        private void SetProjectileLine(int projectileNum, Rectangle projectileCollider)
        {
            LineRenderer projectileLine = projectileLines[projectileNum];
            projectileLine.positionCount = 4;
            projectileLine.SetPositions(projectileCollider.GetVertices());
        }


        // Set projectile destroyer colliders
        private void SetProjectileDestroyer(int projectileNum, Vector2 trajectory, float projHalfWidth, Vector2 endPoint)
        {
            CircleCollider2D projectileDestroyer = projectileDestroyers[projectileNum];
            projectileDestroyer.transform.position = endPoint + trajectory * projHalfWidth;
            projectileDestroyer.radius = projHalfWidth;
        }
    }
}