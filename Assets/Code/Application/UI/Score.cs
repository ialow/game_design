using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ddd.Application
{
    public class Score : MonoBehaviour
    {
        [Header("Loading data")]
        [SerializeField] private Text loadingScroe;

        [Header("Updates data")]
        [SerializeField] private List<Text> textScore;

        private void Awake()
        {
            if (loadingScroe != null)
            {
                Debug.Log(1);
                var scroe = new GameScore();
                loadingScroe.text = scroe.LoadScore().ToString();
            }
        }

        private void Start()
        {
            GameScore.UIEvent += UpdateScore;
        }

        private void UpdateScore(int score)
        {
            for (int i = 0; i < textScore.Count; i++)
                textScore[i].text = score.ToString();
        }

        private void OnDisable()
        {
            GameScore.UIEvent -= UpdateScore;
        }
    }
}