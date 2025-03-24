using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    [SerializeField] private float score = 0;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject coinCollection;
    //[SerializeField] private TextMeshProUGUI scoreText;


    private itemCollection itemCollection;

    private void Awake()
    {
        itemCollection = FindFirstObjectByType<itemCollection>();
        if (itemCollection != null)
        {
            itemCollection.OnCoinCollected.AddListener(IncrementScore);
        }
    }

    private void IncrementScore()
    {
        score++;
        //scoreText.text = $"Coins: {score}";
    }

    //Score, settings, quit game, etc. functions go below
}
