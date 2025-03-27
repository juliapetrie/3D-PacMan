using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private string menuScene = "MainMenu"; //update in inspector as we change these 

    public void ExitToMenu()
    {
        Debug.Log("Exit Button Clicked - Loading Main Menu");
        SceneManager.LoadScene(menuScene); 
    }
}