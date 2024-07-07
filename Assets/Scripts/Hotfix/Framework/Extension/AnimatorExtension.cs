using Framework;
using UnityEngine;
using Event = UnityEngine.Event;

public static class AnimatorExtension
{
    /// <summary>
    /// 播放动画
    /// </summary>
    /// stateName：动画机中的状态名
    public static void PlayAni(this Animator animator, string stateName, float normalizedTime = 0, int layer = 0)
    {
        animator.Play(stateName, layer, normalizedTime);
    }

    /// <summary>
    /// 设置动画机速度
    /// </summary>
    public static void SetSpeed(this Animator animator, float speed)
    {
        if (animator == null)
        {
            Log.Error("动画状态机组件为null");
            return;
        }
        animator.speed = speed;
    }

    /// <summary>
    /// 获取动画机中的动画片段
    /// </summary>
    /// animationClipName：动画片段名
    public static AnimationClip GetClip(this Animator animator, string animationClipName, bool fullMatch = true)
    {
        if (animator == null)
        {
            Log.Error("动画状态机组件为null");
            return null;
        }
        if (animator.runtimeAnimatorController == null)
        {
            Log.Error("动画状态机为null");
            return null;
        }
        if (string.IsNullOrEmpty(animationClipName))
        {
            Log.Error("动画片段名不能为空");
            return null;
        }
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        if (clips == null)
        {
            Log.Error($"{animator.name}动画状态机中动画片段数组为null");
            return null;
        }
        foreach (AnimationClip clip in clips)
        {
            if (fullMatch ? clip.name.Equals(animationClipName) : clip.name.Contains(animationClipName))
            {
                return clip;
            }
        }
        Log.Error($"{animator.name}动画状态机中找不到{animationClipName}动画片段");
        return null;
    }

    /// <summary>
    /// 获取动画机中的动画片段
    /// </summary>
    public static AnimationClip GetClip(this Animator animator, int index)
    {
        if (animator == null)
        {
            Log.Error("动画状态机组件为null");
            return null;
        }
        if (animator.runtimeAnimatorController == null)
        {
            Log.Error("动画状态机为null");
            return null;
        }
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        if (clips == null)
        {
            Log.Error($"{animator.name}动画状态机中动画片段数组为null");
            return null;
        }
        if (clips.Length <= index)
        {
            Log.Error($"{animator.name}动画状态机中动画片段数组长度小于index，");
            return null;
        }
        return clips[index];
    }

    /// <summary>
    /// 获取动画片段的时间
    /// </summary>
    /// animationClipName：动画片段名
    public static float GetClipTime(this Animator animator, string animationClipName, bool fullMatch = true)
    {
        AnimationClip clip = animator.GetClip(animationClipName, fullMatch);
        if (clip == null)
            return 0;
        return clip.length / animator.speed;
    }

    /// <summary>
    /// 获取动画片段的时间
    /// </summary>
    public static float GetClipTime(this Animator animator, int index)
    {
        AnimationClip clip = animator.GetClip(index);
        if (clip == null)
            return 0;
        return clip.length / animator.speed;
    }

    /// <summary>
    /// 获取动画片段中事件的时间
    /// </summary>
    /// return：如果找不到动画片段则返回0，如果动画片段中找不到事件则返回动画片段的时间
    public static float GetClipEventTime(this Animator animator, string animationClipName, bool fullMatch = true, int eventIndex = 0)
    {
        AnimationClip clip = animator.GetClip(animationClipName, fullMatch);
        if (clip == null)
            return 0;
        if (clip.events.Length <= 0)
        {
            Log.Error($"{animator.name}动画状态机中的{animationClipName}动画片段没有添加事件");
            return 0;
        }
        if (clip.events.Length <= eventIndex)
        {
            Log.Error($"{animator.name}动画状态机中的{animationClipName}动画片段事件数组长度小于index");
            return clip.length / animator.speed;
        }
        return clip.events[eventIndex].time / animator.speed;
    }

    /// <summary>
    /// 获取动画片段中事件的时间
    /// </summary>
    /// return：如果找不到动画片段则返回0，如果动画片段中找不到事件则返回动画片段的时间
    public static float GetClipEventTime(this Animator animator, string animationClipName, string functionName, bool fullMatch = true)
    {
        AnimationClip clip = animator.GetClip(animationClipName, fullMatch);
        if (clip == null)
            return 0;
        if (clip.events.Length <= 0)
        {
            Log.Error($"{animator.name}动画状态机中的{animationClipName}动画片段没有添加事件");
            return 0;
        }
        foreach (var e in clip.events)
        {
            if (fullMatch ? e.functionName == functionName : e.functionName.Contains(functionName))
                return e.time / animator.speed;
        }
        Log.Error($"{animator.name}动画状态机中的{animationClipName}动画片段找不到{functionName}事件");
        return clip.length / animator.speed;
    }
}