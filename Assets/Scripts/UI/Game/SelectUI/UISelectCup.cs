using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UISelectCup : MonoBehaviour
{
    UISelectCup _uiSelectCup;

    [SerializeField]
    private GameObject Item;

    public static int _selected = 0;
    
    void Start (){
        _uiSelectCup = this.GetComponent<UISelectCup>();
    }

    void OnEnable() {
        int num = GameGlobalClasses._gameglobal._cups.cups.Length;
        Sprite[] sprites = Resources.LoadAll<Sprite>("Objects/maps");

        for (int i = 0; i < num; i++) {
            CupData cup = GameGlobalClasses._gameglobal._cups.cups[i];
            TerrainData terrain = GameGlobalClasses._gameglobal._terrains.terrains[cup.terrain];

            GameObject obj =  GameObject.Instantiate(Item,Vector3.zero, Quaternion.identity,Item.transform.parent);
            obj.SetActive(true);

            obj.transform.GetChild(3).GetComponent<Text>().text = cup.name;

            obj.transform.GetChild(4).GetComponent<Text>().text = terrain.name;
            obj.transform.GetChild(0).GetComponent<Image>().sprite = sprites[cup.terrain];
            obj.GetComponent<SetValueToObject>().value = cup.id;
            

            for(int j = 0; j < cup.difficulty; j++) {
                GameObject gobj = GameObject.Instantiate(obj.transform.GetChild(1).GetChild(0).gameObject,Vector3.zero, Quaternion.identity,obj.transform.GetChild(1));
                gobj.SetActive(true);
            }
            
        }
    }


    void OnDisable() {
        
        for(int i = Item.transform.parent.childCount - 1; i >= 0; i-- ) {
            if (Item.transform.parent.GetChild(i).gameObject.activeSelf == true){
                Destroy(Item.transform.parent.GetChild(i).gameObject);
            }
        }
    }

    public void SelectToggleButton() {
        UnityEngine.UI.ToggleGroup group = Item.transform.parent.GetComponent<UnityEngine.UI.ToggleGroup>();
        UnityEngine.UI.Toggle activetoggle = group.ActiveToggles().FirstOrDefault();
        _selected = activetoggle.gameObject.GetComponent<SetValueToObject>().value - 1;
        Debug.Log(_selected);
    }

}
