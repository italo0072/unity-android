using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroduccionHistoriaBoton : MonoBehaviour
{
    public void SkipIntro()
    {
        SceneManager.LoadScene("Acto1"); // Cambia "GameScene" por el nombre de tu escena
    }
}
