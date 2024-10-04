using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DineroLogic : MonoBehaviour
{
    public float dineroPasivoPorSegundo = 1f; // Dinero pasivo ganado por segundo
    public TMP_Text moneyText; // Referencia al texto de dinero

    void Start()
    {
        // Cargar el dinero al iniciar
        GameData.LoadMoney();
        UpdateMoneyText();
    }

    void Update()
    {
        // Aumentar el dinero pasivo
        GameData.currentMoney += GameData.moneyPerSecond * Time.deltaTime; // Utiliza moneyPerSecond de GameData

        // Actualizar el texto de dinero
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = Mathf.FloorToInt(GameData.currentMoney).ToString(); // Muestra solo el valor del dinero en el texto
    }

    void OnApplicationQuit()
    {
        GameData.SaveMoney(); // Guarda el dinero al salir
    }
}