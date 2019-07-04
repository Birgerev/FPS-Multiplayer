using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using net.bigdog.game.ui;
using net.bigdog.game.gamemode;

namespace net.bigdog.game.gamemode
{
    public class UI : MonoBehaviour
    {
        public ProgressBar local_progressBar;
        public ProgressBar opposite_progressBar;

        public Text local_pointText;
        public Text opposite_pointText;

        public Gamemode game;
        public int localTeam = 0;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (game == null)
            {
                game = Gamemode.instance;
                return;
            }
            if (local_progressBar == null)
            {
                Debug.LogError("GameUI: local_progressBar is null");
                return;
            }
            if (opposite_progressBar == null)
            {
                Debug.LogError("GameUI: opposite_progressBar is null");
                return;
            }

            localTeam = TeamGame.localTeam;

            if (localTeam == 1)
                local_progressBar.value =
                    (float)((TeamDeathmatch)game).team1Points / (float)((TeamDeathmatch)game).maxPoints;
            else
                local_progressBar.value =
                    (float)((TeamDeathmatch)game).team2Points / (float)((TeamDeathmatch)game).maxPoints;
            
            if (localTeam == 1)
                opposite_progressBar.value =
                    (float)((TeamDeathmatch)game).team2Points / (float)((TeamDeathmatch)game).maxPoints;
            else
                opposite_progressBar.value =
                    (float)((TeamDeathmatch)game).team1Points / (float)((TeamDeathmatch)game).maxPoints;


            //Texts
            if (localTeam == 1)
                local_pointText.text = ""+ ((TeamDeathmatch)game).team1Points;
            else
                local_pointText.text = "" + ((TeamDeathmatch)game).team2Points;


            if (localTeam == 1)
                opposite_pointText.text = "" + ((TeamDeathmatch)game).team2Points;
            else
                opposite_pointText.text = "" + ((TeamDeathmatch)game).team1Points;
        }
    }
}
