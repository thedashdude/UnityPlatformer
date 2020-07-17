using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCameraPreRenderer : MonoBehaviour
{
    public Transform screen;
    private TestPortalCamera tpc;
    //public Transform universeOffset;
    private Transform playerUniverse;
    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        tpc = gameObject.GetComponentInParent<TestPortalCamera>();
        playerUniverse = GameObject.FindGameObjectWithTag("Player Origin").GetComponent<Transform>();
        playerCamera = GameObject.FindGameObjectWithTag("Player Origin").GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void OnPreRender()
    {
        Camera camera = GetComponent<Camera>();
        //Set Oblique Projection Matrix
        Plane p = new Plane(screen.forward, screen.position + tpc.universeOffset.position - playerUniverse.position);
        Vector4 clipPlane = new Vector4(p.normal.x, p.normal.y, p.normal.z, p.distance);
        Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(camera.worldToCameraMatrix)) * clipPlane;
        var oblique = playerCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        camera.projectionMatrix = oblique;
    }
}
