using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictorySceneController : MonoBehaviour
{
    //Escena del menu.
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    //Escena del juego.
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}