using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public GameObject plane;
    public Vector3 dir = new Vector3();
    private Vector3 spawn_point_plane = new Vector3();
    public float lateral_speed = 5.0f;
    public float forward_speed = 6.0f;

    void Start() {
        spawn_point_plane = plane.transform.position;
    }

    public void Run() {
        plane.transform.position += Vector3.forward * forward_speed * Time.deltaTime;
    }

    public void ResetPosition() {
        plane.transform.position = spawn_point_plane;
    }

    void Update() {
        // if(Input.GetKey(KeyCode.W)){Debug.Log("W");}
        if(Input.GetKey(KeyCode.A)){
            plane.transform.position += Vector3.left * lateral_speed * Time.deltaTime;
        }
        // if(Input.GetKey(KeyCode.S)){Debug.Log("S");}
        if(Input.GetKey(KeyCode.D)){
            plane.transform.position += Vector3.right * lateral_speed * Time.deltaTime;
        }
        // if(Input.GetKeyDown(KeyCode.UpArrow)){Debug.Log("UpArrow");}
        if(Input.GetKey(KeyCode.LeftArrow)){
            plane.transform.position += Vector3.left * lateral_speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            plane.transform.position += Vector3.right * lateral_speed * Time.deltaTime;
        }
        // // if(Input.GetKeyDown(KeyCode.DownArrow)){Debug.Log("DownArrow");}
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     Debug.Log("Space");
        // }
        // if(Input.GetKeyDown(KeyCode.Return)){
        //     Debug.Log("Enter");
        // }
        Run();
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Hero"){
            HeroController gc = other.GetComponent<HeroController>();
            gc.ResetPosition();
            ResetPosition();
        }
    }
}
