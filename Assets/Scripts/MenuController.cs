using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //Botones para iniciar y salir del juego.
    public Button playButton;
    public Button exitButton;

    void Start()
    {
        //Se a�aden los listeners a los botones (permite detectar los clics).
        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    //Escena del juego
    void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    //Permite salir de la aplicaci�n
    void ExitGame()
    {
        Application.Quit();
    }
}