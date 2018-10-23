using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {

    public static WeaponUI instance;

    public Text magazineAmount;
    public Text totalAmount;
    public Text grenadeAmount;

	// Use this for initialization
	void Start () {
        instance = this;
    }
	
	// Update is called once per frame
	public void UpdateText (int leftInMagazine, int totalProjectiles,  int grenades) {
        UpdateText(leftInMagazine, totalProjectiles);
        grenadeAmount.text = grenades+"";
    }

    public void UpdateText(int leftInMagazine, int totalProjectiles)
    {
        magazineAmount.text = "" + leftInMagazine;
        totalAmount.text = "/ " + totalProjectiles;
    }
}
