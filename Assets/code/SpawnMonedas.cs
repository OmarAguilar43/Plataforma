using UnityEngine;

public class SpawnMonedas : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject prefabMoneda;

    [Header("Spawn")]
    public float xInicio = 2f;
    public float separacion = 2f;
    public float alturaSobrePiso = 0.5f;
    public int cantidad = 20;
    public LayerMask capaSuelo;

    void Start()
    {
        if (prefabMoneda == null) { Debug.LogWarning("SpawnMonedas: prefabMoneda no asignado"); return; }

        for (int i = 0; i < cantidad; i++)
        {
            float x = xInicio + i * separacion;

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, 50f), Vector2.down, 100f, capaSuelo);

            if (hit.collider != null)
            {
                Vector3 pos = new Vector3(x, hit.point.y + alturaSobrePiso, 0f);
                Instantiate(prefabMoneda, pos, Quaternion.identity);
            }
        }
    }
}
