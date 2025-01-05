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
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null){
            Debug.LogWarning("GameManager::PlayMusic::AudioClip == null");
            return;
        }
        if (audioSource == null){
            Debug.LogError("GameManager::PlayMusic::AudioSource == null");
            return;
        }
        if (audioSource.isPlaying) audioSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();
    }
    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
            Debug.LogWarning("GameManager::PlayMusic::AudioClip == null");
        else if (audioSource == null)
            Debug.LogError("GameManager::PlayMusic::AudioSource == null");
        else
            audioSource.PlayOneShot(clip);
    }
}
