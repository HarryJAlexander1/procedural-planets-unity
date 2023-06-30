using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject planetPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(planetPrefab, new(0,0,0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
