using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using CharacterNS;

namespace GameMapNS
{
    public class GameMap : MonoBehaviour
    {
        // Tilemap
        public Tilemap squareTilemap;
        public Tilemap terrainTilemap;
        public Grid squareGrid;
        public Grid intersectionGrid;

        // Map tile size
        public int mapSquareWidth { get; private set; } = 10;
        public int mapSquareHeight { get; private set; } = 10;

        // Map physical size
        public int mapPixelWidth { get { return mapSquareWidth * (int)(GridArea.SQUARE_WIDTH * 100); } }
        public int mapPixelHeight { get { return mapSquareHeight * (int)(GridArea.SQUARE_HEIGHT * 100); } }
        public Vector2Int mapPixelCenter { get { return new Vector2Int(mapPixelWidth / 2, mapPixelHeight / 2); } }

        public float mapPhysicalWidth { get { return mapSquareWidth * GridArea.SQUARE_WIDTH; } }
        public float mapPhysicalHeight { get { return mapSquareHeight * GridArea.SQUARE_HEIGHT; } }
        public Vector2 mapPhysicalCenter { get { return new Vector2(mapPhysicalWidth / 2, mapPhysicalHeight / 2); } }

        // Squares
        public Dictionary<Vector2Int, Square> squareCoordsDict = new Dictionary<Vector2Int, Square>();
        public Dictionary<Vector2Int, Intersection> intersectionDict = new Dictionary<Vector2Int, Intersection>();
        public List<Vector3> edgePoints = new List<Vector3>();
        


        // Create game map
        public static GameMap InstantiateGameMap()
        {
            GameMap gameMap = Instantiate(Resources.Load<GameObject>("Prefabs/GameMap")).GetComponent<GameMap>();
            gameMap.Initialize();
            return gameMap;
        }


        // Initialize
        public void Initialize()
        {
            CreateGameMap();
            PaintGameMapTerrain();
        }


        // Reset
        public void ResetGameMap()
        {
            ClearSquares();
            PaintGameMap();
        }


        // Clear squares
        private void ClearSquares()
        {
            foreach (Square square in squareCoordsDict.Values)
            {
                square.Reset();
            }
        }


        // Create square and intersection
        private void CreateSquareAndIntersection(Vector2Int squareCoords)
        {
            Square square = new Square(squareCoords);
            square.SetTileInfo(TileInfo.LoadTileInfo());
            squareCoordsDict[squareCoords] = square;

            Vector2Int intersectionCoords = squareCoords + Vector2Int.one;
            if (intersectionCoords.x != mapSquareWidth && intersectionCoords.y != mapSquareHeight)
                intersectionDict[intersectionCoords] = new Intersection(intersectionCoords);
        }


        // Create map
        public void CreateGameMap()
        {
            for (int i = 0; i < mapSquareWidth; i++)
            {
                for (int j = 0; j < mapSquareHeight; j++)
                {
                    Vector2Int squareCoords = new Vector2Int(i, j);
                    CreateSquareAndIntersection(squareCoords);
                }
            }
        }


        // Update map size
        public void UpdateMap()
        {
            for (int i = 0; i < mapSquareWidth; i++)
            {
                for (int j = 0; j < mapSquareHeight; j++)
                {
                    Vector2Int squareCoords = new Vector2Int(i, j);
                    if (!squareCoordsDict.ContainsKey(squareCoords))
                    {
                        CreateSquareAndIntersection(squareCoords);
                    }
                }
            }
            PaintGameMap();
        }


        // Shrink map
        public void ShrinkMap(int startWidth, int mapWidth, int startHeight, int mapHeight)
        {
            for (int i = startWidth; i < mapWidth; i++)
            {
                for (int j = startHeight; j < mapHeight; j++)
                {
                    // Remove square
                    Vector2Int squareCoords = new Vector2Int(i, j);
                    PaintSquare(squareCoords, null);
                    squareCoordsDict.Remove(squareCoords);

                    // Remove intersection
                    Vector2Int intersectionCoords = squareCoords + Vector2Int.one;
                    intersectionDict.Remove(intersectionCoords);
                }
            }
        }


        // Update map width
        public void UpdateMapWidth(int newMapWidth)
        {
            if (newMapWidth > mapSquareWidth)
            {
                mapSquareWidth = newMapWidth;
                UpdateMap();
            }
            else if (newMapWidth < mapSquareWidth)
            {
                ShrinkMap(newMapWidth, mapSquareWidth, 0, mapSquareHeight);
                mapSquareWidth = newMapWidth;
            }
        }


