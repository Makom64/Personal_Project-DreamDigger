using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button QuitButton;

    private void OnEnable()
    {
        BindButtonEvents();
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
        StartButton.onClick.AddListener(SceneController.Instance.LoadStageA);
        QuitButton.onClick.AddListener(Application.Quit);
    }
    
    private void UnbindButtonEvents()
    {
        StartButton.onClick.RemoveAllListeners();
        QuitButton.onClick.RemoveAllListeners();
    }
}
