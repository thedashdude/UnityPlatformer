﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPortalCamera : MonoBehaviour
{
    public Transform universeOffset;
    public Camera playerCamera;
    public Camera skyCamera;
    public Transform player;
    public Transform everything;
    Camera mySkyCamera;

    RenderTexture rt;
    int rtWidth = 4096;
    int rtHeight = 4096;
    int rtDepth = 24;


    Camera myCamera;
    MeshRenderer myMesh;

    public bool hasPlayer = false;

    public bool active = false;
    public bool preSideActive = false;

    // Start is called before the first frame update
    void Start()
    {
        rt = new RenderTexture(rtWidth, rtHeight, rtDepth, RenderTextureFormat.DefaultHDR);
        myCamera = GetComponentInChildren<Camera>();
        myMesh = GetComponentInChildren<MeshRenderer>();
        mySkyCamera = Camera.Instantiate(skyCamera, skyCamera.transform.parent);



        rt.Create();
        if (rt.IsCreated())
        {
            mySkyCamera.targetTexture = rt;
            myCamera.targetTexture = rt;
            myMesh.material.SetTexture("_MainTex", rt);
        }


        //mySkyCamera = Camera.Instantiate(skyCamera, skyCamera.transform);


        myCamera.aspect = playerCamera.aspect;
        myCamera.fieldOfView = playerCamera.fieldOfView;

        mySkyCamera.aspect = playerCamera.aspect;
        mySkyCamera.fieldOfView = playerCamera.fieldOfView;
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        //Set Oblique Projection Matrix
        Plane p = new Plane(transform.forward, transform.position + universeOffset.position - player.position);
        Vector4 clipPlane = new Vector4(p.normal.x, p.normal.y, p.normal.z, p.distance);
        Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(myCamera.worldToCameraMatrix)) * clipPlane;
        var oblique = playerCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        myCamera.projectionMatrix = oblique;



        //End


        myCamera.transform.position = playerCamera.transform.position - everything.rotation * player.position + universeOffset.position;
        myCamera.transform.rotation = playerCamera.transform.rotation;


    }
    void OnTriggerEnterChild(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.position = universeOffset.position;
        }
    }
    /*
    void OnTriggerExitChild(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hasPlayer = false;
        }
    }
    */
}