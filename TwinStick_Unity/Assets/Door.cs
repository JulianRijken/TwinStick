using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private float speed = 4;
    [SerializeField] private string parameterName = "progress";
    [SerializeField] private Animator animator = null;
    [SerializeField] private AnimationCurve curve = null;

    private float progres;
    private bool colliding;

    private void Awake()
    {
        progres = 0;
    }

    private void FixedUpdate()
    {

       

        if (colliding)
            progres += Time.deltaTime * speed;
        else
            progres -= Time.deltaTime * speed;

        progres = Mathf.Clamp(progres, 0, 1);

        animator.SetFloat(parameterName, curve.Evaluate(progres));
    }




    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            colliding = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            colliding = false;
    }

}
