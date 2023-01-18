using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrail : MonoBehaviour
{
    public TrailRenderer trailRenderer;

    private void OnEnable()
    {
        trailRenderer.Clear();
    }
}
