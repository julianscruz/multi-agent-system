using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float wood;
    public string Team = "Red";

    public GameObject Lumberjack;
    public GameObject Soldier;

    private string state;

    private GameObject[] TeamMembers;    
    private int numberOfSoldier;
    private int numberOfLumberjack;

    private int alerts = 0;

    void Start()
    {
        wood = 100;

        state = "Crear Leñador";
        GameObject newLumberjack = Instantiate(Lumberjack, transform.position, transform.rotation);
        newLumberjack.GetComponent<Lumberjack>().Team = Team;
        wood -= 80;
    }

    void Update()
    {
        counting();
        rules();
    }

    void rules()
    {
        if ( numberOfSoldier > 1 && wood >= 120 )
        {
            state = "Crear Solado";
            GameObject newSoldier = Instantiate(Soldier, transform.position, transform.rotation);
            newSoldier.GetComponent<Soldier>().Team = Team;

            wood -= 120;
        }

        if ( numberOfLumberjack > 1 && wood >= 80 )
        {
            state = "Crear Leñador";
            GameObject newLumberjack = Instantiate(Lumberjack, transform.position, transform.rotation);
            newLumberjack.GetComponent<Lumberjack>().Team = Team;

            wood -= 80;
        }        
        
        if ( alerts <= numberOfSoldier && wood >= 200 ) {
            state = "Crear Leñador";
            GameObject newLumberjack = Instantiate(Lumberjack, transform.position, transform.rotation);
            newLumberjack.GetComponent<Lumberjack>().Team = Team;

            wood -= 80;
        }

        if ( alerts >= numberOfSoldier && wood >= 120 )
        {
            state = "Crear Solado";
            GameObject newSoldier = Instantiate(Soldier, transform.position, transform.rotation);
            newSoldier.GetComponent<Soldier>().Team = Team;

            wood -= 120;
        }
    }

    public void deliver_wood(float delivery)
    {
        wood += delivery;
    }

    void counting()
    {
        TeamMembers = GameObject.FindGameObjectsWithTag(Team +"Team");

        numberOfSoldier = 0;
        numberOfLumberjack = 0;

        for(int i = 0; i < TeamMembers.Length; i++)
        {
            //Debug.Log(TeamMembers[i].name); 

            if ( TeamMembers[i].name == "Lumberjack(Clone)" ) //
            {
                numberOfLumberjack += 1;
            }

            if ( TeamMembers[i].name == "Soldier(Clone)" )
            {
                numberOfSoldier += 1;
            }
        }
    }

    public void warning( Vector3 enemyPosition )
    {
        //Debug.Log("enemigo");

        state = "¡se ha visto un enemigo!";
        alerts += 1;

        for(int i = 0; i < TeamMembers.Length; i++)
        {
            if ( TeamMembers[i].name == "Soldier(Clone)" )
            {
                TeamMembers[i].GetComponent<Soldier>().attack(enemyPosition);

                Debug.Log(Team +" - Enemigo: " + enemyPosition); 
            }
        }
    }

    public string[] post()
    {
        //var teamCount =  TeamMembers.Length;
        string[] stateTeam = new string[] { numberOfLumberjack.ToString(), numberOfSoldier.ToString(), Mathf.RoundToInt(wood).ToString(), state, alerts.ToString() };
        return stateTeam;
    }
}
