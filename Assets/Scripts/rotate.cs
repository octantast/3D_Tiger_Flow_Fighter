using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, 400f), Time.deltaTime * 2f);
    }
}
