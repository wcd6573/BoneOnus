using System;
using System.Management.Instrumentation;

public abstract class Weapon
{
	int damage;
	int speed;
	int durability;
	Bone blade;
	Bone hilt;
	Bone handle;

	public Weapon(Bone blade, Bone hilt, Bone handle)
	{
		this.blade = blade;
		this.hilt = hilt;
		this.handle = handle;
	}

	void InflictDamage(Entity entity)
	{
		entity.TakeDamage(this.damage);
	}
}
