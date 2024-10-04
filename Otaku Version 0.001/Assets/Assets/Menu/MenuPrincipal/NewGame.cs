using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public string nombreDeLaEscena; // Nombre de la escena donde comenzará la nueva partida

    // Método que se llama cuando se presiona el botón de nueva partida
    public void NuevaPartida()
    {
        // Restablecer valores predeterminados de GameData
        GameData.currentMoney = 300f; // Dinero inicial
        GameData.moneyPerSecond = 1f; // Dinero por segundo inicial
        GameData.additionalMoneyPerSecond = 5f; // Dinero adicional por segundo inicial
        GameData.penalizationTime = 5f; // Tiempo de penalización inicial
        GameData.maxHealth = 100f; // Salud máxima inicial
        GameData.currentHealth = 100f; // Salud actual inicial
        GameData.playerName = "";

        // Guardar los valores iniciales en el archivo JSON
        GameData.SaveMoney();

        // Cambiar a la escena de juego donde comenzará la nueva partida
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}
