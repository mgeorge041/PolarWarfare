using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterNS;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 5;
    public CircleCollider2D circleCollider;
    private bool hitTarget = false;
    public Character character;
    public ProjectileInfo projectileInfo;
    public CircleCollider2D projectileDestroyer;
    public SpriteRenderer spriteRenderer;


    // Instantiate
    public static Projectile InstantiateProjectile(ProjectileInfo projectileInfo)
    {
        Debug.Log("instantiating projectile: " + projectileInfo.projectileName);
        Projectile projectile = Instantiate(Resources.Load<Projectile>("Prefabs/Projectile")).GetComponent<Projectile>();
        projectile.projectileInfo = projectileInfo;
        projectile.Initialize();
        return projectile;
    }


    // Instantiate
    public static Projectile InstantiateProjectile(string projectileName)
    {
        Projectile projectile = Instantiate(Resources.Load<Projectile>("Prefabs/Projectile")).GetComponent<Projectile>();
        projectile.Initialize();
        return projectile;
    }


    // Start trajectory
    public void StartTrajectory(Vector3 targetPosition)
    {
        AnimateTrajectory(targetPosition);
    }


    // Stop trajectory
    public void StopTrajectory()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }


    // Animate trajectory
    public IEnumerator AnimateTrajectory(Vector3 targetPosition)
    {
        float t = 0;

        while (t < 1)
        {
            Vector3 currentPosition = transform.position;
            t += Time.deltaTime * moveSpeed / 100;
            transform.position = Vector3.Lerp(currentPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition;
        Destroy(gameObject);
    }


    // Collision detection
    public void OnTriggerEnter2D(Collider2D other)
    {
        Tags.TagType otherTag;
        if (!Tags.tags.TryGetValue(other.gameObject.tag, out otherTag))
            return;

        if (otherTag == Tags.TagType.Projectile)
            return;
        else if (otherTag == Tags.TagType.Character)
        {
            Character targetCharacter = other.GetComponent<Character>();
            if (hitTarget || targetCharacter == character)
                return;
            hitTarget = true;
            Debug.Log("projectile hit: " + other.name);

            if (targetCharacter != null)
            {
                targetCharacter.TakeDamage(this);
            }

            StopTrajectory();
            Destroy(projectileDestroyer.gameObject);
        }
        else if (otherTag == Tags.TagType.ProjectileDestroyer)
        {
            StopTrajectory();
            Destroy(other.gameObject);
        }
    }


    // Calculate spread
    public static List<Vector2> CalculateSpread(ProjectileInfo projInfo, Vector3 targetPosition, Vector2 characterPosition)
    {
        List<Vector2> spreads = new List<Vector2>();

        int numProjectiles = projInfo.projectilePath.numberProjectiles;
        for (int i = 0; i < numProjectiles; i++) 
        {
            float spreadAngle = projInfo.projectilePath.projectileSpread / numProjectiles;
            Vector2 direction = new Vector2(targetPosition.x, targetPosition.y) - characterPosition;
            int spreadSign = 1;
            if (i % 2 == 1)
                spreadSign = -1;
            Vector2 spread = (Quaternion.AngleAxis(spreadAngle * spreadSign, Vector3.forward) * direction).normalized;
            spreads.Add(spread);
        }

        return spreads;
    }


    // Initialize
    public void Initialize()
    {
        circleCollider.radius = projectileInfo.pixelRadius / 100f;
        spriteRenderer.sprite = projectileInfo.sprite;
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
