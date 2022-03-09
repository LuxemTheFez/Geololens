using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VirtualObject : MonoBehaviour
{
    // Arborescence
    protected GameObject parent = null;
    protected List<GameObject> children = new List<GameObject>();

    [SerializeField]
    protected string virtualName = "";
    [SerializeField]
    protected string iconName = "";
    [SerializeField]
    protected Color highlightColor = new Color(1.0f,0.0f,0.0f);

    protected Material realMaterial = null;

    protected bool highlighted = false;
    protected bool useGravity = false;

    public TextMeshPro textArbo = null;

    [SerializeField]
    protected GameObject accurateUIPrefab = null;
    protected GameObject accurateUI = null;

    public void Start() {
        if(GetComponent<Renderer>() != null) {
            realMaterial = GetComponent<Renderer>().material;
        }
    }
    
    public string GetTreeText(string indent){
        string strTree = indent + this.GetName() + " {";
        if(children.Count > 0){
            strTree += "\n";
        }
        foreach(GameObject child in children){
            strTree += child.GetComponent<VirtualObject>().GetTreeText(indent+"\t") + "\n";
        }
        strTree += indent + "}";
        return strTree;
    }

    public void AddChild(GameObject child) {
        if(children.Contains(child)) {
            return;
        }
        if(child.GetComponent<VirtualObject>() == null) {
            Debug.LogWarning("Cannot add a child that isn't a VirtualObject.");
            return;
        }
        children.Add(child);
        child.GetComponent<VirtualObject>().SetParent(this.gameObject);
        this.OnAddChild(child);
    }

    /**
     * Called when a new child was added
     * Rewrite me in children classes
     */
    public virtual void OnAddChild(GameObject child) { }

    public virtual void RemoveChild(GameObject child) {
        children.Remove(child);
        child.GetComponent<VirtualObject>().SetParent(null);
    }

    public void SetParent(GameObject parent){
        if(this.parent != null){
            this.parent.GetComponent<VirtualObject>().RemoveChild(this.gameObject);
        } else {
            GameObject.FindGameObjectsWithTag("Virtual Scene")[0].GetComponent<VirtualObjectScene>().RemoveVirtualChild(this.gameObject);
        }
        this.parent = parent;
        parent.GetComponent<VirtualObject>().AddChild(this.gameObject);
        this.OnSetParent(parent);
    }

    /**
     * Called when the parent is set
     * Rewrite me in children classes
     */
    protected virtual void OnSetParent(GameObject parent) { }

    public GameObject GetParent() {
        return parent;
    }

    public List<GameObject> GetChildren() {
        return children;
    }

    public string GetName() {
        return virtualName;
    }

    public string GetIcon() {
        return iconName;
    }

    public void SetHighlightColor(Color color){
        this.highlightColor = color;
    }

    public Color GetHighlightColor(){
        return highlightColor;
    }

    public virtual void DeleteOnScene() {
        foreach (GameObject child in children){
            child.GetComponent<VirtualObject>().DeleteOnScene();
        }
        parent.GetComponent<VirtualObject>().RemoveChild(this.gameObject);
        Destroy(this);
    }

    public virtual void Highlight() {
        if(GetComponent<Renderer>() == null) {
            Debug.LogWarning("Cannot highlight a GameObject without renderer.");
        }
        GetComponent<Renderer>().material.SetColor("_Color", highlightColor);
        this.highlighted = true;
    }

    public virtual void removeHighlight() {
        if(GetComponent<Renderer>() == null) {
            Debug.LogWarning("Cannot remove highlight on a GameObject without renderer.");
            return;
        }
        GetComponent<Renderer>().material = realMaterial;
        this.highlighted = false;
    }

    /**
     * Called when the highlight of an object is toggled
     * Rewrite me in children classes
     */
    protected virtual void OnHighlightChanged() { }

    public void ApplyGravity() {
        if(GetComponent<Rigidbody>() == null){
            Debug.LogWarning("Cannot apply gravity on GameObject without component Rigidbody");
            return;
        }
        this.OnApplyGravity();
    }

    public void RemoveGravity()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            Debug.LogWarning("Cannot remove gravity on GameObject without component Rigidbody");
            return;
        }
        this.OnRemoveGravity();
        
    }

    /**
     * Rewite me in children classes
     */
    protected virtual void OnApplyGravity() {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    /**
     * Rewite me in children classes
     */
    protected virtual void OnRemoveGravity()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    /**
     * Rewrite me in children classes
     */ 
    public virtual void ShowAccurateUI() {
        if(accurateUI != null) {
            Debug.LogWarning("You can't instantiate two AccurateUI.");
            return;
        }
        Vector3 camVect = new Vector3(Camera.current.transform.position.x, Camera.current.transform.position.y - (transform.localScale.y), Camera.current.transform.position.z + 0.8f);
        accurateUI = Instantiate(accurateUIPrefab,camVect,Quaternion.identity);
        accurateUI.GetComponent<ObjectTarget>().SetTarget(this.gameObject);
    }

    /**
     * Rewrite me in children classes
     */ 
    public virtual void HideAccurateUI() {
        if(accurateUI == null) {
            Debug.LogWarning("There is no AccurateUI.");
            return;
        }
        Destroy(accurateUI);
        accurateUI = null;
    }
}
