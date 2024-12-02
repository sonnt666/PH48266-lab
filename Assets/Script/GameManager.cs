using UnityEngine;
using UnityEngine.SceneManagement; // Thêm namespace SceneManagement
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;         // Hiển thị điểm số
    public TextMeshProUGUI livesText;         // Hiển thị số mạng
    public GameObject summaryPanel;          // Panel hiển thị tóm tắt
    public TextMeshProUGUI summaryScoreText; // Điểm số trên màn hình tóm tắt
    public TextMeshProUGUI summaryLivesText; // Mạng còn lại trên màn hình tóm tắt

    private int score = 0;                    // Điểm số ban đầu
    private int lives = 3;                    // Số mạng ban đầu
    private int targetScore = 50;             // Điểm mục tiêu để hoàn thành game
    public int amount = 1;

    public Button restart;

    void Start()
    {
        UpdateScoreUI();
        UpdateLivesUI();
        summaryPanel.SetActive(false); // Đảm bảo panel tóm tắt bị ẩn khi bắt đầu

        // Gắn sự kiện cho nút Restart
        restart.onClick.AddListener(RestartGame);
    }

    // Tăng điểm
    public void AddScore()
    {
        score += amount;
        UpdateScoreUI();

        // Kiểm tra nếu đạt chỉ tiêu
        if (score >= targetScore)
        {
            ShowSummaryPanel();
        }
    }

    // Giảm số mạng
    public void LoseLife()
    {
        lives -= amount;

        if (lives <= 0)
        {
            lives = 0;
            ShowSummaryPanel(); // Hiển thị màn hình tóm tắt khi hết mạng
        }

        UpdateLivesUI();
    }

    // Hiển thị màn hình tóm tắt
    private void ShowSummaryPanel()
    {
        summaryPanel.SetActive(true);
        summaryScoreText.text = "Score: " + score;
        summaryLivesText.text = "Life: " + lives;

        // Dừng thời gian để tạm ngừng trò chơi
        Time.timeScale = 0;
    }

    // Cập nhật điểm số hiển thị
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    // Cập nhật số mạng hiển thị
    private void UpdateLivesUI()
    {
        livesText.text = "Life: " + lives;
    }

    // Reset game (gắn vào nút "Chơi lại")
    public void RestartGame()
    {
        // Khởi động lại thời gian
        Time.timeScale = 1;

        // Tải lại scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
