using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates_Splitter : Gates
{




    public void DrawEnergyBall()
    {
        var temp = Instantiate(EnergyBall);
        var tempC = temp.GetComponent<EnergyBurstAnimation>();
        tempC.startPos = Output1.thisPos;
        tempC.targetPos = Output1.connection.end.thisPos;
        temp.transform.SetParent(this.gameObject.transform, true);

        var temp2 = Instantiate(EnergyBall);
        var tempC2 = temp2.GetComponent<EnergyBurstAnimation>();
        tempC2.startPos = Output2.thisPos;
        tempC2.targetPos = Output2.connection.end.thisPos;
        temp2.transform.SetParent(this.gameObject.transform, true);

    }
    public new void SendEnergy()
    {
        if (this.Input1.state)
        {
            this.Output1.connection.end.state = true;
            this.Output1.state = true;
            this.Output2.connection.end.state = true;
            this.Output2.state = true;
        }
        else
        {
            this.Output1.connection.end.state = false;
            this.Output1.state = false;
            this.Output2.connection.end.state = false;
            this.Output2.state = false ;
        }
    }


}
