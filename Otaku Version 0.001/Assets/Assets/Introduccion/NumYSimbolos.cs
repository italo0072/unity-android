using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumYSimbolos : MonoBehaviour
{
    public TMP_InputField inputField; // Asigna tu InputField desde el inspector

    void Start()
    {
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    private void ValidateInput(string input)
    {
        // Elimina cualquier carácter no deseado
        string filteredInput = System.Text.RegularExpressions.Regex.Replace(input, @"[^a-zA-Z\s]", "");
        inputField.text = filteredInput; // Asigna el texto filtrado de nuevo al InputField
    }
}
