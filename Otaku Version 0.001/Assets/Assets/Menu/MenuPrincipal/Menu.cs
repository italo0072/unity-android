using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Método que se llama cuando se presiona el botón de salir
    public void SalirDelJuego()
    {
        // Cierra la aplicación
        Application.Quit();

        // Este código solo funcionará dentro del editor de Unity
#if UNITY_EDITOR
        // Detiene el juego en el editor de Unity
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
