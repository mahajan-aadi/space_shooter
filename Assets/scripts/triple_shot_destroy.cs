using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triple_shot_destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int lasers = transform.GetChildCount();
        if ( lasers==0)
        {
            Destroy(this.gameObject);
        }
    }
}
