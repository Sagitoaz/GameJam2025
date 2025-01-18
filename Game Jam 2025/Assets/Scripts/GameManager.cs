using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public void LoadNewGame()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadInstruction()
    {
        SceneManager.LoadScene(2);
    }
}
