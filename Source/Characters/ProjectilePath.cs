using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterNS
{
    [CreateAssetMenu(fileName = "New Projectile Path", menuName = "Projectile Path")]
    public class ProjectilePath : ScriptableObject
    {
        public int numberProjectiles = 1;
        public float projectileSpread;
    }
}