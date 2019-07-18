using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ports
{
    public bool state;
    public Transform thisPos;
    public Gates parent;
    public Cable connection;
    public Ports(bool startState,Transform pos,Gates parent)
    {
        this.state = startState;
        this.thisPos = pos;
        this.parent = parent;
    }


}
