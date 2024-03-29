﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class Utils
{
    public static bool IsPlayState = false;
    public static bool IsLevelUpStageOccur;

    #region Static Time Helper

    public static double ConvertToUnixTime(DateTime time)
    {
        DateTime epoch = new System.DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        return (time - epoch).TotalSeconds;
    }

    public static DateTime ConvertFromUnixTime(double timeStamp)
    {
        DateTime epoch = new System.DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        DateTime time = epoch.AddSeconds(timeStamp);
        return time;
    }

    public static int GetHours(float totalSeconds)
    {
        return (int) (totalSeconds / 3600f);
    }

    public static int GetMinutes(float totalSeconds)
    {
        return (int) ((totalSeconds / 60) % 60);
    }

    public static int GetSeconds(float totalSeconds)
    {
        return (int) (totalSeconds % 60);
    }

    public static string FormatTime(int timeMinute)
    {
        int hour = timeMinute / 60;
        int minute = timeMinute % 60;
        if (hour == 0)
        {
            return $"{minute:00}:{0:00}";
        }

        return $"{hour:0}:{minute:00}:{0:00}";
    }

    public static string FormatTimeSecond(int timeSecond)
    {
        int hour = timeSecond / 3600;
        int minute = (timeSecond / 60) % 60;
        int second = timeSecond % 60;
        if (hour == 0)
        {
            return $"{minute:00}:{second:00}";
        }

        return $"{hour:0}:{minute:00}:{second:00}";
    }

    #endregion

    #region Cryptography

    public static string XOROperator(string input, string key)
    {
        char[] output = new char[input.Length];
        for (int i = 0; i < input.Length; i++)
            output[i] = (char) (input[i] ^ key[i % key.Length]);

        return new string(output);
    }

    public static string GenerateSHA256NonceFromRawNonce(string rawNonce)
    {
        var sha = new SHA256Managed();
        var utf8RawNonce = Encoding.UTF8.GetBytes(rawNonce);
        var hash = sha.ComputeHash(utf8RawNonce);

        var result = string.Empty;
        foreach (var t in hash)
        {
            result += t.ToString("x2");
        }

        return result;
    }

    public static string GenerateRandomString(int length)
    {
        if (length <= 0)
        {
            throw new Exception("Expected nonce to have positive length");
        }

        const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._";
        var cryptographicallySecureRandomNumberGenerator = new RNGCryptoServiceProvider();
        var result = string.Empty;
        var remainingLength = length;
        var randomNumberHolder = new byte[1];
        while (remainingLength > 0)
        {
            var randomNumbers = new List<int>(16);
            for (var randomNumberCount = 0; randomNumberCount < 16; randomNumberCount++)
            {
                cryptographicallySecureRandomNumberGenerator.GetBytes(randomNumberHolder);
                randomNumbers.Add(randomNumberHolder[0]);
            }

            for (var randomNumberIndex = 0; randomNumberIndex < randomNumbers.Count; randomNumberIndex++)
            {
                if (remainingLength == 0)
                    break;
                var randomNumber = randomNumbers[randomNumberIndex];
                if (randomNumber >= charset.Length) continue;
                result += charset[randomNumber];
                remainingLength--;
            }
        }

        return result;
    }

    #endregion

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
}