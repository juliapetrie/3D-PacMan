using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPanelController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel; 
     [SerializeField] private CountdownController countdownController; 

    private void Start()
    {
        ShowTutorialIfLevel1();
    }

    private void ShowTutorialIfLevel1()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Merged Level 1 V1")
        {
            Debug.Log("tutorial panel only for level1");
            tutorialPanel.SetActive(true);
        }
        else
        {
            Debug.Log("not level 1");
            tutorialPanel.SetActive(false);
        }
    }

      public void CloseTutorialPanel()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
            Debug.Log("Tutorial Panel Closed.");
             if (countdownController != null)
            {
                countdownController.DelayedCountDown();
            }
        }

    }
}
