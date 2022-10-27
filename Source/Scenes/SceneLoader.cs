using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    // Set scene
    public static void SetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
