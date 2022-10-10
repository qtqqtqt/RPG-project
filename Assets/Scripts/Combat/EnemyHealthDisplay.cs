using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter player;
        Health target;
        TextMeshProUGUI healthValueText;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            healthValueText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            target = player.GetTarget();

            if (target == null)
            {
                healthValueText.text = "N/A";
                return;
            }
           
            healthValueText.text = string.Format("{0:0}/{1:0}", target.GetHealthPoints(), target .GetMaxHealthPoints());
        }
    }
}
