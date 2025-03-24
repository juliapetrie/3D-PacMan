using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    [SerializeField] private float score = 0;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject itemCollection;
    //[SerializeField] private TextMeshProUGUI scoreText;


    private itemCollection itemCollector;

    private void Awake()
    {
        itemCollector = FindFirstObjectByType<itemCollection>();
        if (itemCollector != null)
        {
            itemCollector.OnPelletCollected.AddListener(IncrementScore);
        }
    }

    private void IncrementScore()
    {
        score++;
        Debug.Log($"Score: {score}");
        //scoreText.text = $"Coins: {score}";
    }
}
