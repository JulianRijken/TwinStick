using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private float speed = 4;

    [SerializeField] private float radius = 1;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private string parameterName = "progress";
    [SerializeField] private Animator animator = null;

    [Header("Gizoms")]
    [SerializeField] private Color color = Color.red;

    private void FixedUpdate()
    {
        float progres = animator.GetFloat(parameterName);

        if (GetCollision())
            progres += Time.deltaTime * speed;
        else
            progres -= Time.deltaTime * speed;

        progres = Mathf.Clamp(progres, 0, 1);

        animator.SetFloat(parameterName, progres);
    }

    /// <summary>
    /// Checks Collision
    /// </summary>
    private bool GetCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position + offset, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].GetComponent<Player>() != null)
                return true;
        }
        

        return false;
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position + offset, radius);
    }
#endif

}
