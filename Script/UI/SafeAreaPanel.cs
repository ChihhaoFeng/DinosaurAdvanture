using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaPanel : MonoBehaviour
{

    RectTransform MyPanel;

    private void Awake()
    {
        MyPanel = GetComponent<RectTransform>();

        Vector2 SafeAreaMin = Screen.safeArea.position;                     
        Vector2 SafeAreaMax = Screen.safeArea.position + Screen.safeArea.size;

        SafeAreaMin.x = SafeAreaMin.x / Screen.width;     //image resolution transform to pivot
        SafeAreaMin.y = SafeAreaMin.y / Screen.height;

        SafeAreaMax.x = SafeAreaMax.x / Screen.width;
        SafeAreaMax.y = SafeAreaMax.y / Screen.height;

        MyPanel.anchorMin = SafeAreaMin;
        MyPanel.anchorMax = SafeAreaMax;
    }
}
