using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return; 

        Vector3 mas = new Vector3(1, 2, gameObject.transform.position.z);
        Vector3 pos = player.transform.position;
        transform.position = Vector3.Lerp(transform.position, pos + mas, .1f);
    }
}