using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField]
    private Texture tank_empty_texture;
    [SerializeField]
    private Texture tank_dead_texture;
    [SerializeField]
    private Texture tank_alive_texture;
    [SerializeField] GameManager manager;

    private GameObject sleep_UI;
    private GameObject log_UI;
    private GameObject coral_UI;
    private GameObject buttons_ui;
    private GameObject returnBaseBtn;
    private GameObject no_log_UI;

    private bool coral_tank_status = false; //False is empty, true is full (can only hold one coral)
    private int coral_placed_day = 0; //contains the day the coral was placed in the tank, to know when it should grow.
    private string previous_screen = "SleepBTN";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //Gets Children
        sleep_UI = this.gameObject.transform.Find("SleepUI").gameObject;
        log_UI = this.gameObject.transform.Find("LogUI").gameObject;
        no_log_UI = this.gameObject.transform.Find("NoLogUI").gameObject;
        coral_UI = this.gameObject.transform.Find("CoralTankUI").gameObject;
        buttons_ui = this.gameObject.transform.Find("Buttons").gameObject;
        returnBaseBtn = this.gameObject.transform.Find("BaseBtn").gameObject;

        //activateBaseExplore();
        activateOutside();
    }

    // Update is called once per frame
    void Update()
    {
        //Would be used for managing what camera location it's looking at.
        if (manager.getEnterBase())
        {
            Debug.Log("ACTIVATED IN BASE BEHAVIOUR");
            activateBaseExplore();
            manager.setEnterBase(false);
        }
    }

    public void activateSleep()
    {
        changeSelectedBtn("return");
        //What happens when you go to sleep in the base.
        sleep_UI.SetActive(true);
        log_UI.SetActive(false);
        coral_UI.SetActive(false);
        buttons_ui.SetActive(false);
        returnBaseBtn.SetActive(true);

        //Call game manager to set world for the next day
        manager.nextDay();
        previous_screen = "SleepBTN";
    }
    public void activateLog()
    {
        changeSelectedBtn("return");
        //What happens when you interact with the log book in the bas

        //Change UI status
        sleep_UI.SetActive(false);
        coral_UI.SetActive(false);
        buttons_ui.SetActive(false);
        returnBaseBtn.SetActive(true);
        //If a photo has been taken
        if(manager.getPhotoCount() > 0)
        {
            log_UI.SetActive(true);
        }
        else
        {
            no_log_UI.SetActive(true); 
        }
        previous_screen = "LogBTN";
    }

    public void activateCoral()
    {
        changeSelectedBtn("return");
        //If we collected coral and the tank is empty
        if (manager.getCoralCount() > 0 && !coral_tank_status)
        {
            coral_UI.GetComponent<RawImage>().texture = tank_dead_texture;
            coral_placed_day = manager.getDay();
            coral_tank_status = true;
        }
        //If there's something in the tanks
        else if (coral_tank_status)
        {
            if (manager.getDay() > coral_placed_day)
            {
                coral_UI.GetComponent<RawImage>().texture = tank_alive_texture;
            }
            else
            {
                coral_UI.GetComponent<RawImage>().texture = tank_dead_texture;
            }
            
        }
        else
        {
            coral_UI.GetComponent<RawImage>().texture = tank_empty_texture;
        }
        //What happens when you interact with the coral tank in the base
        sleep_UI.SetActive(false);
        log_UI.SetActive(false);
        coral_UI.SetActive(true);
        buttons_ui.SetActive(false);
        returnBaseBtn.SetActive(true);
        previous_screen = "CoralBTN";
    }

    public void activateOutside()
    {
        //What happens when you go back outside of the base
        this.gameObject.SetActive(false); //Hides BaseUI
                                          //Send camera back to player's camera.
                                          //Send something to gameManager to allow movement of player character again
        manager.setCanMove(true);
    }

    public void activateBaseExplore()
    {
        //Going to the state of being able to choose what base action you want to do.
        changeSelectedBtn(previous_screen);
        sleep_UI.SetActive(false);
        log_UI.SetActive(false);
        no_log_UI.SetActive(false);
        coral_UI.SetActive(false);
        buttons_ui.SetActive(true);
        returnBaseBtn.SetActive(false);
        manager.setCanMove(false);
    }

    public void changeSelectedBtn(string name)
    {
        //TRUE means that we're in the main base selecting our options.
        if (name == "return")
        {
            returnBaseBtn.GetComponent<Button>().Select();
            
        }
        else 
        {
            buttons_ui.transform.Find(name).gameObject.GetComponent<Button>().Select();
        }
    }
}
