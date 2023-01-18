using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SMovingBG : MonoBehaviour
{
    public Image bgImage;
    public Material mat;
    public float horizontalSpeed;
    public float verticalSpeed;
    public float verticalOffset;
    private Vector2 offset;

    private void Reset()
    {
        bgImage = GetComponent<Image>();
    }

    private void Awake()
    {
        offset = new Vector2();
        mat = bgImage.material;
    }

    private void Update()
    {
        offset.x += horizontalSpeed * Time.deltaTime;
        offset.y += (verticalSpeed + verticalOffset) * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", offset);
    }
}

