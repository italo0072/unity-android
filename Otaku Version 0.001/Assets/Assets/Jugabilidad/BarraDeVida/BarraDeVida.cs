using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Slider vidaSlider;  // Referencia al Slider
    public float tiempoVeneno = 5f; // Duración del veneno en segundos
    public float porcentajeDanio = 0.01f; // Porcentaje de vida a reducir cada vez (10%)

    void Start()
    {
        // Asegúrate de cargar los datos de GameData
        GameData.LoadMoney();

        // Inicializa el slider con los valores de GameData
        vidaSlider.maxValue = GameData.maxHealth;  // Establecer el valor máximo desde GameData
        vidaSlider.value = GameData.currentHealth;  // Establecer la vida actual desde GameData
    }

    // Método para recibir daño
    public void RecibirDanio(float cantidadDanio)
    {
        GameData.currentHealth -= cantidadDanio;  // Reduce la vida en GameData
        if (GameData.currentHealth < 0)
            GameData.currentHealth = 0;  // Asegura que no se baje de 0

        vidaSlider.value = GameData.currentHealth;  // Actualiza el Slider
        GameData.SaveMoney(); // Guardar cambios
    }

    // Método para recuperar vida
    public void RecuperarVida(float cantidadVida)
    {
        GameData.currentHealth += cantidadVida;  // Aumenta la vida en GameData
        if (GameData.currentHealth > GameData.maxHealth)
            GameData.currentHealth = GameData.maxHealth;  // Asegura que no se supere la vida máxima

        vidaSlider.value = GameData.currentHealth;  // Actualiza el Slider
        GameData.SaveMoney(); // Guardar cambios
    }

    // Método para iniciar el daño gradual por veneno
    public void IniciarVeneno()
    {
        StartCoroutine(DanioGradual(tiempoVeneno, porcentajeDanio));
    }

    // Coroutine para aplicar daño gradual
    private IEnumerator DanioGradual(float duracion, float porcentaje)
    {
        float tiempoPorAplicacion = duracion / (1 / porcentaje); // Tiempo entre cada aplicación de daño
        for (int i = 0; i < (1 / porcentaje); i++)
        {
            RecibirDanio(GameData.maxHealth * porcentaje); // Aplica el daño en base al porcentaje
            yield return new WaitForSeconds(tiempoPorAplicacion); // Espera antes de aplicar el siguiente daño
        }
    }

    // Método para reiniciar la vida (opcional)
    public void ReiniciarVida()
    {
        GameData.currentHealth = GameData.maxHealth;  // Reinicia la vida actual a la máxima
        vidaSlider.value = GameData.currentHealth;  // Actualiza el Slider
        GameData.SaveMoney(); // Guardar cambios
    }
}