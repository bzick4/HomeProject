using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public void Exit()
    {
       SceneManager.LoadScene(0);
    }
    
    public void Load(int level)
    {
       SceneManager.LoadScene(level);
    }
    
}
