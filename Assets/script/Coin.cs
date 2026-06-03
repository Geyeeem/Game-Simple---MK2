using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1; // Ubah ke 1 jika ingin nambah 1 koin per ambil, atau sesuaikan

    private bool isCollected = false; // Mencegah koin diambil 2 kali dalam 1 frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected) return; // Jika sudah diambil, abaikan deteksi selanjutnya

        // Mengecek apakah yang menyentuh koin adalah Player
        if (other.CompareTag("Player"))
        {
            // Menambah skor ke PlayerController
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                isCollected = true; // Kunci status agar tidak memicu error double destroy

                player.coinCount += scoreValue; // Menambah jumlah koin di Player
                player.UpdateScoreUI();         // Memaksa UI Text langsung update angka terbarunya
            }

            // Hancurkan objek koin setelah diambil
            Destroy(gameObject);
        }
    }
}