using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SVirtualCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtual;

    private void Start() {
        cinemachineVirtual.Follow = SGameInstance.Instance.player.transform;
    }
}
