using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 2f;
    public float rangoPatrulla = 3f;

    Vector3 puntoInicio;
    bool yendoDerecha = true;

    void Start()
    {
        puntoInicio = transform.position;
    }

    void Update()
    {
        float dir = yendoDerecha ? 1f : -1f;
        transform.Translate(Vector2.right * dir * velocidad * Time.deltaTime);
        transform.localScale = new Vector3(yendoDerecha ? 1 : -1, 1, 1);

        if (transform.position.x > puntoInicio.x + rangoPatrulla)
            yendoDerecha = false;
        else if (transform.position.x < puntoInicio.x - rangoPatrulla)
            yendoDerecha = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D rdJugador = collision.gameObject.GetComponent<Rigidbody2D>();

        // Si el jugador cae encima (velocidad Y negativa) → muere el enemigo
        if (rdJugador != null && rdJugador.linearVelocity.y < -0.5f)
        {
            rdJugador.linearVelocity = new Vector2(rdJugador.linearVelocity.x, 6f);
            Destroy(gameObject);
        }
        else
        {
            GameManager.instancia.Perder();
        }
    }
}
