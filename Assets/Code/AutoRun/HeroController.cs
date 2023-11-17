using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ParticleSystem))]
public class HeroController : MonoBehaviour
{
    public GameObject hero;

    public Rigidbody hero_rigid;

    public Vector3 dir = new Vector3();

    private Animator animation;

    //public ParticleSystem particle_system;

    private Vector3 spawn_point = new Vector3();

    public int score = 0;

    public float lateral_speed = 5.0f;
    public float forward_speed = 10.0f;
    private float rotation_speed = 40.0f;
    public float force = 7.0f;

    public bool is_grounded;
    public bool is_jumping = false;
    public bool rot_left = false;
    public bool change_scene = false;

    public List<GameObject> MyCoins;

    void Start() {
        animation = GetComponent<Animator>();
        hero_rigid = GetComponent<Rigidbody>();
        //particle_system = GetComponent<ParticleSystem>();
        spawn_point = hero.transform.position;

        MyCoins = new List<GameObject>();
    }

    public void Run() {
        hero.transform.position += Vector3.forward * forward_speed * Time.deltaTime;
    }

    public void ResetPosition() {

        if (change_scene) {
            SceneManager.LoadScene(0);
        }

        hero.transform.position = spawn_point;

        foreach(GameObject coin in MyCoins)
        {
            coin.SetActive(true);
        }
        score = 0;
    }

    public void AddScore(int score_given)
    {
        score += score_given;
    }

    void Update() {
        // Raycast
        is_grounded = Physics.Raycast(hero.transform.position, Vector3.down, 0.1f);

        if(Input.GetKey(KeyCode.A)){
            hero.transform.position += Vector3.left * lateral_speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D)){
            hero.transform.position += Vector3.right * lateral_speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            hero.transform.position += Vector3.left * lateral_speed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            hero.transform.position += Vector3.right * lateral_speed * Time.deltaTime;
        }
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
            forward_speed = 20.0f;
            //particle_system.Stop(false);
            //particle_system.Play(true);
            animation.SetBool("isJumping_", true);
        }else{
            forward_speed = 10.0f;
            //particle_system.Play(false);
            //particle_system.Stop(true);
            animation.SetBool("isJumping_", false);
        }

        // ROTATION
        if (rot_left)
        {
            transform.Rotate(Vector3.down * rotation_speed * Time.deltaTime);
            Debug.Log("My rotation y: " + transform.rotation.y * 100);
            if (transform.rotation.y * 100 <= -90.0f) {
                Debug.Log("(FINAL) My rotation y: " + transform.rotation.y *100);
                rot_left = false;
            }
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

        if(other.gameObject.name == "ChgScenePlt") {
            change_scene = true;
        }

        if(score >= 18) {
            SceneManager.LoadScene(1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DeathPlane dp = FindObjectOfType<DeathPlane>();
        if(collision.gameObject.tag == "Barrier")
        {
            ProceduralGen pg = FindObjectOfType<ProceduralGen>();
            ResetPosition();
            dp.ResetPosition();
            pg.ResetPosition();
        }
    }
}
