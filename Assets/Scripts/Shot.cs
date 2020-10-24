using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public string label;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLabel(string label)
    {
        this.label = label;
    }

    // apply color to shot
    public void ApplyColor()
    {
        switch (label)
        {
            case "El Verdito":
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case "El Naranjo":
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0.96f, 0.54f, 0f);
                break;
            case "La Rosita":
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0.96f, 0.22f, 0.95f);
                break;
            case "La Negrita":
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case "El Bermillon":
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0.70f, 0.22f, 0.07f);
                break;
            case "El Grisito":
                this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            default:
                Color c = Color.white;
                break;
        }
    }

}
