using UnityEngine;
using TMPro;

public class moneda : MonoBehaviour
{
    public static int contador = 0;
    private static TextMeshProUGUI texto;

    void Start()
    {
        if (texto == null)
        {
            GameObject obj = GameObject.FindWithTag("ContadorMonedas");
            if (obj != null) texto = obj.GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        contador++;
        if (texto != null) texto.text = contador.ToString();
        Destroy(gameObject);
    }
}