using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public static Animator Animator;
    
    public PlayerAnimData[] AnimData;
    public Dictionary<PlayerState, AnimationClip> AnimDict;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        AnimDict = new();
        foreach (PlayerAnimData data in AnimData)
        {
            AnimDict.Add(data.PlayerState, data.AnimationClip);
        }
    }

    private void OnEnable()
    {
    }

    private void Start()
    {
        Debug.Log(AnimDict.Count);
        foreach (PlayerAnimData data in AnimData)
        {
            Debug.Log($"딕셔너리에 들어간 애니메이션 : {AnimDict[data.PlayerState].name}");
        }
    }

    public void PlayAnim(PlayerState playerState)
    {
        Animator.Play(AnimDict[playerState].name);
    }
}
