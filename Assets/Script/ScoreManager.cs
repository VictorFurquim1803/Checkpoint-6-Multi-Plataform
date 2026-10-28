using FusionIntroShared;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public int winningScore = 5;

    private bool gameEnded = false;

    void Update()
    {
        if (scoreText == null)
        {
            return;
        }

        Player[] players = FindObjectsOfType<Player>();

        int blueScore = 0;
        int redScore = 0;
        string winner = "";

        for (int i = 0; i < players.Length; i++)
        {
            if (i == 0)
            {
                blueScore = players[i].Score;
            }
            else if (i == 1)
            {
                redScore = players[i].Score;
            }

            if (players[i].Score >= winningScore)
            {
                winner = "Jogador " + players[i].Object.InputAuthority.PlayerId + " venceu!";
                gameEnded = true;
            }
        }

        scoreText.text =
            "Azul: " + blueScore +
            " | Vermelho: " + redScore;

        if (gameEnded)
        {
            scoreText.text += "\n" + winner;
        }
    }
}
