using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Text RedTeam;
    public Text BlueTeam;

    private GameObject[] REDTeamMembers; 
    private GameObject[] BLUETeamMembers; 

    public Text RedArmy;
    public Text RedWorkers;
    
    public Text RedState;
    public Text RedWood;
    public Text RedAlert;

    public Text BlueArmy;
    public Text BlueWorkers;
    
    public Text BlueState;
    public Text BlueWood;
    public Text BlueAlert;

    private GameObject redLeader;
    private GameObject blueLeader;

    
    public Text RedKills;
    public Text BlueKills;

    private string[] redTeam;
    private string[] blueTeam;
    
    void Start()
    {
        redLeader  = GameObject.Find("castle-RED");
        blueLeader = GameObject.Find("castle-BLUE");
    }

    // Update is called once per frame
    void Update()
    {
        REDTeamMembers = GameObject.FindGameObjectsWithTag("RedTeam");
        BLUETeamMembers = GameObject.FindGameObjectsWithTag("BlueTeam");

        get();

        RedTeam.text = "RED Team (" + REDTeamMembers.Length.ToString() + ")";
        BlueTeam.text = "(" + BLUETeamMembers.Length.ToString() + ") BLUE Team";

        RedWorkers.text = "Workers - " + redTeam[0] ;
        RedArmy.text    = "Soldiers - " + redTeam[1] ;
        RedWood.text    = "Wood - " + redTeam[2] ;
        RedState.text   = redTeam[3] + "\r\n";
        
        RedAlert.text = "Alert - " + redTeam[4] ;

        BlueWorkers.text = blueTeam[0] + " - Workers";
        BlueArmy.text    = blueTeam[1] + " - Soldiers";
        BlueWood.text    = blueTeam[2] + " - Wood";
        BlueState.text   = blueTeam[3] + "\r\n";

        BlueAlert.text = "Alert - " + blueTeam[4] ;

        RedKills.text = "Kills - " + redTeam[4] ;
        BlueKills.text = blueTeam[5] + " - Kills";
    }

    void get() {
        redTeam  = redLeader.GetComponent<Castle>().post();
        blueTeam = blueLeader.GetComponent<Castle>().post();
    }
}
