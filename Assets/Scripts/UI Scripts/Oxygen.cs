using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    public Slider time_slider;
    [SerializeField] int time_limit = 120;
    public float remaining_time = 0;
    float remaining_proportion = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remaining_time = time_limit;
    }

    // Update is called once per frame
    void Update()
    {
        remaining_time = remaining_time - Time.deltaTime;
        remaining_proportion = remaining_time / time_limit;
        time_slider.value = remaining_proportion;

        if(remaining_time <=0.1f)
        {
            //return player to base, end day
        }
        else if (remaining_proportion <= 0.3f)
        {
            //show low oxygen warning
        }
    }

    public void setOxygen(int ox_level)
    {
        remaining_time = ox_level;
        remaining_proportion = remaining_time / time_limit;
        time_slider.value = remaining_proportion;
    }
}
