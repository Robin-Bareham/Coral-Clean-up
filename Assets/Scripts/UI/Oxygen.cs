using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Oxygen : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] Slider time_slider;
    [SerializeField] GameObject warning_text;

    [SerializeField] GameObject spawn;
    [SerializeField] GameObject player;

    [Tooltip("Entered in seconds")] //creates tooltip for when the variable is hovered over in-editor
    public int time_limit = 120;
    public float remaining_time = 0;
    float remaining_proportion = 1;

    public bool underwater = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remaining_time = time_limit;
    }

    // Update is called once per frame
    void Update()
    {
        if (underwater)
        {
           setOxygen(remaining_time - Time.deltaTime);

            if (remaining_time <= 0.1f)
            {
                //return player to base, end day
                manager.BaseUI.GetComponent<Base>().activateSleep();
                player.transform.position = spawn.transform.position;
                manager.setEnterBase(true);
                warning_text.SetActive(false);
            }
            else if (remaining_proportion <= 0.3f)
            {
                //show low oxygen warning
                warning_text.SetActive(true);
            }
        }
    }

    public void setOxygen(float ox_level)
    {
        remaining_time = ox_level;
        remaining_proportion = remaining_time / time_limit;
        time_slider.value = remaining_proportion;
    }
}
