using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable 
{
    public Ports start;
    public Ports end;
    public bool drawLines = true;
    public GameObject LineRendererContainer = new GameObject();
    public LineRenderer thisLine;
    public Cable(Ports from, Ports to)
    {
        this.start = from;
        start.connection = this;

        this.end = to;
        end.connection = this; 
        
        thisLine = LineRendererContainer.AddComponent<LineRenderer>();
        thisLine.material = new Material(Shader.Find("Sprites/Default"));
        thisLine.enabled = true;
        thisLine.positionCount = 2;
        thisLine.SetPosition(0, start.thisPos.position);
        thisLine.SetPosition(1, end.thisPos.position);
        thisLine.SetWidth(0.2f, 0.2f);
        thisLine.startColor = Color.red;
        thisLine.endColor = Color.red;

        LineRendererContainer.transform.SetParent(GameObject.Find("LineCollector").transform, true);

    }
    void Start()
    {
      
    }
    public void sendEnergy(bool recurse)
    {
        if (this.start.state)
        {
            this.end.state = true; 
            if(recurse)
            {
                this.end.parent.SendEnergy();            }
        }
    }
    public void colorTheCable()
    {

        Debug.Log(this);
        thisLine.SetPosition(0, start.thisPos.position);
        thisLine.SetPosition(1, end.thisPos.position);
        if (this.start.state)
        {
            //thisLine.SetColors(Color.green, Color.green);
            thisLine.startColor = Color.green;
            thisLine.endColor = Color.green;
            
        }
        else
        {
            //thisLine.SetColors(Color.red, Color.red);
            thisLine.startColor = Color.red;
            thisLine.endColor = Color.red;
        }
    }
  



}