        // Update map height
        public void UpdateMapHeight(int newMapHeight)
        {
            if (newMapHeight > mapSquareHeight)
            {
                mapSquareHeight = newMapHeight;
                UpdateMap();
            }
            else if (newMapHeight < mapSquareHeight)
            {
                ShrinkMap(0, mapSquareWidth, newMapHeight, mapSquareHeight);
                mapSquareHeight = newMapHeight;
            }
        }


        // Get square tile
        public Tile GetSquareTile(Vector2Int squareCoords)
        {
            if (squareCoordsDict.ContainsKey(squareCoords))
                return (Tile)squareTilemap.GetTile(new Vector3Int(squareCoords.x, squareCoords.y, 0));
            return null;
        }


        // Get square tile
        public Tile GetSquareTile(Square square)
        {
            return GetSquareTile(square.coords);
        }


        // Set square tile
        public void SetSquareTile(Square square, TileInfo tileInfo)
        {
            square.tileInfo = tileInfo;
            PaintTerrainSquare(square, tileInfo.tile);
        }


        // Paint map
        public void PaintGameMap()
        {
            foreach (Square square in squareCoordsDict.Values)
            {
                PaintSquare(square, square.tileInfo.tile);
            }
        }


        // Paint map
        public void PaintGameMapTerrain()
        {
            foreach (Square square in squareCoordsDict.Values)
            {
                PaintTerrainSquare(square, square.tileInfo.tile);
            }
        }


        // Paint map square
        public void PaintSquare(Square square, Tile tile)
        {
            PaintSquare(square.coords, tile);
        }


        // Paint map squares
        public void PaintSquares(List<Square> squares, Tile tile)
        {
            foreach (Square square in squares)
            {
                PaintSquare(square, tile);
            }
        }


        // Paint map square
        public void PaintSquare(Vector2Int squareCoords, Tile tile)
        {
            if (squareCoordsDict.ContainsKey(squareCoords))
            {
                squareTilemap.SetTile(new Vector3Int(squareCoords.x, squareCoords.y, 0), tile);
            }
        }


        // Paint map squares
        public void PaintSquares(List<Vector2Int> squareCoords, Tile tile)
        {
            foreach (Vector2Int coords in squareCoords)
            {
                PaintSquare(coords, tile);
            }
        }


        // Paint map square
        public void PaintTerrainSquare(Square square, Tile tile)
        {
            PaintSquare(square.coords, tile);
        }


        // Paint map squares
        public void PaintTerrainSquares(List<Square> squares, Tile tile)
        {
            foreach (Square square in squares)
            {
                PaintSquare(square, tile);
            }
        }


        // Paint map square
        public void PaintTerrainSquare(Vector2Int squareCoords, Tile tile)
        {
            if (squareCoordsDict.ContainsKey(squareCoords))
            {
                terrainTilemap.SetTile(new Vector3Int(squareCoords.x, squareCoords.y, 0), tile);
            }
        }


        // Paint map squares
        public void PaintTerrainSquares(List<Vector2Int> squareCoords, Tile tile)
        {
            foreach (Vector2Int coords in squareCoords)
            {
                PaintSquare(coords, tile);
            }
        }


        // Convert square coords to world position
        public Vector3 SquareCoordsToWorldPosition(Vector2Int squareCoords)
        {
            Vector3 worldPosition = squareGrid.CellToWorld(new Vector3Int(squareCoords.x, squareCoords.y, 0));
            worldPosition += new Vector3(GridArea.SQUARE_HALFWIDTH, GridArea.SQUARE_HALFHEIGHT, 0);
            return worldPosition;
        }


        // Convert world coords to square coords
        public Vector2Int WorldPositionToSquareCoords(Vector3 worldPosition)
        {
            Vector3Int squareCoords = squareGrid.WorldToCell(worldPosition);
            return new Vector2Int(squareCoords.x, squareCoords.y);
        }


        // Convert world coords to intersection coords
        public Vector2Int WorldPositionToIntersectionCoords(Vector3 worldPosition)
        {
            Vector3Int intersectionCoords = intersectionGrid.WorldToCell(worldPosition);
            return new Vector2Int(intersectionCoords.x, intersectionCoords.y);
        }


        // Get grid area at world position
        public GridArea GetGridAreaAtWorldPosition(CharacterSizeType sizeType, Vector3 worldPosition)
        {
            if (sizeType == CharacterSizeType.Even)
                return GetIntersectionAtWorldPosition(worldPosition);
            else
                return GetSquareAtWorldPosition(worldPosition);
        }


