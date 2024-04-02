using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerColor : MonoBehaviour
{
    public Material playerMaterial;//Nos permite cambiar el color del jugador.
    Color defaultColor = Color.white;//Color original del jugador.

    //Permite cambiar el color del jugador a rojo.
    public void ChangeToRed()
    {
        playerMaterial.color = Color.red;
    }

    //Permite cambiar el color del jugador a azul.
    public void ChangeToBlue()
    {
        playerMaterial.color = Color.blue;
    }

    //Permite cambiar el color del jugador a amarillo.
    public void ChangeToYellow()
    {
        playerMaterial.color = Color.yellow;
    }

    void Start()
    {
        //Permite establecer al color del jugador (blanco) como color predeterminado.
        playerMaterial.color = defaultColor;
    }
}