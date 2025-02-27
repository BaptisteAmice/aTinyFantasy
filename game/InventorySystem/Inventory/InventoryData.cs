using Godot;
using System;

namespace aTinyFantasy.InventorySystem.Inventory {
    [GlobalClass]
    public partial class InventoryData : Resource
    {
        [Export]
        public SlotData[] Slots { get; set; } = null;

        [Signal]
        public delegate void InventoryInteractEventHandler(InventoryData inventory, int index, MouseButton button);
        [Signal]
        public delegate void InventoryUpdatedEventHandler(InventoryData inventory);


        public void OnSlotClicked(int index, MouseButton button)
        {
            //emit a signal
            EmitSignal(SignalName.InventoryInteract, this, index, (int)button);
        }

        public SlotData GrabSlotData(int index)
        {
            if (Slots[index] != null)
            {
                SlotData grabbedSlotData = Slots[index].Duplicate() as SlotData;
                Slots[index] = null;
                EmitSignal(SignalName.InventoryUpdated, this);
                return grabbedSlotData;
            }
            return null;
        }

        public SlotData DropSlotData(SlotData grabbedSlotData, int index)
        {
            SlotData oldSlotData = Slots[index];
            SlotData returnedSlotData = null;
            if (oldSlotData != null && grabbedSlotData.CanFullyMergeWith(oldSlotData))
            {
                GD.Print("Merging");
                oldSlotData.FullyMergeWith(grabbedSlotData);
                GD.Print(grabbedSlotData.Quantity);
            }
            else
            {
                Slots[index] = grabbedSlotData;
                returnedSlotData = oldSlotData;   
            }
            EmitSignal(SignalName.InventoryUpdated, this);
            return returnedSlotData;
        }

        public SlotData DropSingleSlotData(SlotData grabbedSlotData, int index)
        {
            SlotData oldSlotData = Slots[index];
            if (oldSlotData == null) {
                Slots[index] = grabbedSlotData.CreateSingleSlotData();
            } else if (oldSlotData.CanMergeWith(grabbedSlotData))
            {
                oldSlotData.FullyMergeWith(grabbedSlotData.CreateSingleSlotData());
            }
            EmitSignal(SignalName.InventoryUpdated, this);
            if (grabbedSlotData.Quantity > 0)
            {
                return grabbedSlotData;
            }
            return null;
        }
    }
}
