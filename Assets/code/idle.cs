using UnityEngine;

public class idle : MonoBehaviour
{
    Rigidbody2D rd;
    Animator anim;
    public float velocidad;
    public float fuerzaSalto = 8f;

    [Header("Deteccion de suelo")]
    public Transform puntoSuelo;
    public float radioSuelo = 0.2f;
    public LayerMask capaSuelo;

    bool enSuelo;

    // Start se llama una vez al inicio
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update se llama en cada frame
    void Update()
    {
        if (puntoSuelo == null)
        {
            return;
        }

        enSuelo = Physics2D.OverlapCircle(puntoSuelo.position, radioSuelo, capaSuelo);

        // Usar GetAxisRaw para controles más precisos en 2D (devuelve -1, 0 o 1 exactos)
        float movimiento = Input.GetAxisRaw("Horizontal");
        float direccion = transform.rotation.eulerAngles.y;

        // Lógica de Movimiento y Animación "run"
        if (movimiento == 0)
        {
            anim.SetBool("run", false);
        }
        else if (movimiento < 0) // Izquierda
        {
            anim.SetBool("run", true);
            direccion = 180;
        }
        else if (movimiento > 0) // Derecha
        {
            anim.SetBool("run", true);
            direccion = 0;
        }
        
        gameObject.transform.rotation = UnityEngine.Quaternion.Euler(0, direccion, 0);

        // Aplicar velocidad
        rd.linearVelocity = new Vector2(velocidad * movimiento, rd.linearVelocity.y);

        // Lógica de Salto y Animación "jump"
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            rd.linearVelocity = new Vector2(rd.linearVelocity.x, fuerzaSalto);
            if (anim != null)
            {
                anim.SetBool("jump", true); 
            }
        }

        // Desactivar la animación de salto cuando vuelva a tocar el suelo
        if (enSuelo && rd.linearVelocity.y <= 0)
        {
            if (anim != null)
            {
                anim.SetBool("jump", false);
            }


        }

        if(!enSuelo){
            anim.SetBool("jump", true);
            anim.SetBool("run",false);
        }


    }

    // Dibuja el círculo de detección de suelo en el editor
    void OnDrawGizmosSelected()
    {
        if (puntoSuelo == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(puntoSuelo.position, radioSuelo);
    }
}