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
    public TMP_Text gananciaPorSegundoText; // Texto para mostrar la ganancia por segundo

    private int nivelBuff = 0; // Nivel inicial del buff
    private int maxNivelBuff = 20; // Nivel máximo del buff
    private float costoBuff = 25f; // Costo inicial del buff
    private float multiplicadorCosto = 7f; // Factor de multiplicación del costo por nivel
    private float multiplicadorGanancia = 4.5f; // Multiplicador de ganancia por nivel
    private float incrementoBuff = 1.5f; // Incremento del buff por nivel

    void Start()
    {
        // Cargar el dinero al iniciar
        GameData.LoadMoney();
        UpdateMoneyText();
        ActualizarNivelYCosto(); // Actualiza nivel y costo al iniciar
        ActualizarGananciaPorSegundo(); // Muestra la ganancia inicial por segundo

        // Asignar la función de compra al botón de la tienda
        tiendaButton.onClick.AddListener(ComprarBuff);
        ActualizarBotonTienda(); // Actualiza el estado del botón de tienda al iniciar
    }

    void Update()
    {
        // Aumentar el dinero pasivo, usando el buff según el nivel
        float dineroGanado = (dineroPasivoPorSegundo * Mathf.Pow(multiplicadorGanancia, nivelBuff)) * Time.deltaTime;
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
            costoBuff *= multiplicadorCosto; // Multiplica el costo para el siguiente nivel
            dineroPasivoPorSegundo *= incrementoBuff; // Incrementa la ganancia por segundo por el buff

            ActualizarNivelYCosto(); // Actualizar el texto del nivel y el costo
            ActualizarGananciaPorSegundo(); // Actualizar la ganancia por segundo
        }
    }

    void ActualizarNivelYCosto()
    {
        // Mostrar el nivel actual del buff y el costo del siguiente nivel en los textos correspondientes
        nivelText.text = "Nivel Buff: " + nivelBuff + "/" + maxNivelBuff;
        costoText.text = "Costo próximo nivel: " + costoBuff + " monedas";
    }

    void ActualizarGananciaPorSegundo()
    {
        // Calcular y mostrar la ganancia por segundo actual
        float gananciaPorSegundo = dineroPasivoPorSegundo * Mathf.Pow(multiplicadorGanancia, nivelBuff);
        gananciaPorSegundoText.text =  gananciaPorSegundo + " /sg";
    }

    void OnApplicationQuit()
    {
        // Guardar el dinero cuando se cierre la aplicación
        GameData.SaveMoney();
    }
}