using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*****************************************
//�����ˣ�ҹȪ
//����˵������һ��������������Ϸ�Ļ�������
//������Ƶ�������ڻ�����Ϸȫ��ʹ�ã���˿���ͳһ����
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
