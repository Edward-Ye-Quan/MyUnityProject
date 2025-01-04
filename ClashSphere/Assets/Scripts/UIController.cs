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
    public static UIController Instance;
    public Slider slider_Energy;
    public Text test_Energy;
    public Text test_LeftTime;
    // Start is called before the first frame update
    void Awake()
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
    public int GetCurEnergy()
    {
        if (slider_Energy == null)
        {
            Debug.LogError("UIController::GetCurEnergy::slider_Energy == null");
            return 0;
        }
        return (int)slider_Energy.value;
    }

    public void SetCurEnergy()
    {
        if (test_Energy == null)
        {
            Debug.LogError("UIController::SetTestEnergy::test_Energy == null");
            return;
        }
        test_Energy.text = GetCurEnergy().ToString();
    }

    public void UpdateLeftTime(int m, int s)
    {
        string min = m.ToString(); 
        string sec = s.ToString();
        if (s < 10) sec = "0" + sec;
        test_LeftTime.text = min + ":" + sec;
    }
}
