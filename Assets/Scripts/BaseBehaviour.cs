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

    GameManager gameManager;

    private GameObject sleep_UI;
    private GameObject log_UI;
    private GameObject coral_UI;
    private GameObject buttons_ui;
    private GameObject returnBaseBtn;

    private bool coral_tank_status = false; //False is empty, true is full (can only hold one coral)
    private int coral_placed_day = 0; //contains the day the coral was placed in the tank, to know when it should grow.



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //Gets Children
        sleep_UI = this.gameObject.transform.Find("SleepUI").gameObject;
        log_UI = this.gameObject.transform.Find("LogUI").gameObject;
        coral_UI = this.gameObject.transform.Find("CoralTankUI").gameObject;
        buttons_ui = this.gameObject.transform.Find("Buttons").gameObject;
        returnBaseBtn = this.gameObject.transform.Find("BaseBtn").gameObject;

        activateBaseExplore();
    }

    // Update is called once per frame
    void Update()
    {
        //Would be used for managing what camera location it's looking at.
    }

    public void activateSleep()
    {
        //What happens when you go to sleep in the base.
        sleep_UI.SetActive(true);
        log_UI.SetActive(false);
        coral_UI.SetActive(false);
        buttons_ui.SetActive(false);
        returnBaseBtn.SetActive(true);
        
        //Call game manager to set world for the next day
        gameManager.nextDay();
    }
    public void activateLog()
    {
        //What happens when you interact with the log book in the base

        //If the player hasn't taken a photo, tell them, nothings here! go find fish and take a photo.
        if (gameManager.getPhotoCount() > 0)
        {
            log_UI.transform.Find("NoPhotoLog").gameObject.SetActive(true);
        }
        else 
        {
            log_UI.transform.Find("NoPhotoLog").gameObject.SetActive(false);
        }

        //Change UI status
        sleep_UI.SetActive(false);
        log_UI.SetActive(true);
        coral_UI.SetActive(false);
        buttons_ui.SetActive(false);
        returnBaseBtn.SetActive(true);
    }

    public void activateCoral()
    {
        //If we collected coral and the tank is empty
        if(gameManager.getCoralCount() > 0 && !coral_tank_status)
        {
            coral_UI.GetComponent<RawImage>().texture = tank_dead_texture;
            coral_placed_day = gameManager.getDay();
        }
        //If the coral is grown at least a day after planting it
        else if (coral_tank_status && gameManager.getDay() > coral_placed_day)
        {
            coral_UI.GetComponent<RawImage>().texture = tank_alive_texture;
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
    }

    public void activateOutside()
    {
        //What happens when you go back outside of the base
        this.gameObject.SetActive(false); //Hides BaseUI
        //Send camera back to player's camera.
        //Send something to gameManager to allow movement of player character again
    }

    public void activateBaseExplore()
    {
        //Going to the state of being able to choose what base action you want to do.
        sleep_UI.SetActive(false);
        log_UI.SetActive(false);
        coral_UI.SetActive(false);
        buttons_ui.SetActive(true);
        returnBaseBtn.SetActive(false);
    }
}
