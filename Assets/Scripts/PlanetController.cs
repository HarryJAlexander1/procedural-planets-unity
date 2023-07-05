using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public float rotationSpeed; // Adjust the speed of rotation

    private void Update()
    {
        // Rotate the object gradually over time
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

}
