using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.DefaultLayers;
using UnityEngine.Promise;

namespace DH.FCExtensions.GameFoundations
{
    public interface IGameFoundationNode
    {
    }

    public static class GameFoundationNodeExtentions
    {
        private static IInventoryManager Inventory => GameFoundationSdk.inventory;
        private static Catalog Catalog => GameFoundationSdk.catalog;

        private static IWalletManager Wallet => GameFoundationSdk.wallet;

        public static T FindItem<T>(this IGameFoundationNode node, string key) where T : CatalogItem
        {
            return Catalog.Find<T>(key);
        }

        public static void FindItems(this IGameFoundationNode node, Predicate<CatalogItem> predicate,
            ICollection<CatalogItem> target, bool clearTarget = true)
        {
            Catalog.FindItems(predicate, target, clearTarget);
        }

        public static void CreateInventoryItem(this IGameFoundationNode node, InventoryItemDefinition itemDefinition)
        {
            Inventory.CreateItem(itemDefinition);
        }

        public static void CreateStackableInventoryItem(this IGameFoundationNode node,
            StackableInventoryItemDefinition itemDefinition, int quantity)
        {
            Inventory.CreateItem(itemDefinition, quantity);
        }

        public static void AddCurrency(this IGameFoundationNode node, Currency currency, int quantity)
        {
            Wallet.Add(currency, quantity);
        }

        public static void RemoveCurrency(this IGameFoundationNode node, Currency currency, int quantity)
        {
            Wallet.Remove(currency, quantity);
        }

        public static void SetCurrency(this IGameFoundationNode node, Currency currency, int balance)
        {
            Wallet.Set(currency, balance);
        }

        public static long GetCurrencyAmount(this IGameFoundationNode node, Currency currency)
        {
            return Wallet.Get(currency);
        }

        public static IObservable<Unit> Save(this IGameFoundationNode node)
        {
            // Get the persistence data layer used during Game Foundation initialization.
            if (!(GameFoundationSdk.dataLayer is PersistenceDataLayer dataLayer))
            {
                Debug.LogWarning("Data layer is not persistent. Not saving anything!");
                return Observable.ReturnUnit();
            }

            Deferred saveOperation = dataLayer.Save();
            return saveOperation.Wait().ToObservable().Do(unit => saveOperation.Dispose());
        }
    }
}