using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public GameObject coin;
    // private int score_given = 1;
    public float speed_rot = 80.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotation_speed = speed_rot * Time.deltaTime;

        coin.transform.Rotate(Vector3.forward, rotation_speed);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Hero")
        {
            HeroController gc = other.GetComponent<HeroController>();

            if(gc != null)
            {
                gc.AddScore(score_given);
                Debug.Log("Player Score: " + gc.score);
                coin.SetActive(false);
            }
        }
    }*/
}