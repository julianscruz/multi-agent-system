using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class Leñador_v1 : MonoBehaviour
{
    public string estado = "buscar";
    
    public GameObject casita;
    public Vector3 rand;

    public int madera = 0;

    float range = 100.0f;

    NavMeshAgent agente;

    // Start is called before the first frame update
    void Start()
    {
        //rand = new Vector3(Random.Range(-1.0f, 1.0f), 0.15f, Random.Range(-1.0f, 1.0f));
        //randPos();
        agente = GetComponent<NavMeshAgent>();        
    }

    // Update is called once per frame
    void Update()
    {
        if ( estado == "buscar" )
        {
            randPos();
            estado = "caminar";
        }        

        if ( estado == "caminar" )
        {
            agente.SetDestination(rand);
            estado = "buscar";
        }

        if ( estado == "llevar" )
        {
            agente.SetDestination(casita.transform.position);
        }        
    }

    void randPos() {
        rand = new Vector3(Random.Range(-range, range), 0.15f, Random.Range(-range, range));
    }

    void OnTriggerEnter(Collider other) {

        //Debug.Log("toco: " + other.gameObject);

        if (other.gameObject.tag == "tree") {
            madera += 1;
            Destroy(other.gameObject);
            //Debug.Log("arbol");
            estado = "llevar";
        }

        if (other.gameObject == casita) {
            Base_v1.wood += madera;
            madera = 0;
            estado = "buscar";
        }
    }
}
