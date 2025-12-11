using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float xRot, yRot, zRot;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRot, yRot, zRot);
    }
}
