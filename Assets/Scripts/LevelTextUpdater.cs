using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro; 

public class LevelTextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText; 

    private readonly Dictionary<string, int> sceneLevelMapping = new Dictionary<string, int>
    {
        { "Level 1", 1 },
        { "Level 2", 2 },
        { "Level 3", 3 }
    };

    private void Start()
    {
        UpdateLevelText();
    }

   private void UpdateLevelText()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (sceneLevelMapping.TryGetValue(currentSceneName, out int levelNumber))
        {
            levelText.text = $"{levelNumber}";
        }
}
}

