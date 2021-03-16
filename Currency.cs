using System;
using System.Collections.Generic;
using FlowCanvas.Nodes;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.DefaultCatalog;

namespace DH.FCExtensions.GameFoundations
{
    [HasRefreshButton]
    [Category("Functions/Extensions/Game Foundations")]
    public class CurrencyItemKeys : FlowControlNode, IGameFoundationNode
    {
        private List<Tuple<string, string>> items;

        protected override void RegisterPorts()
        {
            var invItems = new List<CurrencyAsset>();
            CatalogSettings.catalogAsset.GetItems(invItems);
            items = new List<Tuple<string, string>>(invItems.Count);
            foreach (CurrencyAsset currencyAsset in invItems)
            {
                items.Add(new Tuple<string, string>(currencyAsset.displayName, currencyAsset.key));
            }

            foreach (var item in items)
            {
                AddValueOutput(item.Item1, () => item.Item2);
            }
        }
    }

    [Category("Functions/Extensions/Game Foundations")]
    public class AddCurrency : CallableActionNode<string, int>, IGameFoundationNode
    {
        public override void Invoke(string currencyKey, int amount)
        {
            this.AddCurrency(this.FindItem<Currency>(currencyKey), amount);
        }
    }

    [Category("Functions/Extensions/Game Foundations")]
    public class RemoveCurrency : CallableActionNode<string, int>, IGameFoundationNode
    {
        public override void Invoke(string currencyKey, int amount)
        {
            this.RemoveCurrency(this.FindItem<Currency>(currencyKey), amount);
        }
    }

    [Category("Functions/Extensions/Game Foundations")]
    public class SetCurrency : CallableActionNode<string, int>, IGameFoundationNode
    {
        public override void Invoke(string currencyKey, int amount)
        {
            this.SetCurrency(this.FindItem<Currency>(currencyKey), amount);
        }
    }

    [Category("DH/Game Foundations")]
    public class GetCurrencyAmount : PureFunctionNode<long, string>, IGameFoundationNode
    {
        public override long Invoke(string currencyKey)
        {
            return this.GetCurrencyAmount(this.FindItem<Currency>(currencyKey));
        }
    }
}