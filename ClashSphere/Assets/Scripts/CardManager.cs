using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static CardManager;
using static UnitManager;
using static UnityEditor.Experimental.GraphView.Port;
//*****************************************
//创建人：夜泉
//功能说明：用来管理牌
//***************************************** 

public class CardManager : MonoBehaviour
{
    // 内部类，表示卡牌
    public class Card : MonoBehaviour
    {
        public int _id; // 卡牌ID
        public Image _image; // 卡牌图像
        public Button _button; // 卡牌按钮
        public int _cost; // 卡牌花费
    };
    [SerializeField]
    private Image prefabOfQueue; // 队列中的卡牌预制体
    [SerializeField]
    private Image prefabOfVector; // 向量中的卡牌预制体
    [SerializeField]
    private Transform slotQueue; // 卡牌队列的父物体
    [SerializeField]
    private Transform[] slotsVector; // 卡牌向量的父物体数组

    private List<UnitManager.UnitInfo> selectedUnit = new List<UnitManager.UnitInfo>(new UnitManager.UnitInfo[10]); // 选中的单位信息列表
    private Queue<Card> cardQueue = new Queue<Card>(); // 卡牌队列

    private void Start()
    {
        if (prefabOfQueue == null || prefabOfVector == null || slotQueue == null || slotsVector == null)
            Debug.LogError("CardManager::Start::One or more required components are not assigned");
        else
            InitCard();
    }

    // 初始化卡牌
    private void InitCard()
    {
        InitUnit();
        InitCardQueue();
        StartCoroutine(InitCardVector());
    }

    // 初始化单位信息
    private void InitUnit()
    {
        for(int i = 0; i < 10; i++)
            selectedUnit[i] = UnitManager.Instance.unitInfos[UnitManager.Instance.selected[i]];
    }

    // 初始化卡牌队列
    private void InitCardQueue()
    {
        System.Random random = new System.Random();
        float count = 0;

        foreach (Transform child in slotQueue)
            Destroy(child.gameObject);

        while (cardQueue.Count < selectedUnit.Count)
        {
            int index = random.Next(0, selectedUnit.Count);

            if (cardQueue.Any(e => e._id == index)) continue;

            Image cardImage = Instantiate(prefabOfQueue, slotQueue);
            cardImage.sprite = selectedUnit[index]._sprite;
            cardImage.transform.localPosition =  new Vector3(0, 0, -1f * count);
            cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 1);

            Button button = cardImage.gameObject.AddComponent<Button>();
            button.enabled = false;

            Card cardComponent = cardImage.gameObject.AddComponent<Card>();
            cardComponent._id = index;
            cardComponent._image = cardImage;
            cardComponent._button = button;
            cardComponent._cost = selectedUnit[index]._cost;
            
            cardQueue.Enqueue(cardComponent);
            count++;
        }
    }
    // 初始化卡牌向量
    private IEnumerator InitCardVector()
    {
        for (int i = 0; i < 4; i++)
        {
            FillCardVector();
            yield return new WaitForSeconds(0.3f);
        }
    }

    // 填充卡牌向量
    private void FillCardVector()
    {
        foreach (Transform slot in slotsVector)
        {
            if (slot.childCount == 0)
            {
                foreach (Card card in cardQueue)
                    card._image.transform.localPosition += new Vector3(0, 0, 1f);

                Card curCard = cardQueue.Dequeue();
                StartCoroutine(MoveCardFromQueueToSlot(curCard, slot));
                curCard._button.enabled = true;
                curCard._button.onClick.AddListener(() => OnCardButtonClick(curCard));
                break;
            }
        }
    }

    // 测试：/////////////////////////////////////////////////////////////////
    // 卡牌按钮点击事件
    private void OnCardButtonClick(Card card)
    {
        Debug.Log($"Card {card._id} clicked!");
    }
    //////////////////////////////////////////////////////////////////////////

    // 将卡牌从队列移动到槽位
    private IEnumerator MoveCardFromQueueToSlot(Card card, Transform slot)
    {
        Vector3 startPos = card._image.transform.position;
        Vector3 endPos = slot.position;
        Vector2 startSize = card._image.rectTransform.sizeDelta;
        Vector2 endSize = prefabOfVector.rectTransform.sizeDelta;

        float duration = 0.2f;
        float cur = 0f;
        while (cur < duration)
        {
            float t = cur / duration;
            card._image.transform.position = Vector3.Lerp(startPos, endPos, t);
            card._image.rectTransform.sizeDelta = Vector2.Lerp(startSize, endSize, t);
            cur += Time.deltaTime;
            yield return null;
        }
        card._image.transform.localPosition = endPos;
        card._image.rectTransform.sizeDelta = endSize;
        card._image.transform.SetParent(slot, false);
        card._image.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        CheckCardStatus();
    }

    // 检查卡牌状态
    private void CheckCardStatus()
    {
        foreach(Transform slot in slotsVector)
        {
            if(slot.childCount > 0)
            {
                Transform cardTrans = slot.GetChild(0);
                Card card = cardTrans.GetComponent<Card>();
                int cost = card._cost;
                if(cost > UIController.Instance.GetCurEnergy())
                {
                    card._image.color = new Color(0.5f, 0.5f, 0.5f);
                    card._button.enabled = false;

                    if(card.GetComponent<DraggableCard>()!= null)
                        Destroy(card.GetComponent<DraggableCard>());
                }
                else
                { 
                    card._image.color = new Color(1f, 1f, 1f);
                    card._button.enabled = true;
                    if (card.GetComponent<DraggableCard>() == null)
                    {
                        DraggableCard draggableCard = card.gameObject.AddComponent<DraggableCard>();
                        draggableCard.SetCardInfo(card); // 设置卡牌信息
                    }
                }
            }
        }
    }
}
