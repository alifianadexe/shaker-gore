using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public string unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int dmg){
        currentHP -= dmg;

        if(currentHP <= 0){
            return true;
        }else{
            return false;
        }
    }

    public bool TakeHeal(int heal){
        currentHP += heal;
        
        if(currentHP > 317){
            currentHP = 317;
        }
        
        return true;
    }
}
