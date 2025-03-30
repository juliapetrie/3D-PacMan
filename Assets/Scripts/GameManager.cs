using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // [SerializeField] private int lives = 3;
    // // [SerializeField] private float score = 0;
    // [SerializeField] private InputManager inputManager;
    // [SerializeField] private GameObject itemCollection;
    //[SerializeField] private TextMeshProUGUI scoreText;

     [SerializeField] private GameObject  levelCompleteText; 
    [SerializeField] private GameObject  gameCompleteText; 

    [SerializeField] private GameObject  LevelFailText; 
    [SerializeField] private CountdownController countdownController; //to access pause method

   


    private itemCollection itemCollector;

    private bool levelCompleted = false;
    private bool gameOverTriggered = false;


//update when level names change
     private string level1Name = "Merged Level 1 V1"; // use current scene for testing update as needed
    private string level2Name = "Merged Level 2 V1";
    private string level3Name = "Merged Level 3 V1"; 
    private string mainMenuScene = "StartMenu - Julia"; 

    private void Awake()
    {
        itemCollector = FindFirstObjectByType<itemCollection>();
        if (itemCollector != null)
        {
           itemCollector.OnPelletCollected.AddListener(CheckPellets);
        }
         if (levelCompleteText != null)
            levelCompleteText.gameObject.SetActive(false); //hide level text when not complete

            if (gameCompleteText != null)
            gameCompleteText.gameObject.SetActive(false); //hide game text when not complete
    }

    private void Update()
    {
       CheckPellets(); // check if level complete
    }

/*
checks if there are any pellet tags in the scene
if no tags and in level 1 or level 2 show level complete message and move to next scene
if in level 3 show game complete message and go back to main menu
*/
      private void CheckPellets()
    {
        if (levelCompleted) return; 

        GameObject[] pellets = GameObject.FindGameObjectsWithTag("Pellet"); //setup tag system for pellets and add fruit and powerups if were considering them for completion

        if (pellets.Length == 0) 
        {
            levelCompleted = true; 

             string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == level3Name) //if level 3 show game complete message
            {
                StartCoroutine(HandleGameComplete());
            }
            else // otherwise show next level
            {
                StartCoroutine(HandleLevelComplete(currentSceneName));
            }
        }
    }

/*
these are for level messages
*/
private IEnumerator HandleLevelComplete(string currentScene)
{
    if (levelCompleteText != null)
    {
        Debug.Log("Level complete");
         AudioManager.Instance.PlayWinSound();
        levelCompleteText.SetActive(true);
    }

    yield return new WaitForSeconds(3f); // Delay for message read

    if (levelCompleteText != null)
    {
        levelCompleteText.SetActive(false); // hide before use
    }

    LoadNextLevel(currentScene);
}

/*
these are for game finish messages aka after level 3 is done
*/
private IEnumerator HandleGameComplete()
{
     if (countdownController != null) //pause gameplay 
    {
        countdownController.DisableGameplay();
    }
    if (gameCompleteText != null)
    {
        Debug.Log("game complete");
         AudioManager.Instance.PlayWinSound();
        gameCompleteText.SetActive(true); 
    }

    yield return new WaitForSeconds(3f); // Delay for message read

    if (gameCompleteText != null)
    {
        gameCompleteText.SetActive(false); // hide before use
    }

    SceneManager.LoadScene(mainMenuScene); // Load main menu
}


    private void LoadNextLevel(string currentScene)
    {
         if (countdownController != null) //pause gameplay 
    {
        countdownController.DisableGameplay();
    }
        string nextLevel = "";

        if (currentScene == level1Name)
            nextLevel = level2Name;
        else if (currentScene == level2Name)
            nextLevel = level3Name;

        if (!string.IsNullOrEmpty(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.LogError("loading error you haven't changed the names of the levels above or are not running the ui prefab from one of those scenes. update list");
        }
    }

     public void TriggerGameOver()
    {
        if (!gameOverTriggered)
        {
            gameOverTriggered = true;
            StartCoroutine(HandleGameOver());
        }
    }

    private IEnumerator HandleGameOver()
{
    Debug.Log("Game Over");
      AudioManager.Instance.PlayLoseLifeSound();

    if (countdownController != null) //pause gameplay when dead
    {
        countdownController.DisableGameplay();
    }

    if (LevelFailText != null)
    {
        LevelFailText.SetActive(true);
    }

    yield return new WaitForSeconds(3f);

    if (LevelFailText != null)
    {
        LevelFailText.SetActive(false);
    }
    SceneManager.LoadScene(level1Name); //reload level 1
}

}
