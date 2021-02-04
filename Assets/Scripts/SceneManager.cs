using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void Scene1() {  
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");  
    }  

    
    public void exitgame() {  
        Debug.Log("exitgame");  
        Application.Quit();  
    }

    public void Retry()
    {
        CarPanretCode _car = FindObjectOfType<CarPanretCode>();
        _car.InstantiateCheckpoint();
    }

    public void backMenu() {  
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");  
    }  


}
