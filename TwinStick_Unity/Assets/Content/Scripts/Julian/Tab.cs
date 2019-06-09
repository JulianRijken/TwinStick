using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Tab : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,IPointerExitHandler
{

    [SerializeField] private bool startInteractable;
    [SerializeField] private Graphic graphic = null;

    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectColor = new Color(56,56,56,255);
    [SerializeField] private Color highlightedColor = new Color(56, 56, 56, 255);
    [SerializeField] private Color disabledColor = new Color(255, 0, 0, 128);

    [SerializeField] private float fadeDuration = 5f;
    [SerializeField] private TabGroup tabGroup = null;
    [SerializeField] private UnityEvent onClick = null;

    private bool isSelected;
    private bool interactable;

    //todo Fix de Interactable bool!

    private void Start()
    {
        interactable = startInteractable;
        if (interactable)
            LerpToColor(normalColor);
        else
            LerpToColor(disabledColor);


        isSelected = false;
    }



    /// <summary>
    /// Is Called When pressed
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (interactable)
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
        if (isSelected == false && interactable)
            LerpToColor(normalColor);
    }

    /// <summary>
    /// Is Called When pointer enterd
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected == false && interactable)
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
        LerpToColor(normalColor);
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



}
