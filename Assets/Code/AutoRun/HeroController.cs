using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ParticleSystem))]
public class HeroController : MonoBehaviour
{
    public GameObject hero, particles, go;
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

    public bool is_grounded = true;
    public bool is_jumping = false;
    public bool change_scene = false;
    public bool can_spawn_part = true;

    public List<GameObject> MyCoins;

    void Start() {
        animation = GetComponent<Animator>();
        hero_rigid = GetComponent<Rigidbody>();
        spawn_point = hero.transform.position;
        //particle_system.Stop();

        MyCoins = new List<GameObject>();
    }

    public void Run() {
        hero.transform.position += Vector3.forward * forward_speed * Time.deltaTime;
    }

    public void ResetPosition() {

        if (change_scene) {
            SceneManager.LoadScene(1);
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
        Vector3 aux_pos;
        aux_pos.x = this.transform.position.x;
        aux_pos.y = this.transform.position.y + 4.144f;
        aux_pos.z = this.transform.position.z - 5.04f;
        // Raycast
        is_grounded = Physics.Raycast(hero.transform.position, Vector3.down, 0.1f);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {hero.transform.position += Vector3.left * lateral_speed * Time.deltaTime;}
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {hero.transform.position += Vector3.right * lateral_speed * Time.deltaTime;}
        if(Input.GetKeyDown(KeyCode.Space) && is_grounded){
            is_jumping = true;
            animation.SetBool("isJumping_", true);
            hero_rigid.AddForce(hero.transform.up * force, ForceMode.Impulse);
        }else if (is_grounded && animation.GetBool("isJumping_")) {
            animation.SetBool("isJumping_", false);
            is_jumping = false;
        }

        if (!is_grounded){
            forward_speed = 20.0f;
            animation.SetBool("isJumping_", true);
        }else{
            forward_speed = 10.0f;
            animation.SetBool("isJumping_", false);
        }

        if (!is_grounded) {
            if (can_spawn_part)
            {
                go = Instantiate<GameObject>(particles, aux_pos, Quaternion.identity);
                go.transform.SetParent(this.transform, true);
                can_spawn_part = false;
            }
        }
        if (is_grounded)
        {
            Destroy(go);
            can_spawn_part = true;
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
        }

        if(other.gameObject.name == "ChgScenePlt") {
            change_scene = true;
        }

        if(score >= 18) {
            SceneManager.LoadScene(2);
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
