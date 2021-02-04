using System;
using System.Collections;
using UnityEngine;

public class Finishline : MonoBehaviour
{
    
    public GameObject completeLevelUI;
    
    [SerializeField] string scene1;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Car")
        {
            CompleteLevel();
            StartCoroutine(wait3Sec());
        }
            
    }

    IEnumerator wait3Sec()
    {
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene1);
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }
}