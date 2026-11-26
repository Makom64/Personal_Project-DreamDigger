using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Slider _bgmVolume;

    private void OnEnable()
    {
        BindButtonEvents();
        BindSliderEvents();
    }

    private void Start()
    {
        Debug.Log($"타이틀 브금 요청합니다.");
        AudioController.Instance.RequestBGM(BGMType.Title);
    }

    private void OnDisable()
    {
        UnbindButtonEvents();
    }
    
    private void BindButtonEvents()
    {
        _startButton.onClick.AddListener(SceneController.Instance.LoadStageA);
        _quitButton.onClick.AddListener(Application.Quit);
    }

    private void BindSliderEvents()
    {
        _bgmVolume.minValue = 0;
        _bgmVolume.maxValue = 100;
        _bgmVolume.wholeNumbers = true;
    }
    
    private void UnbindButtonEvents()
    {
        _startButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }
}
