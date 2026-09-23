using UnityEngine;

public class Personaje : MonoBehaviour
{
    Rigidbody2D rd;
    Animator anim;
    public float velocidad;
    public float fuerzaSalto = 8f;

    [Header("Deteccion de suelo")]
    public float distanciaRayo = 0.6f;
    public LayerMask capaSuelo;

    [Header("Muerte por caida")]
    public float yMinimo = -10f;

    [Header("Slide")]
    public float velocidadSlide = 10f;
    public float duracionSlide = 0.4f;

    bool enSuelo;
    bool sliding;
    float timerSlide;
    float direccionSlide;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        enSuelo = Physics2D.Raycast(transform.position, Vector2.down, distanciaRayo, capaSuelo);

        if (transform.position.y < yMinimo)
        {
            GameManager.instancia.Perder();
            return;
        }

        float movimiento = Input.GetAxisRaw("Horizontal");
        float direccion = transform.rotation.eulerAngles.y;

        if (!sliding)
        {
            if (movimiento < 0) direccion = 180;
            else if (movimiento > 0) direccion = 0;
        }
        else
        {
            direccion = direccionSlide;
        }
        transform.rotation = Quaternion.Euler(0, direccion, 0);

        // Slide
        if (enSuelo && !sliding && Input.GetAxisRaw("Vertical") < 0 && movimiento != 0)
        {
            sliding = true;
            timerSlide = duracionSlide;
            direccionSlide = direccion;
            anim.SetBool("slide", true);
            anim.SetBool("run", false);
        }

        if (sliding)
        {
            timerSlide -= Time.deltaTime;
            float dir = (direccionSlide == 180) ? -1f : 1f;
            rd.linearVelocity = new Vector2(velocidadSlide * dir, rd.linearVelocity.y);

            if (timerSlide <= 0)
            {
                sliding = false;
                anim.SetBool("slide", false);
            }
            return;
        }

        rd.linearVelocity = new Vector2(velocidad * movimiento, rd.linearVelocity.y);

        if (enSuelo)
        {
            anim.SetBool("jump", false);
            anim.SetBool("run", movimiento != 0);

            if (Input.GetButtonDown("Jump"))
            {
                rd.linearVelocity = new Vector2(rd.linearVelocity.x, fuerzaSalto);
                anim.SetBool("jump", true);
                anim.SetBool("run", false);
            }
        }
        else
        {
            anim.SetBool("jump", true);
            anim.SetBool("run", false);
        }
    }
}
