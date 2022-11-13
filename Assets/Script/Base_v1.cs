using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_v1 : MonoBehaviour
{
    public static int wood = 0;
    public string Team = "Red";

    public GameObject leñador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( wood > 0 )
        {
            Debug.Log("hay madera: " + wood);

            wood -= 1;
            GameObject newLeñador = Instantiate(leñador, transform.position, transform.rotation);
            newLeñador.GetComponent<Leñador_v1>().madera = 0;

            //newLeñador.GetComponent<Lumberjack>().Team = Team;
        }
        
    }
}




