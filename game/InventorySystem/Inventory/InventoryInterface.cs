using Godot;
using System;
using aTinyFantasy.Player;
using aTinyFantasy.InventorySystem.Inventory;
using System.Numerics;
using Vector2 = Godot.Vector2;

namespace aTinyFantasy.InventorySystem.Inventory {
    public partial class InventoryInterface : Control
    {
        Inventory playerInventory;
        Player.Player player;

        SlotData grabbedSlotData = null;
        Slot grabbedSlot = null;

        public override void _Ready()
        {
            playerInventory = GetNode<Inventory>("PlayerInventory");
            player = GetNode<Player.Player>("/root/Main/Player");
            grabbedSlot = GetNode<Slot>("GrabbedSlot");
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
                } else {
                    //drop the item
                    grabbedSlotData = player.InventoryData.DropSlotData(grabbedSlotData, index);
                }
                UpdateGrabbedSlot();
            } else if ((MouseButton)button == MouseButton.Right)
            {
                if (grabbedSlotData != null)
                {
                    GD.Print("Dropping single item");
                    grabbedSlotData = player.InventoryData.DropSingleSlotData(grabbedSlotData, index);
                    UpdateGrabbedSlot();
                } else {
                    //todo
                }
            }
        }

        public void UpdateGrabbedSlot()
        {
            if (grabbedSlotData != null)
            {
                grabbedSlot.Show();
                grabbedSlot.SetSlotData(grabbedSlotData);
            } else 
            {
                grabbedSlot.Hide();
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            if (grabbedSlotData != null)
            {
                grabbedSlot.GlobalPosition = GetGlobalMousePosition() + new Vector2(5, 5);
            }
        }

    }
}
