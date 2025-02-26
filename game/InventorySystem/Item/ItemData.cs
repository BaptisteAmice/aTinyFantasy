using Godot;
using System;

namespace aTinyFantasy.InventorySystem.Item {
    [GlobalClass]
    public partial class ItemData : Resource
    {
        [Export]
        public string Name { get; set; } = "";

        [Export(PropertyHint.MultilineText)]
        public string Description { get; set; } = "";

        [Export]
        public bool IsStackable { get; set; } = false;

        [Export]
        public Texture Icon { get; set; }

    }
}
