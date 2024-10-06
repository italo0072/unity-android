using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DineroLogic : MonoBehaviour
{
    public float dineroPasivoPorSegundo = 1f; // Dinero pasivo ganado por segundo
    public TMP_Text moneyText; // Referencia al texto de dinero
    public Button tiendaButton; // Botón de la tienda
    public TMP_Text nivelText; // Texto para mostrar el nivel actual del buff
    public TMP_Text costoText; // Texto para mostrar el costo del siguiente nivel

    private int nivelBuff = 0; // Nivel inicial del buff
    private int maxNivelBuff = 20; // Nivel máximo del buff
    private float costoBuff = 100f; // Costo inicial del buff
    private float incrementoCosto = 100f; // Incremento en el costo por nivel
    private float multiplicadorBuff = 2f; // Multiplicador inicial del buff

    void Start()
    {
        // Cargar el dinero al iniciar
        GameData.LoadMoney();
        UpdateMoneyText();
        ActualizarNivelYCosto(); // Actualiza nivel y costo al iniciar

        // Asignar la función de compra al botón de la tienda
        tiendaButton.onClick.AddListener(ComprarBuff);
        ActualizarBotonTienda(); // Actualiza el estado del botón de tienda al iniciar
    }

    void Update()
    {
        // Aumentar el dinero pasivo, usando el buff según el nivel
        float dineroGanado = (dineroPasivoPorSegundo * (nivelBuff > 0 ? multiplicadorBuff : 1)) * Time.deltaTime;
        GameData.currentMoney += dineroGanado;

        // Actualizar el texto de dinero
        UpdateMoneyText();

        // Actualizar el botón de tienda según el dinero actual y nivel del buff
        ActualizarBotonTienda();
    }

    private void UpdateMoneyText()
    {
        // Mostrar solo el valor entero del dinero
        moneyText.text = Mathf.FloorToInt(GameData.currentMoney).ToString();
    }

    void ActualizarBotonTienda()
    {
        // Habilitar o deshabilitar el botón dependiendo del dinero y si el buff ha alcanzado su nivel máximo
        tiendaButton.interactable = GameData.currentMoney >= costoBuff && nivelBuff < maxNivelBuff;
    }

    void ComprarBuff()
    {
        // Si el jugador tiene suficiente dinero y no ha alcanzado el nivel máximo, aplicar el buff
        if (GameData.currentMoney >= costoBuff && nivelBuff < maxNivelBuff)
        {
            GameData.currentMoney -= costoBuff; // Reducir el dinero del jugador
            nivelBuff++; // Aumentar el nivel del buff
            multiplicadorBuff = 2f * nivelBuff; // Aumentar el multiplicador del buff (ej: x2, x4, x6...)
            costoBuff += incrementoCosto; // Incrementar el costo del siguiente nivel

            ActualizarNivelYCosto(); // Actualizar el texto del nivel y el costo
        }
    }

    void ActualizarNivelYCosto()
    {
        nivelText.text = "Nivel de Buff: " + nivelBuff + "/" + maxNivelBuff;
        costoText.text = "Costo de nivel: " + costoBuff + " monedas";
    }

    void OnApplicationQuit()
    {
        // Guardar el dinero cuando se cierre la aplicación
        GameData.SaveMoney();
    }
}