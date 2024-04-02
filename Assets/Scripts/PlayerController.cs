using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;//Velocidad del jugador.
    public float jumpForce = 5f;//Fuerza aplicada al saltar.
    public int maxLives = 10;//Número máximo de vidas del jugador.
    private int currentLives;//Vidas actuales del jugador.
    public Text livesText;//Texto que muestra la cantidad de vidas.
    private Rigidbody2D rb;//Rigidbody2D del jugador.
    private bool isGrounded;//Indica si el jugador está en el suelo.
    public int score = 0;//Puntaje del jugador.
    public Text scoreText;//Texto que muestra el puntaje.
    //Declaración de eventos.
    public delegate void HealthUpdateEventHandler(int newHealth);
    public static event HealthUpdateEventHandler OnHealthUpdated;
    public delegate void ScoreUpdateEventHandler(int newScore);
    public static event ScoreUpdateEventHandler OnScoreUpdated;
    public delegate void DefeatEventHandler();
    public static event DefeatEventHandler OnDefeat;
    public delegate void VictoryEventHandler();
    public static event VictoryEventHandler OnVictory;

    public float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//Obtenemos el componente Rigidbody2D.
        currentLives = maxLives;//Iniciamos las vidas actuales con el máximo.
        UpdateLivesText(currentLives);//Actualiza el texto de vidas.
        OnHealthUpdated += UpdateLivesText;//Permite suscribirse al evento de actualización de vidas.
        OnScoreUpdated += UpdateScoreText;//Permite suscribirse al evento de actualización de puntaje.
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);//Calcula el movimiento.
        rb.velocity = movement;//Aplica el movimiento.
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<float>();//Obtenemos la entrada horizontal del jugador.
    }

    public void OnJump(InputAction.CallbackContext context)//Metodo que permite realizar el salto.
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)//Verifica si se presiona la tecla de salto y si el jugador esta en el suelo.
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);//Aplica una velocidad vertical para el salto.
        }
    }

    void OnCollisionEnter2D(Collision2D other)//Metodo que se ejecuta cuando se produce una colision.
    {
        if (other.gameObject.CompareTag("Ground"))//Verifica si la colision es con el suelo.
        {
            isGrounded = true;//Establece que el jugador está en el suelo.
        }
        else if (other.gameObject.CompareTag("Enemy"))//Verifica si la colisión es con un enemigo.
        {
            TakeDamage(1);//Reduce la cantidad de vidas.
        }
        else if (other.gameObject.CompareTag("FireBlue"))//Verifica si la colision es con el objeto de victoria.
        {
            //Ejecuta la función de victoria.
            PlayerWins();
            Victory();
        }
        else if (other.gameObject.CompareTag("LifePickup"))//Verifica si la colision es con el objeto de vida.
        {
            GainLife(1);//Aumenta la cantidad de vidas.
            Destroy(other.gameObject);//Destruye el objeto de vida.
        }
        else if (other.gameObject.CompareTag("Coin"))//Verificar si la colisión es con una moneda.
        {
            AddScore(100);//Aumenta el puntaje.
            Destroy(other.gameObject);//Destruye la moneda.
        }
    }

    void OnCollisionExit2D(Collision2D other)//Metodo que se ejecuta cuando el jugador sale de una colisión.
    {
        if (other.gameObject.CompareTag("Ground"))//Verifica si la colisión era con el suelo.
        {
            isGrounded = false;//Indica que el jugador ya no está en el suelo.
        }
    }

    void TakeDamage(int damageAmount)//Método para reducir la cantidad de vidas del jugador.
    {
        currentLives -= damageAmount;//Reduce la cantidad de vidas.
        currentLives = Mathf.Max(currentLives, 0);//Se asegura de que las vidas no sean negativas.
        OnHealthUpdated?.Invoke(currentLives);//Invoca el evento de actualización de vidas.
        if (currentLives <= 0)//Verifica si el jugador se queda sin vidas.
        {
            //Ejecuta la función de derrota.
            PlayerLoses();
            Die();
        }
    }

    void GainLife(int lifeAmount)//Metodo para aumentar la cantidad de vidas del jugador.
    {
        currentLives += lifeAmount;//Aumenta la cantidad de vidas.
        currentLives = Mathf.Min(currentLives, maxLives);//Asegurarse de que las vidas no excedan el maximo.
        OnHealthUpdated?.Invoke(currentLives);//Invoca el evento de actualización de vidas.
    }

    void AddScore(int scoreAmount)//Metodo para aumentar el puntaje del jugador.
    {
        score += scoreAmount;//Aumenta el puntaje.
        OnScoreUpdated?.Invoke(score);//Invoca el evento de actualización de puntaje.
    }

    void Victory()//Metodo que carga la escena de victoria.
    {
        SceneManager.LoadScene("VictoryScene");
    }

    void Die()//Metodo que carga la escena de derrota.
    {
        SceneManager.LoadScene("DefeatScene");
    }

    void UpdateLivesText(int newHealth)//Metodo para actualizar el texto de vidas.
    {
        if (livesText != null)//Verifica si el texto de vidas no es nulo.
        {
            livesText.text = "Lives: " + newHealth.ToString();//Actualiza el texto de vidas.
        }
    }

    void UpdateScoreText(int newScore)//Metodo para actualizar el texto de puntaje.
    {
        if (scoreText != null)//Verifica si el texto de puntaje no es nulo.
        {
            scoreText.text = "Score: " + newScore.ToString();//Actualiza el texto de puntaje.
        }
    }

    public void PlayerWins()//Metodo que invoca el evento de victoria.
    {
        OnVictory?.Invoke();
    }

    public void PlayerLoses()//Metodo que invoca el evento de derrota.
    {
        OnDefeat?.Invoke();
    }
}