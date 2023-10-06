using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public GameObject hero;
    public Rigidbody hero_rigid;
    public Vector3 dir = new Vector3();
    private Vector3 spawn_point = new Vector3();
    private int jump_counter = 0;
    public float lateral_speed = 5.0f;
    public float forward_speed = 6.0f;
    public float force = 5.0f;
    public bool is_jumping = false;
    // Start is called before the first frame update
    void Start() {
        hero_rigid = GetComponent<Rigidbody>();
        spawn_point = hero.transform.position;
    }

    public void Run() {
        hero.transform.position += Vector3.forward * forward_speed * Time.deltaTime;
    }

    public void ResetPosition() {
        hero.transform.position = spawn_point;
    }

    // Update is called once per frame
    void Update() {
        // if(Input.GetKey(KeyCode.W)){Debug.Log("W");}
        if(Input.GetKey(KeyCode.A)){
            hero.transform.position += Vector3.left * lateral_speed * Time.deltaTime;
        }
        // if(Input.GetKey(KeyCode.S)){Debug.Log("S");}
        if(Input.GetKey(KeyCode.D)){
            hero.transform.position += Vector3.right * lateral_speed * Time.deltaTime;
        }
        // if(Input.GetKeyDown(KeyCode.UpArrow)){Debug.Log("UpArrow");}
        if(Input.GetKey(KeyCode.LeftArrow)){
            hero.transform.position += Vector3.left * lateral_speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            hero.transform.position += Vector3.right * lateral_speed * Time.deltaTime;
        }
        // if(Input.GetKeyDown(KeyCode.DownArrow)){Debug.Log("DownArrow");}
        if(Input.GetKeyDown(KeyCode.Space) && !is_jumping){
            hero_rigid.AddForce(hero.transform.up * force, ForceMode.Impulse);
            is_jumping = true;
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("Enter");
        }

        if(is_jumping){
            jump_counter++;
            if(jump_counter >= 200){
                is_jumping = false;
                jump_counter = 0;
            }
        }
        Run();
    }
}
