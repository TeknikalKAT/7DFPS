using UnityEngine;

public class SizeAnimation : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] AnimationCurve scaleCurve;
    public float duration = 1f;

    [Header("Scale Settings")]
    public float baseScale = 1f;
    public float scaleFactor = 1f;

    float timer = 0f;
    Vector3 originalScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //looping time over curve
        float t = (timer % duration) / duration;

        //curve value at a specific time 't'
        float curveValue = scaleCurve.Evaluate(t);


        //applying scale
        transform.localScale = originalScale * (baseScale + curveValue * scaleFactor);
    }
}
