using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboardUpdate : MonoBehaviour
{
    private Camera facingCamera;
    public Transform targetTransform;
    private Vector3 localEulerAngle;

    private void OnEnable()
    {
        FaceCamera();
        RotateTransform();
    }

    public void FaceCamera()
    {
        if (facingCamera == null) facingCamera = Camera.main;
        transform.forward = facingCamera.transform.forward;
    }

    protected void RotateTransform()
    {
        if (targetTransform == null) return;

        localEulerAngle = targetTransform.localEulerAngles;
        localEulerAngle.z = localEulerAngle.y;
        targetTransform.localEulerAngles = localEulerAngle;
    }

    private void LateUpdate() {
        FaceCamera();
        RotateTransform();
    }
}
