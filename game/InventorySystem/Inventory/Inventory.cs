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

            //var invData = ResourceLoader.Load("res://InventorySystem/Inventory/TestInventory.tres") as InventoryData;
            //PopulateItemGrid(invData.Slots);
        }

        public void SetInventoryData(InventoryData inventoryData) {
            PopulateItemGrid(inventoryData.Slots);
        }

        public void PopulateItemGrid(SlotData[] slotDatas)
        {
            foreach (Node child in itemGrid.GetChildren())
            {
                child.QueueFree();
            }

            foreach (SlotData slotData in slotDatas)
            {
                var slotInstance = slot.Instantiate();
                itemGrid.AddChild(slotInstance);

                if (slotData != null)
                {
                    Slot slotScript = slotInstance as Slot;
                    slotScript.SetSlotData(slotData);
                }
            }
        }
    }
}
           
