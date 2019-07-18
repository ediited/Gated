using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycaster : MonoBehaviour
{

        void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                   
                    
                        hit.collider.gameObject.GetComponentInParent<Gates_Switch>().active = !(hit.collider.gameObject.GetComponentInParent<Gates_Switch>().active);
                    
                }
                else
                {
                    Debug.Log("missed");
                }
            }
        }
    }
}
 

