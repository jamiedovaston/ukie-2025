using System.Collections;
using System.Collections.Generic;
using JD.Utility.General;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public Canvas m_CountdownCanvas;
    public TMP_Text m_CountdownText;

    public TMP_Text m_CorruptionPercentageText;

    public Canvas m_GameOverCanvas;

    public GameObject m_ColourSprite;
    public Transform m_ColourSpriteHolder;

    public void Start()
    {
        ChangeCorruptionAmount(0.0f);
    }
    
    public IEnumerator C_Countdown()
    {
        yield return new WaitForSeconds(1);
        m_CountdownCanvas.gameObject.SetActive(true);
        
        m_CountdownText.text = "3";
        m_CountdownText.color = Color.green;
        yield return new WaitForSeconds(.5f);
        
        m_CountdownText.text = "2";
        m_CountdownText.color = Color.yellow;
        yield return new WaitForSeconds(.5f);
        
        m_CountdownText.text = "1";
        m_CountdownText.color = Color.red;
        yield return new WaitForSeconds(.5f); 
        
        m_CountdownText.text = "GO!";
        m_CountdownText.color = Color.white;
        yield return new WaitForSeconds(.5f); 
        
        m_CountdownCanvas.gameObject.SetActive(false);
    }

    public void ChangeCorruptionAmount(float corruptedPerc)
    {
        m_CorruptionPercentageText.text = $"{ corruptedPerc.ToString("0.0") }%";
    }

    public void GameOver()
    {
        m_GameOverCanvas.gameObject.SetActive(true);
    }

    public void SpawnColoursThatCanBeHit(List<TeamData> teams)
    {
        DeleteColoursThatCanBeHit();
        
        foreach (TeamData t in teams)
        {
            Image image = Instantiate(m_ColourSprite, m_ColourSpriteHolder).GetComponent<Image>();
            image.color = t.color;
        }
    }

    public void DeleteColoursThatCanBeHit()
    {
        foreach (Transform child in m_ColourSpriteHolder)
        {
            Destroy(child.gameObject);
        }
    }
}