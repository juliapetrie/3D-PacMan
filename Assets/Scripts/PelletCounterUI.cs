using TMPro;
using UnityEngine;

public class PelletCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current; // Main score text
    private int score = 0;

    void Start()
    {
        current.SetText(" ");
    }

    // only for testing the score actual updateScore() call is in itemCollection.cs
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Debug.Log("testing score update");
    //         UpdateScore();
    //     }
    // }

    public void UpdateScore()
    {
        score++; 
        current.SetText($"{score}"); // update ScoreValue text
    }
}
