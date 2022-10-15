using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI damageText = null;

        public void DestroyText()
        {
            Destroy(gameObject);
        }

        public void SetValue(float damageAmount)
        {
          damageText.text = string.Format("{0:0}", damageAmount);

        }
    }

}