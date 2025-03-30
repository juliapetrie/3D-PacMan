using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private GameObject threeLivesIcon;
    [SerializeField] private GameObject twoLivesIcon;
    [SerializeField] private GameObject oneLifeIcon;
    // [SerializeField] private string level1Name = "Merged Level 1 V1"; //update if we change Level 1 scene to different name

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

 public void LoseLife()
{
    if (lives <= 0) return;

    lives--;

    Debug.Log($"lives: {lives}");

    UpdateLivesUI();

    if (lives <= 0)
    {
        Debug.Log("dead");

        FindFirstObjectByType<GameManager>()?.TriggerGameOver();
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
