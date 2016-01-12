using UnityEngine;
using Assets.Scripts.Attachables;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using System;
using Assets.Scripts.Ship;

namespace Assets.Scripts.Managers
{
    public class ResearchManager : Singleton<ResearchManager>, IInitializable
    {
        private List<string> playerUnlockedAttachables;

        private bool isInitialized;

        public void Init()
        {
            playerUnlockedAttachables = new List<string>();
            LoadPlayerPrefs();
            isInitialized = true;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }

        private void SavePlayerPrefs()
        {
            playerUnlockedAttachables.ForEach(attach => PlayerPrefs.SetInt(attach + "_research", 1));
        }

        private void LoadPlayerPrefs()
        {
            var loadedUnlockedAttachables = new List<string>();
            foreach (var attach in AttachablesDatabase.instance.GetAllAttachables())
            {
                if (PlayerPrefs.HasKey(attach.AttachableID + "_research"))
                {
                    loadedUnlockedAttachables.Add(attach.AttachableID);
                }
            }
            if (loadedUnlockedAttachables.Count == 0) //No unlocked attachables
            {
                loadedUnlockedAttachables.Add("LiquidEngine1");
                loadedUnlockedAttachables.Add("BulletCannon1");
            }

            playerUnlockedAttachables.Clear();
            playerUnlockedAttachables.AddRange(loadedUnlockedAttachables.ToArray());
        }

        public bool IsAttachableUnlocked(string attachableID)
        {
            foreach (var attach in playerUnlockedAttachables)
            {
                if (attach == attachableID) return true;
            }
            return false;
        }

        public string[] GetAllUnlockedAttachables()
        {
            return playerUnlockedAttachables.ToArray();
        }

        public bool TryUnlockAttachable(string attachableID)
        {
            foreach (var attach in playerUnlockedAttachables)
            {
                if (attach == attachableID)
                {
                    return false;
                }
            }

            if (AttachablesDatabase.instance.GetAttachable(attachableID).ScrapCost <= PlayerManager.instance.CollectedScrap)
            {
                playerUnlockedAttachables.Add(attachableID);
                PlayerManager.instance.CollectedScrap -= AttachablesDatabase.instance.GetAttachable(attachableID).ScrapCost;
                EventManager.AttachableResearched.Invoke(new AttachableResearchedEventArgs(attachableID));
                SavePlayerPrefs();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryUnlockShip(string shipID)
        {
            return VehiclesManager.instance.TryUnlockNewShip(shipID);
        }

        public void UnlockAllAttachables()
        {
            bool test;

            foreach (var attach in AttachablesDatabase.instance.GetAllAttachables())
            {
                test = false;

                foreach (var unlockedAttach in playerUnlockedAttachables)
                {
                    if (attach.AttachableID == unlockedAttach)
                    {
                        test = true;
                    }
                }

                if (test == false)
                {
                    playerUnlockedAttachables.Add(attach.AttachableID);
                }
            }

            InventoryManager.instance.RefreshInventory();
        }
    }
}
