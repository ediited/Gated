using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates_Switch : Gates
    
{


    //public Ports Output;
    public bool active = false;
    public GameObject slider;
    private Vector3 velocity = Vector3.zero;

 
    private bool pActive = false;

    void Update()
    {
        if (!pActive && this.active)
        {
            pActive = true;
           this.DrawEnergyBall();
            this.SendEnergy();
        }
        if (pActive && !this.active)
        {
            //this.SendEnergy();
            pActive = false;
        }

        if (this.active)
        {
            this.gs = true;
            slider.transform.localPosition = Vector3.SmoothDamp(slider.transform.localPosition, new Vector3(0,0.2f,0.57f), ref velocity, 0.3f);
        }
        else
        {
            this.gs = false;
            slider.transform.localPosition = Vector3.SmoothDamp(slider.transform.localPosition, new Vector3(0, 0.2f, 0), ref velocity, 0.3f);
        }
    }

    public new void SendEnergy()
    {
        if (this.active)
        {
            this.Output1.connection.end.state = true;
            this.Output1.state = true;
        }
        else
        {
            this.Output1.state = false;
            this.Output1.connection.end.state = false;
        }

    }



}
