using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour, IPointerClickHandler {
    public Image image;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetColor(Color color) {
        image.color = color;
    }

    public void OnPointerClick(PointerEventData eventData) {
        
    }
}
