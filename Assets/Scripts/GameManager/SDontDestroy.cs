using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDontDestroy : MonoBehaviour
{
    [HideInInspector] public string objectID;
    private void Awake()
    {
        objectID = name + transform.position.ToString() + transform.eulerAngles.ToString();
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<SDontDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<SDontDestroy>()[i] != this)
            {
                if (FindObjectsOfType<SDontDestroy>()[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
