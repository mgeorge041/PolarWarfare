using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterNS;

namespace GameMapNS {
    public class Pathfinding
    {
        // Print path
        public static void PrintPath(List<GridArea> path)
        {
            foreach (GridArea area in path)
            {
                Debug.Log("Grid area (" + area.GetType() + "): " + area.coords);
            }
        }


        // Get path
        public static List<GridArea> GetPath(GameMap gameMap, Character character, Vector2Int startCoords, Vector2Int targetCoords)
        {
            List<GridArea> path = new List<GridArea>();
            ResetPathfinding(gameMap);

            GridArea startArea = gameMap.GetGridAreaForCharacter(character, startCoords);
            GridArea targetArea = gameMap.GetGridAreaForCharacter(character, targetCoords);
            if (startArea == null || targetArea == null)
            {
                return path;
            }

            MinHeap openAreas = new MinHeap(gameMap.squareCoordsDict.Count);
            List<GridArea> closedGridAreas = new List<GridArea>();
            openAreas.Insert(startArea);

            // Loop through areas
            while (openAreas.size > 0)
            {
                GridArea currentArea = openAreas.Pop<GridArea>();
                closedGridAreas.Add(currentArea);
                //Debug.Log("Current area: " + currentArea.coords + ": " + currentArea.GetType());

                // Return when reached target
                if (currentArea == targetArea)
                {
                    path = GetPathBack(startArea, targetArea);
                    return path;
                }

                // Get squares for move position
                List<Vector2Int> neighborMoveCoords = currentArea.GetMovementNeighborCoords();
                foreach (Vector2Int neighborCoords in neighborMoveCoords)
                {
                    GridArea neighbor = gameMap.GetGridAreaForCharacter(character, neighborCoords);
                    if (neighbor == null)
                        continue;

                    // Check grid area for mobility
                    if (!gameMap.CanMoveToCoords(character, neighbor.coords) || closedGridAreas.Contains(neighbor))
                    {
                        continue;
                    }

                    // Update neighbor costs
                    int newDist = currentArea.fCost + Direction.GetDistanceBetweenGridAreas(currentArea, neighbor);
                    if (newDist < neighbor.fCost || !openAreas.Contains(neighbor))
                    {
                        neighbor.fCost = newDist;
                        neighbor.gCost = Direction.GetDistanceBetweenGridAreas(neighbor, targetArea);
                        neighbor.pathParent = currentArea;

                        // Add new neighbor to process
                        if (!openAreas.Contains(neighbor))
                        {
                            openAreas.Insert(neighbor);
                        }
                    }
                }
            }

            return path;
        }


        // Get max path
        public static List<GridArea> GetMaxPath(GameMap gameMap, Character character)
        {
            List<GridArea> maxPath = new List<GridArea>();

            if (character.sizeType == CharacterSizeType.Even)
            {
                foreach (GridArea area in gameMap.intersectionDict.Values)
                {
                    if (Direction.GetDistanceBetweenGridAreas(area, character.gridArea) <= character.speed)
                    {
                        maxPath.Add(area);
                    }
                }
            }
            else
            {
                foreach (GridArea area in gameMap.squareCoordsDict.Values)
                {
                    if (Direction.GetDistanceBetweenGridAreas(area, character.gridArea) <= character.speed)
                    {
                        maxPath.Add(area);
                    }
                }
            }
            
            return maxPath;
        }


        // Get path back
        public static List<GridArea> GetPathBack(GridArea startArea, GridArea targetArea)
        {
            List<GridArea> path = new List<GridArea>();
            while (startArea != targetArea)
            {
                path.Add(targetArea);
                targetArea = targetArea.pathParent;
            }
            path.Add(startArea);
            path.Reverse();
            return path;
        }


        // Reset pathfinding
        public static void ResetPathfinding(GameMap gameMap)
        {
            foreach (GridArea area in gameMap.intersectionDict.Values)
            {
                area.fCost = 0;
                area.gCost = 0;
                area.pathParent = null;
            }
            foreach (GridArea area in gameMap.squareCoordsDict.Values)
            {
                area.fCost = 0;
                area.gCost = 0;
                area.pathParent = null;
            }
        }
    }
}