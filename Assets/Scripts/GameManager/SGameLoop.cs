using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGameLoop : MonoBehaviour
{
    private static SGameLoop instance = null;
    public static SGameLoop Instance { get { return instance; } }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
