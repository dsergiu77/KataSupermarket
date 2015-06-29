using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataSupermarket.Lib
{
    public class PricingEngine : KataSupermarket.Lib.IPricingEngine
    {
        List<IPricingRule> pricingRuleList;
        Dictionary<int, Type> rulesPriorityDict;

        #region C-tors
        public PricingEngine()
        {
            pricingRuleList = new List<IPricingRule>();
            rulesPriorityDict = new Dictionary<int, Type>();

            rulesPriorityDict.Add(1, typeof(QuantityDiscountPricingRule));
            rulesPriorityDict.Add(2, typeof(QuantityPricingRule));
        }

        public PricingEngine(IEnumerable<IPricingRule> pricingRules)
            : this()
        {
            pricingRuleList = new List<IPricingRule>(pricingRules);
        }
        #endregion

        #region Public Methods
        public void AddPricingRule(IPricingRule rule)
        {
            pricingRuleList.Add(rule);
        }

        public void AddQuantityPricingRule(QuantityPricingRule rule)
        {
            pricingRuleList.Add(rule);
        }

        public void AddQuantityDiscountPricingRule(QuantityDiscountPricingRule rule)
        {
            pricingRuleList.Add(rule);
        }

        public decimal GetItemTotalPrice(string item, int totalQuantity)
        {
            IEnumerable<IPricingRule> sortedRules = this.GetPricingRulesSortedByPriority();
            var sortedItemRules = sortedRules.Where(r => r.Sku == item);

            decimal itemTotalPrice = 0;

            int restQuantity = totalQuantity;

            bool foundQtyDiscountRule = false;

            foreach (var itemRule in sortedItemRules)
            {
                if (!foundQtyDiscountRule)
                {
                    QuantityDiscountPricingRule qtyDiscountRule = itemRule as QuantityDiscountPricingRule;
                    if (qtyDiscountRule != null)
                    {
                        foundQtyDiscountRule = true;
                        restQuantity = qtyDiscountRule.GetTotalDiscountQuantity(restQuantity);

                        continue;
                    }
                }
                else
                {
                    // ignore the other quantity discount rules
                }

                QuantityPricingRule qtyRule = itemRule as QuantityPricingRule;
                if (qtyRule != null)
                {
                    int multiple = restQuantity / qtyRule.Quantity;
                    restQuantity = restQuantity % qtyRule.Quantity;
                    itemTotalPrice += qtyRule.GetPrice() * multiple;

                    continue;
                }
            }

            return itemTotalPrice;
        }
        #endregion

        #region Private Methods
        private IEnumerable<IPricingRule> GetPricingRulesSortedByPriority()
        {
            List<IPricingRule> sortedRules;

            sortedRules = new List<IPricingRule>();

            var priorities = rulesPriorityDict.Keys.OrderBy(p => p);

            foreach (int prio in priorities)
            {
                Type ruleType = rulesPriorityDict[prio];

                var rulesForType = pricingRuleList.Where(r => r.GetType() == ruleType);

                if (ruleType == typeof(QuantityDiscountPricingRule))
                {
                    var rulesForTypeByQtyDesc = rulesForType.Cast<QuantityDiscountPricingRule>().OrderByDescending(r => r.DiscountQuantity);
                    foreach (var rule in rulesForTypeByQtyDesc)
                    {
                        sortedRules.Add(rule);
                    }
                }
                if (ruleType == typeof(QuantityPricingRule))
                {
                    var rulesForTypeByQtyDesc = rulesForType.Cast<QuantityPricingRule>().OrderByDescending(r => r.Quantity);
                    foreach (var rule in rulesForTypeByQtyDesc)
                    {
                        sortedRules.Add(rule);
                    }
                }
            }

            return sortedRules;
        }
        #endregion
    }
}
