using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnitManager;

//*****************************************
//创建人：夜泉
//功能说明：管理全局的单位信息
//***************************************** 

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    public class UnitInfo
    {
        public int _UId;
        public string _unitName;
        public int _cost;
        public Sprite _sprite;
    };

    public Sprite[] sprites; // 导入图片
    public List<UnitInfo> unitInfos = new List<UnitInfo>();
    public List<int> selected = new List<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeUnits();
            SelectCard();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void InitializeUnits()
    {
        unitInfos = new List<UnitInfo>()
        {
            new UnitInfo() { _UId = 0, _unitName = "射手", _cost = 3, _sprite = sprites[0]},
            new UnitInfo() { _UId = 1, _unitName = "射手", _cost = 3, _sprite = sprites[1]},
            new UnitInfo() { _UId = 2, _unitName = "射手", _cost = 3, _sprite = sprites[2]},
            new UnitInfo() { _UId = 3, _unitName = "射手", _cost = 8, _sprite = sprites[3]},
            new UnitInfo() { _UId = 4, _unitName = "射手", _cost = 8, _sprite = sprites[4]},
            new UnitInfo() { _UId = 5, _unitName = "射手", _cost = 8, _sprite = sprites[5]},
            new UnitInfo() { _UId = 6, _unitName = "射手", _cost = 10, _sprite = sprites[6]},
            new UnitInfo() { _UId = 7, _unitName = "射手", _cost = 10, _sprite = sprites[7]},
            new UnitInfo() { _UId = 8, _unitName = "射手", _cost = 10, _sprite = sprites[8]},
            new UnitInfo() { _UId = 9, _unitName = "射手", _cost = 10, _sprite = sprites[9]},
            new UnitInfo() { _UId = 10, _unitName = "射手", _cost = 10, _sprite = sprites[10]},
            new UnitInfo() { _UId = 11, _unitName = "射手", _cost = 10, _sprite = sprites[11]}
        };
    }

    public void SelectCard()
    {
        selected = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }
}
