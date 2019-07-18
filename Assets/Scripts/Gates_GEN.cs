using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates_GEN : Gates 
{

    public bool isActive;

 
    public void Reset()
    {
        this.inputCount = 0;
        this.outputCount = 1;
    }

    // Update is called once per frame
    void Update() {
        this.gs = this.isActive;
    }
 
    

}
