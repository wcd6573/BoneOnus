using System;
using System.Net.Http;

namespace BoneOnus.Model;
public class Dagger : Weapon
{
    public Dagger(BoneType blade, BoneType hilt, BoneType handle) : base(blade, hilt, handle)
    {
    }
}
