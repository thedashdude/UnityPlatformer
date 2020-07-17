using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTeleport : MonoBehaviour
{
    //GameControlScript gcs;
    // Start is called before the first frame update
    void Start()
    {
        //gcs = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            GameControlScript.GotoNextLevel();// NextLevelStuff();
        }
    }
    //private void SetLevel
}
