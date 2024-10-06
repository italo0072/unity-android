using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEfects : MonoBehaviour
{
    public RectTransform subMenu; // El menu que se deslizara
    public Vector3 startPosition; // Posicion inicial del menu
    public Vector3 endPosition; // Posicion final del menu
    public float tiempoDeMovimiento = 0.5f; // Tiempo que tarda en desplazarse

    private bool isOpen = false; // Controla si el menu esta abierto o cerrado

    void Start(){
        // Establecer la posicion inicial del menu
        subMenu.localPosition = startPosition; // Asegurate de que use localPosition para la UI
    }

    public void ToggleMenu(){
        if (isOpen){
            // Si el menu esta abierto, se movera hacia la posicion inicial
            StartCoroutine(Mover(tiempoDeMovimiento, subMenu.localPosition, startPosition));
        }
        else{
            // Si el menu esta cerrado, se movera hacia la posicion final
            StartCoroutine(Mover(tiempoDeMovimiento, subMenu.localPosition, endPosition));
        }

        isOpen = !isOpen; // Cambiar el estado del menu
    }

    IEnumerator Mover(float time, Vector3 posInit, Vector3 posFin){
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            subMenu.localPosition = Vector3.Lerp(posInit, posFin, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        subMenu.localPosition = posFin; // Asegurarse de que termine exactamente en la posicion final
    }
}