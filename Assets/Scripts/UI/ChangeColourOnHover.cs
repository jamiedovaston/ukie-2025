using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.


public class ChangeColourOnHover : MonoBehaviour, IPointerEnterHandler
{
    [System.Serializable]
    public struct MenuSlice
    {
        public UnityEngine.UI.Image image;
        public Color color;
    }

    [SerializeField]
    private List<MenuSlice> _menuSlices;

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
        _menuSlices[0].image.color = _hoverColor;
        //this.gameObject.GetComponentInParent<Image>().color = _hoverColor;
        Debug.Log("The cursor entered the selectable UI element.");
    }
}
