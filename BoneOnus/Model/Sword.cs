using System;
using BoneOnus.Model;

namespace BoneOnus.Model;
public class Sword : Weapon
{
    public Sword(BoneType blade, BoneType hilt, BoneType handle) : base(blade, hilt, handle)
    {
    }
}
