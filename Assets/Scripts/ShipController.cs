using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //Stats
    public int hp;
    public int shields;
    public float MaxSpeed;
    public enum Input_Type
    {
        Left,
        Right
    }
    public Input_Type inputType;

    //Weaps
    public GameObject Slot1;
    public GameObject Slot2;
    public float Slot1Cool;
    private float Slot1Timer;
    public float Slot2Cool;
    private float Slot2Timer;

    //Movement
    public float Turn;
    public float Thrust;

    //Misc
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //scrolling screen
        EscapeScreen();

        //Player 1 Input
        if (inputType == Input_Type.Left)
        {

            //Movement
            float thrustInput;

            if (Input.GetKey(KeyCode.Joystick1Button6)){ thrustInput = 1; } else { thrustInput = 0; }

            float y = thrustInput;
            float x = Input.GetAxisRaw("Horizontal") * -1;

            transform.Rotate(0, 0, x);
            Vector3 force = transform.up * (Mathf.Atan2(y, 0) * Mathf.Rad2Deg);

            rb.AddForce(force * Thrust, ForceMode2D.Force);

            //Fire Weapon
            if (Input.GetKey(KeyCode.Joystick1Button4))
            {
                if (Slot1Timer <= Time.time)
                {
                    GameObject projectile = Instantiate(Slot1, transform.position, transform.rotation);
                    projectile.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.up * Slot1.GetComponent<Projectile>().Speed);
                    Slot1Timer = Time.time + Slot1Cool;
                }
            }
        }

        //Player 2 Input
        else
        {
            float thrustInput;

            if (Input.GetKey(KeyCode.Joystick1Button7)) { thrustInput = 1; } else { thrustInput = 0; }

            float y = thrustInput;
            float x = Input.GetAxisRaw("RightHorizontal") * -1;

            transform.Rotate(0, 0, x);
            Vector3 force = transform.up * (Mathf.Atan2(y, 0) * Mathf.Rad2Deg);

            rb.AddForce(force * Thrust, ForceMode2D.Force);


            //Fire Weapon
            if (Input.GetKey(KeyCode.Joystick1Button5))
            {
                if (Slot1Timer <= Time.time)
                {
                    GameObject projectile = Instantiate(Slot1, transform.position, transform.rotation);
                    projectile.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.up * Slot1.GetComponent<Projectile>().Speed);
                    Slot1Timer = Time.time + Slot1Cool;
                }
            }
        }
    }
    
    public void EscapeScreen()
    {
        if (transform.position.y < -15.5f)
        {
            transform.position = new Vector2(transform.position.x, 15.5f);
        }
        else if (transform.position.y > 15.5f)
        {
            transform.position = new Vector2(transform.position.x, -15.5f);
        }

        //scrolling screen
        if (transform.position.x < -27.12f)
        {
            transform.position = new Vector2(27.12f, transform.position.y);
        }
        else if (transform.position.x > 27.12f)
        {
            transform.position = new Vector2(-27.12f, transform.position.y);
        }
    }

    public void FireWeapon()
    {
        GameObject projectile = Instantiate(Slot1, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector2.up * Slot1.GetComponent<Projectile>().Speed);
        Slot1Timer = Time.time + Slot1Cool;
    }
}
