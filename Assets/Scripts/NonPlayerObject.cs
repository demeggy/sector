using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerObject : MonoBehaviour
{
    public enum Type
    {
        Enemy,
        Scenery
    }
    public bool Destructable;
    public Type type;
    private Rigidbody2D rb;
    public float Hp;
    public string Name;
    public float Speed;
    private float Rotate;
    public GameObject ExplosionFx;
    public GameObject Waypoint;
    private GameObject Target;

    private void Start()
    {
        Rotate = Random.Range(0.25f, 0.75f);

        if (type == Type.Enemy)
        {
            //instantiate first waypoint within screen
            NewWaypoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (type == Type.Scenery)
        {
            //spin the object
            transform.Rotate(0, 0, Rotate);
        }

        //Enemy code
        else
        {
            MoveTowardsWaypoint();
        }

        //Destroy if hp less than zero
        if (Hp <= 0)
        {
            Instantiate(ExplosionFx, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    //Move the enemy towards the waypoint
    private void MoveTowardsWaypoint()
    {
        //float step = Speed * Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, step);

        float y = 1;
        float x = Vector2.Angle(transform.position,Target.transform.position);

        transform.Rotate(0, 0, x);
        Vector3 force = transform.up * (Mathf.Atan2(y, 0) * Mathf.Rad2Deg);

        rb.AddForce(force * Speed, ForceMode2D.Force);

    }

    private void NewWaypoint()
    {
        //instantiate first waypoint within screen
        float x = Random.Range(-25, 25);
        float y = Random.Range(-14, 14);

        Target = Instantiate(Waypoint, new Vector2(x, y), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Destructable)
        {
            if (collision.gameObject.tag == "Projectile")
            {
                Debug.Log("ouch");
                Hp -= 1;
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "EnemyWaypoint" && type == Type.Enemy)
            {
                Destroy(collision.gameObject);
                NewWaypoint();
            }
        }        
    }

}
