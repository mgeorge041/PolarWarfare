using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePieceNS.CharacterNS
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
    public class ProjectileInfo : ScriptableObject
    {
        public string projectileName;
        public Sprite sprite;
        public int pixelRadius;
        public float radius;
        public int damage;
        public ProjectilePath projectilePath;
    }
}