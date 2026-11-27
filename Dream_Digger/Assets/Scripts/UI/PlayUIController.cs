using TMPro;
using UnityEngine;

public class PlayUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _altitudeText;

    private PlayerStatus _status;

    private void OnEnable()
    {
        _status = GetComponent<PlayerStatus>();
        _status.CurrentHealth.AddListener(RefreshHealthText);
        _status.CurrentAltitude.AddListener(RefreshAltitudeText);
    }
    
    private void RefreshHealthText(int health)
    {
        _healthText.text = $"{_status.CurrentHealth}";
    }

    private void RefreshAltitudeText(int altitude)
    {
        _altitudeText.text = $"{_status.CurrentAltitude}";
    }
}