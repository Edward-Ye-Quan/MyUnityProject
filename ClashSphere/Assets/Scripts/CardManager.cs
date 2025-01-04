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
    public class Card : MonoBehaviour
    {
        public int _id;
        public Image _image;
        public Button _button;
        public int _cost;
    };

    public Image PerfabOfQueue;
    public Image PerfabOfVector;

    public List<UnitInfo> selectedUnit = new List<UnitInfo>(new UnitInfo[10]);
    public Transform slot_queue;
    public Transform[] slots_vector;

    private Card[] card_slots;
    private Queue<Card> card_queue = new Queue<Card>();

    private void Start()
    {
        if(slot_queue == null)
            Debug.LogError("CardManager::Start::slot_queue == null");
        else if (slots_vector == null)
            Debug.LogError("CardManager::Start::slots_vector == null");
        else
            Init_Card();
    }

    private void Init_Card()
    {
        Init_Unit();
        Init_CardQueue();
        StartCoroutine("Init_CardVector");
    }

    private void Init_Unit()
    {
        for(int i = 0; i < 10; i++)
            selectedUnit[i] = UnitManager.Instance.unitInfos[UnitManager.Instance.selected[i]];
    }

    private void Init_CardQueue()
    {
        System.Random random = new System.Random();
        float count = 0;

        if (slot_queue.childCount > 0)
            foreach (Transform child in slot_queue)
                Destroy(child.gameObject);

        while (card_queue.Count < 10)
        {
            int index = random.Next(0, 10);

            // 去重，用的Lambda表达式
            if (card_queue.Any(e => e._id == index)) continue;
            // 测试：/////////////////////////////////////////////////////////////////
            Debug.Log(count + ":" + index);

            // 图片信息：
            Image card_image = Instantiate(PerfabOfQueue, slot_queue);
            card_image.sprite = selectedUnit[index]._sprite;
            // 让队首在最上面：
            card_image.transform.localPosition =  new Vector3(0, 0, -1f * count);
            // 设置牌的不透明度为1：
            Color color = card_image.color;
            color.a = 1;
            card_image.color = color;

            // 设置按钮状态为不可触
            Button button = card_image.gameObject.AddComponent<Button>();
            button.enabled = false;
            // 测试：/////////////////////////////////////////////////////////////////
            button.onClick.AddListener(() => Show(index));

            // 入队：（错误写法）
            // card_queue.Enqueue(new Card { _id = index, _image = card_image, _button = button, _cost = selectedUnit[index]._cost });
            // MonoBehaviour 类的实例不能通过 new 关键字来创建
            // 正确的做法是使用 AddComponent<Card>() 方法
            // 将 Card 组件添加到 GameObject 上，
            // 然后初始化其属性。

            // 添加 Card 组件并初始化:
            Card cardComponent = card_image.gameObject.AddComponent<Card>();
            cardComponent._id = index;
            cardComponent._image = card_image;
            cardComponent._button = button;
            cardComponent._cost = selectedUnit[index]._cost;
            // 入队：
            card_queue.Enqueue(cardComponent);

            count++;
        }
    }

    private void Show(int index)
    {
        Debug.Log(index);
    }

    private IEnumerator Init_CardVector()
    {
        for (int i = 0; i < 4; i++)
        {
            FillCardVector();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void FillCardVector()
    {
        foreach (Transform slot in slots_vector)
        {
            if (slot.childCount == 0)
            {
                foreach (Card card in card_queue)
                    card._image.transform.localPosition += new Vector3(0, 0, 1f);

                Card cur_card = card_queue.Dequeue();
                StartCoroutine(MoveCardFromQueueToSlot(cur_card, slot));
                cur_card._button.enabled = true;
                cur_card._button.onClick.AddListener(() => OnCardButtonClick(cur_card));
                break;
            }
        }
    }

    // 测试：/////////////////////////////////////////////////////////////////
    private void OnCardButtonClick(Card card)
    {
        Debug.Log($"Card {card._id} clicked!");
    }
    private IEnumerator MoveCardFromQueueToSlot(Card card, Transform slot)
    {
        Vector3 startPos = card._image.transform.position;
        Vector3 endPos = slot.position;
        Vector2 startSize = card._image.rectTransform.sizeDelta;
        Vector2 endSize = PerfabOfVector.rectTransform.sizeDelta;

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

    private void CheckCardStatus()
    {
        foreach(Transform slot in slots_vector)
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
                }
                else
                { 
                    card._image.color = new Color(1f, 1f, 1f);
                    card._button.enabled = true;
                }
            }
        }
    }
}
