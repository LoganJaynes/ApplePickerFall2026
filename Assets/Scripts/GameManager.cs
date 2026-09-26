using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public GameObject startScreen;      // Panel shown before the game begins (has the Start button)
    public GameObject gameOverPanel;    // Panel shown when the game ends (has the Restart button)
    public Text roundText;              // Centered top-of-screen text: "Round 1" ... "Round 4" ... "Game Over"

    [Header("Round Settings")]
    public int totalRounds = 4;
    public float roundLength = 20f;     // seconds per round - tweak to taste

    public bool GameActive { get; private set; } = false;

    private int currentRound = 0;
    private float roundTimer = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (startScreen != null) startScreen.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        Time.timeScale = 0f; // freeze the game until the Start button is pressed
    }

    void Update()
    {
        if (!GameActive) return;

        roundTimer += Time.deltaTime;
        if (roundTimer >= roundLength)
        {
            roundTimer = 0f;
            AdvanceRound();
        }
    }

    // Hook this to the Start button's OnClick() in the Inspector
    public void StartGame()
    {
        if (startScreen != null) startScreen.SetActive(false);
        Time.timeScale = 1f;
        currentRound = 1;
        roundTimer = 0f;
        GameActive = true;
        UpdateRoundText();
    }

    void AdvanceRound()
    {
        currentRound++;
        if (currentRound > totalRounds)
        {
            EndGame();
        }
        else
        {
            UpdateRoundText();
        }
    }

    void UpdateRoundText()
    {
        if (roundText != null) roundText.text = "Round " + currentRound;
    }

    // Called from Basket.cs when a branch is caught, or automatically after the last round
    public void EndGame()
    {
        GameActive = false;
        if (roundText != null) roundText.text = "Game Over";
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Hook this to the Restart button's OnClick() in the Inspector
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}