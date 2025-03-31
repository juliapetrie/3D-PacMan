using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private string menuScene = "StartMenu - Julia"; //update in inspector as we change these 

    public void ExitToMenu()
    {
        Debug.Log("Exit Button Clicked - Loading Main Menu");
         SceneHandler.Instance.LoadMenuScene(); 
    }
}