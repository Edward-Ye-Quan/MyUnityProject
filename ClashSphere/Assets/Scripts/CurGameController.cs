using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*****************************************
//创建人：夜泉
//功能说明：对单局游戏进行管理
//***************************************** 

public class CurGameController : MonoBehaviour
{
    public static CurGameController Instance; // 单例模式的实例
    private float curEnergy = 4; // 当前能量值
    private double leftTime = 180; // 剩余时间

    void Start()
    {
        if(UIController.Instance == null)
        {
            Debug.LogError("CurGameController::Start::UIController.Instance == null");
            return;
        }
        UIController.Instance.UpdateEnergy(curEnergy);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // 每帧更新能量和时间
    void Update()
    {
        curEnergy += Time.deltaTime;
        UIController.Instance.UpdateEnergy(curEnergy);
        UpdateTime();
    }

    // 更新剩余时间
    private void UpdateTime()
    {
        leftTime -= Time.deltaTime;
        int left = (int)leftTime;
        int min = left / 60;
        int sec = left % 60;
        UIController.Instance.UpdateLeftTime(min, sec);
    }
}