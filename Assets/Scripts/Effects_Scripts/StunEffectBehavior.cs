using UnityEngine;

public class StunEffectBehavior : MonoBehaviour
{
    public Transform target;    //object the stun effect will follow
    void Start()
    {
        
    }

    void Update()
    {
        //follow the object stun effect is attached to
        if (target != null)
        {
            transform.position = target.position + new Vector3(0f, 0.7f, 0f);
        }
    }
}
