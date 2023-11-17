using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralBarrier : MonoBehaviour
{
    public GameObject barrier, limit_1, limit_2;
    private float distance = 12.0f;
    public float speed = 3.0f;

    private Vector3 initial_position;
    private Vector3 final_position;
    private bool movingRight = true;

    void Start() {
        initial_position = limit_1.transform.position + Vector3.left * 10;
        final_position = limit_2.transform.position + Vector3.right * distance;
    }

    void Update(){
        initial_position = limit_1.transform.position + Vector3.left * 10;
        final_position = limit_2.transform.position + Vector3.right * distance;
        if (movingRight){
            transform.position = Vector3.MoveTowards(transform.position, final_position, speed * Time.deltaTime);
        }else{
            transform.position = Vector3.MoveTowards(transform.position, initial_position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name == "Limit_1") {
            movingRight = true;
        }
        if (other.gameObject.name == "Limit_2") {
            movingRight = false;
        }
    }
}
