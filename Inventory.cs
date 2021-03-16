using System;
using System.Collections;
using System.Collections.Generic;
using FlowCanvas.Nodes;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.DefaultCatalog;

namespace DH.FCExtensions.GameFoundations
{
    [Category("Functions/Extensions/Game Foundations")]
    [HasRefreshButton]
    public class InventoryItemKeys : FlowControlNode, IGameFoundationNode
    {
        private List<Tuple<string, string>> items;

        protected override void RegisterPorts()
        {
            var invItems = new List<InventoryItemDefinitionAsset>();
            CatalogSettings.catalogAsset.GetItems(invItems);
            items = new List<Tuple<string, string>>(invItems.Count);
            foreach (InventoryItemDefinitionAsset invAsset in invItems)
            {
                items.Add(new Tuple<string, string>(invAsset.displayName, invAsset.key));
            }

            foreach (var item in items)
            {
                AddValueOutput(item.Item1, () => item.Item2);
            }
        }
    }

    [Category("Functions/Extensions/Game Foundations")]
    public class CreateInventoryItem : CallableActionNode<string>, IGameFoundationNode
    {
        public override void Invoke(string itemKey)
        {
            var item = this.FindItem<InventoryItemDefinition>(itemKey);
            this.CreateInventoryItem(item);
        }
    }

    [Category("Functions/Extensions/Game Foundations")]
    public class CreateStackableInventoryItem : CallableActionNode<string, int>, IGameFoundationNode
    {
        public override void Invoke(string itemKey, int quantity)
        {
            var item = this.FindItem<StackableInventoryItemDefinition>(itemKey);
            this.CreateStackableInventoryItem(item, quantity);
        }
    }
}