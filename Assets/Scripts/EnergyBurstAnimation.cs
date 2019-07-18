using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBurstAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform startPos;
    public Transform targetPos;
    public float speed = 1;
    private Vector3 velocity = Vector3.zero;
    private ParticleSystem thisParticle;


    

    void Start()
    {
        this.thisParticle = this.GetComponent < ParticleSystem > ();
        this.transform.position = startPos.position;
    }
    float onstart = 0;
    // Update is called once per frame
    void Update()
    {
        onstart++;
        //moves the energyball over and destroys it once it reaches its goal
        Vector3 targetPosition = targetPos.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed);
        if (Vector3.Distance(this.transform.position, targetPos.position) <= 0.2 && this.thisParticle.particleCount <= 10 ){
            Destroy(this.gameObject);
        }
    }
}

/* from Unity Exaples for smooth animation:
 *  public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
*/