using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollisionMessager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision other)
    {
        SendMessageUpwards("OnCollisionEnterChild", other);
    }
    void OnCollisionExit(Collision other)
    {
        SendMessageUpwards("OnCollisionExitChild", other);
    }
    void OnCollisionStay(Collision other)
    {
        SendMessageUpwards("OnCollisionStayChild", other);
    }
    void OnTriggerEnter(Collider other)
    {
        SendMessageUpwards("OnTriggerEnterChild", other);
    }
    void OnTriggerExit(Collider other)
    {
        SendMessageUpwards("OnTriggerExitChild", other);
    }
    void OnTriggerStay(Collider other)
    {
        SendMessageUpwards("OnTriggerStayChild", other);
    }
}
