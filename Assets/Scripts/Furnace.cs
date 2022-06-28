using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    Game.Receipt activeReceipt;
    //TextMeshPro textMesh;
    // Start is called before the first frame update
    void Start()
    {
        activeReceipt = new Game.Receipt(Game.DoObject.Furnace);
      //  textMesh = transform.GetChild(0).GetComponent<TextMeshPro>();
    }
    private void OnTriggerEnter(Collider other)
    {
        print("Hit");
        if (other.tag == "Picked")
            return;
        if (other.GetComponent<FoodObject>())
        {
            Game.Food food = other.GetComponent<FoodObject>().food;
            if (Game.GetReceipt(food, activeReceipt.objectType) != null)
            {
                activeReceipt = Game.GetReceipt(food, activeReceipt.objectType);
                Destroy(other.gameObject);
                StartCoroutine(Cook());
            }
        }
    }
    IEnumerator Cook()
    {
        int time = activeReceipt.time;
        while (time > 0)
        {
           // textMesh.text = time.ToString() + " sec";
            yield return new WaitForSeconds(1f);
            time--;
        }
      //  textMesh.text = "";
        Instantiate(Game.GetFoodObject(activeReceipt.output), transform.position + Vector3.up, Quaternion.identity);
        print("Cooked");
    }
}
