using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public GameObject hpSlider;

    public void SetHUD(Unit unit){
        nameText.text = unit.unitName;
        levelText.text = unit.unitLevel;
        
        RectTransform hpCurrent = hpSlider.GetComponent<RectTransform>();
        hpCurrent.sizeDelta = new Vector2 (hpCurrent.sizeDelta.x, unit.maxHP);
;
    } 

    public void setHP(int currentHP){
        RectTransform hpCurrent = hpSlider.GetComponent<RectTransform>();
        hpCurrent.sizeDelta = new Vector2 (hpCurrent.sizeDelta.x, currentHP);
    }
}
