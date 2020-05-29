using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[ExecuteInEditMode]
public class SetSortingLayer : MonoBehaviour 
{
    public Renderer Renderer;
    public string SortingLayer;
    public int SortingOrderInLayer;
     
    // Use this for initialization
    void Start () 
    {
        
        if (Renderer == null)
        {
            Renderer = this.GetComponent<Renderer>();
        }
             
 
        SetLayer();
    }
 
 
    public void SetLayer() {
        if (Renderer == null)
        {
            Renderer = this.GetComponent<Renderer>();
        }
             
        Renderer.sortingLayerName = SortingLayer;
        Renderer.sortingOrder = SortingOrderInLayer;
         
        //Debug.Log(MyRenderer.sortingLayerName + " " + MyRenderer.sortingOrder);
    }
  
}