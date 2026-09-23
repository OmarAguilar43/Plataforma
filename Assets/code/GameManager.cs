using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Paneles UI")]
    public GameObject panelGanar;
    public GameObject panelPerder;

    [Header("Timer")]
    public float tiempoTotal = 60f;
    public TextMeshProUGUI textoTimer;

    float tiempoRestante;
    bool juegoTerminado = false;

    void Awake()
    {
        instancia = this;
        tiempoRestante = tiempoTotal;
        if (panelGanar != null) panelGanar.SetActive(false);
        if (panelPerder != null) panelPerder.SetActive(false);
    }

    void Update()
    {
        if (juegoTerminado) return;

        tiempoRestante -= Time.deltaTime;

        if (textoTimer != null)
        {
            int segundos = Mathf.CeilToInt(tiempoRestante);
            textoTimer.text = segundos.ToString();
            textoTimer.color = tiempoRestante <= 10f ? Color.red : Color.white;
        }

        if (tiempoRestante <= 0)
            Perder();
    }

    public void Ganar()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;
        if (panelGanar != null) panelGanar.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Perder()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;
        if (panelPerder != null) panelPerder.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
