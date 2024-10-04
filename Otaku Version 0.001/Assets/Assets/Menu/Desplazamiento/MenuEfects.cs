using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEfects : MonoBehaviour
{
    public RectTransform subMenu; // El menú que se deslizará
    public Vector3 startPosition; // Posición inicial del menú
    public Vector3 endPosition; // Posición final del menú
    public float tiempoDeMovimiento = 0.5f; // Tiempo que tarda en desplazarse

    private bool isOpen = false; // Controla si el menú está abierto o cerrado

    void Start()
    {
        // Establecer la posición inicial del menú
        subMenu.localPosition = startPosition; // Asegúrate de que use localPosition para la UI
    }

    public void ToggleMenu()
    {
        if (isOpen)
        {
            // Si el menú está abierto, se moverá hacia la posición inicial
            StartCoroutine(Mover(tiempoDeMovimiento, subMenu.localPosition, startPosition));
        }
        else
        {
            // Si el menú está cerrado, se moverá hacia la posición final
            StartCoroutine(Mover(tiempoDeMovimiento, subMenu.localPosition, endPosition));
        }

        isOpen = !isOpen; // Cambiar el estado del menú
    }

    IEnumerator Mover(float time, Vector3 posInit, Vector3 posFin)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            subMenu.localPosition = Vector3.Lerp(posInit, posFin, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        subMenu.localPosition = posFin; // Asegurarse de que termine exactamente en la posición final
    }
}