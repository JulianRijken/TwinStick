using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class Tab : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private bool _interactable;
    [SerializeField] private bool startSelected;
    [SerializeField] private Graphic graphic = null;

    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectColor = new Color(56,56,56,255);
    [SerializeField] private Color highlightedColor = new Color(56, 56, 56, 255);
    [SerializeField] private Color disabledColor = new Color(255, 0, 0, 128);

    [SerializeField] private float fadeDuration = 5f;
    [SerializeField] private TabGroup tabGroup = null;
    [SerializeField] private UnityEvent onClick = null;

    private bool isSelected;


    private void Start()
    {
        Interactable = _interactable;
        isSelected = false;

        if(startSelected)
        {
            graphic.color = selectColor;

            if (tabGroup != null)
                tabGroup.SetDiffrentTab(this);
            else
                Debug.LogWarning("Tab Group not set");

            onClick.Invoke();
        }
    }

    private void OnEnable()
    {
        if (Interactable)
        {
            if (isSelected)
                graphic.color = selectColor;
            else
                graphic.color = normalColor;
        }
        else
            graphic.color = disabledColor;


    }

    /// <summary>
    /// Sets the color correct
    /// </summary>
    public bool Interactable
    {
        get { return _interactable; }
        set
        {
            if (_interactable != value)
            {
                _interactable = value;

                if (value == true)
                {
                    if (isSelected)                   
                        LerpToColor(selectColor);                    
                    else                    
                        LerpToColor(normalColor);               
                }
                else
                {
                    LerpToColor(disabledColor);
                }
            }
        }
    }


    /// <summary>
    /// Is Called When pressed
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Interactable)
        {
            if (tabGroup != null)
                tabGroup.SetDiffrentTab(this);
            else
                Debug.LogWarning("Tab Group not set");

            onClick.Invoke();
        }
    }

    /// <summary>
    /// Is Called When pointer exited
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelected == false && Interactable)
            LerpToColor(normalColor);
    }

    /// <summary>
    /// Is Called When pointer enterd
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected == false && Interactable)
            LerpToColor(highlightedColor);
    }



    /// <summary>
    /// inlks to the courutine "lerps to the color"
    /// </summary>
    private void LerpToColor(Color _toColor)
    {
        StopAllCoroutines();
        StartCoroutine(LerpToColorCoroutine(_toColor));
    }

    /// <summary>
    /// lerps to the color
    /// </summary>
    private IEnumerator LerpToColorCoroutine(Color _toColor)
    {
        if (graphic != null)
        {
            Color startColor = graphic.color;
            float timer = 0;

            while (timer < 1)
            {
                timer += Time.deltaTime / fadeDuration;

                graphic.color = Color.Lerp(startColor, _toColor, timer);

                yield return new WaitForSeconds(Time.deltaTime);
            }

            graphic.color = _toColor;
        }
    }
 
    /// <summary>
    /// Resets The tab to normal
    /// </summary>
    public void SetToNormal()
    {
        if (Interactable)        
            LerpToColor(normalColor);      
        else
            LerpToColor(disabledColor);

        isSelected = false;
    }

    /// <summary>
    /// Resets The tab to selected
    /// </summary>
    public void SetToSelected()
    {
        LerpToColor(selectColor);
        isSelected = true;
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }


}
