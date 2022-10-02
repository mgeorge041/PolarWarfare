using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterNS
{
    public enum CharacterType
    {
        Penguin,
        PolarBear,
        Rabbit,
        Test,
        None
    };

    [CreateAssetMenu(fileName = "New Character", menuName = "Character")]
    public class CharacterStats : ScriptableObject
    {
        public RuntimeAnimatorController runtimeAnimatorController;
        public string characterName;
        public Sprite sprite;
        public int health;
        public int range;
        public int damage;
        public int speed;
        public int size;
        public int gridSize { get { return size * size; } }
        public int radius { get { return size / 2; } }
        public CharacterSizeType sizeType { get { return size % 2 == 0 ? CharacterSizeType.Even : CharacterSizeType.Odd; } }
        public ProjectileInfo projectileInfo;


        public static Dictionary<CharacterType, string> characterFilePaths = new Dictionary<CharacterType, string>()
        {
            { CharacterType.Penguin, "Penguins" },
            { CharacterType.PolarBear, "Polar Bears" },
            { CharacterType.Rabbit, "Rabbits" },
            { CharacterType.Test, "Test" },
        };


        // Load info for character
        public static CharacterStats LoadCharacterStats()
        {
            CharacterStats stats = Instantiate(Resources.Load<CharacterStats>(ENV.CHARACTER_RESOURCE_PATH + "Test/2x2/Test Character"));
            return stats;
        }


        // Load info for character
        public static CharacterStats LoadCharacterStats(string characterName)
        {
            CharacterStats stats = Instantiate(Resources.Load<CharacterStats>(ENV.CHARACTER_RESOURCE_PATH + "Test/3x3/" + characterName));
            return stats;
        }


        // Load info for character
        public static CharacterStats LoadCharacterStats(CharacterType characterType, string characterName)
        {
            CharacterStats stats = Instantiate(Resources.Load<CharacterStats>(ENV.CHARACTER_RESOURCE_PATH + characterFilePaths[characterType] + "/" + characterName));
            return stats;
        }
    }
}