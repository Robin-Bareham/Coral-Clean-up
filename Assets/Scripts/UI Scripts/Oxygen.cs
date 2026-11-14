using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Oxygen : MonoBehaviour
{
    [SerializeField] Slider time_slider;
    [SerializeField] GameObject warning_text;
    [SerializeField] int time_limit = 120;
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
            //remaining_time = remaining_time - Time.deltaTime;
            //remaining_proportion = remaining_time / time_limit;
            //time_slider.value = remaining_proportion;

            setOxygen(remaining_time - Time.deltaTime);

            if (remaining_time <= 0.1f)
            {
                //return player to base, end day
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
