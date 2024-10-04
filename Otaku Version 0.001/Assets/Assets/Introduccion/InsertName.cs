using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;
public class InsertName : MonoBehaviour
{
    public TMP_InputField inputField; // Asigna tu InputField desde el inspector
    public Button submitButton; // Asigna tu botón desde el inspector

    void Start()
    {
        // Cargar el nombre del jugador desde GameData si existe
        inputField.text = GameData.playerName;

        // Asignar la función SetPlayerName al botón
        submitButton.onClick.AddListener(SetPlayerName);
    }

    private void SetPlayerName()
    {
        // Guardar el nombre del jugador en GameData
        GameData.playerName = inputField.text;

        // Guardar los datos actualizados en el archivo JSON
        GameData.SaveMoney();

        // Mensaje de depuración
        Debug.Log("Nombre del jugador guardado: " + inputField.text);

        // Limpiar el campo de texto (opcional)
        inputField.text = "";

        // Cambiar a la escena "IntroduccionHistoria" (o cualquier otra que desees)
        SceneManager.LoadScene("IntroduccionHistoria");
    }
}