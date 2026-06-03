using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instance ini digunakan agar script lain bisa mengakses GameManager dengan mudah
    public static GameManager instance;

    public int currentHealth = 3; // Tempat menyimpan nyawa sesungguhnya

    void Awake()
    {
        // Logika Singleton: Memastikan hanya ada 1 GameManager di seluruh game
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // JANGAN hancurkan objek ini saat ganti level
        }
        else
        {
            Destroy(gameObject); // Hancurkan duplikat jika tidak sengaja terbuat
        }
    }
}