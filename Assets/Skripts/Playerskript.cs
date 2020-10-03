using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator anim;
    Camera cam;
    [SerializeField] public GameObject statusScreen;
    [SerializeField] float viewField = 8f;

    bool moving = false;


    // Variablen für Bewegung
    #region
    [SerializeField] float speed = 300f;
    float vertical = 0f;
    float horizontal = 0f;
    public Vector2 movingDirection = Vector2.zero;
    public bool movementLocked = false;
    Vector3 camPos = new Vector3(0, 0, -10);
        #endregion

    
    // Methoden für Animationen
    #region
    void SetFalse()
    {
        StandStill();
        anim.SetBool("attackLeft", false);
        anim.SetBool("attackRight", false);
        anim.SetBool("magicLeft", false);
        anim.SetBool("magicRight", false);
        vertical = 0f;
        horizontal = 0f;
        movingDirection = Vector2.zero;
    }

    public void PlayerDeath()
    {
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        anim.SetBool("dead", true);

        yield return new WaitForSeconds(0.7f);
    }

    void SetIdlesFalse()
    {
        anim.SetBool("idle", false);
        anim.SetBool("idleLeft", false);
        anim.SetBool("idleRight", false);
        anim.SetBool("idleUp", false);
    }

    public void StandStill()
    {
        moving = false;

        if (vertical > 0)
        {
            anim.SetBool("idleUp", true);
            vertical = 0f;
        }
        else if (vertical < 0)
        {
            anim.SetBool("idle", true);
            vertical = 0f;
        }

        if (horizontal > 0)
        {
            anim.SetBool("idleRight", true);
            horizontal = 0f;
        }

        else if (horizontal < 0)
        {
            anim.SetBool("idleLeft", true);
            horizontal = 0f;
        }
    }
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = Camera.main;

        // UI-Elemente holen
        #region
        atkLabel = GameObject.Find("ATK_label").GetComponent<Text>();
        defLabel = GameObject.Find("DEF_label").GetComponent<Text>();
        magLabel = GameObject.Find("MAG_label").GetComponent<Text>();
        plevel = GameObject.Find("Player_Level").GetComponent<Text>();
        pName = GameObject.Find("Player_Name").GetComponent<Text>();
        swordLevel = GameObject.Find("SwordLevelText").GetComponent<Text>();
        spearLevel = GameObject.Find("SpearLevelText").GetComponent<Text>();
        daggerLevel = GameObject.Find("DaggerLevelText").GetComponent<Text>();
        staffLevel = GameObject.Find("StaffLevelText").GetComponent<Text>();
        fireLevel = GameObject.Find("FireLevelText").GetComponent<Text>();
        earthLevel = GameObject.Find("EarthLevelText").GetComponent<Text>();
        windLevel = GameObject.Find("WindLevelText").GetComponent<Text>();
        waterLevel = GameObject.Find("WaterLevelText").GetComponent<Text>();
        lightLevel = GameObject.Find("LightLevelText").GetComponent<Text>();
        darkLevel = GameObject.Find("DarknessLevelText").GetComponent<Text>();
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("idle", true);
               
    }

    void FixedUpdate()
    {
        // Bewegung des Spielers
        if (moving)
            rb.velocity = movingDirection.normalized * speed * Time.deltaTime;
        else
            rb.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        cam.transform.position = transform.position + camPos;
        cam.orthographicSize = viewField;
    }

    // Update is called once per frame
    void Update()
    {
        // Bewegung
        #region
        if(!movementLocked)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");

            if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
                moving = true;
            else
            {
                StandStill();
            }
                

            // Bewegungsanimation
            #region
            

            if (horizontal != 0f && anim.GetBool("walkLocked") == false)
            {
                SetIdlesFalse();
                anim.SetFloat("walkHorizontal", horizontal);
                anim.SetFloat("walkVertical", 0f);
            }
            else if(vertical != 0f && anim.GetBool("walkLocked") == false)
            {
                SetIdlesFalse();
                anim.SetFloat("walkVertical", vertical);
                anim.SetFloat("walkHorizontal", 0f);
            }
            else
            {
                anim.SetFloat("walkHorizontal", 0f);
                anim.SetFloat("walkVertical", 0f);
            }
            #endregion

            

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 600f;
            }

            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 300f;
            }

            movingDirection = new Vector2(horizontal, vertical);
        }

        if(movementLocked)
        {
           SetFalse();
        }
        #endregion

      
    }
}
