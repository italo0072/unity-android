using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DineroLogic : MonoBehaviour
{
    public float dineroPasivoBase = 1f; // Dinero pasivo base por segundo
    private float dineroTotalPorSegundo; // Dinero total ganado por segundo (base + buffs acumulados)
    public TMP_Text moneyText; // Referencia al texto de dinero
    public TMP_Text gananciaTotalText; // Texto para mostrar la ganancia total acumulada

    // Configuración de cada buff
    [System.Serializable]
    public class Buff
    {
        public Button boton; // Botón del buff
        public TMP_Text nivelText; // Texto para mostrar el nivel actual
        public TMP_Text costoText; // Texto para mostrar el costo del siguiente nivel
        public float costoInicial; // Costo inicial del buff
        public float aumento; // Aumento base por compra
        public float gananciaPorNivel; // Ganancia acumulada por nivel
        public int maxNivel; // Nivel máximo del buff
        public float multiplicadorCosto; // Factor de multiplicación del costo por nivel

        [HideInInspector] public int nivelBuff = 0; // Nivel actual
        [HideInInspector] public float costoActual; // Costo actual
        [HideInInspector] public float gananciaTotal = 0; // Ganancia total acumulada
    }

    public Buff[] buffs; // Array con los buffs configurables

    void Start()
    {
        // Cargar el dinero al iniciar
        GameData.LoadMoney();
        UpdateMoneyText();

        // Inicializar cada buff con su configuración y asignar el evento al botón
        foreach (Buff buff in buffs)
        {
            buff.costoActual = buff.costoInicial;
            ActualizarNivelYCosto(buff); // Actualiza el nivel y costo al iniciar
            buff.boton.onClick.AddListener(() => ComprarBuff(buff));
            ActualizarBotonTienda(buff);
        }

        // Inicializar el dinero total por segundo con el pasivo base
        dineroTotalPorSegundo = dineroPasivoBase;
        gananciaTotalText.text = dineroTotalPorSegundo + " /sg"; // Mostrar la ganancia total inicial
    }

    void Update()
    {
        // Acumular el dinero total por segundo con todos los buffs activos
        GameData.currentMoney += dineroTotalPorSegundo * Time.deltaTime;
        UpdateMoneyText();

        // Calcular la ganancia total y actualizar el texto correspondiente
        CalcularGananciaTotal();

        // Actualizar el botón de tienda según el dinero actual y nivel del buff para cada uno
        foreach (Buff buff in buffs)
        {
            ActualizarBotonTienda(buff);
        }
    }

    private void UpdateMoneyText()
    {
        // Mostrar solo el valor entero del dinero
        moneyText.text = Mathf.FloorToInt(GameData.currentMoney).ToString();
    }

    void ActualizarBotonTienda(Buff buff)
    {
        // Habilitar o deshabilitar el botón dependiendo del dinero y si el buff ha alcanzado su nivel máximo
        buff.boton.interactable = GameData.currentMoney >= buff.costoActual && buff.nivelBuff < buff.maxNivel;
    }

    void ComprarBuff(Buff buff)
    {
        // Si el jugador tiene suficiente dinero y no ha alcanzado el nivel máximo, aplicar el buff
        if (GameData.currentMoney >= buff.costoActual && buff.nivelBuff < buff.maxNivel)
        {
            GameData.currentMoney -= buff.costoActual; // Reducir el dinero del jugador
            buff.nivelBuff++; // Aumentar el nivel del buff
            buff.costoActual *= buff.multiplicadorCosto; // Multiplica el costo para el siguiente nivel

            // Actualizar la ganancia total del buff
            if (buff.nivelBuff == 1) // Si estamos en el nivel 1
            {
                buff.gananciaTotal = buff.aumento; // Asigna el aumento base al total
            }
            else
            {
                // Sumar la ganancia total anterior más el aumento por nivel
                buff.gananciaTotal += buff.gananciaPorNivel; // Suma la ganancia por nivel
            }

            // Actualizar el dinero total por segundo
            dineroTotalPorSegundo = dineroPasivoBase; // Reinicia a la base
            foreach (Buff b in buffs)
            {
                dineroTotalPorSegundo += b.gananciaTotal; // Sumar todas las ganancias
            }

            // Actualizar el texto del nivel y el costo
            ActualizarNivelYCosto(buff);
        }
    }

    void ActualizarNivelYCosto(Buff buff)
    {
        // Mostrar el nivel actual del buff y el costo del siguiente nivel en los textos correspondientes
        buff.nivelText.text = "Nivel Buff: " + buff.nivelBuff + "/" + buff.maxNivel;
        buff.costoText.text = "Costo próximo nivel: " + Mathf.FloorToInt(buff.costoActual) + " monedas";
    }

    void CalcularGananciaTotal()
    {
        // Reiniciar la ganancia total a la base
        float gananciaTotal = dineroPasivoBase;

        // Sumar las ganancias de cada buff
        foreach (Buff buff in buffs)
        {
            gananciaTotal += buff.gananciaTotal; // Añadir la ganancia acumulada
        }

        // Actualizar el dinero total por segundo
        dineroTotalPorSegundo = gananciaTotal;

        // Mostrar la ganancia total acumulada en el texto correspondiente
        gananciaTotalText.text = dineroTotalPorSegundo + " /sg";
    }

    void OnApplicationQuit()
    {
        // Guardar el dinero cuando se cierre la aplicación
        GameData.SaveMoney();
    }
}
