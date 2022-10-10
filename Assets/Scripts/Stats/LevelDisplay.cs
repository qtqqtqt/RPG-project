using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats playerStats;
        TextMeshProUGUI levelValueText;

        private void Awake()
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
            levelValueText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            levelValueText.text = playerStats.GetLevel().ToString();
        }
    }
}
