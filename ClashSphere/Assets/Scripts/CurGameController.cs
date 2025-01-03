using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*****************************************
//创建人：夜泉
//功能说明：对单局游戏进行管理
//***************************************** 

public class CurGameController : MonoBehaviour
{
    public static CurGameController Instance;
    private float curEnergy = 4;

    // Start is called before the first frame update
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

    void Update()
    {
        curEnergy += Time.deltaTime;
        UIController.Instance.UpdateEnergy(curEnergy);
    }
}
