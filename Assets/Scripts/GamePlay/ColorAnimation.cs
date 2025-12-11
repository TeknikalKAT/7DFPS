using UnityEngine;
using UnityEngine.UI;

public class ColorAnimation : MonoBehaviour
{

    [Header("Animation")]
    [SerializeField] AnimationCurve scaleCurve;
    public float duration = 1f;


    [Header("Color Settings")]
    public Color baseColor = Color.white;
    public Color pulseColor = Color.red;

    float timer = 0f;
    Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //looping time over curve
        float t = (timer % duration) / duration;

        //curve value at a specific time 't'
        float curveValue = scaleCurve.Evaluate(t);


        Color current = Color.Lerp(baseColor, pulseColor, curveValue);
        image.color = current;
    }
}
