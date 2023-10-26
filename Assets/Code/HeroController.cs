using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public GameObject hero;
    public Rigidbody hero_rigid;
    public Vector3 dir = new Vector3();
    public int score = 0;
    private Animator animation;
    private Vector3 spawn_point = new Vector3();
    public float lateral_speed = 5.0f;
    public float forward_speed = 6.0f;
    public float rotation_speed = 30.0f;
    public float force = 7.0f;
    public bool is_grounded;
    public bool is_jumping = false;

    public List<GameObject> MyCoins;

    // Start is called before the first frame update
    void Start() {
        animation = GetComponent<Animator>();
        hero_rigid = GetComponent<Rigidbody>();
        spawn_point = hero.transform.position;

        MyCoins = new List<GameObject>();
    }

    public void Run() {
        hero.transform.position += Vector3.forward * forward_speed * Time.deltaTime;
    }

    public void ResetPosition() {
        hero.transform.position = spawn_point;

        foreach(GameObject coin in MyCoins)
        {
            coin.SetActive(true);
        }
    }

    public void AddScore(int score_given)
    {
        score += score_given;
    }

    // Update is called once per frame
    void Update() {
        // Raycast
        is_grounded = Physics.Raycast(hero.transform.position, Vector3.down, 0.1f);

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
            is_jumping = true;
            animation.SetBool("isJumping_", true);
            hero_rigid.AddForce(hero.transform.up * force, ForceMode.Impulse);
        }else if (is_grounded && animation.GetBool("isJumping_")) {
            animation.SetBool("isJumping_", false);
            is_jumping = false;
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("Enter");
        }

        if (!is_grounded){
            animation.SetBool("isJumping_", true);
        }else{
            animation.SetBool("isJumping_", false);
        }

        Run();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Coin")
        {
            GameObject coin = other.gameObject;
            MyCoins.Add(coin);
            coin.SetActive(false);
            AddScore(1);
            Debug.Log("Player Score: " + score);
        }

        if(other.gameObject.name == "SpeedUp")
        {
            forward_speed = 15.0f;
        }

        if(other.gameObject.name == "ChangeDir")
        {
            hero.transform.Rotate(0.0f, - 90.252f, 0.0f, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "SpecialCube")
        {
            forward_speed = 6.0f;
        }
    }
}
