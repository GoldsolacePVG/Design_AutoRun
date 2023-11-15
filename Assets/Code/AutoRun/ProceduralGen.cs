using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGen : MonoBehaviour
{
    public HeroController hero;
    private Vector3 spawn_point_plane = new Vector3();
    private float lateral_speed = 5.0f;
    private float forward_speed = 10.0f;
    private float offsetZ = 415.0f;

    void Start() {
        spawn_point_plane = this.transform.position;
    }

    public void Run() {
        this.transform.position += Vector3.forward * forward_speed * Time.deltaTime;
    }

    public void ResetPosition() {
        this.transform.position = spawn_point_plane;
    }

    void Update() {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.position += Vector3.left * lateral_speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            this.transform.position += Vector3.right * lateral_speed * Time.deltaTime;
        }

        if(hero.is_grounded) {
            forward_speed = 10.0f;
        }else{
            forward_speed = 20.0f;
        }

        Run();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Platform") {
            other.gameObject.transform.position += new Vector3(0.0f, 0.0f, offsetZ);
        }
    }
}
