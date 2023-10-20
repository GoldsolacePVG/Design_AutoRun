using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralBarrier : MonoBehaviour
{
    public GameObject barrier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barrier.transform.Translate(Vector3.right * Time.deltaTime * 3);
        
        if(barrier.transform.position.x == 3.14f)
        {
            barrier.transform.Translate(Vector3.left * Time.deltaTime * 3);
        }else if (barrier.transform.position.x <= -3.26f)
        {
        }
    }
}
