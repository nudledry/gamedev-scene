using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Transform pointA;          // Titik awal
    public Transform pointB;          // Titik tujuan
    public float speed = 2f;          // Kecepatan gerak
    public float rotationSpeed = 5f;  // Kecepatan rotasi
    public Animator animator;         // Referensi Animator (jika ada)

    private Transform target;         // Target yang sedang dituju

    void Start()
    {
        target = pointB;

        // Aktifkan animasi berjalan jika ada animator
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }

    void Update()
    {
        // Gerakkan NPC menuju target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Rotasi NPC menghadap arah target
        Vector3 direction = target.position - transform.position;
        direction.y = 0f; // Hindari rotasi ke atas/bawah
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        // Ganti target jika sudah sampai tujuan
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }

    void OnDrawGizmos()
    {
        // Gambar garis bantu di editor
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
