using UnityEngine;
using UnityEngine.SceneManagement; // Digunakan untuk restart level

public class PlayerDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah Player menyentuh objek dengan Tag "Water"
        if (collision.CompareTag("Water"))
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Ninja Frog Tenggelam!");

        // Restart level yang sedang dimainkan
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}