using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Emily : MonoBehaviour
{
    [Header("Configuración del Diálogo")]
    public TMP_Text dialogueText; // Referencia al texto donde se mostrará el diálogo
    public string[] emilyLines; // Líneas de diálogo de Emily
    public string[] playerLines; // Líneas de diálogo del jugador
    private int currentLineIndex = 0; // Índice actual del diálogo
    private bool isEmilyTurn = true; // Bandera para saber de quién es el turno

    void Start()
    {
        // Iniciar diálogo desde el primer turno de Emily
        ShowNextDialogue();
    }

    void Update()
    {
        // Si el jugador presiona la tecla Espacio, el diálogo avanza
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextDialogue();
        }
    }

    private void ShowNextDialogue()
    {
        if (isEmilyTurn)
        {
            // Si es el turno de Emily y hay líneas para mostrar
            if (currentLineIndex < emilyLines.Length)
            {
                dialogueText.text = "Emily: " + emilyLines[currentLineIndex];
                isEmilyTurn = false; // Cambia el turno al jugador
            }
        }
        else
        {
            // Si es el turno del jugador y hay líneas para mostrar
            if (currentLineIndex < playerLines.Length)
            {
                dialogueText.text = "Jugador: " + playerLines[currentLineIndex];
                currentLineIndex++; // Avanza al siguiente índice
                isEmilyTurn = true; // Cambia el turno a Emily
            }
        }
    }
}
