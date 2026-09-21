using UnityEngine;
using TMPro;

public class moneda : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public static int contador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        contador++;
        texto.text = contador + "";
        Destroy(gameObject);
    }
}