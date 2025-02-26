using Godot;
using System;
using aTinyFantasy.Player;
using aTinyFantasy.InventorySystem.Inventory;

namespace aTinyFantasy.InventorySystem.Inventory {
    public partial class InventoryInterface : Control
    {
        Inventory playerInventory;
        Player.Player player;

        SlotData grabbedSlotData = null;

        public override void _Ready()
        {
            playerInventory = GetNode<Inventory>("PlayerInventory");
            player = GetNode<Player.Player>("/root/Main/Player");
            //connect to the inventory data signal
            player.InventoryData.InventoryInteract += OnInventoryInteract;
        }

        public void SetPlayerInventoryData(InventoryData inventoryData)
        {
            playerInventory.SetInventoryData(inventoryData);
        }

        public void OnInventoryInteract(InventoryData inventory, int index, MouseButton button)
        {
            GD.Print("InventoryInterface received signal from InventoryData");
            GD.Print("Slot " + index + " clicked with button " + button);

            if ((MouseButton)button == MouseButton.Left)
            {
                if (grabbedSlotData == null)
                {
                    grabbedSlotData = player.InventoryData.GrabSlotData(index);
                    GD.Print("Grabbed slot data: " + grabbedSlotData);
                } //28:34 todo

            }
        }

    }
}
