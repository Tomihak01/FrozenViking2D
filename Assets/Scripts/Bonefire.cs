using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonefire : MonoBehaviour
{
    public float counter;
    public float maxCounter = 5 ;


    void Start()
    {
        
    }

  
    void Update()
    {
      if(counter < maxCounter)
        {
            counter += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
