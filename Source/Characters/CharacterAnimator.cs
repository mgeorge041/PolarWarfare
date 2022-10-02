using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterNS
{
    public enum AnimationState
    {
        Attack,
        PrepareAttack,
        Move,
        Idle,
        Damage,
        Walk,
        None,
    };


    public class CharacterAnimator : MonoBehaviour
    {
        public Animator animator;
        public Character character;
        public GameObject healthbar;
        public Image lifebar;
        public Dictionary<Vector2, CircleCollider2D> trajectories = new Dictionary<Vector2, CircleCollider2D>();
        public float moveSpeed = 1;
        public float moveTime = 1;


        public static Dictionary<AnimationState, string> animationStates = new Dictionary<AnimationState, string>()
        {
            { AnimationState.Attack, "Attack" },
            { AnimationState.PrepareAttack, "Prepare Attack" },
            { AnimationState.Move, "Move" },
            { AnimationState.Idle, "Idle" },
            { AnimationState.Damage, "Damage" },
            { AnimationState.Walk, "Walk" },
        };


        // Change animation
        public void ChangeAnimation(string animationName)
        {
            animator.Play(animationName);
        }


        // Change animation
        public void ChangeAnimation(AnimationState state)
        {
            animator.Play(animationStates[state]);
        }


        // Move
        public void Move(List<GridArea> path) 
        {
            ChangeAnimation(AnimationState.Walk);
            StartCoroutine(AnimateMove(path));
        }


        // Animate move
        public IEnumerator AnimateMove(List<GridArea> path)
        {
            // Remove start tile
            if (path[0].bounds.center == character.transform.position)
                path.RemoveAt(0);

            while (path.Count > 0)
            {
                float t = 0;
                GridArea targetArea = path[0];
                path.Remove(targetArea);
                Vector3 currentPosition = character.transform.position;
                while (t < moveTime)
                {
                    t += Time.deltaTime * moveSpeed;
                    character.SetPosition(Vector3.Lerp(currentPosition, targetArea.bounds.center, t));
                    yield return null;
                }
                character.SetPosition(targetArea.bounds.center);
            }
            EndMove();
        }


        // End move
        public void EndMove()
        {
            ChangeAnimation(AnimationState.Idle);
            character.EndMove();
        }


        // Attack
        public void Attack(Dictionary<Vector2, CircleCollider2D> trajectories)
        {
            ChangeAnimation(AnimationState.Attack);
            Debug.Log("setting trajectories: " + trajectories.Count);
            this.trajectories = trajectories;
            Debug.Log("animator trajectories: " + this.trajectories.Count);
        }


        public void LaunchProjectile(Vector2 endPoint, CircleCollider2D projectileDestroyer)
        {
            Projectile projectile = Projectile.InstantiateProjectile(character.projectileInfo);
            projectile.character = character;
            projectile.transform.position = character.worldPosition;
            projectile.projectileDestroyer = projectileDestroyer;
            projectile.StartCoroutine(projectile.AnimateTrajectory(endPoint));
        }


        // Launch projectiles
        public void LaunchProjectiles()
        {
            Debug.Log("laumching projectiles: " + trajectories.Count);
            foreach (KeyValuePair<Vector2, CircleCollider2D> trajectory in trajectories)
            {
                LaunchProjectile(trajectory.Key, trajectory.Value);
            }
            trajectories.Clear();
        }


        // Animate attack
        public IEnumerator AnimateAttack()
        {
            yield return null;
        }


        // End attack
        public void EndAttack()
        {
            ChangeAnimation(AnimationState.Idle);
            //LaunchProjectiles();
        }


        // Take damage
        public void TakeDamage(float healthPercentage)
        {
            healthbar.gameObject.SetActive(true);
            ChangeAnimation("Damage");
            StartCoroutine(AnimateTakeDamage(healthPercentage));
        }


        // Animate take damage
        public IEnumerator AnimateTakeDamage(float percentage)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / 2;
                lifebar.fillAmount = Mathf.Lerp(lifebar.fillAmount, percentage, t);
                yield return null;
            }
            lifebar.fillAmount = percentage;
            EndTakeDamage();
        }


        // End take damage
        public void EndTakeDamage()
        {
            healthbar.gameObject.SetActive(false);
            ChangeAnimation("Idle");
            character.EndTakeDamage();
        }


        // Set lifebar
        public void SetLifebar(float percentage)
        {
            lifebar.fillAmount = percentage;
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