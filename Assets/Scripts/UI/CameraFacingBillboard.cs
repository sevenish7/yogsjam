using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{

    private Camera cam;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        cam = Camera.main;
    }

    private void FaceCamera()
    {
        Vector3 cameraVector = Vector3.zero;// = transform.position - cam.transform.position;
        // cameraVector.x = transform.position.x - cam.transform.position.x;
        cameraVector.y = transform.position.y - cam.transform.position.y;
        cameraVector.z = transform.position.z - cam.transform.position.z;

        var invCamDist = 1f / (float)System.Math.Sqrt(cameraVector.x * cameraVector.x + cameraVector.y * cameraVector.y + cameraVector.z * cameraVector.z);

        cameraVector *= invCamDist;

        transform.rotation = Quaternion.LookRotation(cameraVector);
    }

    private void LateUpdate()
    {
        FaceCamera();
    }
}
