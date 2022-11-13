//using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class Lumberjack : MonoBehaviour
{
    public float live = 100;
    public string state = "cortar";

    public string Team;
    private string Enemy;

    public float wood = 0;

    public GameObject Base;
    
    private Animator animationLenador;

    NavMeshAgent LumberjackAgent;
        
    private GameObject View; // = transform.Find("View").gameObject; // vista
    private float viewingDistance = 1.5f; // distancia de vista

    private GameObject targetTree;

    private Vector3 goTo;
    private float y = 0.0f;
    private float delta = 0f;
    private bool packed;
    
    void Start()
    {
        LumberjackAgent = GetComponent<NavMeshAgent>();
        animationLenador = this.GetComponent<Animator>();

        View = transform.Find("View").gameObject; // cubo raycast
        dress();
    }

    // Update is called once per frame
    void Update()
    {
        if ( wood >= 100) 
        {
            packed = true;
            state = "Packed";
        } else {
            packed = false;
        }

        switch (state)
        {
            case "Walk":
                walk(goTo);
                break;
            case "Run":
                walk(Base.transform.position);
                break;
            case "Cut":
                cut(targetTree);                
                break;
            case "Packed":
                walk(Base.transform.position);                
                break;
            case "Search":
                search();
                break;
        }        
        
        animate();        
    }

    void walk( Vector3 destination )
    {
        LumberjackAgent.isStopped = false;
        LumberjackAgent.SetDestination(destination); // ir al punto de destino;        

        /*
        if ( transform.position == destination )
        {
            state = "Search";
        }
        */
         if (!LumberjackAgent.pathPending)
         {
            if (LumberjackAgent.remainingDistance <= LumberjackAgent.stoppingDistance)
            {
                if (!LumberjackAgent.hasPath || LumberjackAgent.velocity.sqrMagnitude == 0f)
                {
                    state = "Search";
                }
            }
 }
    }

    void search()
    {
        var watch = new Ray(View.transform.position, View.transform.forward);

        RaycastHit look;
/*
        for ( int i = 0; i < 360; i++ ) 
        {
            var angle = i + transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0,angle,0);        
        }
        
*/
        //Debug.Log("Time.deltaTime: " + Time.time);

        if ( y < 360 )
        {
            y += Time.time; // / 10;
            transform.rotation = Quaternion.Euler(0,y,0);
            
            delta += y; //transform.eulerAngles.y;

            //Debug.Log("delta if: " + y);
            //Debug.Log("delta: " + transform.eulerAngles.y);

        } else {
            randPos();
            state = "Walk";

            delta = 0;
            y = 0;
        }
    

        if (Physics.Raycast(watch, out look, viewingDistance))
        {
            Debug.DrawRay(watch.origin, watch.direction * look.distance, Color.red);

            if (look.collider.gameObject.tag == "tree")
            {
                goTo = look.collider.gameObject.transform.position;
                state = "Walk";
                //LumberjackAgent.SetDestination(look.point);
            }

            if (look.collider.gameObject.tag == Enemy)
            {
                anyone(look.collider.gameObject.transform.position);
            }
            
        } else
        {
            Debug.DrawRay(watch.origin, watch.direction * (viewingDistance), Color.yellow);
        }
    }

    void randPos() {
        float range = 13.0f;
        goTo = new Vector3(Random.Range(-range, range), 0.15f, Random.Range(-range, range));
    }

    void OnTriggerEnter(Collider other) {

        //LumberjackAgent.isStopped = true;

        if ( other.gameObject.tag == Enemy )
        {
            anyone(other.gameObject.transform.position);
            //LumberjackAgent.isStopped = false;
        }

        if ( other.gameObject.tag == "tree" && packed == false )
        {
            LumberjackAgent.isStopped = true;
            targetTree = other.gameObject;
            state = "Cut";
        }

        if ( other.gameObject == Base )
        {
            other.gameObject.GetComponent<Castle>().deliver_wood(wood);
            wood = 0;

            randPos();
            state = "Walk";
        }        
    }

    void cut( GameObject tree )
    {
        var force = 0.1f;

        if ( tree.GetComponent<Tree>().getWood() > 0 )
        {
            tree.GetComponent<Tree>().cut_down(force);
            wood += force;
        } else {

            if ( packed == false )
            {
                state = "Search";
            } else {
                goTo = Base.transform.position;
                state = "Walk";
            }            
        }
    }

    void animate()
    {
        switch (state)
        {
        case "Walk":
            animationLenador.Play("Walk");
            break;
        case "Run":
            animationLenador.Play("Run");
            break;
        case "Cut":
            animationLenador.Play("Cut");
            break;
        case "Search":
            animationLenador.Play("Look");
            break;
        case "Packed":
            animationLenador.Play("Walk");                
            break;
        default:
            animationLenador.Play("Idle");
            break;
        }
    }

    void anyone( Vector3 enemyPosition )
    {
        Base.GetComponent<Castle>().warning(enemyPosition);
    }

    public void damage( float damage )
    {
        live -= damage;
        Debug.Log(Team + " - Me han herido: " + damage);

        if ( live <= 0 )
        {
            state = "Die";
            Base.GetComponent<Castle>().addKill();
            Destroy(gameObject, 5);
        }
    }

    public float getLive()
    {
        return live;
    }

    void dress()
    {
        GameObject obj = transform.Find("lumberjack_full").gameObject;

        SkinnedMeshRenderer renderer = obj.GetComponentInChildren<SkinnedMeshRenderer>(); 
        Material[] mats = renderer.materials;

        if ( Team == "Red" )
        {
            mats[4].color = Color.red;
            mats[5].color = Color.red;
            gameObject.tag = "RedTeam";
            Base = GameObject.Find("castle-RED");
            Enemy = "BlueTeam";
        }
        
        if ( Team == "Blue" )
        {
            mats[4].color = Color.blue;
            mats[5].color = Color.blue;
            gameObject.tag = "BlueTeam";
            Base = GameObject.Find("castle-BLUE");
            Enemy = "RedTeam";
        }
    }
}
