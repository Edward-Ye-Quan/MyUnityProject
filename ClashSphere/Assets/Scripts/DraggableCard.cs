using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//*****************************************
//创建人：夜泉
//功能说明：拖动卡牌
//***************************************** 
public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup; // CanvasGroup 组件，用于控制 UI 元素的透明度和交互性
    private RectTransform rectTransform; // RectTransform 组件，用于控制 UI 元素的位置和大小
    private Vector2 originalPosition; // 记录卡牌的原始位置
    private CardManager.Card cardInfo; // 添加一个 Card 类型的字段
    private Sprite originalSprite; // 添加一个字段来存储原始图片
    private Vector2 originalSize; // 添加一个字段来存储原始大小


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

    // 添加一个方法来设置 cardnfo
    public void SetCardInfo(CardManager.Card card)
    {
        cardInfo = card;
        originalSprite = card._image.sprite; // 存储原始图片
        originalSize = rectTransform.sizeDelta; // 存储原始大小
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
        rectTransform.anchoredPosition += eventData.delta;
        if (rectTransform.anchoredPosition.y > originalPosition.y + 100)
        {
            ChangeCardImage(); // 更改卡牌图片
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = originalPosition;
        cardInfo._image.sprite = originalSprite; // 恢复原始图片
        rectTransform.sizeDelta = originalSize; // 恢复原始大小
    }

    private void ChangeCardImage()
    {
        Image image = UnitManager.Instance.imagesOfPrefabs[UnitManager.Instance.selected[cardInfo._id]];
        cardInfo._image.sprite = image.sprite;
        rectTransform.sizeDelta = image.rectTransform.sizeDelta;
    }
}