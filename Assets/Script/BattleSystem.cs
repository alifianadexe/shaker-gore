using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState { START , PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleState state;

    public Text dialogText;

    Unit playerUnit;
    Unit enemyUnit;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START; 
        SetupBattle();
    }

    void SetupBattle(){
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogText.text = playerUnit.unitName + " Versus " + enemyUnit.unitName;
    }

}
