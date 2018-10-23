using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPD40 : Gun {

    void Start() {
        //calibre = new cal7_62();
        aimFoV = 30;
        totalBullets = 71*2;
        magazine = new Magazine(71, new cal7_62());
        handleTime = 0.066666f;
    }
}
