using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : SingletonBase<AudioController>
{
    [SerializeField] private AudioSource _bgmPlayer;
    [SerializeField] private AudioSource _sfxPlayer;

    public List<BGMData> BGMList;
    public List<SFXData> SFXList;
    private Dictionary<BGMType, AudioClip> _bgmDict;
    private Dictionary<SFXType, AudioClip> _sfxDict;

    protected override void Awake()
    {
        base.Awake();
        _bgmDict = new ();
        _sfxDict = new();

        for (int a = 0; a < BGMList.Count; a++)
        {
            _bgmDict.Add(BGMList[a].Type, BGMList[a].Clip);
        }

        for (int a = 0; a < SFXList.Count; a++)
        {
            _sfxDict.Add(SFXList[a].Type, SFXList[a].Clip);
        }
    }

    private void OnEnable()
    {
        _bgmPlayer = GetComponent<AudioSource>();
        _sfxPlayer = GetComponent<AudioSource>();
        Debug.Log($"재생할 준비 끝났습니다.");
    }

    public void RequestBGM(BGMType bgmType)
    {
        Debug.Log("브금 재생합니다");
        _bgmPlayer.clip = _bgmDict[bgmType];
        _bgmPlayer.Play();
    }

    public void RequestSFX(SFXType sfxType)
    {
        Debug.Log($"효과음 재생합니다");
        _sfxPlayer.PlayOneShot(_sfxDict[sfxType]);
    }
    
    public void SetBGMVolume(int volume)
    {
        _bgmPlayer.volume = volume;
    }

    public void SetSFXVolume(int volume)
    {
        _sfxPlayer.volume = volume;
    }
}

public enum BGMType
{
    Title,
    Stage_A,
    Ending
}

public enum SFXType
{
    Bite,
    TileBreak,
}

[Serializable]
public struct BGMData
{
    public BGMType Type;
    public AudioClip Clip;
}

[Serializable]
public struct SFXData
{
    public SFXType Type;
    public AudioClip Clip;
}