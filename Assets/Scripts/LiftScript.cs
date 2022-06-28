using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LiftScript : MonoBehaviour {
    public enum Direction { Up, Down, Idle };
    public LiftDoor[] doors;
    private float defY;
    public float speed;
    public float doorsOpenTime = 5f;
    public float floorHeight;
    private int floor = 1;
    private Direction dir;
    private LiftDoor liftDoors;
    public LiftIndicators[] indicators;
    private bool isOpened = false;
	// Use this for initialization
	void Start () {
        defY = transform.localPosition.y;
        liftDoors = transform.GetChild(0).GetComponent<LiftDoor>();
	}
    public void MoveToFloor(int count)
    {
        StopAllCoroutines();
        if (count != floor)
        {
            if (count > floor)
                dir = Direction.Up;
            else
                dir = Direction.Down;
            UpdateIndicators();
            StartCoroutine(Move((count - 1) * floorHeight + defY));
            print("MOVETOFLOOR");
        }
        else if (!isOpened)
        {
            UseDoors();
        }
    }
    private IEnumerator CloseDoors()
    {
        yield return new WaitForSeconds(doorsOpenTime);
        UseDoors();
    }
    private IEnumerator Move(float yValue)
    {
        if(isOpened)
            UseDoors();
        Vector3 targetVector = new Vector3(transform.localPosition.x, yValue + 0.01f, transform.localPosition.z);
        while (transform.localPosition != targetVector)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetVector, speed * Time.fixedDeltaTime);
            if (Mathf.FloorToInt((transform.localPosition.y - defY) / floorHeight) + 1 != floor)
            {
                floor = Mathf.FloorToInt((transform.localPosition.y - defY) / floorHeight) + 1;
                UpdateIndicators();
            }
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = targetVector;
        dir = Direction.Idle;
        UpdateIndicators();
        UseDoors();
    }
    private void UpdateIndicators()
    {
        foreach(LiftIndicators indicator in indicators)
        {
            indicator.Floor = floor;
            indicator.Dir = dir;
        }
    }
    public void UseDoors()
    {
        isOpened = !isOpened;
        if (isOpened)
            StartCoroutine(CloseDoors());
        liftDoors.Use();
        try { (from door in doors where door.floor == floor select door).SingleOrDefault().Use(); }
        catch { print("Не установлены двери на " + floor + " этаже"); }
    }
}
