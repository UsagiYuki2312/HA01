using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraFacingBillboard : MonoBehaviour
{
    private Camera facingCamera;
    public Transform targetTransform;
    public UnityEvent OnAfterFacingCamera;
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
        OnAfterFacingCamera?.Invoke();
    }

    protected void RotateTransform()
    {
        if (targetTransform == null) return;

        localEulerAngle = targetTransform.localEulerAngles;
        localEulerAngle.z = localEulerAngle.y;
        targetTransform.localEulerAngles = localEulerAngle;
    }
}
