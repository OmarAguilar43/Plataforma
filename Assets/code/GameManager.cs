using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Paneles UI")]
    public GameObject panelGanar;
    public GameObject panelPerder;

    bool juegoTerminado = false;

    void Awake()
    {
        instancia = this;
        if (panelGanar != null) panelGanar.SetActive(false);
        if (panelPerder != null) panelPerder.SetActive(false);
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
