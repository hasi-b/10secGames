using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject player;         // Reference to the player object
    public TextMeshProUGUI instructionText;      // UI Text to show "Find Building X"
    public TextMeshProUGUI timerText;            // UI Text to show the countdown timer
    public float timeLimit = 10f;     // 10-second timer
    private float timeRemaining;
    public bool isGameOver = false;
    [SerializeField]
    GameObject universalGameController;

    // Array to store building tags ('G', 'Z', 'S', 'F')
    private string[] buildingTags = { "BuildingG", "BuildingZ", "BuildingS"};
    public string targetBuilding;    // The building the player needs to find
    // UI Background Image and Sprites
    public Image backgroundImage;             // Reference to the Image component in the Canvas
    public Sprite normalBackground;           // The normal background during gameplay
    public Sprite victoryBackground;          // The background to show when the player wins
    public Sprite defeatBackground;           // The background to show when the player loses
    [SerializeField]
    GameObject timer;
    void Start()
    {
        // Set the timer to the time limit
        timeRemaining = timeLimit;

        // Select a random building from the array
        targetBuilding = buildingTags[Random.Range(0, buildingTags.Length)];

        // Update the instruction text to show the correct building to find
        instructionText.text = targetBuilding.Substring(8); // Get the 'G', 'Z', 'S' part from tag
    }

    void Update()
    {
        // Update the countdown timer
        if (!isGameOver && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();

            // Check if time has run out
            if (timeRemaining <= 0)
            {
                GameOver(false);
            }
        }
    }

    // End the game and display a win or lose message
    public void GameOver(bool won)
    {
        // Set the game over flag to true to stop the timer
        isGameOver = true;
        if (won)
        {
            timer.SetActive(false);
            SoundManager.instance.PlayWinClip();
            universalGameController.GetComponent<GameManager>().enabled = true;
            GameManager.instance.isWinCondition = true;
            // instructionText.text = "You win!";
            if (backgroundImage != null && victoryBackground != null)
            {
                backgroundImage.sprite = victoryBackground;
            }
        }
        else
        {
            timer.SetActive(false);
            SoundManager.instance.PlayLoseClip();
            universalGameController.GetComponent<GameManager>().enabled = true;
            GameManager.instance.isWinCondition = false;
            // instructionText.text = "Time's up! You lose!";
            if (backgroundImage != null && defeatBackground != null)
            {
                backgroundImage.sprite = defeatBackground;
            }
        }

        // Optionally restart the game after a delay
         // Restart after 2 seconds
    }

    // Restart the game (reload the scene)
    
}
