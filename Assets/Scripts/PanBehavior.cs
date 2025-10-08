using UnityEngine;

public class PanBehavior : MonoBehaviour
{

    private Animator anim;

    private PlayerController playerControllerScript;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // anim.SetTrigger("PanSlap");
            // Debug.Log("Pan Slap");
        }
        Destroy(gameObject, 0.8f);
    }
}
