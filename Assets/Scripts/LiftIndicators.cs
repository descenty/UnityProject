using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftIndicators : MonoBehaviour {
    private LiftScript.Direction dir;
    public LiftScript.Direction Dir
    {
        get { return dir; }
        set
        {
            dir = value;
            switch (dir)
            {
                case LiftScript.Direction.Up:
                    upSprite.color = new Color32(255, 0, 0, 200);
                    downSprite.color = new Color32(50, 50, 50, 255);
                    break;
                case LiftScript.Direction.Down:
                    upSprite.color = new Color32(50, 50, 50, 255);
                    downSprite.color = new Color32(255, 0, 0, 200);
                    break;
                case LiftScript.Direction.Idle:
                    upSprite.color = new Color32(50, 50, 50, 255);
                    downSprite.color = new Color32(50, 50, 50, 255);
                    break;
            }
        }
    }
    private int floor;
    public int Floor
    {
        get { return floor; }
        set
        {
            floor = value;
            floorText.text = floor.ToString();
        }
    }
    private TextMesh floorText;
    private SpriteRenderer upSprite;
    private SpriteRenderer downSprite;
	// Use this for initialization
	void Start () {
        floorText = transform.GetChild(0).GetComponent<TextMesh>();
        upSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        downSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    private IEnumerator Flashing()
    {

    }
    */
}
