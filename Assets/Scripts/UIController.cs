using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform speedometerNeedle;
    [SerializeField] private TMP_Text maxSpeedText;
    [SerializeField] private TMP_Text currentSpeedText;
    [SerializeField] private TMP_Text killCountText;
    [SerializeField] private TMP_Text fuelPercentText;
    [SerializeField] private float needleMinRotation;
    [SerializeField] private float needleMaxRotation;
    [SerializeField] private Image fuelFill, fuelIcon;
    private void Start()
    {
        if (Player.Instance == null) return;

        InitializeSpeedometer(Player.Instance.TopSpeed);
        GameStateManager.Instance.zombieKilledEvent.AddListener(UpdateKillCount);
        UpdateKillCount(); //set start value to (hopefully) 0
    }

    void Update()
    {
        if (Player.Instance == null) return;

        UpdateSpeedometer(Player.Instance.CurrentSpeed, Player.Instance.TopSpeed);
        UpdateFuelGauge(Player.Instance.fuelPercentage);
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

    private void UpdateFuelGauge(float percent)
    {
        fuelFill.fillAmount = percent;
        fuelPercentText.text = (percent * 100).ToString();
        
        if (percent <= 0.1f) //alert player when fuel is below 10%
        {
            fuelIcon.color = Color.yellow;
        }
        else
        {
            fuelIcon.color = Color.black;
        }
    }

    private void UpdateKillCount()
    {
        killCountText.text = GameStateManager.Instance.playerKillCount.ToString();
    }
}
