// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Cinemachine;
// using Pixelplacement;

// public class SOrthoCameraFrustumCollider : MonoBehaviourCore
// {
//     public CinemachineVirtualCamera vCam;
//     private GameStateData currentStateData;
//     public GameObject[] frustumColliders;
//     private BoxCollider[] boxColliders;
//     private Vector3[] colliderPositions;
//     private Vector3[] colliderSizes;
//     private Camera mainCam;
//     private Action<float> OnOrthographicSizeUpdate;
//     private float zPosition = 22.83f;
//     private float currentOrthoSize;

//     // Start is called before the first frame update
//     void Start()
//     {
//         colliderPositions = new Vector3[4];
//         colliderSizes = new Vector3[2];
//         boxColliders = new BoxCollider[4];

//         mainCam = Camera.main;
//         float height = mainCam.orthographicSize * 2;
//         float width = height * mainCam.aspect;
//         float orthoSizeX = mainCam.orthographicSize * mainCam.aspect;
//         currentOrthoSize = mainCam.orthographicSize;

//         colliderPositions[0] = new Vector3(0, mainCam.orthographicSize, zPosition);
//         colliderPositions[1] = new Vector3(0, -mainCam.orthographicSize, zPosition);
//         colliderPositions[2] = new Vector3(-orthoSizeX, 0, zPosition);
//         colliderPositions[3] = new Vector3(orthoSizeX, 0, zPosition);

//         colliderSizes[0] = new Vector3(width, 0.5f, 47.98f);
//         colliderSizes[1] = new Vector3(0.5f, height, 47.98f);

//         for (int i = 0; i < frustumColliders.Length; i++)
//         {
//             boxColliders[i] = frustumColliders[i].GetComponent<BoxCollider>();
//             boxColliders[i].isTrigger = true;
//             boxColliders[i].center = colliderPositions[i];
//             if (i <= 1) boxColliders[i].size = colliderSizes[0];
//             else boxColliders[i].size = colliderSizes[1];
//         }

//         OnOrthographicSizeUpdate = UpdateColliders;
//         GameInstance.gameEvent.OnCameraZoomOut = StartOrthoSizeTransition;
//         currentStateData = DataController.GameStateData;
//     }

//     private void UpdateColliders(float orthographicSize)
//     {
//         vCam.m_Lens.OrthographicSize = orthographicSize;

//         float height = mainCam.orthographicSize * 2;
//         float width = height * mainCam.aspect;
//         float orthoSizeX = mainCam.orthographicSize * mainCam.aspect;

//         colliderPositions[0].y = mainCam.orthographicSize;
//         colliderPositions[0].z = zPosition;
//         colliderPositions[1].y = -mainCam.orthographicSize;
//         colliderPositions[1].z = zPosition;
//         colliderPositions[2].x = -orthoSizeX;
//         colliderPositions[2].z = zPosition;
//         colliderPositions[3].x = orthoSizeX;
//         colliderPositions[3].z = zPosition;

//         colliderSizes[0].x = width;
//         colliderSizes[1].y = height;

//         for (int i = 0; i < frustumColliders.Length; i++)
//         {
//             boxColliders[i].center = colliderPositions[i];
//             if (i <= 1) boxColliders[i].size = colliderSizes[0];
//             else boxColliders[i].size = colliderSizes[1];
//         }
//     }
    
//     public void StartOrthoSizeTransition()
//     {
//         currentOrthoSize = mainCam.orthographicSize;
//         Tween.Value(currentOrthoSize, currentOrthoSize + 2, OnOrthographicSizeUpdate, 1, 0, Tween.EaseInOut);
//     }
// }
