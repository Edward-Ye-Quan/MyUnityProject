using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; // 引入场景管理的命名空间
//*****************************************
//创建人：夜泉
//功能说明：用来启动游戏
//***************************************** 

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        PlayMusic();
        Invoke("LoadChooseCardScene", 2f);
    }
    // Start方法：
    // Invoke：

    private void LoadChooseCardScene()
    {
        SceneManager.LoadScene(2);
    }
    // SceneManager
    // LoadScene

    public AudioClip audioClip;
    private void PlayMusic()
    {
        if (audioClip == null)
        {
            Debug.LogWarning("GameStarter::PlayMusic::AudioClip == null");
            return;
        }
        GameManager.Instance.PlayMusic(audioClip);
    }
}
