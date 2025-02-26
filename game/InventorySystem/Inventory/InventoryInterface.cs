using Godot;
using System;
using aTinyFantasy.Player;

namespace aTinyFantasy.InventorySystem.Inventory {
    public partial class InventoryInterface : Control
    {
        Inventory playerInventory;
        public override void _Ready()
        {
            playerInventory = GetNode<Inventory>("PlayerInventory");
        }

        public void SetPlayerInventoryData(InventoryData inventoryData)
        {
            playerInventory.SetInventoryData(inventoryData);

        }
    }
}
