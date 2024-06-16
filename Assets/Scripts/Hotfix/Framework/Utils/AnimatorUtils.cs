using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorUtils
{
    /// <summary>
    /// 得到动画片段的长度
    /// </summary>
    public static float GetAnimationClipLength(Animator animator, string animationName, bool needAllMatch = true)
    {
        if (animator == null)
        {
            Debug.LogError("动画状态机组件为null");
            return 0;
        }
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError("动画状态机为null");
            return 0;
        }
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        if (clips == null)
        {
            Debug.LogError("动画状态机中动画片段数组为null");
            return 0;
        }
        foreach (AnimationClip clip in clips)
        {
            if (needAllMatch ? clip.name.Equals(animationName) : clip.name.Contains(animationName))
            {
                return clip.length;
            }
        }
        Debug.LogError($"找不到此动画片段，animator挂载的物体：{animator.name}，animationName：{animationName}");
        return 0;
    }

    /// <summary>
    /// 得到动画片段的长度
    /// </summary>
    public static float GetAnimationClipLength(Animator animator, int index)
    {
        if (animator == null)
        {
            Debug.LogError("动画状态机为null");
            return 0;
        }
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError("动画状态机为null");
            return 0;
        }
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        if (clips == null)
        {
            Debug.LogError("动画状态机中动画片段数组为null");
            return 0;
        }
        if (clips == null
            || clips.Length <= index)
        {
            Debug.LogError($"动画片段下标超出数组索引，animator挂载的物体：{animator.name}，index：{index}");
            return 0;
        }
        return clips[index].length;
    }
}
