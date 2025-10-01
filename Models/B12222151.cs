using System;
using System.Collections.Generic;
using B1222151_1.Models; // ⚠️ 根據你專案實際命名空間調整

namespace B1222151_1.Models
{
    public class CarbonCalModule
    {
        // 子類型對應的碳排係數（kg／單位）
        private readonly Dictionary<string, double> CO2Factors = new Dictionary<string, double>
        {
            // 交通
            { "開車", 0.2 },
            { "高鐵", 0.035 },
            { "公車", 0.089 },

            // 飲食
            { "牛肉", 27.0 },
            { "素食", 2.0 },

            // 電梯
            { "電梯", 0.003 }
        };

        /// <summary>
        /// 根據輸入行為資訊計算一筆 CarbonCalRecord 碳排記錄（尚未儲存到資料庫）
        /// </summary>
        public CarbonCalRecord Calculate(int userId, string behaviorType, string subType, double quantity, string unit)
        {
            if (!CO2Factors.ContainsKey(subType))
            {
                throw new ArgumentException("找不到對應的碳排係數：" + subType);
            }

            double factor = CO2Factors[subType];
            double total = factor * quantity;

            return new CarbonCalRecord
            {
                UserId = userId,
                BehaviorType = behaviorType,
                SubType = subType,
                Quantity = quantity,
                Unit = unit,
                CO2PerUnit = factor,
                TotalCO2 = total,
                CreatedAt = DateTime.Now
            };
        }
    }
}
