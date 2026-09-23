using UnityEngine;

public class Personaje : MonoBehaviour
{
    Rigidbody2D rd;
    Animator anim;
    Collider2D col;

    public float velocidad = 5f;
    public float fuerzaSalto = 8f;

    [Header("Deteccion de suelo")]
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
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Detectar suelo usando los bordes del collider
        Bounds b = col.bounds;
        enSuelo = Physics2D.OverlapBox(
            new Vector2(b.center.x, b.min.y),
            new Vector2(b.size.x * 0.9f, 0.1f),
            0f,
            capaSuelo
        );

        if (transform.position.y < yMinimo && GameManager.instancia != null)
        {
            GameManager.instancia.Perder();
            return;
        }

        float movimiento = Input.GetAxisRaw("Horizontal");

        // Voltear personaje
        if (!sliding)
        {
            if (movimiento < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
            else if (movimiento > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Slide
        if (enSuelo && !sliding && Input.GetAxisRaw("Vertical") < 0)
        {
            sliding = true;
            timerSlide = duracionSlide;
            direccionSlide = transform.rotation.eulerAngles.y == 180 ? -1f : 1f;
            anim.SetBool("slide", true);
            anim.SetBool("run", false);
            anim.SetBool("jump", false);
        }

        if (sliding)
        {
            rd.linearVelocity = new Vector2(velocidadSlide * direccionSlide, rd.linearVelocity.y);
            timerSlide -= Time.deltaTime;
            if (timerSlide <= 0)
            {
                sliding = false;
                anim.SetBool("slide", false);
            }
            return;
        }

        // Movimiento normal
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
