using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PruebaMejora : MonoBehaviour
{
    public Button aumentarPenalizacionButton; // Referencia al botón en la interfaz
    public float incrementoPenalizacion = 1f; // Valor de incremento de penalización

    void Start()
    {
        // Asigna el método al botón
        aumentarPenalizacionButton.onClick.AddListener(AumentarTiempoPenalizacion);
    }

    void AumentarTiempoPenalizacion()
    {
        // Aumentar el tiempo de penalización en GameData
        GameData.penalizationTime += incrementoPenalizacion;

        // Guardar el nuevo valor de la penalización
        GameData.SaveMoney();

        // Imprimir en la consola para ver el nuevo tiempo de penalización
        Debug.Log("Nuevo tiempo de penalización: " + GameData.penalizationTime + " segundos");
    }
}