using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFOV : MonoBehaviour
{
    public HeroController hc;
    public GameObject jump_pos, initial_pos;
    private float speed = 8.0f;

    void Update() {
        if (!hc.is_grounded) {
            transform.position = Vector3.MoveTowards(transform.position, jump_pos.transform.position, speed * Time.deltaTime);
        }else{
            transform.position = Vector3.MoveTowards(transform.position, initial_pos.transform.position, speed * Time.deltaTime);
        }
    }
}