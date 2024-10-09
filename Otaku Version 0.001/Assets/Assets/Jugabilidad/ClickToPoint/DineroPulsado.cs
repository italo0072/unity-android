using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DineroPulsado : MonoBehaviour
{
    public float dineroAdicionalBasePorSegundo = 5; // Dinero extra base por mantener la pantalla presionada
    public TMP_Text moneyText; // Referencia al texto de dinero
    public Slider staminaSlider; // Referencia al Slider de estamina
    public float maxStamina = 100f; // Estamina m�xima
    private float currentStamina = 0f; // Estamina actual
    public float staminaDecayRate = 1f; // Tasa a la que la estamina disminuye cuando no se est� presionando

    private bool isPressing = false; // Detectar si el jugador est� presionando
    private bool isPenalized = false; // Indica si el jugador est� penalizado
    private float penalizationTimer = 0f; // Temporizador de penalizaci�n

    void Start()
    {
        // Cargar el dinero inicial y la penalizaci�n
        GameData.LoadMoney();
        currentStamina = 0f; // Inicializa la estamina
        staminaSlider.maxValue = maxStamina; // Establecer el valor m�ximo del Slider
        staminaSlider.value = currentStamina; // Actualiza el Slider al valor inicial
    }

    void Update()
    {
        // Si el jugador est� penalizado, contar el tiempo de penalizaci�n
        if (isPenalized)
        {
            penalizationTimer += Time.deltaTime;
            if (penalizationTimer >= GameData.penalizationTime) // Usar el valor guardado de penalizaci�n
            {
                // Fin de la penalizaci�n
                isPenalized = false;
                penalizationTimer = 0f;
                currentStamina = 0f; // Reinicia la estamina despu�s de la penalizaci�n
                staminaSlider.value = currentStamina; // Asegura que el Slider se actualice tambi�n
            }
            return; // Evita ganar dinero durante la penalizaci�n
        }

        // L�gica de entrada (mantener presionado)
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                isPressing = true;
            }
            else
            {
                isPressing = false;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            isPressing = true;
        }
        else
        {
            isPressing = false;
        }

        // Aumentar dinero adicional si se est� presionando y la estamina no est� llena
        if (isPressing && !isPenalized && currentStamina < maxStamina)
        {
            // Calcular el dinero adicional total
            float dineroAdicionalTotal = dineroAdicionalBasePorSegundo + GameData.additionalMoneyPerSecond;
            GameData.currentMoney += dineroAdicionalTotal * Time.deltaTime; // Incrementa el dinero adicional

            // Aumentar la estamina
            currentStamina += Time.deltaTime; // Incrementa la estamina con el tiempo
            staminaSlider.value = currentStamina; // Actualiza el Slider

            // Desactivar la funci�n si la estamina llega al m�ximo
            if (currentStamina >= maxStamina)
            {
                isPenalized = true; // Activar penalizaci�n
                isPressing = false; // Detener el aumento de dinero
            }
        }
        else if (!isPressing && currentStamina > 0)
        {
            // Reducir la estamina si no se est� presionando
            currentStamina -= staminaDecayRate * Time.deltaTime; // Decrementa la estamina
            staminaSlider.value = currentStamina; // Actualiza el Slider
        }

        // Asegurarse de que la estamina no baje de 0
        if (currentStamina < 0)
        {
            currentStamina = 0;
            staminaSlider.value = currentStamina; // Asegura que el Slider est� en 0
        }

        // Actualizar el texto de dinero
        moneyText.text = Mathf.FloorToInt(GameData.currentMoney).ToString();
    }

    void OnApplicationQuit()
    {
        GameData.SaveMoney(); // Guarda el dinero y la penalizaci�n al cerrar el juego
    }
}