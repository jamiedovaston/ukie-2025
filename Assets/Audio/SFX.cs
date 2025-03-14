using JD.Utility.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

public class SFX : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //this.gameObject.GetComponentInParent<Image>().color = _hoverColor;
        Debug.Log("The cursor entered the selectable UI element.");
        AudioUtility.PlaySound(AudioData.Get("2"), AudioSourceType.SFX);
    }
}
