using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void SetLayer(int layer){
        this.GetComponent<SpriteRenderer>().sortingOrder = layer;
    }
}
