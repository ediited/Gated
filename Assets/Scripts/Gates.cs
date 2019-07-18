using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  class Gates : MonoBehaviour 
{

    public Ports Input1 = null;
    public Ports Input2 = null;
    public Ports Output1 = null;
    public GameObject EnergyBall = null;
    public Transform TInput1 = null;
    public Transform TInput2 = null;
    public Transform TOutput1 = null;
    public Ports Output2 = null;
    public Transform TOutput2 = null;
    public int inputCount = 2;
    public int outputCount = 1;
    private GameObject temp;
    public bool gs;

    public Gates()
    {
       // Debug.Log("created Gate");
    }
    // Start is called before the first frame update


    public void DrawEnergyBall()
    {
        Debug.Log(this);
        temp = Instantiate(EnergyBall);
        EnergyBurstAnimation tempC = temp.GetComponent<EnergyBurstAnimation>();
        tempC.startPos = this.Output1.thisPos;
        tempC.targetPos = this.Output1.connection.start.thisPos;
        temp.transform.SetParent(this.gameObject.transform, true);
    }
    public void SendEnergy()
    {
        if (this.gs)
        {
            if (Random.Range(0, 1000) >= 999)
            {
                this.DrawEnergyBall();
            }

            Output1.connection.start.state = true;
            Output1.state = true;
        }
        else
        {
            Output1.state = false;
            Output1.connection.start.state = false;
        }
        
    }

}

