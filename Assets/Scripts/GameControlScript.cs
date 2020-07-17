using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void GotoNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //if (SceneManager.sceneCount > nextSceneIndex)
        //{
            SceneManager.LoadScene(nextSceneIndex);
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(nextSceneIndex));
        //}
    }
    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex));
    }
}
