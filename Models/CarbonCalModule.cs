using System;
using ESGArk.Models;

namespace ESGArk.Models
{
    public class CarbonCalModule
    {
        /// <summary>
        /// 根據使用者輸入資料計算碳排放紀錄
        /// </summary>
        public CarbonCalRecord Calculate(int userId, string behaviorType, string subType, double quantity, string unit)
        {
            double co2PerUnit = GetCO2Factor(behaviorType, subType, unit);
            double totalCO2 = quantity * co2PerUnit;

            return new CarbonCalRecord
            {
                UserId = userId,
                BehaviorType = behaviorType,
                SubType = subType,
                Quantity = quantity,
                Unit = unit,
                CO2PerUnit = co2PerUnit,
                TotalCO2 = totalCO2,
                CreatedAt = DateTime.Now
            };
        }

        /// <summary>
        /// 根據行為類型與單位取得每單位的 CO2 排放係數（kg CO2e）
        /// 可根據實際情況擴充為資料庫查表或設定檔驅動
        /// </summary>
        private double GetCO2Factor(string behaviorType, string subType, string unit)
        {
            if (behaviorType == "交通")
            {
                if (subType == "開車" && unit == "公里")
                    return 0.25;  // 每公里 0.25 公斤 CO2
                if (subType == "騎機車" && unit == "公里")
                    return 0.1;
                if (subType == "搭捷運" && unit == "公里")
                    return 0.05;
            }

            if (behaviorType == "飲食")
            {
                if (subType == "牛肉" && unit == "份")
                    return 5.0;
                if (subType == "雞肉" && unit == "份")
                    return 2.0;
            }

            // 預設值（找不到時）
            return 0.1;
        }
    }
}
