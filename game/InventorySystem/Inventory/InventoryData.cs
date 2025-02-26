using Godot;
using System;

namespace aTinyFantasy.InventorySystem.Inventory {
    [GlobalClass]
    public partial class InventoryData : Resource
    {
        [Export]
        public SlotData[] Slots { get; set; } = null;

    }
}
