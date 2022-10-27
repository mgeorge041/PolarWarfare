using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMapNS;
using GamePieceNS.CharacterNS;
using UnityEngine.U2D;

namespace PlayerNS.PlayerStateNS
{
    public class PlayerStateMove : PlayerState
    {
        private GridArea hoverGridArea;
        private List<GridArea> path = new List<GridArea>();
        private List<GridArea> pathableAreas = new List<GridArea>();
        private List<LineRenderer> moveRanges = new List<LineRenderer>();
        private LineRenderer attackCircle;
        public Circle attackCircleCollider { get; private set; }
        private bool updatedCircle = false;
        public SpriteShapeController pathArrow;


        // Constructor
        public PlayerStateMove(GameMap gameMap, PlayerStateController gameMapInputController)
        {
            this.gameMap = gameMap;
            this.gameMapInputController = gameMapInputController;
        }


        // Start and end state
        public override void StartState(Character selectedCharacter)
        {
            this.selectedCharacter = selectedCharacter;
            ResetGameMap();
            CreateAttackCircle();
            CreateMovePathArrow();
            ClearMoveRanges();
            OutlineMoveRange();
            //Hover(Input.mousePosition);
        }
        public override void EndState(PlayerStateType playerStateType)
        {
            gameMap.ClearActionMap();
            gameMapInputController.SetPlayerState(playerStateType, selectedCharacter);

            hoverGridArea = null;
            selectedCharacter = null;
            updatedCircle = false;
            ClearMoveRanges();
            C.DestroyGameObject(attackCircle);
            C.DestroyGameObject(pathArrow);
        }


        // Reset game map
        private void ResetGameMap()
        {
            gameMap.ClearActionMap();
            gameMap.PaintCharacterSelectSquares(selectedCharacter);
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
            Debug.Log("Setting selected character: " + selectedCharacter.characterName);
            ResetGameMap();
            ClearMoveRanges();
            OutlineMoveRange();
        }


        // Right click 
        public override void RightClick(Vector3 worldPosition)
        {
            if (selectedCharacter == null)
                return;

            GridArea gridArea = gameMap.GetGridAreaAtWorldPosition(selectedCharacter.sizeType, worldPosition);
            if (gridArea == null)
                return;

            if (pathableAreas.Contains(gridArea))
            {
                gameMap.MoveCharacterToCoords(selectedCharacter, gridArea.coords);
                selectedCharacter.MoveCharacter(path);
            }
            EndState(PlayerStateType.Default);
        }


        // Hover
        public override void Hover(Vector3 worldPosition)
        {
            if (selectedCharacter == null)
                return;

            GridArea gridArea = gameMap.GetGridAreaAtWorldPosition(selectedCharacter.sizeType, worldPosition);
            if (gridArea == hoverGridArea)
            {
                return;
            }

            if (gridArea == null)
            {
                ResetGameMap();
                attackCircle.gameObject.SetActive(false);
                return;
            }

            Debug.Log("Hovering grid area: " + gridArea.coords);
            hoverGridArea = gridArea;
            ResetGameMap();

            // Check whether can move to target
            if (pathableAreas.Contains(gridArea))
            {
                path = Pathfinding.GetPath(gameMap, selectedCharacter, selectedCharacter.gridArea.coords, gridArea.coords);
                List<Square> pathSquares = gameMap.GetSquaresForPath(path, selectedCharacter);
                UpdateMovePathArrow(path);
                gameMap.PaintSquares(gameMap.intersectionTilemap, path, GameTiles.moveTile);
                gameMap.PaintCharacterSelectSquares(selectedCharacter);
                gameMap.PaintCharacterMoveSquares(selectedCharacter, gridArea.coords);
                UpdateAttackCircle(gridArea);
            }
            else
            {
                ResetGameMap();
                attackCircle.gameObject.SetActive(false);
            }
        }


        // Press button
        public override void PressedButton(KeyCode keyCode)
        {
            if (keyCode == KeyCode.A)
            {
                EndState(PlayerStateType.Attack);
            }
        }


        // Create edge line
        private List<Vector3> ProcessEdgeLine(Square startSquare, Vector2Int edgeDirection, List<Square> squares)
        {
            List<Vector3> edgePoints = new List<Vector3>();
            Vector3 startPoint = Vector3.zero;
            Vector3 currentPoint = Vector3.one;
            Square currentSquare = startSquare;
            Square prevSquare;
            Vector2Int currentDirection = edgeDirection;
            int iterations = 0;
            while (currentPoint != startPoint)
            {
                prevSquare = currentSquare; 
                currentSquare = gameMap.GetSquareAtSquareCoords(currentSquare.GetNeighborCoords(currentDirection));
                
                if (currentSquare == null || !squares.Contains(currentSquare))
                {
                    if (edgePoints.Count > 0)
                    {
                        currentPoint = prevSquare.GetEdgePointAtDirection(currentDirection);
                        edgePoints.Add(currentPoint);
                    }
                    else
                    {
                        startPoint = startSquare.GetEdgePointAtDirection(edgeDirection);
                        edgePoints.Add(startPoint);
                    }
                    currentDirection = Direction.GetNextSquareClockwiseDirection(currentDirection);
                    currentSquare = prevSquare;

                }
                else 
                {
                    currentDirection = Direction.GetNextSquareCounterClockwiseDirection(currentDirection);
                }
                iterations++;
                if (iterations > 1000)
                    break;

            }

            return edgePoints;
        }