        // Get grid area at coords
        public GridArea GetGridAreaAtCoords<T>(Vector2Int coords) where T : GridArea
        {
            if (typeof(T) == typeof(Intersection))
                return GetIntersectionAtIntersectionCoords(coords);
            else
                return GetSquareAtSquareCoords(coords);
        }


        // Get square at world position
        public Square GetSquareAtWorldPosition(Vector3 worldPosition)
        {
            Square square;
            if (squareCoordsDict.TryGetValue(WorldPositionToSquareCoords(worldPosition), out square)){
                return square;
            }
            return null;
        }


        // Get square at square coords
        public Square GetSquareAtSquareCoords(Vector2Int squareCoords)
        {
            Square square;
            if (squareCoordsDict.TryGetValue(squareCoords, out square))
            {
                return square;
            }
            return null;
        }


        // Get squares around target square coords
        public List<Square> GetSquaresAtSquareCoords(List<Vector2Int> squareCoords)
        {
            List<Square> squares = new List<Square>();
            foreach (Vector2Int coords in squareCoords)
            {
                squares = TryAddSquareToList(GetSquareAtSquareCoords(coords), squares);
            }

            return squares;
        }


        // Add square to list if not null
        private List<Square> TryAddSquareToList(Square square, List<Square> squares)
        {
            if (square != null && !squares.Contains(square))
                squares.Add(square);
            return squares;
        }


        // Get intersection from world position
        public Intersection GetIntersectionAtWorldPosition(Vector3 worldPosition)
        {
            Intersection intersection;
            if (intersectionDict.TryGetValue(WorldPositionToIntersectionCoords(worldPosition), out intersection))
                return intersection;
            return null;
        }


        // Get intersection from intersection coords
        public Intersection GetIntersectionAtIntersectionCoords(Vector2Int intersectionCoords)
        {
            Intersection intersection;
            if (intersectionDict.TryGetValue(intersectionCoords, out intersection))
                return intersection;
            return null;
        }


        // Convert grid area to squares
        public List<Square> GridAreasToSquares(List<GridArea> areas, Character character)
        {
            List<Square> squares = new List<Square>();

            foreach (GridArea area in areas)
            {
                List<Vector2Int> squareCoords = area.GetSquareCoords(character.radius);
                foreach (Vector2Int coords in squareCoords)
                {
                    squares = TryAddSquareToList(GetSquareAtSquareCoords(coords), squares);
                }
            }

            return squares;
        }


        // Get grid area for character
        public GridArea GetGridAreaForCharacter(Character character, Vector2Int targetCoords)
        {
            return GetGridAreaForCharacterStats(character.characterStats, targetCoords);
        }


        // Get grid area for character stats
        public GridArea GetGridAreaForCharacterStats(CharacterStats stats, Vector2Int targetCoords)
        {
            if (stats.sizeType == CharacterSizeType.Even)
                return GetGridAreaAtCoords<Intersection>(targetCoords);
            else
                return GetGridAreaAtCoords<Square>(targetCoords);
        }


        // Get squares for grid area
        private List<Square> GetSquaresForGridArea(GridArea area, Character character)
        {
            return GetSquaresAtSquareCoords(area.GetSquareCoords(character.radius));
        }


        // Get squares for path
        public List<Square> GetSquaresForPath(List<GridArea> areas, Character character)
        {
            List<Square> squares = new List<Square>();

            foreach (GridArea area in areas)
            {
                List<Square> areaSquares = GetSquaresForGridArea(area, character);
                foreach (Square areaSquare in areaSquares)
                {
                    squares = TryAddSquareToList(areaSquare, squares);
                }
            }
            return squares;
        }


        // Paint character select squares
        public void PaintCharacterSelectSquares(Character character)
        {
            if (character == null || character.gridArea == null)
                return;

            List<Vector2Int> cornerCoords = character.gridArea.GetCornerCoords(character.radius);
            foreach (Vector2Int corner in cornerCoords)
            {
                if (character.sizeType == CharacterSizeType.Even)
                {
                    Vector2Int diff = corner - character.gridArea.coords;
                    PaintSquare(corner, GameTiles.selectTilesEvenSize[diff]);
                }
                else
                {
                    Vector2Int diff = (corner - character.gridArea.coords) / character.radius;
                    PaintSquare(corner, GameTiles.selectTilesOddSize[diff]);
                }
            }
        }


