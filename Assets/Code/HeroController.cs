using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public GameObject hero;
    public Rigidbody hero_rigid;
    public Vector3 dir = new Vector3();
    private Vector3 spawn_point = new Vector3();
    public int score = 0;
    public float lateral_speed = 5.0f;
    public float forward_speed = 6.0f;
    public float force = 5.0f;
    public bool is_grounded;

    public List<GameObject> MyCoins;

    // Start is called before the first frame update
    void Start() {
        hero_rigid = GetComponent<Rigidbody>();
        spawn_point = hero.transform.position;

        MyCoins = new List<GameObject>();
    }

    public void Run() {
        hero.transform.position += Vector3.forward * forward_speed * Time.deltaTime;
    }

    public void ResetPosition() {
        hero.transform.position = spawn_point;
    }

    public void AddScore(int score_given)
    {
        score += score_given;
    }

    // Update is called once per frame
    void Update() {
        // Raycast
        is_grounded = Physics.Raycast(hero.transform.position, Vector3.down, 1.0f);

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
        if(Input.GetKeyDown(KeyCode.Space) && is_grounded){
            hero_rigid.AddForce(hero.transform.up * force, ForceMode.Impulse);
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("Enter");
        }

        Run();
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
