using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerScript : MonoBehaviour
{
    public GameObject FridgeButton;
    public GameObject BombButton;
    public GameObject BombFridgeButton;
    public GameObject FirePlaceButton;
    public GameObject LightSwitchONButton;
    public GameObject LightswitchOFFButton;
    public GameObject EnergyButton;//buy in shop;
    public GameObject WaterButton; //buy in shop;
    public GameObject CatButton;
    public GameObject eighttext;
    public GameObject fivetext;

    public GameObject KitchenBG;
    public GameObject LivingRoomBG; //maybe add a version on fire later on?
    public GameObject BedroomBGLight;
    public GameObject BedroomBGDark;
    public GameObject ShopBG;

    public bool InbedRoom;
    public bool InLivingRoom;
    public bool InKitchen;
    public bool InShop;

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
    public bool CatDelay; 
    public float CatDelayTimer;

    public bool YouDied;
    public bool YouWin;
    public float WinTimer;
    public bool UsedWater;
    public float Money;

    public AudioSource Ambience;
    public AudioSource Meow;
    public AudioSource Door;
    public AudioSource Fridgey;
    public AudioSource Splash;
    public AudioSource ChaChing;
    public AudioSource Hissing;
    public AudioSource Burning;

    // Start is called before the first frame update
    void Start()
    {
        Ambience.Play();
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

        Money = 0;

        InKitchen = true;
    }


    // Update is called once per frame
    void Update()
    {
        WinTimer += 1 * Time.deltaTime;

        if (IsEating == false)
        {
            Hunger -= 1 * Time.deltaTime; //when ur eating from the fridge, it replenishes hunger very slowly, but there is unlimited food
        }
        if (Hunger <= 0)
        {
            YouDied = true;
        }
        if (IsEating == true)
        {
            Hunger += 2 * Time.deltaTime;
        }

        if (IsFridged == false) 
        {
            BombTime -= 1 * Time.deltaTime;

            if (CatNumber == 1 && IsCatAngry == true)
            {
                BombTime -= 2 * Time.deltaTime;
            }
        }
        if (BombTime <= 0)
        {
            YouDied = true;
        }
        if (Hunger >= 100) 
        {
            Hunger = 100;
        }

        if (IsBurning == true && FireDelay == false) // loses like 20 to 30 on progress when splashed wityh water
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
        if (FireDelay == true)
        {
            FireDelayTimer += 1 * Time.deltaTime;
        }
        if (FireDelayTimer >= 5)
        {
            FireDelay = false;
            FireDelayTimer = 0;
        }
        if (FireSpread >= 60)
        {
            YouDied = true;
        }
        if (UsedWater == true)
        {
            FireSpread -= 20;
            FireDelay = true;
            Water -= 1;
            UsedWater = false;
        }
        if (FireSpread <= 0) //prevents firespread from going below zero
        {
            FireSpread = 0;
        }

        if (LightsOn == false)
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
        if (MonsterTime >= 35)
        {
            YouDied = true;
        }
        

        if (IsCatAngry == false)
        {
            CatTemper -= 1 * Time.deltaTime;
        }
        else
        {

            if (HasMoved == false)
            {
                CatNumber = Random.Range(1, 4);
                HasMoved = true;
            }
            Hissing.Play();
        }
        if (CatTemper <= 0)
        {
            IsCatAngry = true;
            CatTemper = 0;
        }

        if(Energy <=0)
        {
            LightsOn = false;
            Energy = 0;

            if(InbedRoom  == true)
            {
                BedroomBGDark.SetActive(true);
                LightswitchOFFButton.SetActive(true);
            }
        }


        if(CatNumber == 1 && InKitchen == true) 
        {
            CatButton.SetActive(true);
        }
        else
        {
            CatButton.SetActive(false);
        }


        if (CatNumber == 2 && InLivingRoom == true) 
        {
            CatButton.SetActive(true);
        }
       


        if (CatNumber == 3 && InbedRoom == true)
        {
            CatButton.SetActive(true);
        }
        
        


        if (WinTimer >= 300)
        {
            YouWin = true;
        }

        if(YouWin == true)
        {
            SceneManager.LoadScene(1);
        }

        if(YouDied == true)
        {
            SceneManager.LoadScene(2);
        }
        //losing conditions down here 

    }
        public void Fridge()
        {
            Hunger += 3;
            Debug.Log("Player Ate from Fridge");
        }
        public void Bomb()
        {
            IsFridged = true;
            BombFridgeButton.SetActive(true);
            BombButton.SetActive(false);
            FridgeButton.SetActive(false);
            Debug.Log("FRidgebomb should be here now");
            Fridgey.Play();
        }
        public void BombFridge()
        {
            IsFridged = false;
            Debug.Log("test for bomb fridge destruction");
            BombButton.SetActive(true);
            FridgeButton.SetActive(true);
            BombFridgeButton.SetActive(false);
            Fridgey.Play();
        }

        public void FirePlace()
        {
            if (Water >= 1)
            {
                UsedWater = true;
                Splash.Play();
            }
        }

        public void SwitchUp() // the button u can press when the ligths are ON
        {
            LightsOn = false;
            LightSwitchONButton.SetActive(false);
            LightswitchOFFButton.SetActive(true);
            BedroomBGLight.SetActive(false);
            BedroomBGDark.SetActive(true);
            Fridgey.Play();
        }

        public void SwitchDown()
        {
            if(Energy >= 0.01)
            {
                LightsOn = true;
                LightSwitchONButton.SetActive(true);
                LightswitchOFFButton.SetActive(false);
                BedroomBGLight.SetActive(true);
                BedroomBGDark.SetActive(false);
            Fridgey.Play();
            }
            
        }

        public void Bedroom()
        {
            if(LightsOn == true)
            {
                LightSwitchONButton.SetActive(true);
                LightswitchOFFButton.SetActive(false);
                BedroomBGLight.SetActive(true);
                BedroomBGDark.SetActive(false);
                
            }
            else
            {
                LightSwitchONButton.SetActive(false);
                LightswitchOFFButton.SetActive(true);
                BedroomBGLight.SetActive(false);
                BedroomBGDark.SetActive(true);

                 
            }

            InbedRoom = true;
            InLivingRoom = false;
            InKitchen = false;
            InShop = false;

            FridgeButton.SetActive(false);
            BombFridgeButton.SetActive(false);
            BombButton.SetActive(false);
            KitchenBG.SetActive(false);
            LivingRoomBG.SetActive(false);
            FirePlaceButton.SetActive(false);
            ShopBG.SetActive(false);
            WaterButton.SetActive(false);
            EnergyButton.SetActive(false);
            fivetext.SetActive(false);
            eighttext.SetActive(false);
            Door.Play();
        }

        public void LivingRoom()
        {
            LivingRoomBG.SetActive(true);
            FirePlaceButton.SetActive(true);
            
            
            InbedRoom = false;
            
            FridgeButton.SetActive(false);
            BombFridgeButton.SetActive(false);
            BombButton.SetActive(false);
            KitchenBG.SetActive(false);
            LightSwitchONButton.SetActive(false);
            LightswitchOFFButton.SetActive(false);
            BedroomBGLight.SetActive(false);
            BedroomBGDark.SetActive(false);
            ShopBG.SetActive(false);
            WaterButton.SetActive(false);
            EnergyButton.SetActive(false);
            fivetext.SetActive(false);
            eighttext.SetActive(false);

            InLivingRoom = true;
            InbedRoom = false;
            InKitchen = false;
            InShop = false;
            Door.Play();
        }

        public void Kitchen()
        {
            KitchenBG.SetActive(true);
            if (IsFridged == true)
            {
               BombFridgeButton.SetActive(true);
               
            }

            else
            {
               BombButton.SetActive(true);
               FridgeButton.SetActive(true);

               
            }

           InLivingRoom = false;
           InKitchen = true;
           InbedRoom = false;
           InShop = false;

            LivingRoomBG.SetActive(false);
            FirePlaceButton.SetActive(false);
            LightSwitchONButton.SetActive(false);
            LightswitchOFFButton.SetActive(false);
            BedroomBGLight.SetActive(false);
            BedroomBGDark.SetActive(false);
            ShopBG.SetActive(false);
            WaterButton.SetActive(false);
            EnergyButton.SetActive(false);
            fivetext.SetActive(false);
            eighttext.SetActive(false);
            Door.Play();
        }

    public void Cat ()
    {
        CatTemper += 6;
        IsCatAngry = false;
        HasMoved = false;
        Money += 1;
        Meow.Play();
    }

    public void Shop ()
    {
        ShopBG.SetActive(true);
        WaterButton.SetActive(true);
        EnergyButton.SetActive(true);
        fivetext.SetActive(true);
        eighttext.SetActive(true);
        
        InShop = true;
        InKitchen = false;
        InbedRoom = false;
        InLivingRoom = false;

        FridgeButton.SetActive(false);
        BombFridgeButton.SetActive(false);
        BombButton.SetActive(false);
        KitchenBG.SetActive(false);
        LivingRoomBG.SetActive(false);
        FirePlaceButton.SetActive(false);
        LightSwitchONButton.SetActive(false);
        LightswitchOFFButton.SetActive(false);
        BedroomBGLight.SetActive(false);
        BedroomBGDark.SetActive(false);

        Door.Play();
    }

    public void WateraButton ()
    {
        if(Money >= 5)
        {
            Money -= 5;
            Water += 1;
            ChaChing.Play();
        }
    }

    public void EnergyaButton ()
    {
        if(Money >= 8)
        {
            Money -= 8;
            Energy += 1;
            ChaChing.Play();
        }
    }

        
 
}
