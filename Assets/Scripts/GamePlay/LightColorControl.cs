using UnityEngine;

public class LightColorControl : MonoBehaviour
{
    [SerializeField] float changeTime = 1;


    [Header("Switching Colors")]
    [SerializeField] bool differentColors = true;
    [SerializeField] Color[] colors;


    [Header("Switching Colors")]
    [SerializeField] bool intensitySwitch = false;
    [SerializeField] float intensity = 0.05f;

    Light lightCompo;
    int colorIndex;
    float _changeTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _changeTime = changeTime;
        lightCompo = GetComponent<Light>();
        colorIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_changeTime > 0)
        {
            _changeTime -= Time.deltaTime;  
        }
        else
        {
            if (differentColors)
            {
                if (colorIndex + 1 != colors.Length)
                    colorIndex += 1;
                else
                    colorIndex = 0;
                ColorChange();
            }
            if(intensitySwitch)
            {
                IntensitySwitch();
            } 
            _changeTime = changeTime;
        }
    }

    void ColorChange()
    {
        lightCompo.color = colors[colorIndex];
    }

    void IntensitySwitch()
    {
        if (lightCompo.intensity == intensity)
            lightCompo.intensity = 0;
        else
            lightCompo.intensity = intensity;
    }
}
