using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public bool Cangrab = true;
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy" && Cangrab==true)
        {
          
            this.transform.root.GetComponent<BrawlerManager>().PerformThrow(other);
            Cangrab = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
