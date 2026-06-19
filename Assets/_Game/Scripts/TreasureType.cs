// Defines the two treasure categories. Treasure.cs uses this to determine
// what happens on pickup without needing separate scripts per object type.
public enum TreasureType
{
    Minor,  // increases score
    Major   // triggers Escape Phase (Milestone 4)
}
