using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public string label;

    void Start()
    {
        // random first sprite played in the animation
        this.gameObject.transform.GetChild(1).GetComponent<Animator>().Play("liquid_anim", 0, Random.value);
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
