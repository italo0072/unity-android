using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CelularPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject background;
    public float animationDuration = 0.5f;

    private Image bgImage;

    void Start()
    {
        // Ocultar panel y fondo al inicio
        panel.SetActive(false);
        panel.transform.localScale = Vector3.zero;
        bgImage = background.GetComponent<Image>();
        background.SetActive(false);

        // Ajuste del panel según el tamaño de la pantalla
        AdjustPanelSize();
    }

    void AdjustPanelSize()
    {
        RectTransform panelRect = panel.GetComponent<RectTransform>();

        // Ajuste para el 80% de ancho y 70% de alto de la pantalla, manteniendo anclado al centro
        panelRect.anchorMin = new Vector2(0.1f, 0.15f);
        panelRect.anchorMax = new Vector2(0.9f, 0.85f);
        panelRect.offsetMin = panelRect.offsetMax = Vector2.zero;
    }

    public void TogglePanel()
    {
        bool isPanelActive = panel.activeSelf;

        // Si el panel está activo, esconder; de lo contrario, mostrar
        panel.SetActive(true);
        background.SetActive(true);

        Vector3 targetScale = isPanelActive ? Vector3.zero : Vector3.one;
        float targetAlpha = isPanelActive ? 0f : 0.5f;

        // Iniciar ambas animaciones simultáneamente
        StartCoroutine(ScalePanel(panel, targetScale, animationDuration, isPanelActive));
        StartCoroutine(FadeBackground(targetAlpha, animationDuration));
    }

    private IEnumerator ScalePanel(GameObject panel, Vector3 targetScale, float duration, bool deactivateAfter)
    {
        Vector3 initialScale = panel.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            panel.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.transform.localScale = targetScale;

        if (deactivateAfter)
        {
            panel.SetActive(false);
        }
    }

    private IEnumerator FadeBackground(float targetAlpha, float duration)
    {
        Color initialColor = bgImage.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, targetAlpha);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            bgImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bgImage.color = targetColor;

        if (targetAlpha == 0f)
        {
            background.SetActive(false);
        }
    }
}