using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI的命名空间
//*****************************************
//创建人：夜泉
//功能说明：对游戏UI进行管理
//***************************************** 

public class UIController : MonoBehaviour
{
    public static UIController Instance; // 单例模式的实例
    public Slider slider_Energy; // 能量条的滑动条组件
    public Text test_Energy; // 显示当前能量的文本组件
    public Text test_LeftTime; // 显示剩余时间的文本组件

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
    // 更新能量条的值
    public void UpdateEnergy(float newEnergy)
    {
        if (slider_Energy == null)
        {
            Debug.LogError("UIController::UpdateEnergy::slider_Energy == null");
            return;
        }
        slider_Energy.value = newEnergy;
        SetCurEnergy();
    }
    // 获取当前能量值
    public int GetCurEnergy()
    {
        if (slider_Energy == null)
        {
            Debug.LogError("UIController::GetCurEnergy::slider_Energy == null");
            return 0;
        }
        return (int)slider_Energy.value;
    }

    // 设置当前能量值的文本显示
    public void SetCurEnergy()
    {
        if (test_Energy == null)
        {
            Debug.LogError("UIController::SetTestEnergy::test_Energy == null");
            return;
        }
        test_Energy.text = GetCurEnergy().ToString();
    }

    // 更新剩余时间的文本显示
    public void UpdateLeftTime(int m, int s)
    {
        if (test_LeftTime == null)
        {
            Debug.LogError("UIController::UpdateLeftTime::test_LeftTime == null");
            return;
        }
        string min = m.ToString(); 
        string sec = s.ToString();
        if (s < 10) sec = "0" + sec;
        test_LeftTime.text = min + ":" + sec;
    }
}
