using System;
using BoneOnus.Model;

namespace BoneOnus.Model;
public class Sword : Weapon
{
    private int damage;
    private int speed;
    private int durability;
    private BoneType blade;
    private BoneType hilt;
    private BoneType handle;
    public Sword(BoneType blade, BoneType hilt, BoneType handle) : base(blade, hilt, handle)
    {
        this.blade = blade;
        this.hilt = hilt;
        this.handle = handle;
    }
}
