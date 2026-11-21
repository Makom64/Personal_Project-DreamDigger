using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : SingletonBase<AudioController>
{
    public List<BGMData> BGMList;
    private Dictionary<BGMType, AudioClip> _bgmDict;
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _bgmDict = new ();
        _audioSource = GetComponent<AudioSource>();

        for (int a = 0; a < BGMList.Count; a++)
        {
            _bgmDict.Add(BGMList[a].Type, BGMList[a].Clip);
        }
    }

    public void RequestBGM(BGMType bgmType)
    {
        _audioSource.clip = _bgmDict[bgmType];
        _audioSource.Play();
    }
    
    public void SetVolume(int volume)
    {
        _audioSource.volume = volume;
    }
}

public enum BGMType
{
    Title,
    Stage_A,
    Ending
}

[Serializable]
public struct BGMData
{
    public BGMType Type;
    public AudioClip Clip;
}
