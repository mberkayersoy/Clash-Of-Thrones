using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject unitPrefab;
    public UnitAgent unitData;
    public Sprite unitSprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI manaText;
    public int index;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetCardData()
    {
        if (unitPrefab != null)
        {
            unitData = unitPrefab.GetComponent<UnitAgent>();
            nameText.text = unitData.unitName;
            manaText.text = "Mana: " + unitData.mana.ToString();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Runs when the card is started to be dragged.
        // Save the original position and move the drifting card forward.
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false; //Allow other elements at the bottom of the card to interact.
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the position of the card according to the touched position.
        rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }   

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("BlueArea")))
        {
            // Return the point where the raycast hit. 
            Debug.Log("Raycast Hit Point: " + hit.point);
            if (BluePlayer.Instance.CurrentMana >= unitData.mana)
            {
                BluePlayer.Instance.InstantiateUnit(hit.point, index, unitPrefab);
                Destroy(gameObject);
            }
            else
            {
                // Move the card back to its original position and let other elements interact.
                rectTransform.anchoredPosition = originalPosition;
                canvasGroup.blocksRaycasts = true;
            }

        }
        else
        {
            // Move the card back to its original position and let other elements interact.
            rectTransform.anchoredPosition = originalPosition;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
