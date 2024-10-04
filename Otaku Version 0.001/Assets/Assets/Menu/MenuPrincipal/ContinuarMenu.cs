using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class ContinuarMenu : MonoBehaviour
{
    public string nombreDeLaEscena; // El nombre de la escena a la que quieres cambiar

    // Método que se llama cuando se presiona el botón de continuar
    public void ContinuarPartida()
    {
        // Cargar los datos guardados del archivo JSON en GameData
        GameData.LoadMoney(); // Carga el dinero, salud, penalización, etc.

        // Cambiar a la escena donde se continúa la partida
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}