using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuTab : MonoBehaviour
{
    [SerializeField] private UnityEvent onClick = null;
    [SerializeField] private Image icon = null;
    [SerializeField] private Color selectColor = Color.black;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private float fadeSpeed = 5f;

    private Color toColor;
    private bool isOn;

    private void Awake()
    {
        GameManager.instance.notificationCenter.OnMenuScreenSwitched += SetToNormal;
    }

    private void Start()
    {
        toColor = Color.white;
    }

    private void Update()
    {
        icon.color = Color.Lerp(icon.color, toColor, Time.deltaTime * fadeSpeed);
    }


    /// <summary>
    /// Is called When the the pointer presses down
    /// </summary>
    public void PointerDown()
    {
        GameManager.instance.notificationCenter.FireMenuScreenSwitched();
        toColor = selectColor;
        isOn = true;
        onClick.Invoke();
    }

    /// <summary>
    /// Is called When the the pointer enters
    /// </summary>
    public void PointerEnter()
    {
        if(isOn == false)
        {
            toColor = selectColor;
        }
    }

    /// <summary>
    /// Is called When the the pointer exits
    /// </summary>
    public void PointerExit()
    {
        if (isOn == false)
        {
            toColor = normalColor;
        }
    }

    /// <summary>
    /// Resets The tab to normal
    /// </summary>
    private void SetToNormal()
    {
        toColor = normalColor;
        isOn = false;
    }



    private void OnDestroy()
    {
        GameManager.instance.notificationCenter.OnMenuScreenSwitched -= SetToNormal;
    }
}
