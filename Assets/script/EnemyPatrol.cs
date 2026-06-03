using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform pointA; // Titik batas kiri
    public Transform pointB; // Titik batas kanan
    public float speed = 2f; // Kecepatan jalan musuh

    private Transform currentTarget;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        // Mulai berjalan menuju titik B (kanan) saat game dimulai
        if (pointB != null)
        {
            currentTarget = pointB;
        }
    }

    void Update()
    {
        if (currentTarget == null) return;

        // Gerakkan musuh menuju target titik saat ini
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // Jika musuh sudah sangat dekat dengan titik target, balik arah
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            if (currentTarget == pointB)
            {
                currentTarget = pointA;
                if (sprite != null) sprite.flipX = true; // Balik gambar menghadap kiri
            }
            else
            {
                currentTarget = pointB;
                if (sprite != null) sprite.flipX = false; // Balik gambar menghadap kanan
            }
        }
    }
}