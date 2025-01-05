using UnityEngine;
using UnityEngine.EventSystems;

//*****************************************
//创建人：夜泉
//功能说明：拖动卡牌
//***************************************** 
public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // CanvasGroup 组件，用于控制 UI 元素的透明度和交互性
    private CanvasGroup canvasGroup;
    // RectTransform 组件，用于控制 UI 元素的位置和大小
    private RectTransform rectTransform;
    // 记录卡牌的原始位置
    private Vector2 originalPosition;
    // 添加一个 Card 类型的字段
    private CardManager.Card cardInfo;

    // 在脚本实例化时调用，初始化组件引用
    private void Awake()
    {
        // 获取 RectTransform 组件
        rectTransform = GetComponent<RectTransform>();
        // 获取 CanvasGroup 组件，如果没有则添加一个
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    // 添加一个方法来设置 cardInfo
    public void SetCardInfo(CardManager.Card card)
    {
        cardInfo = card;
    }

    // 当开始拖动时调用
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 记录卡牌的原始位置
        originalPosition = rectTransform.anchoredPosition;
        // 禁用射线检测，使卡牌在拖动过程中不会阻挡其他 UI 元素
        canvasGroup.blocksRaycasts = false;
    }

    // 当拖动时调用
    public void OnDrag(PointerEventData eventData)
    {
        // 更新卡牌的位置
        rectTransform.anchoredPosition += eventData.delta;
    }

    // 当结束拖动时调用
    public void OnEndDrag(PointerEventData eventData)
    {
        // 启用射线检测，恢复卡牌的交互性
        canvasGroup.blocksRaycasts = true;
        // 重置卡牌位置到原始位置
        rectTransform.anchoredPosition = originalPosition;
    }
}