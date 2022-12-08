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

    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START; 
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle(){
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogText.text = playerUnit.unitName + " Versus " + enemyUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(5f);

        state = BattleState.PLAYERTURN;
        PlayerTurn(playerUnit);
    }

    IEnumerator PlayerAttack(){

        // Damage the enemy


        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        
        enemyHUD.setHP(enemyUnit.currentHP);
        dialogText.text = playerUnit.unitName + " Attack for " + playerUnit.damage + "!!";

        yield return new WaitForSeconds(2f);

        if(isDead){
            state = BattleState.WON;
            EndBattle();
        }else{
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn(){
        dialogText.text = enemyUnit.unitName + " Attack You!!";

        yield return new WaitForSeconds(1f);

        playerUnit.TakeDamage(enemyUnit.damage);
    }

    void EndBattle(){
        if(state == BattleState.WON){
            dialogText.text = playerUnit.unitName + " Won The Battle!!";
        }else if( state == BattleState.LOST){
            dialogText.text = playerUnit.unitName + " LOSSERR!!";
        }
    }

    void PlayerTurn(Unit playerUnit){
        dialogText.text = playerUnit.unitName + " Time To Attack!!"; 
    }

    public void OnAttackButton(){
        if(state != BattleState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerAttack());
    }

}
