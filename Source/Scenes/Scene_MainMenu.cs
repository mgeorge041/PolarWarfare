using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_MainMenu : MonoBehaviour
{
    public Transform mainButtonsContainer;
    public Transform playButtonsContainer;


    // Click play or back button
    public void ClickPlayBackButton(bool play)
    {
        mainButtonsContainer.gameObject.SetActive(!play);
        playButtonsContainer.gameObject.SetActive(play);
    }


    // Click quit button
    public void ClickQuitButton()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
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
