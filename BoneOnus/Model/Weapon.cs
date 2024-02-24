using System;
namespace BoneOnus.Model;
public abstract class Weapon
{
	private int damage;
	private int speed;
	private int durability;
	private BoneType blade;
	private BoneType hilt;
	private BoneType handle;

	public Weapon(BoneType blade, BoneType hilt, BoneType handle)
	{
		this.blade = blade;
		this.hilt = hilt;
		this.handle = handle;
		
		// inset damage, speed, durability calculation here
	}

	void InflictDamage(Entity entity)
	{
		entity.TakeDamage(this.damage);
	}
}
