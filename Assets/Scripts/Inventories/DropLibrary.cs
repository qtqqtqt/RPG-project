using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = "RPG/Inventory/Drop Library")]
    public class DropLibrary : ScriptableObject
    {
        [SerializeField] float[] dropChance;
        [SerializeField] int[] minimumDrops;
        [SerializeField] int[] maximumDrops;
        [SerializeField] DropConfig[] potentialDrops;

        [System.Serializable]
        class DropConfig
        {
            public InventoryItem item;
            public float[] relativeChance;
            public int[] minimumItems;
            public int[] maximunItems;
            public int GetRandomNumber(int level)
            {
                if (!item.IsStackable())
                {
                    return 1;
                }
                int min = GetByLevel(minimumItems, level);
                int max = GetByLevel(maximunItems, level);
                return Random.Range(min, max + 1);
            }
        }

        public struct Drop
        {
            public InventoryItem item;
            public int number;
        }

        public IEnumerable<Drop> GetRandomDrops(int level)
        {
            if (!ShouldDrop(level))
            {
                yield break;
            }
            for (int i = 0; i < GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }
        }

        private bool ShouldDrop(int level)
        {
            return Random.Range(0, 100) < GetByLevel(dropChance, level);  
        }

        private int GetRandomNumberOfDrops(int level)
        {
            int min = GetByLevel(minimumDrops, level);
            int max = GetByLevel(maximumDrops, level);
            return Random.Range(min, max);
        }

        private Drop GetRandomDrop(int level)
        {
            DropConfig selectedDropConfig = SelectRandomItem(level);
            var result = new Drop
            {
                item = selectedDropConfig.item,
                number = selectedDropConfig.GetRandomNumber(level)
            };
            return result;
        }

        private DropConfig SelectRandomItem(int level)
        {
            float totalChance = GetTotalChance(level);
            float randomRoll = Random.Range(0, totalChance);
            float chanceTotal = 0f;
            foreach (var item in potentialDrops)
            {
                chanceTotal += GetByLevel(item.relativeChance, level);
                if (chanceTotal > randomRoll)
                {
                    return item;
                }
            }
            return null;
        }

        private float GetTotalChance(int level)
        {
            float totalChance = 0f;
            foreach (var item in potentialDrops)
            {
                totalChance += GetByLevel(item.relativeChance, level);
            }
            return totalChance;
        }

        static T GetByLevel<T>(T[] values, int level)
        {
            if (values.Length == 0)
            {
                return default;
            }
            if (level > values.Length)
            {
                return values[values.Length - 1];
            }
            if (level <= 0)
            {
                return default;
            }
            return values[level - 1];
        }
    }
}