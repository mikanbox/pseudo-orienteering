using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIShowRaceDetail : MonoBehaviour
{
    private CupData _selectedCup;
    private TerrainData _selectedTerrain;

    [SerializeField]
    private List<Text> _terrainTopTile = new List<Text>(3);
    [SerializeField]
    private Text _terrainComplexity;
    [SerializeField]
    private Text _cupDistance;
    [SerializeField]
    private Text _cupImportance;




    [SerializeField]
    private List<Text> _userParam = new List<Text>(4);


    void OnEnable() {
        // if (UISelectCup._selected < 0)
        //     UISelectCup._selected = 0;
        // _selectedCup = GameGlobalClasses._gameglobal._cups.cups[ UISelectCup._selected ];
        // _selectedTerrain = GameGlobalClasses._gameglobal._terrains.terrains[_selectedCup.terrain];

        _terrainComplexity.text = "難易度 : " + _selectedCup.difficulty;
        _cupDistance.text = "距離　 : " + _selectedCup.distance;
        



    }


    void OnDisable() {
        

    }


}
