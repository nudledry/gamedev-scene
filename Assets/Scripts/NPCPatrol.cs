using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Transform pointA;  // Titik awal
    public Transform pointB;  // Titik tujuan
    public float speed = 2f;  // Kecepatan gerak

    private Transform target; // Target yang sedang dituju

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        // Gerakkan NPC menuju target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Jika sudah sampai target, ganti target
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }
}
