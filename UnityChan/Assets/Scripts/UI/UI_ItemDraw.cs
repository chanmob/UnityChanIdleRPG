using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UI_ItemDraw : MonoBehaviour
{
    private List<ItemData> _drawItemList = new List<ItemData>();

    [SerializeField]
    private GameObject _fireworkParticle;
    [SerializeField]
    private GameObject _magicalParticle;
    [SerializeField]
    private GameObject _drawButton;
    [SerializeField]
    private GameObject _drawEndButton;
    [SerializeField]
    private GameObject _gridItem;

    [SerializeField]
    private Text _text_Item;
    [SerializeField]
    private Text _text_Count;
    [SerializeField]
    private Image _image_Item;
    private Image[] _image_GetItems;

    [SerializeField]
    private Sprite _default_Sprite;

    private int _count;
    private int _idx = 0;

    private void Start()
    {
        _image_GetItems = _gridItem.GetComponentsInChildren<Image>();
    }

    private void OnEnable()
    {
        SetDefault();
    }

    private void SetDefault()
    {        
        _fireworkParticle.SetActive(false);
        _magicalParticle.SetActive(false);
        
        _text_Item.gameObject.SetActive(false);
        
        _image_Item.sprite = _default_Sprite;
        _image_Item.rectTransform.DOAnchorPos(Vector2.zero, 0);

        _drawButton.SetActive(true);
        _drawEndButton.SetActive(true);
    }

    public void SetDrawItem(List<ItemData> items)
    {
        _drawItemList = items;
        _count = _drawItemList.Count;
    }

    public void DrawClick()
    {
        _fireworkParticle.SetActive(true);
        _image_Item.rectTransform.DOAnchorPos(new Vector2(-300,0), 0.5f).OnComplete(ShowItem);

        _drawButton.SetActive(false);
    }

    public void DrawEnd()
    {
        _idx++;

        if(_idx == _count)
        {
            DrawSkip();
        }

        SetDefault();
    }

    public void DrawSkip()
    {

    }

    private void ShowItem()
    {
        ItemData item = _drawItemList[_idx];

        if(item.tier == ItemData.Tier.LEGENDARY)
        {
            _magicalParticle.SetActive(true);
        }

        _text_Item.text = item.itemName;
        _text_Item.gameObject.SetActive(true);

        _image_Item.sprite = item.itemSprite;

        _drawEndButton.SetActive(true);
    }
}
