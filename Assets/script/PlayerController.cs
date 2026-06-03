using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int coinCount = 0;
    private int health;
    private bool isDead = false; // Mencegah trigger damage berkali-kali dalam satu waktu

    [Header("UI Settings")]
    public GameObject finishPanel;
    public GameObject deathPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI liveScoreText;
    public TextMeshProUGUI healthText;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        if (GameManager.instance != null)
        {
            health = GameManager.instance.currentHealth;
        }
        else
        {
            health = 3; // Fallback jika GameManager lupa ditaruh di scene awal
        }

        if (finishPanel != null) finishPanel.SetActive(false);
        if (deathPanel != null) deathPanel.SetActive(false);

        UpdateScoreUI();
        UpdateHealthUI();
    }

    void Update()
    {
        if (isDead) return; // Jika sudah mati/finish, hentikan input kontrol

        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0f) sprite.flipX = false;
        else if (moveInput < 0f) sprite.flipX = true;

        // --- BAGIAN ANIMASI ---
        anim.SetBool("isRunning", moveInput != 0);
        anim.SetBool("isJumping", !isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    public void UpdateHealthUI()
    {
        if (healthText != null) healthText.text = "Lives: " + health;
    }

    void TakeDamage()
    {
        if (isDead) return; // Mencegah terkena damage berlapis dalam satu frame

        health -= 1;

        if (GameManager.instance != null)
        {
            GameManager.instance.currentHealth = health;
        }

        UpdateHealthUI();

        if (health <= 0)
        {
            PlayerDeath();
        }

        // Coroutine tetap dijalankan untuk memberikan efek kedip merah sebelum restart/muncul panel mati
        StartCoroutine(FlashDamageAndReload());
    }

    System.Collections.IEnumerator FlashDamageAndReload()
    {
        isDead = true; // Kunci kontrol sebentar saat kena damage
        rb.linearVelocity = Vector2.zero; // Hentikan gerakan player

        // MENGGUNAKAN Realtime AGAR EFEK KEDIP TIDAK IKUT MEMBEKU SAAT NYAWA 0
        sprite.color = Color.red;
        yield return new WaitForSecondsRealtime(0.15f);
        sprite.color = Color.white;
        yield return new WaitForSecondsRealtime(0.15f);
        sprite.color = Color.red;
        yield return new WaitForSecondsRealtime(0.15f);
        sprite.color = Color.white;

        yield return new WaitForSecondsRealtime(0.2f);

        // Jika nyawa masih ada, otomatis reload scene tempat player berada
        if (health > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void PlayerDeath()
    {
        isDead = true;
        if (deathPanel != null) deathPanel.SetActive(true);
        if (GameManager.instance != null) GameManager.instance.currentHealth = 3; // Reset nyawa kembali ke 3 untuk restart total
        DisableControl();

        // MEMBEKUKAN GAME SETELAH PANEL MATI AKTIF
        Time.timeScale = 0f;
    }

    void DisableControl()
    {
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isRunning", false);
        anim.SetBool("isJumping", false);
    }

    // Menggunakan Stay2D jauh lebih aman untuk mendeteksi Ground daripada Exit2D tunggal
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void UpdateScoreUI()
    {
        if (liveScoreText != null) liveScoreText.text = "Score: " + coinCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return; // Jika sudah mati, abaikan trigger objek lain

        // --- BAGIAN COIN DIKOSONGKAN (DITANGANI OLEH Coin.cs) ---
        if (collision.CompareTag("Coin"))
        {
            return;
        }

        if (collision.CompareTag("Water") || collision.CompareTag("Trap"))
        {
            TakeDamage();
        }

        if (collision.CompareTag("Finish"))
        {
            ShowFinishMenu();
        }
    }

    void ShowFinishMenu()
    {
        isDead = true;
        if (finishPanel != null) finishPanel.SetActive(true);
        if (finalScoreText != null) finalScoreText.text = "Final Score: " + coinCount;
        DisableControl();
    }

    // --- KEMBALIKAN TIME SCALE KE 1f AGAR GAME TIDAK FREEZE SAAT RESTART/PINDAH ---
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    // --- TAMBAHAN BARU: FUNGSI PINDAH KE LEVEL 3 ---
    public void LoadLevel3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level3"); // Pastikan nama file scene kamu di folder Assets tulisannya 'Level3' juga
    }
}