        // Paint character move squares
        public void PaintCharacterMoveSquares(Character character, Vector2Int targetCoords)
        {
            if (character == null || character.gridArea == null)
                return;

            GridArea targetArea = GetGridAreaForCharacter(character, targetCoords);
            List<Vector2Int> targetSquareCoords = targetArea.GetSquareCoords(character.radius);
            foreach (Vector2Int targetSquare in targetSquareCoords)
            {
                if (character.sizeType == CharacterSizeType.Even)
                {
                    Vector2Int diff = targetSquare - targetArea.coords;
                    PaintSquare(targetSquare, GameTiles.moveTilesEvenSize[diff]);
                }
                else
                {
                    Vector2Int diff = (targetSquare - targetArea.coords) / character.radius;
                    if (Mathf.Abs(diff.x) == Mathf.Abs(diff.y) && diff.x != 0)
                        PaintSquare(targetSquare, GameTiles.moveTilesOddSize[diff]);
                    else
                        PaintSquare(targetSquare, GameTiles.moveTile);
                }
            }
        }


        // Paint character attack squares
        public void PaintCharacterAttackSquares(Character character)
        {
            if (character == null)
                return;

            foreach (Square square in character.squares)
            {
                PaintSquare(square, GameTiles.attackTile);
            }
        }


        // Place character at coords
        public Character PlaceCharacterAtCoords(CharacterStats stats, Vector2Int coords)
        {
            if (stats == null)
                return null;

            Character character = Character.InstantiateCharacter(stats);
            AddCharacterAtCoords(character, coords, true);
            return character;
        }


        // Add character at coords
        public void AddCharacterAtCoords(Character character, Vector2Int coords, bool setPosition = false)
        {
            if (character == null)
                return;

            GridArea gridArea = GetGridAreaForCharacter(character, coords);
            if (gridArea == null)
                return;

            // Add character to squares
            List<Square> squares = GetSquaresAtSquareCoords(gridArea.GetSquareCoords(character.radius));
            SetCharacterSquares(character, squares, gridArea, setPosition);
        }


        // Get whether character can move to grid area
        public bool CanMoveToGridArea(Character character, GridArea gridArea)
        {
            if (character == null || gridArea == null)
                return false;

            // Get target squares
            List<Square> targetSquares = GetSquaresForGridArea(gridArea, character);

            // Check number of target squares
            if (targetSquares.Count != character.gridSize)
                return false;

            // Check each target square
            foreach (Square targetSquare in targetSquares)
            {
                if (!targetSquare.CanMove(character))
                    return false;
            }
            return true;
        }


        // Get whether character can move to squares
        public bool CanMoveToCoords(Character character, Vector2Int targetCoords)
        {
            if (character == null)
                return false;

            // Get target grid area
            GridArea gridArea = GetGridAreaForCharacter(character, targetCoords);
            if (gridArea == null)
                return false;

            return CanMoveToGridArea(character, gridArea);
        }


        // Get whether character can be placed at coords
        public bool CanPlaceAtCoords(CharacterStats stats, Vector2Int targetCoords)
        {
            if (stats == null)
                return false;

            // Get target squares
            GridArea gridArea = GetGridAreaForCharacterStats(stats, targetCoords);
            if (gridArea == null)
                return false;

            // Check number of target squares
            List<Square> targetSquares = GetSquaresAtSquareCoords(gridArea.GetSquareCoords(stats.radius));
            if (targetSquares.Count != stats.gridSize)
                return false;

            // Check each target square
            foreach (Square targetSquare in targetSquares)
            {
                if (!targetSquare.CanPlace())
                    return false;
            }
            return true;
        }


        // Move character
        public void MoveCharacterToCoords(Character character, Vector2Int targetCoords)
        {
            if (character == null)
                return;

            // Clear prior squares
            foreach (Square priorSquare in character.squares)
            {
                priorSquare.SetCharacter(null);
            }

            AddCharacterAtCoords(character, targetCoords);
        }


        // Set character squares
        private void SetCharacterSquares(Character character, List<Square> squares, GridArea gridArea, bool setPosition)
        {
            // Set new squares
            character.SetGridArea(gridArea);
            character.SetSquares(squares);
            foreach (Square square in squares)
            {
                square.SetCharacter(character);
            }

            if (setPosition)
                character.SetPosition(gridArea.bounds.center);
        }


        // Print squares with characters
        public void PrintCharacterSquares()
        {
            Debug.Log("Total squares: " + squareCoordsDict.Count);
            foreach (Square square in squareCoordsDict.Values)
            {
                if (square.character != null)
                {
                    Debug.Log("Character at square: " + square.coords + ", " + square.character.characterName);
                }
            }
        }


        // Remove character
        public void RemoveCharacter(Character character)
        {
            foreach (Square square in character.squares)
            {
                square.SetCharacter(null);
            }
            C.Destroy(character);
        }


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}