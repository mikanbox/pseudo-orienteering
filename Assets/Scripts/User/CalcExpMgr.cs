using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcExpMgr : MonoBehaviour
{
    GameUserExp tmpexp;

    public CalcExpMgr(){
        tmpexp = new GameUserExp();
    }

    public void MoveToNewMapTip(MapCode code) {
        float stamina = 0;
        float speed = 0;
        float intelligence = 0;
        float guts = 0;
        float maxhp = 0;

        speed += 0.01f;

        switch(code) {
            case MapCode.Forest:
            break;
        }
        tmpexp.addExp(speed,stamina,intelligence,guts,maxhp);
    }



    public void GoalCup() {
        float stamina = 0;
        float speed = 0;
        float intelligence = 0;
        float guts = 0;
        float maxhp = 0;

        maxhp += 1;
        guts += 0.1f;


        tmpexp.addExp(speed,stamina,intelligence,guts,maxhp);

        updateMasterExpData();
    }

    public void updateMasterExpData(){
        User._user.UpdateExp(tmpexp);
    }

}
