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

    private itemCollection itemCollector;

    private bool levelCompleted = false;

//update if/when level names change
     private string level1Name = "Level 1 NavMesh"; // use current scene for testing update as needed
    private string level2Name = "Level 2";
    private string level3Name = "Level 3"; 
    private string mainMenuScene = "StartMenu - Julia"; 

    private void Awake()
    {
        itemCollector = FindFirstObjectByType<itemCollection>();
        if (itemCollector != null)
        {
           // itemCollector.OnPelletCollected.AddListener(CheckPellets);
        }
         if (levelCompleteText != null)
            levelCompleteText.gameObject.SetActive(false); //hide level text when not complete

            if (gameCompleteText != null)
            gameCompleteText.gameObject.SetActive(false); //hide game text when not complete
    }

    private void Update()
    {
       // CheckPellets(); // check if level complete
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
        levelCompleteText.SetActive(true);
    }

    yield return new WaitForSeconds(5f); // Delay for message read

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
    if (gameCompleteText != null)
    {
        Debug.Log("game complete");
        gameCompleteText.SetActive(true); 
    }

    yield return new WaitForSeconds(5f); // Delay for message read

    if (gameCompleteText != null)
    {
        gameCompleteText.SetActive(false); // hide before use
    }

    SceneManager.LoadScene(mainMenuScene); // Load main menu
}


    private void LoadNextLevel(string currentScene)
    {
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


    // private void IncrementScore()
    // {
    //     score++;
    //     Debug.Log($"Score: {score}");
    //     //scoreText.text = $"Coins: {score}";
    // }
}
