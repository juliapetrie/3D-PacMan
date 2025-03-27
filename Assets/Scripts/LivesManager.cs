using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesUpdater : MonoBehaviour
{
    [SerializeField] private GameObject threeLivesIcon;
    [SerializeField] private GameObject twoLivesIcon;
    [SerializeField] private GameObject oneLifeIcon;
    [SerializeField] private string level1Name = "Level 1"; //update if we change Level 1 scene to different name

    private int lives = 3;

    private void Start()
    {
        lives = 3; 
        UpdateLivesUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost")) // ensure ghosts are all tagged
        {
            LoseLife();
        }
    }

    private void LoseLife()
    {
        if (lives <= 0) return; 

        lives--;

        Debug.Log($"lives: {lives}");

        if (lives <= 0)
        {
            Debug.Log("dead");
            SceneManager.LoadScene(level1Name);
        }
        else
        {
            UpdateLivesUI();
        }
    }

    private void UpdateLivesUI()
    {
        Debug.Log($"updating ui for: {lives}");

        threeLivesIcon.SetActive(lives == 3);
        twoLivesIcon.SetActive(lives == 2);
        oneLifeIcon.SetActive(lives == 1);
    }
}
