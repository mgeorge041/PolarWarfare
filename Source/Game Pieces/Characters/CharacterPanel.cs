using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GamePieceNS.CharacterNS
{
    public class CharacterPanel : MonoBehaviour
    {
        public CharacterStats characterStats;
        public Image characterPortrait;
        public TextMeshProUGUI characterNameLabel;
        public Image characterSize;
        public Image characterHealthBar;
        public Image characterDamageBar;
        public Image characterRangeBar;
        public Image characterSpeedBar;


        // Create character panel
        public static CharacterPanel InstantiateCharacterPanel()
        {
            CharacterStats characterStats = CharacterStats.LoadCharacterStats();
            return InstantiateCharacterPanel(characterStats);
        }


        // Create character panel
        public static CharacterPanel InstantiateCharacterPanel(CharacterStats characterStats)
        {
            CharacterPanel characterPanel = Instantiate(Resources.Load<CharacterPanel>("Prefabs/Character Panel")).GetComponent<CharacterPanel>();
            characterPanel.SetPanelInfo(characterStats);
            return characterPanel;
        }


        // Set panel info
        public void SetPanelInfo(CharacterStats characterStats)
        {
            this.characterStats = characterStats;
            characterPortrait.sprite = characterStats.sprite;
            characterNameLabel.text = characterStats.characterName;
            if (characterStats.size % 2 == 0)
            {
                characterSize.sprite = Resources.Load<Sprite>("Characters/2x2 Grid Icon");
            }
            else
            {
                characterSize.sprite = Resources.Load<Sprite>("Characters/3x3 Grid Icon");
            }
            characterHealthBar.fillAmount = 100f;
            characterDamageBar.fillAmount = 100f;
            characterRangeBar.fillAmount = 100f;
            characterSpeedBar.fillAmount = 100f;
        }


        // Clicked on character panel
        public void ClickedCharacterPanel()
        {
            GameMapEventManager.OnClickedCharacterPanel(this);
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