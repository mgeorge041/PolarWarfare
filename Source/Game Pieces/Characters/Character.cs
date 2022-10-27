using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePieceNS;

namespace GamePieceNS.CharacterNS
{
    public class Character : GamePiece
    {
        // Character info
        public CharacterStats characterStats { get; private set; }
        public string characterName;
        public int maxHealth;
        public int health;
        public int range;
        public int damage;
        public int maxSpeed;
        public int speed;

        // Character object
        public CharacterAnimator characterAnimator;

        // Projectiles
        public Projectile projectile;
        public ProjectileInfo projectileInfo;

        // Map info
        public bool canMove { get { return speed > 0; } }
        
        // Player info
        public int playerId { get; private set; }


        // Instantiate character
        public static Character InstantiateCharacter()
        {
            CharacterStats characterStats = CharacterStats.LoadCharacterStats();
            return InstantiateCharacter(characterStats);
        }


        // Instantiate character
        public static Character InstantiateCharacter(string characterName)
        {
            CharacterStats characterStats = CharacterStats.LoadCharacterStats(characterName);
            return InstantiateCharacter(characterStats);
        }


        // Instantiate character
        public static Character InstantiateCharacter(CharacterStats characterStats)
        {
            Character character = Instantiate(Resources.Load<GameObject>("Prefabs/Character")).GetComponent<Character>();
            character.SetCharacterInfo(characterStats);
            character.Initialize();
            return character;
        }


        // Set character info
        public void SetCharacterInfo(CharacterStats characterStats)
        {
            this.characterStats = characterStats;
            characterName = characterStats.characterName;
            maxHealth = characterStats.health;
            health = characterStats.health;
            range = characterStats.range;
            damage = characterStats.damage;
            maxSpeed = characterStats.speed;
            speed = characterStats.speed;
            size = characterStats.size;
            spriteRenderer.sprite = characterStats.sprite;
            bounds = new Bounds(Vector3.zero, new Vector3(GridArea.SQUARE_WIDTH * size, GridArea.SQUARE_HEIGHT *size, 0));
            boxCollider.size = new Vector2(characterStats.size * GridArea.SQUARE_WIDTH, characterStats.size * GridArea.SQUARE_HEIGHT);
            characterAnimator.animator.runtimeAnimatorController = characterStats.runtimeAnimatorController;
            projectileInfo = characterStats.projectileInfo;
        }


        // Set player ID
        public void SetPlayerId(int playerId)
        {
            this.playerId = playerId;
        }


        // Set grid area
        public void SetGridArea(GridArea gridArea)
        {
            this.gridArea = gridArea;
        }


        // Set squares
        public void SetSquares(List<Square> squares)
        {
            this.squares = squares;
        }


        // Set position
        public void SetPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
            bounds.center = worldPosition;
        }


        // Set facing direction
        public void SetFaceRight(bool faceRight)
        {
            characterAnimator.SetFaceRight(faceRight);
        }


        // Move character
        public void MoveCharacter(List<GridArea> path)
        {
            characterAnimator.Move(path);
        }


        // End move
        public void EndMove()
        {

        }


        // Prepare attack
        public void PrepareAttack()
        {
            characterAnimator.ChangeAnimation(AnimationState.PrepareAttack);
        }


        // End prepare attack
        public void EndPrepareAttack()
        {
            characterAnimator.ChangeAnimation(AnimationState.Idle);
        }


        // Launch projectiles
        public void SetProjectiles(Dictionary<Vector2, CircleCollider2D> trajectories)
        {

            characterAnimator.Attack(trajectories);
        }


        // Take damage
        public void TakeDamage(Projectile projectile)
        {
            this.projectile = projectile;
            int damage = projectile.projectileInfo.damage;
            health -= damage;
            float healthPercentage = (float)health / maxHealth;
            characterAnimator.TakeDamage(healthPercentage);
        }


        // End take damage
        public void EndTakeDamage()
        {
            
        }


        // Initialize
        public void Initialize()
        {
            squares = new List<Square>();
            characterAnimator.character = this;
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