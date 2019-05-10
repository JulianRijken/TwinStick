using UnityEngine;

public class Door : MonoBehaviour
{

    [Header("Animation")]
    [SerializeField] private float speed = 4;
    [SerializeField] private string parameterName = "progress";
    [SerializeField] private Animator animator = null;
    [SerializeField] private AnimationCurve curve = null;

    [Header("Item Requirement")]
    [SerializeField] private ItemID requiredItem = ItemID.Ammo;
    [SerializeField] private int requiredItemCount = 1;
    [SerializeField] private bool requireItem = false;

    private float progres;
    private bool colliding;

    private void Awake()
    {
        progres = 0;
    }

    private void Update()
    {
        if (CanOpen())
            progres += Time.deltaTime * speed;
        else
            progres -= Time.deltaTime * speed;

        progres = Mathf.Clamp(progres, 0, 1);

        animator.SetFloat(parameterName, curve.Evaluate(progres));
    }

    private bool CanOpen()
    {
        return (colliding && GameManager.instance.inventory.CheckItemSlot(requiredItem, requiredItemCount) || colliding && requireItem == false ? true : false);
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
