using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoUtility
{
    #region "Transform"
    public static List<Vector3> CreateListOfCirclePos(float radius, int numberOfPosition, Vector3 offset)
    {
        List<Vector3> listOfPos = new List<Vector3>();
        for (int i = 0; i < numberOfPosition; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfPosition;
            listOfPos.Add(new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius) + offset);
        }

        return listOfPos;
    }

    public static List<Vector3> CreateListOfCirclePos(float radius, int numberOfPosition)
    {
        List<Vector3> listOfPos = new List<Vector3>();
        for (int i = 0; i < numberOfPosition; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfPosition;
            listOfPos.Add(new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius));
        }

        return listOfPos;
    }
    #endregion

    #region "ArrayNList"
    public static T PickRandomMemberOf<T>(ref List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static int PickRandomMemberOf(ref int[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static void Shuffle<T>(ref List<T> list, int number)
    {
        int index;
        int maxCount = list.Count;
        T temp;
        for (int i = 0; i < number; i++)
        {
            index = Random.Range(0, maxCount);
            temp = list[0];
            list[0] = list[index];
            list[index] = temp;
        }
    }

    public static void Shuffle<T>(ref T[] list, int number)
    {
        int index;
        int maxCount = list.Length;
        T temp;
        for (int i = 0; i < number; i++)
        {
            index = Random.Range(0, maxCount);
            temp = list[0];
            list[0] = list[index];
            list[index] = temp;
        }
    }

    #endregion

    #region "Time"
    public static void SetTimeScale(int timeScaleValue, object origion)
    {
        Time.timeScale = timeScaleValue;
        Debug.Log($"Timescale is set to {timeScaleValue} by {origion.GetType()}");
    }

    #endregion

    #region "Rating"
    public static bool GetTrueByRate(int rate)
    {
        if (rate >= 100) return true;
        return Random.Range(1, 101) <= rate;
    }

    public static bool GetTrueByRate(float rate)
    {
        if (rate >= 100) return true;
        return Random.Range(0.1f, 100) <= rate;
    }
    #endregion


    #region Color Maker
    public static string AddColor(int content, string hexaColor)
    {
        string colorContent = $"<COLOR={hexaColor}>{content}</color>";
        return colorContent;
    }

    public static string AddColor(float content, string hexaColor)
    {
        string colorContent = $"<COLOR={hexaColor}>{content}</color>";
        return colorContent;
    }

    public static string AddColor(string content, string hexaColor)
    {
        string colorContent = $"<COLOR={hexaColor}>{content}</color>";
        return colorContent;
    }
    #endregion

    #region Camera
    /// <summary>
    /// Calculate orthographic camera size in world space
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static Vector2 CalculateOrthoCameraSize(Camera camera)
    {
        Vector2 orthoSize = new Vector2();
        orthoSize.y = camera.orthographicSize * 2;
        orthoSize.x = orthoSize.y * camera.aspect;
        return orthoSize;
    }
    #endregion

    #region ConfigData
    public static int[] ResolveData(string dataString)
    {
        string[] contents = dataString.Split(' ');
        int[] toIntArray = new int[contents.Length];
        for (int i = 0; i < toIntArray.Length; i++)
            toIntArray[i] = System.Convert.ToInt32(contents[i]);

        return toIntArray;
    }

    public static int[][] ResolveData(string[] dataStrings)
    {
        int[][] result = new int[dataStrings.Length][];
        for (int i = 0; i < dataStrings.Length; i++)
            result[i] = ResolveData(dataStrings[i]);

        return result;
    }
    #endregion
}
