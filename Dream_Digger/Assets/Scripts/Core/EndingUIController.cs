using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class EndingUIController : MonoBehaviour
{
    [SerializeField] private Button _titleButton;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _quitButton;

    private void OnEnable()
    {
        BindButtonEvents();
    }

    private void OnDisable()
    {
        UnbindButtonEvents();
    }

    private void BindButtonEvents()
    {
        _titleButton.onClick.AddListener(SceneController.Instance.LoadTitle);
        _retryButton.onClick.AddListener(SceneController.Instance.LoadStageA);
        _quitButton.onClick.AddListener(Application.Quit);
    }

    private void UnbindButtonEvents()
    {
        _titleButton.onClick.RemoveAllListeners();
        _retryButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }
}
