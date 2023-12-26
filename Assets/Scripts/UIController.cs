using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform speedometerNeedle;
    [SerializeField] private TMP_Text maxSpeedText;
    [SerializeField] private TMP_Text currentSpeedText;
    [SerializeField] private float needleMinRotation;
    [SerializeField] private float needleMaxRotation;
    private void Start()
    {
        if(Player.Instance == null) return;
        
        InitializeSpeedometer(Player.Instance.TopSpeed);
    }

    void Update()
    {
        if(Player.Instance == null) return;

        UpdateSpeedometer(Player.Instance.CurrentSpeed, Player.Instance.TopSpeed);
    }
    
    private void InitializeSpeedometer(float topSpeed)
    {
        maxSpeedText.text = topSpeed.ToString();
    }

    private void UpdateSpeedometer(float speed, float topSpeed)
    {
        float needleRange = needleMaxRotation + (needleMinRotation * -1f);
        float percent = (speed * needleRange) / topSpeed;
        Quaternion needleRotation = new Quaternion();
        needleRotation.eulerAngles = new Vector3(0f, 0f, (percent + needleMinRotation) * -1f);
        speedometerNeedle.rotation = needleRotation;
        currentSpeedText.text = ((int)speed).ToString();
    }
}
