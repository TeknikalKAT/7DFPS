using UnityEngine;

public class LIghtControl : MonoBehaviour
{
    [SerializeField] float minIntensity, maxIntensity;
    [SerializeField] float minChangeTime, maxChangeTime;
    Light light;
    float _changeTime;
    float _intensity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponent<Light>();
        _changeTime = RandomTime();
        _intensity = RandomIntensity();
    }

    // Update is called once per frame
    void Update()
    {
        if(_changeTime > 0)
        {
            _changeTime -= Time.deltaTime;
            light.intensity = _intensity;
        }
        else
        {
            _changeTime = RandomTime();
            _intensity = RandomIntensity();
        }
    }

    float RandomIntensity()
    {
        return Random.Range(minIntensity, maxIntensity);
    }

    float RandomTime()
    {
        return Random.Range(minChangeTime, maxChangeTime);
    }
}
