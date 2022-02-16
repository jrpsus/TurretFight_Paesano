using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    float lifespan = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan += Time.deltaTime;
        if (lifespan >= 0.7f)
        {
            Destroy(gameObject);
            DestroyImmediate(gameObject, true);
        }
    }
}
