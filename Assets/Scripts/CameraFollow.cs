using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform folCam;
    // Start is called before the first frame update
    void Start()
    {
        folCam = GameObject.FindGameObjectWithTag("Player Origin").GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = folCam.rotation;
    }
}
