using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePieceNS
{
    public enum GamePieceSizeType
    {
        Even,
        Odd,
        None
    };

    public abstract class GamePiece : MonoBehaviour
    {
        // Character info
        public GamePieceStats pieceStats { get; protected set; }
        public int size;
        public int gridSize { get { return size * size; } }
        public int radius { get { return size / 2; } }
        public GamePieceSizeType sizeType { get { return size % 2 == 0 ? GamePieceSizeType.Even : GamePieceSizeType.Odd; } }

        // Character object
        public SpriteRenderer spriteRenderer;
        public BoxCollider2D boxCollider;
        public GamePieceAnimator pieceAnimator;

        // Map info
        public GridArea gridArea;
        public List<Square> squares { get; protected set; }
        public Vector3 worldPosition { get { return bounds.center; } }
        public Bounds bounds;

        // Player info
        public int playerId { get; private set; }

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