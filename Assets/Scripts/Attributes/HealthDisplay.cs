using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI healthValueText;

        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            healthValueText = GetComponent<TextMeshProUGUI>();

        }

        private void Update()
        {
            healthValueText.text = string.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}
