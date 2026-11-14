using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //For instantiating objects in day reset (basic for Alpha)
    public Transform coral_instance;
    public Transform trash_instance;
    private List<Vector3> coral_instance_pos = new List<Vector3>();
    private List<Vector3> trash_instance_pos = new List<Vector3>();
    // private Vector3[] coral_instance_pos = new[] { new Vector3(52, 121, 42), new Vector3(57, 121, 49) }; //XYZ
    //private Vector3[] trash_instance_pos = new[] { new Vector3( 73, 121, 33 ), new Vector3( 73, 124, 33 ), new Vector3( 77, 121, 29 ), new Vector3( 87, 120, 35 ) };

    private string error_message = "";
    private string error_log_path = Application.dataPath + "/!Dev Info/Error Log.txt";
    private bool error_occured = false;
    private Rect centre_window_pos = new Rect(0,0,0,0);


    private int photo_count = 0;
    private int coral_count = 0;
    private int trash_count = 0;
    private int current_day = 0;
    private bool can_move = true;
    private bool entering_base = false;
    private float just_exited_base = -0.0f;

    public GameObject BaseUI; //I am struggling to get it to just .Find it so it's going here for now

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createTextFile(error_log_path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void error(string message)
    {
        error_message = message;
        Debug.LogError(message);
        File.AppendAllText(error_log_path, error_message + "\n");
        error_occured = true;        
    }

    private void OnGUI()
    {     
        if (error_occured) 
        {
            centre_window_pos = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200);
            GUI.Window(0, centre_window_pos, errorWindow, "Something Fishy Happened");
        }
        
    }

    void errorWindow(int windowID)
    {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.MiddleCenter;

        GUI.Label(new Rect(0, 0, 400, 150), error_message, centeredStyle);
        
        if(GUI.Button( new Rect((centre_window_pos.width/2) - 75, centre_window_pos.height - 60, 70, 50), "Close"))
        {
            error_occured = false;
        }
        else if(GUI.Button(new Rect (centre_window_pos.width/2 + 5, centre_window_pos.height - 60, 70, 50), "Quit"))
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false; 
        }
    }

    void createTextFile(string filepath)
    {
        if(!File.Exists(filepath))
        {
            File.WriteAllText(filepath,"Log Started: " + DateTime.Now.ToString() + "\n\n");
        }
        else
        {
            File.AppendAllText(filepath, "New Log: " + DateTime.Now.ToString() + "\n\n");
        }

    }

    public void fishSeen(string fish_index)
    {
        
    }

    //Function called upon starting day / after going to sleep
    //This function will require expanding on as time goes on and more functions are added to the game.
    public void nextDay()
    {
        //Increase Day Counter.
        current_day += 1;
        //Reset oxygen counter.

        //Replaces the coral and trash (for alpha it just respawns them)
        recreateObjects();
        


    }

    public void recreateObjects()
    {
        for (int i = 0; i < coral_instance_pos.Count; i++)
        {
            var newCoral = Instantiate(coral_instance, transform.position, transform.rotation);
            newCoral.position = coral_instance_pos[i];
        }
        for (int i = 0; i < trash_instance_pos.Count; i++)
        {
            var newTrash = Instantiate(trash_instance, transform.position, transform.rotation);
            newTrash.position = trash_instance_pos[i];
        }
        coral_instance_pos.Clear();
        trash_instance_pos.Clear();
    }

    //GET SET FUNCTIONS
    public int getPhotoCount()  {   return photo_count;  }

    public void setPhotoCount(int new_num) {  photo_count = new_num; }

    public int getCoralCount()   {   return coral_count; }

    public void setCoralCount(int new_num)  {  coral_count = new_num;   }

    public int getTrashCount()  {    return trash_count;  }

    public void setTrashCount(int new_num)  {    trash_count = new_num;  }

    public int getDay() {   return current_day; }

    public bool getCanMove() {  return can_move; }
    
    public void setCanMove(bool value)  
    {
        can_move = value;
        //Gets the frame you exit the base to stop same-frame inputs causing you to get stuck in the base.
        just_exited_base = Time.frameCount;
    }

    public float getJustExitedBase()
    {
        return just_exited_base;
    }

    public bool getEnterBase() {   return entering_base;  }

    public void setEnterBase(bool value)
    {
        entering_base = value;
        if (value) 
        {
            BaseUI.GetComponent<Base>().activateBaseExplore(); //Makes sure correct visual is shown
            BaseUI.SetActive(true); // Then makes the UI Active

            //This is where you'd also be able to 'empty' the trash that's collected.
            trash_count = 0;
            //BaseUI.gameObject.transform.Find("Trash Emptied").gameObject.SetActive(true);
            //Something where the icon would show that the trash is emptied (text saying, trash emptied that only appears when entering)
        }
    }

    //These add the coral / trash's position of the one that's just been collected.
    public void add_to_coral_instance(Vector3 new_location)
    {
        coral_instance_pos.Add(new_location);
    }

    public void add_to_trash_instance(Vector3 new_location)
    {
        trash_instance_pos.Add(new_location);
    }
}
