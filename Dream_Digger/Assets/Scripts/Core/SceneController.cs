
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonBase<SceneController>
{
    protected override void Awake()
    {
        base.Awake();
    }
    
    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }
    
    public void LoadStageA()
    {
        SceneManager.LoadScene("Stage_A");
        AudioController.Instance.RequestBGM(BGMType.Stage_A);
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene("Ending");
        AudioController.Instance.RequestBGM(BGMType.Ending);
    }
}