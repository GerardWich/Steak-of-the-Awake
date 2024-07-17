using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{ 
    public float Hunger;
    public bool IsEating;

    public float Energy;
    public float Water;

    public float BombTime;
    public bool IsFridged;

    public float FireSpread;
    public bool IsBurning; //For the fire, IsBuring will be turned off for a few seconds when splashed with water.

    public float MonsterTime;
    public bool LightsOn; // all of the monster's progress is reset the second the lights turn off, but its timer is much shorter, maybe goes up to like 35 and u die.

    public float CatTemper;
    public bool IsCatAngry;
    public int CatNumber;
    public bool HasMoved;

    public bool YouDied;
    public bool YouWin;
    public float WinTimer;

    
    // Start is called before the first frame update
    void Start()
    {
        Hunger = 100;
        IsEating = false;

        BombTime = 120;
        IsFridged = false;

        FireSpread = 0; 
        IsBurning = true;

        MonsterTime = 0; 
        LightsOn = false;

        CatTemper = 40; 
        IsCatAngry = false;

        Energy = 3;

        CatNumber = 1; //room 1 is the kitchen
        HasMoved = false;

        WinTimer = 0;
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

        if(IsBurning == true) // when fire is splashed with water, fire is delayed for a few seconds , and loses like 20 to 30 on progress.
        {
            FireSpread += 1 * Time.deltaTime; 
           
            if (CatNumber == 2 && IsCatAngry == true)
            {
                FireSpread += 2 * Time.deltaTime;
            }
        }
        if (FireSpread >= 70)
        {
            YouDied = true;
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

}
