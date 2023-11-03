using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralBarrier : MonoBehaviour
{
    public GameObject barrier;
    public float distance = 6.5f;
    public float speed = 3.0f;

    private Vector3 initial_position;
    private Vector3 final_position;
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        initial_position = transform.position;
        final_position = transform.position + Vector3.right * distance;
    }

    // Update is called once per frame
    void Update(){
        if (movingRight){
            transform.position = Vector3.MoveTowards(transform.position, final_position, speed * Time.deltaTime);
            if (transform.position == final_position){
                movingRight = false;
            }
        }else{
            transform.position = Vector3.MoveTowards(transform.position, initial_position, speed * Time.deltaTime);
            if(transform.position == initial_position){
                movingRight = true;
            }
        }
    }
}
