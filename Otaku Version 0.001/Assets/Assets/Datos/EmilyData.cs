using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class EmilyData : MonoBehaviour
{
    public static string EmilyName = "Emily"; // Nombre de Emily
    public static float EmilyCariño = 0f; // Cariño de Emily
    public static List<string> EmilyRecuerdos = new List<string>(); // Lista de recuerdos de Emily
    public static string EmilyEstado = "Soltera"; // Estado de Emily
    public static bool EmilyStatus = true; // True si está soltera, false si está en una relación

    // Método para guardar los datos de Emily en un archivo JSON
    public static void SaveEmilyData()
    {
        string path = Application.persistentDataPath + "/emilydata.json"; // Ruta del archivo JSON
        EmilySaveData data = new EmilySaveData
        {
            name = EmilyName,
            cariño = EmilyCariño,
            recuerdos = EmilyRecuerdos.ToArray(), // Convierte la lista a un array para guardarla
            estado = EmilyEstado,
            status = EmilyStatus
        };
        string json = JsonUtility.ToJson(data); // Convierte el objeto en formato JSON
        File.WriteAllText(path, json); // Escribe el JSON en el archivo
    }

    // Método para cargar los datos de Emily desde un archivo JSON
    public static void LoadEmilyData()
    {
        string path = Application.persistentDataPath + "/emilydata.json"; // Ruta del archivo JSON
        if (File.Exists(path)) // Verifica si el archivo existe
        {
            string json = File.ReadAllText(path); // Lee el contenido del archivo
            EmilySaveData data = JsonUtility.FromJson<EmilySaveData>(json); // Convierte el JSON a un objeto EmilySaveData
            EmilyName = data.name; // Asigna el valor cargado al nombre
            EmilyCariño = data.cariño; // Asigna el valor cargado al cariño
            EmilyRecuerdos = new List<string>(data.recuerdos); // Asigna la lista de recuerdos
            EmilyEstado = data.estado; // Asigna el valor cargado al estado
            EmilyStatus = data.status; // Asigna el valor cargado al estado de relación
        }
    }
}

// Clase auxiliar para almacenar los datos que se guardan en el archivo JSON
[System.Serializable]
public class EmilySaveData
{
    public string name; // El nombre de Emily
    public float cariño; // El cariño de Emily
    public string[] recuerdos; // La lista de recuerdos de Emily
    public string estado; // El estado de Emily
    public bool status; // El estado de relación de Emily
}