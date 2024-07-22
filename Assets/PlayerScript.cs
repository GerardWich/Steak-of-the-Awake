using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject FridgeButton;
    public GameObject BombButton;
    public GameObject BombFridgeButton;

    public float Hunger;
    public bool IsEating;

    public float Energy;
    public float Water;

    public float BombTime;
    public bool IsFridged;

    public float FireSpread;
    public bool IsBurning; //For the fire, IsBuring will be turned off for a few seconds when splashed with water.
    public bool FireDelay;
    public float FireDelayTimer;

    public float MonsterTime;
    public bool LightsOn; // all of the monster's progress is reset the second the lights turn off, but its timer is much shorter, maybe goes up to like 35 and u die.

    public float CatTemper;
    public bool IsCatAngry;
    public int CatNumber;
    public bool HasMoved;

    public bool PetCat;
    public bool CatDelay;  //WORK ON CAT TIMER DELAY AFTER PETTING CAT AND THEN MOVE ON TO BUTTONS AND UI MECHANICS TO MAKE GAME WORK!!!
    public float CatDelayTimer;

    public bool YouDied;
    public bool YouWin;
    public float WinTimer;

    public bool UsedWater;

    // Start is called before the first frame update
    void Start()
    {
        Hunger = 100;
        IsEating = false;

        BombTime = 150;
        IsFridged = false;

        FireSpread = 0; 
        IsBurning = true;

        MonsterTime = 0; 
        LightsOn = false;

        CatTemper = 40; 
        IsCatAngry = false;

        Energy = 2;

        CatNumber = 1; //room 1 is the kitchen
        HasMoved = false;

        WinTimer = 0;

        Water = 3;

        FireDelay = false;
        FireDelayTimer = 0;

        BombFridgeButton.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        WinTimer += 1 * Time.deltaTime;

        if(IsEating == false)
        {
            Hunger -= 1 * Time.deltaTime; //when ur eating from the fridge, it replenishes hunger very slowly, but there is unlimited food
        }
        if (Hunger <=0)
        {
            YouDied = true;
        }
        if(IsEating == true)
        {
            Hunger += 2 * Time.deltaTime;
        }

        if(IsFridged == false) //bomb time can't be undone, however it can be indefinitely stalled by keeping it in the fridge, so long as you dont need to eat
        {
            BombTime -= 1 * Time.deltaTime;

            if(CatNumber == 1 && IsCatAngry == true)
            {
                BombTime -= 2 * Time.deltaTime;
            }
        }
        if (BombTime <= 0)
        {
            YouDied = true;
        }

        if(IsBurning == true && FireDelay == false) // when fire is splashed with water, fire is delayed for a few seconds , and loses like 20 to 30 on progress.
        {
            FireSpread += 1 * Time.deltaTime; 
           
            if (CatNumber == 2 && IsCatAngry == true)
            {
                FireSpread += 2 * Time.deltaTime;
            }
        }
        else
        {
            FireDelay = true;
        }
        if(FireDelay == true)
        {
            FireDelayTimer += 1 * Time.deltaTime;
        }
        if(FireDelayTimer >= 5)
        {
            FireDelay = false;
            FireDelayTimer = 0;
        }
        if (FireSpread >= 60)
        {
            YouDied = true;
        }
        if(UsedWater == true)
        {
            FireSpread -= 20;
            FireDelay = true;
            Water -= 1;
            UsedWater = false;
        }

        if(LightsOn == false)
        {
            MonsterTime += 1 * Time.deltaTime; 

            if (CatNumber == 3 && IsCatAngry == true)
            {
                MonsterTime += 2 * Time.deltaTime;
            }
        }
        else
        {
            MonsterTime = 0;
            if (CatNumber == 3 && IsCatAngry == true)
            {
                MonsterTime = 0;
            }

            Energy -= 1 * Time.deltaTime;
        }
        if(MonsterTime >= 35)
        {
            YouDied = true;
        }
        if (Energy <= 0)
        {
            LightsOn = false;
        }
       


        if(IsCatAngry == false)
        {
            CatTemper -= 1 * Time.deltaTime;
        }
        else
        {

            if(HasMoved == false)
            {
                CatNumber = Random.Range(1, 4);
                HasMoved = true;
            }
        }
        if(CatTemper <= 0)
        {
            IsCatAngry = true;
            CatTemper = 0;
        }



        if(WinTimer >= 300)
        {
            YouWin = true;
        }
        //losing conditions down here (maybe i can combine all of them into one if statement combininbg when fire is more than 70 and monster more than 35 maybe and the rest exept cat are less than zero, idk ask the teachers or sumthing.
    }

   public void Fridge()
    {
        Hunger += 3;
        Debug.Log("Player Ate from Fridge");
    }
    public void Bomb()
    {
        IsFridged = true;
        Instantiate(BombFridgeButton, FridgeButton.transform);
        Destroy(BombButton);
        Destroy(FridgeButton);
        Debug.Log("FRidgebomb should be here now");
    }
    public void BombFridge()
    {
        IsFridged = false;
        Debug.Log("test for bomb fridge destruction");
        Instantiate(BombButton);
        Instantiate(FridgeButton);
        Destroy(BombFridgeButton);
    }

}
