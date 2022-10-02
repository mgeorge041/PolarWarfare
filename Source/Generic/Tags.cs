using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tags
{
    public enum TagType
    {
        Character,
        Projectile,
        ProjectileDestroyer,
        None,
    };



    public static Dictionary<string, TagType> tags = new Dictionary<string, TagType>()
    {
        { "Character", TagType.Character },
        { "Projectile", TagType.Projectile },
        { "Projectile Destroyer", TagType.ProjectileDestroyer },
    };
}
