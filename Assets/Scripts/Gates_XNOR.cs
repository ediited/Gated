﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates_XNOR : Gates
{

    bool Gatestate()
    {
        //Example AND 
        if ((this.Input1.state && this.Input2.state))//true //true
        {
            return true;
        }
        if ((this.Input1.state && !this.Input2.state))//true false
        {
            return false;
        }
        if ((!this.Input1.state && this.Input2.state))//false true
        {
            return false;
        }
        if ((!this.Input1.state && !this.Input2.state))//false false
        {
            return true;
        }
        return false;
    }
    void Update()
    {
        this.gs = this.Gatestate();
    }


}
