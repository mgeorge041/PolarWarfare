using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterNS;

namespace GameMapNS.GameMapBuilderNS
{
    public class CharacterButton : MonoBehaviour
    {
        public CharacterStats characterStats;
        public Image buttonBackground;
        public Image buttonImage;


        // Instantiate button
        public static CharacterButton InstantiateCharacterButton(CharacterStats stats)
        {
            CharacterButton button = Instantiate(Resources.Load<CharacterButton>(ENV.GAMEMAPBUILDER_RESOURCE_PREFAB_PATH + "Game Map Character Button")).GetComponent<CharacterButton>();
            button.Initialize();
            button.SetStats(stats);
            return button;
        }


        // Instantiate button
        public static CharacterButton InstantiateCharacterButton()
        {
            return InstantiateCharacterButton(CharacterStats.LoadCharacterStats());
        }


        // Initialize
        public void Initialize()
        {
            
        }


        // Set stats
        public void SetStats(CharacterStats stats)
        {
            characterStats = stats;
            buttonImage.sprite = stats.sprite;
        }


        // Set button background
        public void SetButtonBackground(bool selected)
        {
            if (selected)
                buttonBackground.sprite = Resources.Load<Sprite>("GameMap/Tiles/Terrain Tiles/Map Tile Button Border Selected");
            else
                buttonBackground.sprite = Resources.Load<Sprite>("GameMap/Tiles/Terrain Tiles/Map Tile Button Border");
        }


        // Clicked on character button
        public void ClickedCharacterButton()
        {
            GameMapBuilderEventManager.OnClickedCharacterButton(this);
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