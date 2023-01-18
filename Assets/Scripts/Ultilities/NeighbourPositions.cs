using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighbourPositions
{
    private Vector3[] directions;
    private Camera camera;
    private Vector2 cameraSize;
    private Quaternion rightAngle;
    private List<Quaternion> forwardAngles;

    public NeighbourPositions(Camera camera)
    {
        this.camera = camera;
        rightAngle = Quaternion.Euler(0, 90, 0);
        cameraSize = MonoUtility.CalculateOrthoCameraSize(this.camera);
        directions = new Vector3[360];

        for (int i = 0; i < directions.Length; i++)
        {
            directions[i] = Quaternion.Euler(0, i * 1, 0) * Vector3.forward;
        }

        CreateForwardAngles();
    }

    private void CreateForwardAngles()
    {
        forwardAngles = new List<Quaternion>(10);
        for (int i = 0; i < forwardAngles.Capacity; i++)
        {
            float angle = Random.Range(1, 340);
            if (angle == 180) angle += 10;
            forwardAngles.Add(Quaternion.Euler(0, angle, 0));
        }
    }

    public Vector3 GetPosition(int index, float range)
    {
        return directions[index] * range;
    }

    public Quaternion PickRandomForwardAngle()
    {
        return MonoUtility.PickRandomMemberOf<Quaternion>(ref forwardAngles);
    }

    public Vector3 GetNeighbourPos(float range)
    {
        return directions[UnityEngine.Random.Range(0, directions.Length)] * range;
    }
}
