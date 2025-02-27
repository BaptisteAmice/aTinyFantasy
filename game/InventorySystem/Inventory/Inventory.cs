using Godot;
using System;

namespace aTinyFantasy.InventorySystem.Inventory {
    public partial class Inventory : PanelContainer
    {
        PackedScene slot = ResourceLoader.Load("res://InventorySystem/Inventory/Slot.tscn") as PackedScene;

        GridContainer itemGrid;

        public override void _Ready()
        {
            itemGrid = GetNode<GridContainer>("MarginContainer/ItemGrid");  
        }

        public void SetInventoryData(InventoryData inventoryData) {
            inventoryData.InventoryUpdated += PopulateItemGrid;
            PopulateItemGrid(inventoryData);
        }

        public void PopulateItemGrid(InventoryData inventoryData)
        {
            foreach (Node child in itemGrid.GetChildren())
            {
                child.QueueFree();
            }

            foreach (SlotData slotData in inventoryData.Slots)
            {
                Slot slotInstance = (Slot)slot.Instantiate();
                itemGrid.AddChild(slotInstance);

                //subscribe
                slotInstance.SlotClicked += inventoryData.OnSlotClicked;

                if (slotData != null)
                {
                    Slot slotScript = slotInstance as Slot;
                    slotScript.SetSlotData(slotData);
                }
            }
        }
    }
}
           
