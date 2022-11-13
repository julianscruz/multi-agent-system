using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    //public int live = 100;
    public float live = 100.0f;   
    
    string[] parts = new string[] { "Box001", "Box002", "Box003", "Box004", "Box005", "Rama001", "Rama002", "Rama003", "Rama004", "Tronco" };

    void Start()
    {
        
    }

    public float cut_down(float cut)
    {
        var wood = live;
        live -= cut;

        return (wood - live);
    }

    public float getWood()
    {
        return live;
    }

    // Update is called once per frame
    void Update()
    {
        if ( live < 90 )
        {
            //Destroy(transform.Find(parts[0]).gameObject);
            //transform.Find(parts[0]).SetActive(false);

            GameObject childObj = transform.Find(parts[0]).gameObject;
            childObj.SetActive(false);
        }

        if ( live < 80 )
        {
            GameObject childObj = transform.Find(parts[1]).gameObject;
            childObj.SetActive(false);
        }

        if ( live < 70 )
        {
            GameObject childObj = transform.Find(parts[2]).gameObject;
            childObj.SetActive(false);
        }

        if ( live < 60 )
        {
            GameObject childObj = transform.Find(parts[3]).gameObject;
            childObj.SetActive(false);
        }

        if ( live < 50 )
        {
            GameObject childObj = transform.Find(parts[4]).gameObject;
            childObj.SetActive(false);
        }

        if ( live < 40 )
        {
            GameObject childObj = transform.Find(parts[5]).gameObject;
            childObj.SetActive(false);
        }

        if ( live < 30 )
        {
            GameObject childObj = transform.Find(parts[6]).gameObject;
            childObj.SetActive(false);
        }

        if ( live < 20 )
        {
            GameObject childObj = transform.Find(parts[7]).gameObject;
            childObj.SetActive(false);
        }

        if ( live < 10 )
        {
            GameObject childObj = transform.Find(parts[8]).gameObject;
            childObj.SetActive(false);
        }

        if ( live <= 0 )
        {
            GameObject childObj = transform.Find(parts[9]).gameObject;
            childObj.SetActive(false);

            Destroy(gameObject, 3);
        }
        
    }
}
