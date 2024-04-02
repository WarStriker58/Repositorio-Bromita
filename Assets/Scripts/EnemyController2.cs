using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2 : MonoBehaviour
{
    public float moveSpeed = 2f;//Velocidad  del enemigo 2.
    public float leftLimit = -8f;//Limite izquierdo del enemigo 2.
    public float rightLimit = 5f;//Limite derecho del enemigo 2.
    private bool movingRight = true;//Indica si el enemigo se esta moviendo hacia la derecha.

    void Update()
    {
        //Si el enemigo se mueve hacia la derecha, lo desplaza hacia la derecha.
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        //Si el enemigo se mueve hacia la izquierda, lo desplaza hacia la izquierda.
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        //Si llega al límite derecho, cambia la dirección a la izquierda.
        if (transform.position.x >= rightLimit)
        {
            movingRight = false;
        }
        //Si llega al límite izquierdo, cambia la dirección a la derecha.
        else if (transform.position.x <= leftLimit)
        {
            movingRight = true;
        }
    }
}