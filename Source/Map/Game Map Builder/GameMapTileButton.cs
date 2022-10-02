using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameMapNS.GameMapBuilderNS
{
    public class GameMapTileButton : MonoBehaviour
    {
        public TileInfo tileInfo;
        public Image buttonImage;
        public Image buttonBackground;


        // Instantiate tile button
        public static GameMapTileButton InstantiateGameMapTileButton(TileInfo tileInfo)
        {
            GameMapTileButton tileButton = Instantiate(Resources.Load<GameMapTileButton>(ENV.GAMEMAPBUILDER_RESOURCE_PREFAB_PATH + "Game Map Tile Button")).GetComponent<GameMapTileButton>();
            tileButton.SetTileInfo(tileInfo);
            return tileButton;
        }


        // Instantiate tile button
        public static GameMapTileButton InstantiateGameMapTileButton()
        {
            return InstantiateGameMapTileButton(TileInfo.LoadTileInfo());
        }


        // Set tile info
        public void SetTileInfo(TileInfo tileInfo)
        {
            this.tileInfo = tileInfo;
            buttonImage.sprite = tileInfo.tile.sprite;
        }


        // Set button background
        public void SetButtonBackground(bool selected)
        {
            if (selected)
                buttonBackground.sprite = Resources.Load<Sprite>("GameMap/Tiles/Terrain Tiles/Map Tile Button Border Selected");
            else
                buttonBackground.sprite = Resources.Load<Sprite>("GameMap/Tiles/Terrain Tiles/Map Tile Button Border");
        }


        // Clicked on game map tile button
        public void ClickedGameMapTileButton()
        {
            Debug.Log("this: " + this);
            GameMapBuilderEventManager.OnClickedGameMapTileButton(this);
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