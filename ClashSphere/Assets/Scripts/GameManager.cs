using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//创建人：夜泉
//功能说明：用一个单例，管理游戏的基本功能
//比如音频管理，由于会在游戏全局使用，因此可以统一管理
//***************************************** 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private AudioSource audioSource;
    // [SerializeField] 是 Unity 中的一个属性，
    // 用于将私有字段暴露给 Unity 编辑器，
    // 使其可以在编辑器中进行赋值和调整，
    // 而无需将字段的访问级别设置为 public。
    // 这有助于保持封装性，同时提供编辑器的灵活性。
    // 简单讲：
    // [SerializeField] 加 private 的字段
    // 只能在 类内部进行赋值
    // 或者在 Unity 编辑器中通过拖动组件进行赋值，
    // 不能在 其他类或方法中直接访问和赋值。

    //  为了确保 GameManager 类的单例模式在整个游戏生命周期中有效，
    //  需要做到以下几点：
    //1.确保 Instance 只被赋值一次。
    //2.防止在场景切换时销毁 GameManager 实例。
    //3.确保只有一个 GameManager 实例存在。
    private void Awake()
    {
        if (Instance == null) // 1
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 2

            // 确保 audioSource 被正确初始化
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 3
        }
    }
    //  其中：
    //  一、gameObject
    //  如果用C++来类比，gameObject 类似于一个指向基类的指针
    //  通过 gameObject，可以访问和操作该游戏对象的各种组件和属性。
    //  二、Awake方法
    //  会在脚本实例被加载时自动调用。
    //  具体讲，有以下两种情况：
    //1.游戏对象被激活时：当包含该脚本的游戏对象被激活时，Awake 方法会被调用。
    //2.场景加载时：当场景加载时，所有激活的游戏对象上的 Awake 方法会被调用。

    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="clip">音乐资源</param>
    public void PlayMusic(AudioClip clip)
    {
        // 检测异常情况：
        if (clip == null){
            Debug.LogWarning("GameManager::PlayMusic::AudioClip == null");
            return;
        }
        if (audioSource == null){
            Debug.LogError("GameManager::PlayMusic::AudioSource == null");
            return;
        }
        if (audioSource.isPlaying) audioSource.Stop(); // 停止当前播放的音乐

        audioSource.clip = clip; // 将audioSource中的音频换为该方法传进来的音频clip
        audioSource.Play(); // 播放音频
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">音效资源</param>
    public void PlaySound(AudioClip clip)
    {
        // 检测异常情况：
        if (clip == null)
        {
            Debug.LogWarning("GameManager::PlayMusic::AudioClip == null");
            return;
        }
        if (audioSource == null)
        {
            Debug.LogError("GameManager::PlayMusic::AudioSource == null");
            return;
        }

        audioSource.PlayOneShot(clip); // 播放一次
    }
}
