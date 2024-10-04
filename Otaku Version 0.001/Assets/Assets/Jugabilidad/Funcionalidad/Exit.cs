using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    

    // Método para cerrar la aplicación en Android
    public void ExitGame()
    { 
        Application.Quit();
       
        UnityEditor.EditorApplication.isPlaying = false;

    }
}
