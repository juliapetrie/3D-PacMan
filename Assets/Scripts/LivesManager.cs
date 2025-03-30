using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private GameObject threeLivesIcon;
    [SerializeField] private GameObject twoLivesIcon;
    [SerializeField] private GameObject oneLifeIcon;

    private int lives;

    private void Start()
    {
        lives = 3;
        UpdateLivesUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost")) 
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
        else
        {
            GameObject pacman = GameObject.FindGameObjectWithTag("Pacman"); //cooldown delay to give chance to escape
            if (pacman != null)
            {
                PlayerController playerController = pacman.GetComponent<PlayerController>();
                PacmanFlashingEffect flash = pacman.GetComponent<PacmanFlashingEffect>();

                if (playerController != null)
                    playerController.hasPelletPowerup = true;

                if (flash != null)
                    flash.StartFlashing(1.5f); // 1.5 second of invincibility

                StartCoroutine(ResetInvincibility(pacman, 1.5f));
            }
        }
    }

    private System.Collections.IEnumerator ResetInvincibility(GameObject pacman, float delay)
    {
        yield return new WaitForSeconds(delay);

        PlayerController playerController = pacman.GetComponent<PlayerController>();
        if (playerController != null)
            playerController.hasPelletPowerup = false;

        Debug.Log("reset to normal powers");
    }

    private void UpdateLivesUI()
    {
        Debug.Log($"updating ui for: {lives}");

        threeLivesIcon.SetActive(lives == 3);
        twoLivesIcon.SetActive(lives == 2);
        oneLifeIcon.SetActive(lives == 1);
    }
}
