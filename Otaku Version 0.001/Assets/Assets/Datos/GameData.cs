using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public static float currentMoney = 0f; // Dinero actual
    public static float moneyPerSecond = 1f; // Dinero ganado por segundo
    public static float additionalMoneyPerSecond = 5f; // Dinero adicional por segundo al presionar
    public static float penalizationTime = 5f; // Tiempo de penalización

    // Variables de salud
    public static float maxHealth = 100f; // Salud máxima inicial
    public static float currentHealth = 100f; // Salud actual inicial

    // Nueva variable para el nombre del jugador
    public static string playerName = ""; // Nombre del jugador

    // Método para guardar el dinero, dinero por segundo, dinero adicional por segundo, penalización, salud y nombre en un archivo JSON
    public static void SaveMoney()
    {
        string path = Application.persistentDataPath + "/gamedata.json"; // Ruta del archivo JSON
        SaveData data = new SaveData
        {
            money = currentMoney,
            moneyPerSecond = moneyPerSecond,
            additionalMoneyPerSecond = additionalMoneyPerSecond,
            penalizationTime = penalizationTime,
            maxHealth = maxHealth, // Guardar la salud máxima
            currentHealth = currentHealth, // Guardar la salud actual
            playerName = playerName // Guardar el nombre del jugador
        };
        string json = JsonUtility.ToJson(data); // Convierte el objeto en formato JSON
        File.WriteAllText(path, json); // Escribe el JSON en el archivo
    }

    // Método para cargar el dinero, dinero por segundo, dinero adicional por segundo, penalización, salud y nombre desde un archivo JSON
    public static void LoadMoney()
    {
        string path = Application.persistentDataPath + "/gamedata.json"; // Ruta del archivo JSON
        if (File.Exists(path)) // Verifica si el archivo existe
        {
            string json = File.ReadAllText(path); // Lee el contenido del archivo
            SaveData data = JsonUtility.FromJson<SaveData>(json); // Convierte el JSON a un objeto SaveData
            currentMoney = data.money; // Asigna el valor cargado a la variable de dinero
            moneyPerSecond = data.moneyPerSecond; // Asigna el valor cargado a la variable de dinero por segundo
            additionalMoneyPerSecond = data.additionalMoneyPerSecond; // Asigna el valor cargado a la variable de dinero adicional por segundo
            penalizationTime = data.penalizationTime; // Asigna el valor cargado al tiempo de penalización
            maxHealth = data.maxHealth; // Asigna el valor cargado a la salud máxima
            currentHealth = data.currentHealth; // Asigna el valor cargado a la salud actual
            playerName = data.playerName; // Asigna el valor cargado al nombre del jugador
        }
    }
}

// Clase auxiliar para almacenar los datos que se guardan en el archivo JSON
[System.Serializable]
public class SaveData
{
    public float money; // El dinero que se va a guardar
    public float moneyPerSecond; // El dinero por segundo que se va a guardar
    public float additionalMoneyPerSecond; // El dinero adicional por segundo que se va a guardar
    public float penalizationTime; // El tiempo de penalización que se va a guardar
    public float maxHealth; // La salud máxima que se va a guardar
    public float currentHealth; // La salud actual que se va a guardar
    public string playerName; // El nombre del jugador que se va a guardar
}