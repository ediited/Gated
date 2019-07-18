using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates_NOT: Gates
{
    // Start is called before the first frame update

    public new void SendEnergy()
    {
        if (this.Input1.state)
        {
            this.Output1.connection.end.state = false;
            this.Output1.state = false;
           
        }
        else
        {
            this.Output1.connection.end.state = true;
            this.Output1.state = true;
            if (Random.Range(0, 100) >= 99.5)
            {
                this.DrawEnergyBall();
            }

        }
    }
    void Update()
    {
        if (this.Input1.state)
        {
            this.gs = false;

        }
        else
        {
            this.gs = true;
        }
    }

}
