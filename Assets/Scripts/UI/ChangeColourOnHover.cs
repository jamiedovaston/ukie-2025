using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using TMPro;
using NUnit.Framework.Internal;

public class ChangeColourOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /*[System.Serializable]
    public struct MenuSlice
    {
        public UnityEngine.UI.Image image;
        public Color color;
    }
*/
    [SerializeField] private UnityEngine.UI.Image _parentImage;

    [SerializeField] private TMP_Text _childText;
    [SerializeField] private Color _textColor;

    [SerializeField] private Color _color;
    [SerializeField] private Color _hoverColor = Color.red;

    private int _UILayer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _UILayer = LayerMask.NameToLayer("UI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _parentImage.color = _hoverColor;
        _childText.color = Color.white;
        //this.gameObject.GetComponentInParent<Image>().color = _hoverColor;
        Debug.Log("The cursor entered the selectable UI element.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _parentImage.color = _color;
        _childText.color = _textColor;
        //this.gameObject.GetComponentInParent<Image>().color = _hoverColor;
        Debug.Log("The cursor exited the selectable UI element.");
    }
}
