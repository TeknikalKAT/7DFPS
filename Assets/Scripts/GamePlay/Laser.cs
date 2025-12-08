using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] Transform shootPoint;
    [SerializeField] float forwardDistance;
    [SerializeField] float damageAmount = 0.5f;
    [SerializeField] bool isConstant = false;       //one-shot or constant laser
    [SerializeField] float angleOffset = 0f;
    [SerializeField] float fadeDuration = 2f;       //for the one-shot laser
    [SerializeField] LayerMask layerToHit;

    public bool isShooting;
    LineRenderer laser;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (shootPoint == null)
            shootPoint = this.transform;
        laser = GetComponent<LineRenderer>();
        if (!isConstant)
        {
            LaserOn();
            FadingColor();
        }
        else
            LaserOff();
    }

    // Update is called once per frame
    void Update()
    {
        if(isConstant)
        {
            if (isShooting)
            {
                LaserOn();
            }
            else
                LaserOff();

        }
    }

    public void LaserOn()
    {
        laser.enabled = true;
        Vector3 startPosition = shootPoint.position;
        Vector3 endPosition = shootPoint.forward * forwardDistance;

        laser.SetPosition(0, startPosition);
        laser.SetPosition(1, endPosition);
        Vector3 direction = (endPosition - startPosition).normalized;
        float distance = Vector3.Distance(startPosition, endPosition);

        RaycastHit hit;

        if(Physics.Raycast(startPosition, transform.forward, out hit, distance, layerToHit, QueryTriggerInteraction.Ignore))
        {
            if (isConstant)
                laser.SetPosition(1, hit.point);
            else
                laser.SetPosition(1, new Vector3(shootPoint.position.x, shootPoint.position.y, hit.point.z));
            
            if(hit.transform.GetComponent<Enemy_Health>())
            {
                hit.transform.GetComponent<Enemy_Health>().DamageEnemy(damageAmount);
            }
        }
    }

    public void LaserOff()
    {
        laser.enabled = false;
    }
    
    void FadingColor()
    {
        StartCoroutine(FadingLineRenderer());
    }

    IEnumerator FadingLineRenderer()
    {
        Gradient gradient = laser.colorGradient;

        GradientColorKey[] colorKeys = gradient.colorKeys;
        GradientAlphaKey[] alphaKeys = gradient.alphaKeys;

        float elapsedTime = 0f;

        while(elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            for(int i = 0; i < alphaKeys.Length; i++)
            {
                alphaKeys[i].alpha = alpha;
            }
            gradient.SetKeys(colorKeys, alphaKeys);
            laser.colorGradient = gradient;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