        // Clear move ranges
        private void ClearMoveRanges()
        {
            foreach (LineRenderer moveRange in moveRanges)
            {
                C.DestroyGameObject(moveRange.gameObject);
            }
            moveRanges.Clear();
            pathableAreas.Clear();
        }


        // Outline move range
        private void OutlineMoveRange()
        {
            List<GridArea> targetMoveAreas = Pathfinding.GetMaxPath(gameMap, selectedCharacter);
            if (targetMoveAreas.Count == 0)
                return;

            // Filter move areas
            List<GridArea> moveableAreas = new List<GridArea>();
            foreach (GridArea targetArea in targetMoveAreas)
            {
                if (gameMap.CanMoveToCoords(selectedCharacter, targetArea.coords))
                    moveableAreas.Add(targetArea);
            }

            // Filter path to move areas
            foreach (GridArea moveableArea in moveableAreas)
            {
                //Debug.Log("Moveable area: " + moveableArea.coords);
                List<GridArea> path = Pathfinding.GetPath(gameMap, selectedCharacter, selectedCharacter.gridArea.coords, moveableArea.coords);
                //Pathfinding.PrintPath(path);
                if (path.Count <= selectedCharacter.speed && path.Count > 0)
                    pathableAreas.Add(moveableArea);
            }

            // Find first edge
            List<Square> targetMoveSquares = gameMap.GridAreasToSquares(pathableAreas, selectedCharacter);
            List<List<Vector3>> edgeLines = new List<List<Vector3>>();
            
            foreach (Square square in targetMoveSquares)
            {
                foreach (Vector2Int direction in Direction.squareDirections.Values)
                {
                    Square targetSquare = gameMap.GetSquareAtSquareCoords(square.GetNeighborCoords(direction));

                    if (targetSquare == null || !targetMoveSquares.Contains(targetSquare))
                    {
                        List<Vector3> newEdgeLine = ProcessEdgeLine(square, direction, targetMoveSquares);
                        bool processedLine = false;
                        foreach (List<Vector3> edgeLine in edgeLines)
                        {
                            foreach (Vector3 edgePoint in newEdgeLine)
                            {
                                if (edgeLine.Contains(edgePoint))
                                    processedLine = true;
                            }
                        }
                        if (!processedLine)
                            edgeLines.Add(newEdgeLine);
                    }
                }
            }


            
            foreach (List<Vector3> edgeLine in edgeLines)
            {
                LineRenderer moveRange = GameObject.Instantiate(Resources.Load<LineRenderer>("Prefabs/Move Range Outline")).GetComponent<LineRenderer>();
                moveRange.positionCount = edgeLine.Count;
                moveRange.SetPositions(edgeLine.ToArray());
                moveRanges.Add(moveRange);
            }
        }


        // Update attack circle
        private void UpdateAttackCircle(GridArea gridArea)
        {
            float range = selectedCharacter.range * GridArea.SQUARE_WIDTH;
            if (!updatedCircle)
            {
                attackCircle.transform.localScale = new Vector3(attackCircle.transform.localScale.x * range, attackCircle.transform.localScale.y * range, 1);
                updatedCircle = true;
            }
            attackCircle.transform.position = gridArea.bounds.center;
            attackCircle.gameObject.SetActive(true);

            attackCircleCollider.UpdateCenter(gridArea.bounds.center);
            attackCircleCollider.UpdateRadius(range);
            Debug.Log("Attack circle collider: " + attackCircleCollider);

            // Show attackable targets
            foreach (Square square in gameMap.squareCoordsDict.Values)
            {
                if (attackCircleCollider.OverlapsRectangle(square.collider) &&
                    square.hasCharacter && square.character != selectedCharacter)
                {
                    List<Square> characterSquares = square.character.squares;
                    foreach (Square characterSquare in characterSquares)
                    {
                        gameMap.PaintSquare(gameMap.actionTilemap, characterSquare, GameTiles.attackTile);
                    }
                }
            }
        }


        // Create attack circle
        private void CreateAttackCircle()
        {
            attackCircleCollider = new Circle();
            attackCircle = GameObject.Instantiate(Resources.Load<LineRenderer>("Prefabs/Attack Circle"));

            int positionCount = 30;
            attackCircle.positionCount = positionCount;

            float x;
            float y;
            float angle = 0;
            for (int i = 0; i < positionCount; i++)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * angle);
                y = Mathf.Cos(Mathf.Deg2Rad * angle);
                attackCircle.SetPosition(i, new Vector3(x, y, 0));
                angle += 360f / positionCount;
            }
            attackCircle.loop = true;
            attackCircle.gameObject.SetActive(false);
        }


        // Create move path arrow
        private void CreateMovePathArrow()
        {
            pathArrow = GameObject.Instantiate(Resources.Load<SpriteShapeController>("Prefabs/Path Arrow")).GetComponent<SpriteShapeController>();
        }


        // Update move path arrow
        private void UpdateMovePathArrow(List<GridArea> path)
        {
            Spline spline = pathArrow.spline;
            spline.Clear();
            foreach (GridArea gridArea in path)
            {
                spline.InsertPointAt(0, gridArea.bounds.center);
            }
        }
    }
}