using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro; 

public class LevelTextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText; 

    private readonly Dictionary<string, int> sceneLevelMapping = new Dictionary<string, int>
    {
        { "Merged Level 1 V1", 1 },
        { "Merged Level 2 V1", 2 },
        { "Merged Level 3 V1", 3 }
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

