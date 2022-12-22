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
    public Text timeText;
    
    public float timeRemaining = 5;
    public bool timerIsRunning = false;
    public int valueShake;
    
    Animator attackPlayer;
    Animator attackWizard;

    public BattleState state;

    public Text dialogText;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    GameObject playerGO;
    GameObject enemyGO;
    // public List<int> damageInterval = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START; 
        Accelerometer.Instance.OnShake += ActionToRunWhenShakingDevice;
        StartCoroutine(SetupBattle());
    
    }


    IEnumerator SetupBattle(){
        playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        attackPlayer = playerGO.GetComponent<Animator>();

        enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        attackWizard = enemyGO.GetComponent<Animator>();

        dialogText.text = playerUnit.unitName + " Versus " + enemyUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack(){
        // Damage the enemy
        playerGO.transform.position = new Vector3(5,  playerGO.transform.position.y,  playerGO.transform.position.z);
        attackPlayer.SetBool("attack", true);
        bool isDead = false;
        int accumulateDamage = 0;
        while(timeRemaining > 0 && !isDead){
            valueShake = 0;
            timeRemaining--;
            yield return new WaitForSeconds(1f);
            accumulateDamage += valueShake;
            isDead = enemyUnit.TakeDamage(valueShake);
            enemyHUD.setHP(enemyUnit.currentHP);
            dialogText.text = "Attacking Time " + timeRemaining + "s  Damage  for " + accumulateDamage + "!!";   
        }
        timerIsRunning = false;
    
        attackPlayer.SetBool("attack", false);
        
        dialogText.text = playerUnit.unitName + " Total Attack is " + accumulateDamage + "!!";
        playerGO.transform.position = new Vector3(-8,  playerGO.transform.position.y,  playerGO.transform.position.z);
        
        yield return new WaitForSeconds(2f);

        if(isDead){
            state = BattleState.WON;
            EndBattle();
        }else{
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerDeffend(){
        // Heal the enemy
        
        while(timeRemaining > 0){
            timeRemaining--;
            yield return new WaitForSeconds(1f);
            dialogText.text = "Healing for time " + timeRemaining + "!!";
        }

        timerIsRunning = false;

        dialogText.text = playerUnit.unitName + " Get A Heal For " + valueShake + "!!";
        playerUnit.TakeHeal(valueShake);
        playerHUD.setHP(playerUnit.currentHP);
                
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    private void OnDestroy(){
        Accelerometer.Instance.OnShake -= ActionToRunWhenShakingDevice;
    }

    IEnumerator EnemyTurn(){
        
        enemyGO.transform.position = new Vector3(-6,  enemyGO.transform.position.y,  enemyGO.transform.position.z);
        attackWizard.SetBool("attack", true);
        dialogText.text = enemyUnit.unitName + " Attack You!!";

        yield return new WaitForSeconds(1f);

        enemyGO.transform.position = new Vector3(7,  enemyGO.transform.position.y,  enemyGO.transform.position.z);        
        dialogText.text = enemyUnit.damage + " Damage Taken";
        attackWizard.SetBool("attack", false);
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.setHP(playerUnit.currentHP);
        
        if(isDead){
            state = BattleState.LOST;
            EndBattle();
        }else{
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle(){
        if(state == BattleState.WON){
            dialogText.text = playerUnit.unitName + " Won The Battle!!";
        }else if( state == BattleState.LOST){
            dialogText.text = playerUnit.unitName + " LOSSERR!!";
        }
    }
    
    void ActionToRunWhenShakingDevice(){
        if(timerIsRunning){
            valueShake++;
        }
    }

    void PlayerTurn(){
        dialogText.text = playerUnit.unitName + " Time To Attack!!"; 
    }

    public void OnAttackButton(){
        if(state != BattleState.PLAYERTURN)
            return;
        
        timeRemaining = 5;
        
        valueShake = 0;
        timerIsRunning = true;
        StartCoroutine(PlayerAttack());
    }

    public void OnDeffendButton(){
        if(state != BattleState.PLAYERTURN)
            return;
        timeRemaining = 5;

        valueShake = 0;
        timerIsRunning = true;
        StartCoroutine(PlayerDeffend());
    }



}
