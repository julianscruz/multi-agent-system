using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float live = 100;
    public string state = "Patrol";

    public string Team;
    private string Enemy;

    private GameObject Base;

    UnityEngine.AI.NavMeshAgent SoldierAgent;

    private Vector3 goTo;

    private GameObject View; // vista
    private float viewingDistance = 5f; // distancia de vista

    private float y = 0f;

    
    void Start()
    {
        SoldierAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        View = transform.Find("View").gameObject; // cubo raycast
        dress();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "Patrol":
                walk(goTo);
                break;
            case "Search":
                search();
                break;
        }

        
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay"+other.gameObject.tag);

        if (other.gameObject.tag == Enemy)
        {
            //other.attachedRigidbody.AddForce(Vector3.up * 10);
            Debug.Log("Enemigo"); 

            cut(other.gameObject);
        }            
    }

    void walk( Vector3 destination )
    {
        SoldierAgent.isStopped = false;
        SoldierAgent.SetDestination(destination); // ir al punto de destino;

        //search();    

        if (!SoldierAgent.pathPending)
        {
            if (SoldierAgent.remainingDistance <= SoldierAgent.stoppingDistance)
            {
                if (!SoldierAgent.hasPath || SoldierAgent.velocity.sqrMagnitude == 0f)
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

        if ( y < 360 )
        {
            y += Time.time;
            transform.rotation = Quaternion.Euler(0,y,0);

        } else {
            randPos();
            state = "Patrol";
            y = 0;
        }
    

        if (Physics.Raycast(watch, out look, viewingDistance))
        {
            Debug.DrawRay(watch.origin, watch.direction * look.distance, Color.red);

            if (look.collider.gameObject.tag == Enemy)
            {
                anyone(look.collider.gameObject.transform.position);
                //cut(look.collider.gameObject);
                //attack(look.collider.gameObject);

            }else {
                goTo = look.collider.gameObject.transform.position;
            }
            
        } else
        {
            Debug.DrawRay(watch.origin, watch.direction * (viewingDistance), Color.yellow);
        }
    }

    public void attack( Vector3 enemy )
    {
        //state = "Attack";
        Debug.Log(Team + " - Ir a enemigo: " + enemy); 
        goTo = enemy;
        walk(goTo);  
        state = "Patrol";      
    }

    void cut( GameObject enemy )
    {
        var force = 30f;

        Debug.Log("cut: " + enemy.name); 

        if ( enemy.name == "Lumberjack(Clone)" )
        {
            enemy.GetComponent<Lumberjack>().damage(force);    
        }

        if ( enemy.name == "Soldier(Clone)" )
        {
            enemy.GetComponent<Soldier>().damage(force);            
        }
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

    void anyone( Vector3 enemyPosition )
    {
        Base.GetComponent<Castle>().warning(enemyPosition);
    }

    void randPos() {
        float range = 13.0f;
        goTo = new Vector3(Random.Range(-range, range), 0.15f, Random.Range(-range, range));
    }

    void dress()
    {
        GameObject obj = transform.Find("soldier").gameObject;

        var soldier = obj.GetComponentInChildren<Renderer>(); 
        //Material[] mats = renderer.materials;

        if ( Team == "Red" )
        {
            soldier.material.SetColor("_Color", Color.red);

            Base = GameObject.Find("castle-RED");
            
            gameObject.tag = "RedTeam";
            Enemy = "BlueTeam";
        }
        
        if ( Team == "Blue" )
        {
            soldier.material.SetColor("_Color", Color.blue);

            Base = GameObject.Find("castle-BLUE");
            
            gameObject.tag = "BlueTeam";
            Enemy = "RedTeam";
        }
    }
}
